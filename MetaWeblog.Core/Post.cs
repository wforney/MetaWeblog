namespace MetaWeblog
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// The post class
    /// </summary>
    public class Post
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
        /// Gets or sets the link.
        /// </summary>
        /// <value>The link.</value>
        [XmlAttribute(AttributeName = "link")]
        public string? Link { get; set; }

        /// <summary>
        /// Gets or sets the MT excerpt.
        /// </summary>
        /// <value>The MT excerpt.</value>
        [XmlAttribute(AttributeName = "mt_excerpt")]
        public string? MTExcerpt { get; set; }

        /// <summary>
        /// Gets or sets the permalink.
        /// </summary>
        /// <value>The permalink.</value>
        [XmlAttribute(AttributeName = "permalink")]
        public string? Permalink { get; set; }

        /// <summary>
        /// Gets or sets the post identifier.
        /// </summary>
        /// <value>The post identifier.</value>
        [XmlAttribute(AttributeName = "postid")]
        public object? PostId { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [XmlAttribute(AttributeName = "title")]
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        [XmlAttribute(AttributeName = "userid")]
        public string? UserId { get; set; }

        /// <summary>
        /// Gets or sets the WordPress slug.
        /// </summary>
        /// <value>The WordPress slug.</value>
        [XmlAttribute(AttributeName = "wp_slug")]
        public string? WordPressSlug { get; set; }
    }
}
