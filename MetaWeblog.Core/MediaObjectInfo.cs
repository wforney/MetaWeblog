namespace MetaWeblog
{
    using System.Xml.Serialization;

    /// <summary>
    /// The media object information class
    /// </summary>
    public class MediaObjectInfo
    {
        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        [XmlAttribute(AttributeName = "url")]
        public string? Url { get; set; }
    }
}
