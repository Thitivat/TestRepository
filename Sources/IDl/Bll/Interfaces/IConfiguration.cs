using System.Security.Cryptography.X509Certificates;

namespace BND.Services.Payments.iDeal.iDealClients.Interfaces
{
    /// <summary>
    /// Interface IConfiguration that provide properties for using.
    /// </summary>
    public interface IConfiguration
    {
        #region [Properties]
        /// <summary>
        /// Gets the merchant identifier.
        /// </summary>
        string MerchantId { get; }

        /// <summary>
        /// Gets the merchant sub identifier.
        /// </summary>
        int MerchantSubId { get; }

        /// <summary>
        /// Gets the acquirer URL.
        /// </summary>
        string AcquirerUrl { get; }

        /// <summary>
        /// Gets the acceptant certificate.
        /// </summary>
        X509Certificate2 AcceptantCertificate { get; }
        #endregion
    }
}
