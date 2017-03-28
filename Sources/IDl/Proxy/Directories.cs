using BND.Services.Payments.iDeal.Proxy.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace BND.Services.Payments.iDeal.Proxy
{
    /// <summary>
    /// Class Directories is a proxy component to get Directories resource from iDeal rest api.
    /// </summary>
    public class Directories : ResourceBase, IDirectories
    {

        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="Directories" /> class.
        /// </summary>
        /// <param name="baseAddress">The base address of api.</param>
        /// <param name="token">The token string that implement the authentication key .</param>
        public Directories(string baseAddress, string token)
            : base(baseAddress, token)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Directories" /> class.
        /// </summary>
        /// <param name="baseAddress">The base address of api.</param>
        /// <param name="token">The token string that implement the authentication key .</param>
        /// <param name="httpClient">The HTTP client.</param>
        public Directories(string baseAddress, string token, HttpClient httpClient)
            : base(baseAddress, token, httpClient)
        { }

        #endregion


        #region [Public Methods]
        /// <summary>
        /// Interface IDirectories is an interface providing Directory resource.
        /// </summary>
        public IEnumerable<Models.DirectoryModel> GetDirectories()
        {
            // Calls api with get method to gets issuer bank list.
            HttpResponseMessage result = base._httpClient.GetAsync(Uri.EscapeUriString(Properties.Resources.URL_RES_DIRECTORY)).Result;

            // Returns result by calling 
            return base.SetResult<IEnumerable<Models.DirectoryModel>>(result);
        }

        #endregion
    }
}
