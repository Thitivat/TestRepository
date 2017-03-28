using System;

using BND.Services.MailMan.Proxy.Interfaces;

namespace BND.Services.MailMan.Proxy.Implementations
{
    /// <summary>
    /// The mail man api config.
    /// </summary>
    public class MailManApiConfig : IMailManApiConfig
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MailManApiConfig"/> class.
        /// </summary>
        /// <param name="serviceUrl">
        /// The service url.
        /// </param>
        /// <exception cref="Exception"> Exception if the service url is not provided
        /// </exception>
        public MailManApiConfig(string serviceUrl)
        {
            if (string.IsNullOrWhiteSpace(serviceUrl))
            {
                throw new Exception("ServiceUrl cannot be null.");
            }

            ServiceUrl = serviceUrl.Trim().TrimEnd(new[] { '/', '\\' });
        }

        /// <summary>
        ///     Gets or sets the service url.
        /// </summary>
        public string ServiceUrl { get; set; }
    }
}
