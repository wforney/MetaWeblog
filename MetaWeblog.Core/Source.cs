namespace MetaWeblog
{
    using System.Xml.Serialization;

    /// <summary>
    /// The source class
    /// </summary>
    public class Source
    {
        /// <summary>
        /// Gets or sets the name of the source.
        /// </summary>
        /// <value>The name of the source.</value>
        [XmlAttribute(AttributeName = "name")]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the source URL.
        /// </summary>
        /// <value>The source URL.</value>
        [XmlAttribute(AttributeName = "url")]
        public string? Url { get; set; }
    }
}
