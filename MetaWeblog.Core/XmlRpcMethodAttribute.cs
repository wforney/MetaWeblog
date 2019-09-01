namespace MetaWeblog
{
    using System;

    /// <summary>
    /// The XML RPC method attribute class.
    /// </summary>
    public class XmlRpcMethodAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XmlRpcMethodAttribute"/> class.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        public XmlRpcMethodAttribute(string methodName) => this.MethodName = methodName;

        /// <summary>
        /// Gets or sets the name of the method.
        /// </summary>
        /// <value>The name of the method.</value>
        public string MethodName { get; set; }
    }
}
