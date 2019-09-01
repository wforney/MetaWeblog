namespace MetaWeblog
{
    using System.Xml.Serialization;

    /// <summary>
    /// The category information class
    /// </summary>
    public class CategoryInfo
    {
        /// <summary>
        /// Gets or sets the category identifier.
        /// </summary>
        /// <value>The category identifier.</value>
        [XmlAttribute(AttributeName = "categoryid")]
        public string? CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [XmlAttribute(AttributeName = "description")]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the HTML URL.
        /// </summary>
        /// <value>The HTML URL.</value>
        [XmlAttribute(AttributeName = "htmlUrl")]
        public string? HtmlUrl { get; set; }

        /// <summary>
        /// Gets or sets the RSS URL.
        /// </summary>
        /// <value>The RSS URL.</value>
        [XmlAttribute(AttributeName = "rssUrl")]
        public string? RssUrl { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [XmlAttribute(AttributeName = "title")]
        public string? Title { get; set; }
    }
}
