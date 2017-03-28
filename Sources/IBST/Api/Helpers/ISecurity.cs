using BND.Services.IbanStore.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BND.Services.IbanStore.Api.Helpers
{
    /// <summary>
    /// Interface ISecurity for security components.
    /// </summary>
    public interface ISecurity
    {
        /// <summary>
        /// Authenticates the specified credentials.
        /// </summary>
        /// <param name="credentials">The credentials.</param>
        /// <returns><c>true</c> if authorized, <c>false</c> otherwise.</returns>
        bool Authenticate(CredentialsModel credentials);
        CredentialsModel GetUserCredential(string token, string uidPrefix);
    }
}
