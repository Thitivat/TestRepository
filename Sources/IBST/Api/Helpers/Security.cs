using BND.Services.IbanStore.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BND.Services.IbanStore.Api.Helpers
{
    /// <summary>
    /// Class Security is a helper class for manipulate about security task.
    /// </summary>
    public class Security : ISecurity
    {
        /// <summary>
        /// Authenticates the specified credentials.
        /// </summary>
        /// <param name="credentials">The credentials.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Authenticate(CredentialsModel credentials)
        {
            return true;
        }

        /// <summary>
        /// Getusers the credential.
        /// </summary>
        /// <returns>CredentialsModel.</returns>
        public CredentialsModel GetUserCredential(string token, string uidPrefix)
        {
            CredentialsModel credential = new CredentialsModel(token);
            credential.Token = token;
            credential.UidPrefix = uidPrefix;
            return credential;
        }



    }
}