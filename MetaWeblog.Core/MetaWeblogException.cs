namespace MetaWeblog
{
    using System;

    /// <summary>
    /// The MetaWeblog exception class.
    /// Implements the <see cref="Exception" />
    /// </summary>
    /// <seealso cref="Exception" />
    public class MetaWeblogException : Exception
    {
        /// <summary>
        /// Gets the code.
        /// </summary>
        /// <value>The code.</value>
        public int Code { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MetaWeblogException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="code">The code.</param>
        public MetaWeblogException(string message, int code = 1) : base(message) => this.Code = code;
    }
}
