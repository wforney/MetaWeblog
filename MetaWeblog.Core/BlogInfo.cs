namespace MetaWeblog
{
    using System.Xml.Serialization;

    /// <summary>
    /// The blog information class
    /// </summary>
    public class BlogInfo
    {
        /// <summary>
        /// The blog identifier
        /// </summary>
        [XmlAttribute(AttributeName = "blogid")]
        public string? BlogId { get; set; }

        /// <summary>
        /// The blog name
        /// </summary>
        [XmlAttribute(AttributeName = "blogName")]
        public string? BlogName;

        /// <summary>
        /// The blog URL
        /// </summary>
        [XmlAttribute(AttributeName = "url")]
        public string? Url;
    }
}
