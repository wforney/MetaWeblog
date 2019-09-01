namespace MetaWeblog
{
    using System.Xml.Serialization;

    /// <summary>
    /// The media object class
    /// </summary>
    public class MediaObject
    {
        /// <summary>
        /// Gets or sets the bits.
        /// </summary>
        /// <value>The bits.</value>
        [XmlAttribute(AttributeName = "bits")]
        public string? Bits { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [XmlAttribute(AttributeName = "name")]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        [XmlAttribute(AttributeName = "type")]
        public string? Type { get; set; }
    }
}
