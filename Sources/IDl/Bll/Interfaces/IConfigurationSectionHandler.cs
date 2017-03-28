using System.Security.Cryptography.X509Certificates;

namespace BND.Services.Payments.iDeal.iDealClients.Interfaces
{
    /// <summary>
    /// Interface IConfigurationSectionHandler that provides the properties for using.
    /// </summary>
    public interface IConfigurationSectionHandler
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
        /// Gets the acceptant certificate store location.
        /// </summary>
        StoreLocation? AcceptantCertificateStoreLocation { get; }

        /// <summary>
        /// Gets the acceptant certificate thumbprint.
        /// </summary>
        string AcceptantCertificateThumbprint { get; }

        /// <summary>
        /// Gets the name of the acceptant certificate store.
        /// </summary>
        string AcceptantCertificateStoreName { get; }

        /// <summary>
        /// Gets the acceptant certificate filename.
        /// </summary>
        string AcceptantCertificateFilename { get; }

        /// <summary>
        /// Gets the acceptant certificate password.
        /// </summary>
        string AcceptantCertificatePassword { get; }
        #endregion
    }
}