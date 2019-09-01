namespace MetaWeblog
{
    using System.Xml.Serialization;

    /// <summary>
    /// The user information class
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        [XmlAttribute(AttributeName = "email")]
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        [XmlAttribute(AttributeName = "firstname")]
        public string? FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        [XmlAttribute(AttributeName = "lastname")]
        public string? LastName { get; set; }

        /// <summary>
        /// Gets or sets the nickname.
        /// </summary>
        /// <value>The nickname.</value>
        [XmlAttribute(AttributeName = "nickname")]
        public string? Nickname { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        [XmlAttribute(AttributeName = "url")]
        public string? Url { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        [XmlAttribute(AttributeName = "userid")]
        public string? UserId { get; set; }
    }
}
