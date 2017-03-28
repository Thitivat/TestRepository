using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace BND.Services.Payments.iDeal.iDealClients.Interfaces
{
    /// <summary>
    /// Default configuration will load information from .config file.
    /// </summary>
    public class DefaultConfiguration : IConfiguration
    {
        #region [Properties]
        /// <summary>
        /// Gets the merchant identifier.
        /// </summary>
        public string MerchantId { get; private set; }

        /// <summary>
        /// Gets the merchant sub identifier.
        /// </summary>
        public int MerchantSubId { get; private set; }

        /// <summary>
        /// Gets the acquirer URL.
        /// </summary>
        public string AcquirerUrl { get; private set; }

        /// <summary>
        /// Gets the acceptant certificate.
        /// </summary>
        public X509Certificate2 AcceptantCertificate { get; private set; }
        #endregion


        #region [Constructor]
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultConfiguration"/> class.
        /// </summary>
        /// <param name="configurationSectionHandler">The configuration section handler.</param>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="subId">The sub identifier.</param>
        /// <exception cref="ConfigurationErrorsException">
        /// Password is required when acceptant's certificate is loaded from filesystem
        /// or
        /// Acceptant's certificate store name is required when loading certificate from the certificate store
        /// or
        /// Acceptant's certificate thumbprint is required when loading certificate from the certificate store
        /// or
        /// You should either specify a filename or a certificate store location to specify the acceptant's certificate.
        /// or
        /// Acquirer's certificate store name is required when loading certificate from the certificate store
        /// or
        /// Acquirer's certificate thumbprint is required when loading certificate from the certificate store
        /// or
        /// You should either specify a filename or a certificate store location to specify the acquirer's certificate.
        /// </exception>
        public DefaultConfiguration(IConfigurationSectionHandler configurationSectionHandler, string merchantId, int subId)
        {
            if (merchantId == null)
            {
                MerchantId = configurationSectionHandler.MerchantId;
            }
            else
            {
                MerchantId = merchantId;
            }

            MerchantSubId = subId;
            AcquirerUrl = configurationSectionHandler.AcquirerUrl;

            // Retrieve acceptant's certificate
            if (!String.IsNullOrWhiteSpace(configurationSectionHandler.AcceptantCertificateFilename))
            {
                // Retrieve certificate from file
                if (String.IsNullOrWhiteSpace(configurationSectionHandler.AcceptantCertificatePassword))
                {
                    throw new ConfigurationErrorsException("Password is required when acceptant's certificate is loaded from filesystem");
                }

                AcceptantCertificate = GetCertificateFromFile(configurationSectionHandler.AcceptantCertificateFilename,
                                                              configurationSectionHandler.AcceptantCertificatePassword);
            }
            else if (configurationSectionHandler.AcceptantCertificateStoreLocation != null)
            {
                // Retrieve certificate from certificate store
                if (String.IsNullOrWhiteSpace(configurationSectionHandler.AcceptantCertificateStoreName))
                {
                    throw new ConfigurationErrorsException
                        ("Acceptant's certificate store name is required when loading certificate from the certificate store");
                }

                if (String.IsNullOrWhiteSpace(configurationSectionHandler.AcceptantCertificateThumbprint))
                {
                    throw new ConfigurationErrorsException
                        ("Acceptant's certificate thumbprint is required when loading certificate from the certificate store");
                }

                AcceptantCertificate = GetCertificateFromStore(configurationSectionHandler.AcceptantCertificateStoreLocation.Value,
                                                               configurationSectionHandler.AcceptantCertificateStoreName,
                                                               configurationSectionHandler.AcceptantCertificateThumbprint);
            }
            else
            {
                // Neither filename nor store location is specified
                throw new ConfigurationErrorsException
                    ("You should either specify a filename or a certificate store location to specify the acceptant's certificate.");
            }
        }
        #endregion


        #region [Method]
        /// <summary>
        /// Gets the certificate from file.
        /// </summary>
        /// <param name="relativePath">The relative path.</param>
        /// <param name="password">The password.</param>
        /// <returns>X509Certificate2.</returns>
        /// <exception cref="ConfigurationErrorsException">Could not load certificate file</exception>
        private static X509Certificate2 GetCertificateFromFile(string relativePath, string password)
        {
            try
            {
                var absolutePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
                return new X509Certificate2(absolutePath, password, X509KeyStorageFlags.Exportable);
            }
            catch (Exception exception)
            {
                throw new ConfigurationErrorsException("Could not load certificate file", exception);
            }
        }

        /// <summary>
        /// Gets the certificate from store.
        /// </summary>
        /// <param name="storeLocation">The store location.</param>
        /// <param name="storeName">Name of the store.</param>
        /// <param name="thumbprint">The thumbprint.</param>
        /// <returns>X509Certificate2.</returns>
        /// <exception cref="ConfigurationErrorsException">
        /// Could not retrieve certificate from store  + storeName
        /// or
        /// Certificate with thumbprint ' + thumbprint + ' not found
        /// </exception>
        private static X509Certificate2 GetCertificateFromStore(StoreLocation storeLocation, string storeName, string thumbprint)
        {
            try
            {
                var certificateStore = new X509Store(storeName, storeLocation);
                certificateStore.Open(OpenFlags.OpenExistingOnly);

                foreach (var certificate in certificateStore.Certificates)
                {
                    if (certificate.Thumbprint.Trim().ToUpper() == thumbprint.Replace(" ", "").ToUpper())
                    {
                        return certificate;
                    }
                }
            }
            catch (Exception exception)
            {
                throw new ConfigurationErrorsException("Could not retrieve certificate from store " + storeName, exception);
            }

            throw new ConfigurationErrorsException("Certificate with thumbprint '" + thumbprint + "' not found");
        }
        #endregion
    }
}