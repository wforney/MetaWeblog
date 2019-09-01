namespace MetaWeblog
{
    using System.Xml.Serialization;

    /// <summary>
    /// The enclosure class
    /// </summary>
    public class Enclosure
    {
        /// <summary>
        /// Gets or sets the length.
        /// </summary>
        /// <value>The length.</value>
        [XmlAttribute(AttributeName ="length")]
        public int? Length { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        [XmlAttribute(AttributeName ="type")]
        public string? Type { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        [XmlAttribute(AttributeName ="url")]
        public string? Url { get; set; }
    }
}
