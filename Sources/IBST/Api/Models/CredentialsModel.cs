using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BND.Services.IbanStore.Api.Models
{
    /// <summary>
    /// Class CredentialsModel.
    /// </summary>
    public class CredentialsModel
    {
        /// <summary>
        /// Gets the token.
        /// </summary>
        /// <value>The token.</value>
        public string Token { get;  set; }
        /// <summary>
        /// Gets the username.
        /// </summary>
        /// <value>The username.</value>
        public string Username { get;  set; }

        /// <summary>
        /// Gets or sets the uif prefix.
        /// </summary>
        /// <value>The uif prefix.</value>
        public string UidPrefix { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CredentialsModel"/> class.
        /// </summary>
        /// <param name="authorizationHeader">The authorization header.</param>
        public CredentialsModel(string authorizationHeader)
        {
            Token = authorizationHeader;
            Username = authorizationHeader;
        }
    }
}