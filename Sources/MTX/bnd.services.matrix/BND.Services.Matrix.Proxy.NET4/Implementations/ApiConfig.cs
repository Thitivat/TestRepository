using System;

using BND.Services.Matrix.Proxy.NET4.Interfaces;

namespace BND.Services.Matrix.Proxy.NET4.Implementations
{
    /// <summary>
    /// The accounts api config.
    /// </summary>
    public class ApiConfig : IApiConfig
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiConfig"/> class.
        /// </summary>
        /// <param name="serviceUrl">
        /// The service url.
        /// </param>
        /// <exception cref="Exception"> 
        /// The exception
        /// </exception>
        public ApiConfig(string serviceUrl)
        {
            if (string.IsNullOrWhiteSpace(serviceUrl))
            {
                throw new Exception("ServiceUrl cannot be null.");
            }

            ServiceUrl = serviceUrl.Trim().TrimEnd(new[] { '/', '\\' });
        }

        /// <summary>
        /// Gets or sets the service url.
        /// </summary>
        public string ServiceUrl { get; set; }
    }
}
