namespace MetaWeblog
{
    using System.Xml.Serialization;

    /// <summary>
    /// The new category class
    /// </summary>
    public class NewCategory
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [XmlAttribute(AttributeName = "description")]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [XmlAttribute(AttributeName = "name")]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the parent identifier.
        /// </summary>
        /// <value>The parent identifier.</value>
        [XmlAttribute(AttributeName = "parent_id")]
        public int? ParentId { get; set; }

        /// <summary>
        /// Gets or sets the slug.
        /// </summary>
        /// <value>The slug.</value>
        [XmlAttribute(AttributeName = "slug")]
        public string? Slug { get; set; }
    }
}
