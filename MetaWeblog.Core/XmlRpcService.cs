namespace MetaWeblog
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// The XML RPC service class.
    /// </summary>
    public class XmlRpcService
    {
        private readonly ILogger logger;
        private string? method;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlRpcService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public XmlRpcService(ILogger logger) => this.logger = logger;

        /// <summary>Invokes the service.</summary>
        /// <param name="xml">The XML.</param>
        /// <returns>A <see cref="Task{String}"/>.</returns>
        public async Task<string> InvokeAsync(string xml)
        {
            try
            {
                var doc = XDocument.Parse(xml);
                var methodNameElement = doc.Descendants("methodName").FirstOrDefault();
                if (methodNameElement != null)
                {
                    this.method = methodNameElement.Value;

                    this.logger.LogInformation($"Invoking {this.method} on XMLRPC Service");

                    var theType = this.GetType();

                    foreach (var typeMethod in theType.GetMethods())
                    {
                        var attr = typeMethod.GetCustomAttribute<XmlRpcMethodAttribute>();
                        if (attr != null && this.method.ToLower() == attr.MethodName.ToLower())
                        {
                            var parameters = this.GetParameters(doc);
                            var resultTask = (Task?)typeMethod.Invoke(this, parameters);
                            if (resultTask != null)
                            {
                                await resultTask;

                                // get result via reflection
                                var resultProperty = resultTask.GetType().GetProperty("Result");
                                if (resultProperty != null)
                                {
                                    var result = resultProperty.GetValue(resultTask);
                                    if (result != null)
                                    {
                                        return this.SerializeResponse(result);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (MetaWeblogException ex)
            {
                return this.SerializeResponse(ex);
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Exception thrown during serialization: Exception: {ex}");
                return this.SerializeResponse(new MetaWeblogException($"Exception during XmlRpcService call: {ex.Message}"));
            }

            return this.SerializeResponse(new MetaWeblogException("Failed to handle XmlRpcService call"));
        }

        private string SerializeResponse(object result)
        {
            var doc = new XDocument();
            var response = new XElement("methodResponse");
            doc.Add(response);

            if (result is MetaWeblogException)
            {
                response.Add(this.SerializeFaultResponse((MetaWeblogException)result));
            }
            else
            {
                var theParams = new XElement("params");
                response.Add(theParams);

                this.SerializeResponseParameters(theParams, result);
            }

            return doc.ToString(SaveOptions.None);
        }

        private XElement SerializeValue(object result)
        {
            var theType = result.GetType();
            var newElement = new XElement("value");

            if (theType == typeof(int))
            {
                newElement.Add(new XElement("i4", result.ToString()));
            }
            else if (theType == typeof(long))
            {
                newElement.Add(new XElement("long", result.ToString()));
            }
            else if (theType == typeof(double))
            {
                newElement.Add(new XElement("double", result.ToString()));
            }
            else if (theType == typeof(bool))
            {
                newElement.Add(new XElement("boolean", ((bool)result) ? 1 : 0));
            }
            else if (theType == typeof(string))
            {
                newElement.Add(new XElement("string", result.ToString()));
            }
            else if (theType == typeof(DateTime))
            {
                var date = (DateTime)result;
                newElement.Add(new XElement("dateTime.iso8601", date.ToString("yyyyMMdd'T'HH':'mm':'ss",
                                DateTimeFormatInfo.InvariantInfo)));
            }
            else if (result is IEnumerable)
            {
                var data = new XElement("data");
                foreach (var item in ((IEnumerable)result))
                {
                    if (item != null)
                    {
                        data.Add(this.SerializeValue(item));
                    }
                }

                newElement.Add(new XElement("array", data));
            }
            else
            {
                var theStruct = new XElement("struct");
                // Reference Type
                foreach (var field in theType.GetFields(BindingFlags.Public | BindingFlags.Instance))
                {
                    var member = new XElement("member");
                    member.Add(new XElement("name", field.Name));
                    var value = field.GetValue(result);
                    if (value != null)
                    {
                        member.Add(this.SerializeValue(value));
                        theStruct.Add(member);
                    }
                }
                newElement.Add(theStruct);
            }

            return newElement;
        }

        private void SerializeResponseParameters(XElement theParams, object result) => theParams.Add(new XElement("param", this.SerializeValue(result)));

        private XElement CreateStringValue(string typeName, string value) => new XElement("value", new XElement(typeName, value));

        private XElement SerializeFaultResponse(MetaWeblogException result) => new XElement(
            "fault",
            new XElement("value",
                new XElement("struct",
                    new XElement("member",
                        new XElement("name", "faultCode"),
                        this.CreateStringValue("int", result.Code.ToString())),
                    new XElement("member",
                        new XElement("name", "faultString"),
                        this.CreateStringValue("string", result.Message)))));

        private object[] GetParameters(XDocument doc)
        {
            var parameters = new List<object>();
            var paramsEle = doc.Descendants("params");

            // Handle no parameters
            if (paramsEle == null)
            {
                return parameters.ToArray();
            }

            foreach (var p in paramsEle.Descendants("param"))
            {
                parameters.AddRange(this.ParseValue(p.Element("value")));
            }

            return parameters.ToArray();
        }

        private List<object> ParseValue(XElement value)
        {
            var type = value.Descendants().FirstOrDefault();
            if (type != null)
            {
                var typename = type.Name.LocalName;
                switch (typename)
                {
                    case "array":
                        return this.ParseArray(type);
                    case "struct":
                        return this.ParseStruct(type);
                    case "i4":
                    case "int":
                        return this.ParseInt(type);
                    case "i8":
                        return this.ParseLong(type);
                    case "string":
                        return this.ParseString(type);
                    case "boolean":
                        return this.ParseBoolean(type);
                    case "double":
                        return this.ParseDouble(type);
                    case "dateTime.iso8601":
                        return this.ParseDateTime(type);
                    case "base64":
                        return this.ParseBase64(type);
                }
            }

            throw new MetaWeblogException("Failed to parse parameters");

        }

        private List<object> ParseBase64(XElement type) => new List<object> { type.Value };

        private List<object> ParseLong(XElement type) => new List<object> { long.Parse(type.Value) };

        private List<object> ParseDateTime(XElement type)
        {
            try
            {
                var parsed = type.Value.ParseISO8601();

                return new List<object> { parsed };
            }
            catch
            {
                throw new MetaWeblogException("Failed to parse date");
            }
        }

        private List<object> ParseBoolean(XElement type) => new List<object> { type.Value == "1" };

        private List<object> ParseString(XElement type) => new List<object> { type.Value };

        private List<object> ParseDouble(XElement type) => new List<object> { double.Parse(type.Value) };

        private List<object> ParseInt(XElement type) => new List<object> { int.Parse(type.Value) };

        private List<object> ParseStruct(XElement type)
        {
            var dict = new Dictionary<string, object>();
            var members = type.Descendants("member");
            foreach (var member in members)
            {
                var name = member.Element("name").Value;
                var value = this.ParseValue(member.Element("value"));
                dict[name] = value;
            }

            switch (this.method)
            {
                case "metaWeblog.newMediaObject":
                    return this.ConvertToType<MediaObject>(dict);

                case "metaWeblog.newPost":
                case "metaWeblog.editPost":
                    return this.ConvertToType<Post>(dict);

                case "wp.newCategory":
                    return this.ConvertToType<NewCategory>(dict);

                case "wp.newPage":
                case "wp.editPage":
                    return this.ConvertToType<Page>(dict);

                default:
                    throw new InvalidOperationException("Unknown type of struct discovered.");
            }

        }

        private List<object> ConvertToType<T>(Dictionary<string, object> dict) where T : new()
        {
            var info = typeof(T).GetTypeInfo();

            // Convert it
            var result = new T();
            foreach (var key in dict.Keys)
            {
                var field = info.GetDeclaredField(key);
                if (field != null)
                {
                    var container = (List<object>)dict[key];
                    var value = container.Count() == 1 ? container.First() : container.ToArray();
                    if (field.FieldType != value.GetType())
                    {
                        if (field.FieldType.IsArray && value.GetType().IsArray)
                        {
                            var valueArray = (Array)value;
                            var elementType = field.FieldType.GetElementType();
                            if (elementType != null)
                            {
                                var newValue = Array.CreateInstance(elementType, valueArray.Length);
                                Array.Copy(valueArray, newValue, valueArray.Length);
                                value = newValue;
                            }
                        }
                        else if (value.GetType().IsAssignableFrom(field.FieldType))
                        {
                            value = Convert.ChangeType(value, field.FieldType);
                        }
                        else
                        {
                            this.logger.LogWarning($"Skipping conversion to type as not supported: {field.FieldType.Name}");
                            continue;
                        }
                    }

                    field.SetValue(result, value);
                }
                else
                {
                    this.logger.LogWarning($"Skipping field {key} when converting to {typeof(T).Name}");
                }
            }

            Debug.WriteLine(result);

            return new List<object>() { result };
        }

        private List<object> ParseArray(XElement type)
        {
            try
            {
                var result = new List<object>();
                var data = type.Element("data");
                foreach (var ele in data.Elements())
                {
                    result.AddRange(this.ParseValue(ele));
                }
                return new List<object>() { result.ToArray() }; // make an array;
            }
            catch
            {
                this.logger.LogCritical($"Failed to Parse Array: {type}");
                throw;
            }
        }
    }
}
