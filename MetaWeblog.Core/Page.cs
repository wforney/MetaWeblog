namespace MetaWeblog
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// The page class
    /// </summary>
    public class Page
    {
        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        /// <value>The categories.</value>
        [XmlAttribute(AttributeName = "categories")]
        public string?[]? Categories { get; set; } = Array.Empty<string?>();

        /// <summary>
        /// Gets or sets the date created.
        /// </summary>
        /// <value>The date created.</value>
        [XmlAttribute(AttributeName = "dateCreated")]
        public DateTime? DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [XmlAttribute(AttributeName = "description")]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the page identifier.
        /// </summary>
        /// <value>The page identifier.</value>
        [XmlAttribute(AttributeName = "page_id")]
        public string? PageId { get; set; }

        /// <summary>
        /// Gets or sets the page parent identifier.
        /// </summary>
        /// <value>The page parent identifier.</value>
        [XmlAttribute(AttributeName = "page_parent_id")]
        public string? PageParentId { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [XmlAttribute(AttributeName = "title")]
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets the WordPress author identifier.
        /// </summary>
        /// <value>The WordPress author identifier.</value>
        [XmlAttribute(AttributeName = "wp_author_id")]
        public string? WordPressAuthorId { get; set; }
    }
}
