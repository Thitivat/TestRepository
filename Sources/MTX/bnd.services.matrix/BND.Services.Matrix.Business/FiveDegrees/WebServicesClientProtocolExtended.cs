using System;
using System.Collections.Concurrent;
using System.Net;

using Microsoft.Web.Services3;

namespace BND.Services.Matrix.Business.FiveDegrees
{
    /// <summary>
    /// The base soap http headers.
    /// </summary>
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public class WebServicesClientProtocolExtended : WebServicesClientProtocol
    {
        /// <summary>
        /// The request headers concurrent collection.
        /// </summary>
        private ConcurrentDictionary<string, string> requestHeaders = new ConcurrentDictionary<string, string>();

        /// <summary>
        /// This adds custom headers
        /// </summary>
        /// <param name="headerName">The header name</param>
        /// <param name="headerValue">The header value</param>
        public void SetRequestHeader(string headerName, string headerValue)
        {
            this.requestHeaders.AddOrUpdate(headerName, headerValue, (key, oldValue) => headerValue);
        }

        /// <summary>
        /// This method activates custom http headers in soap requests implementing the WebServicesClientProtocol
        /// </summary>
        /// <param name="request">The WebRequest object</param>
        /// <returns>Returns the request with custom Http headers</returns>
        public WebRequest WithCustomHttpHeaders(WebRequest request)
        {
            var httpRequest = request as HttpWebRequest;
            if (httpRequest != null)
            {
                foreach (string headerName in this.requestHeaders.Keys)
                {
                    httpRequest.Headers[headerName] = this.requestHeaders[headerName];
                }
            }

            return request;
        }

        /// <summary>
        /// The get web request.
        /// </summary>
        /// <param name="uri">
        /// The uri.
        /// </param>
        /// <returns>
        /// The <see cref="WebRequest"/>.
        /// </returns>
        protected override WebRequest GetWebRequest(Uri uri)
        {
            var request = base.GetWebRequest(uri);
            return WithCustomHttpHeaders(request);
        }
    }
}
