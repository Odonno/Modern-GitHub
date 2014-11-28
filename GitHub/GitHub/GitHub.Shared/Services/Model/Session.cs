using System;

namespace GitHub.Services.Model
{
    /// <summary>
    /// The session.
    /// </summary>
    public class Session
    {
        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        /// <value>
        /// The access token.
        /// </value>
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id value.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the expire date.
        /// </summary>
        /// <value>
        /// The expire date.
        /// </value>
        public DateTime ExpireDate { get; set; }
    }
}
