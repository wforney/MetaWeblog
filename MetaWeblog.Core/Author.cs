using System.Xml.Serialization;

namespace MetaWeblog
{
    /// <summary>
    /// The author class.
    /// </summary>
    public class Author
    {
        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>The display name.</value>
        [XmlAttribute(AttributeName = "display_name")]
        public string? DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the meta value.
        /// </summary>
        /// <value>The meta value.</value>
        [XmlAttribute(AttributeName = "meta_value")]
        public string? MetaValue { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        [XmlAttribute(AttributeName = "user_id")]
        public string? UserId { get; set; }

        /// <summary>
        /// Gets or sets the user login.
        /// </summary>
        /// <value>The user login.</value>
        [XmlAttribute(AttributeName = "user_login")]
        public string? UserLogin { get; set; }
    }
}
