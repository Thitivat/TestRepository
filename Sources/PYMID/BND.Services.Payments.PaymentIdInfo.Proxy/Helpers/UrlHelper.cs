using System;

namespace BND.Services.Payments.PaymentIdInfo.Proxy.Helpers
{
    /// <summary>
    /// Class UrlHelper.
    /// </summary>
    public class UrlHelper
    {
        /// <summary>
        /// Generates the URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="filterTypes">The filter types.</param>
        /// <returns>System.String.</returns>
        public static string GenerateUrl(string url, string filterTypes)
        {
            return
                Uri.EscapeUriString(
                    !String.IsNullOrEmpty(filterTypes) ? url + String.Format(Properties.Resources.FILTER_TYPE, filterTypes) : url
                );
        }
    }
}
