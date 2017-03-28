using System;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;


namespace BND.Services.Payments.iDeal.iDealClients.Interfaces
{
    /// <summary>
    /// Class ConfigurationSectionHandler is representing a section within a configuration file.
    /// </summary>
    public class ConfigurationSectionHandler : ConfigurationSection, IConfigurationSectionHandler
    {
        #region [Properties]
        /// <summary>
        /// Gets the merchant identifier.
        /// </summary>
        public string MerchantId { get { return Merchant.Id; } }

        /// <summary>
        /// Gets the merchant sub identifier.
        /// </summary>
        public int MerchantSubId { get { return Merchant.SubId; } }

        /// <summary>
        /// Gets the acquirer URL.
        /// </summary>
        public string AcquirerUrl { get { return Acquirer.Url; } }

        /// <summary>
        /// Gets the acceptant certificate store location.
        /// </summary>
        public StoreLocation? AcceptantCertificateStoreLocation { get { return AcceptantCertificate.StoreLocation; } }

        /// <summary>
        /// Gets the acceptant certificate thumbprint.
        /// </summary>
        public string AcceptantCertificateThumbprint { get { return AcceptantCertificate.Thumbprint; } }

        /// <summary>
        /// Gets the name of the acceptant certificate store.
        /// </summary>
        public string AcceptantCertificateStoreName { get { return AcceptantCertificate.StoreName; } }

        /// <summary>
        /// Gets the acceptant certificate filename.
        /// </summary>
        public string AcceptantCertificateFilename { get { return AcceptantCertificate.Filename; } }

        /// <summary>
        /// Gets the acceptant certificate password.
        /// </summary>
        public string AcceptantCertificatePassword { get { return AcceptantCertificate.Password; } }

        /// <summary>
        /// Gets the merchant.
        /// </summary>
        [ConfigurationProperty("merchant")]
        public MerchantElement Merchant { get { return (MerchantElement)this["merchant"]; } }

        /// <summary>
        /// Gets the acquirer.
        /// </summary>
        [ConfigurationProperty("acquirer")]
        public AcquirerElement Acquirer { get { return (AcquirerElement)this["acquirer"]; } }

        /// <summary>
        /// Gets the acceptant certificate.
        /// </summary>
        [ConfigurationProperty("acceptantCertificate")]
        public AcceptantCertificateElement AcceptantCertificate { get { return (AcceptantCertificateElement)this["acceptantCertificate"]; } }
        #endregion
    }


    /// <summary>
    /// Class MerchantElement is a kind of xml in iDeal section.
    /// </summary>
    public class MerchantElement : ConfigurationElement
    {
        #region [Propertoes]
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        [ConfigurationProperty("id", IsRequired = true)]
        public String Id { get { return (String)this["id"]; } }

        /// <summary>
        /// Gets the sub identifier.
        /// </summary>
        [ConfigurationProperty("subId", IsRequired = false)]
        public int SubId { get { return (int)this["subId"]; } }
        #endregion
    }


    /// <summary>
    /// Class AcquirerElement is a kind of xml in iDeal section.
    /// </summary>
    public class AcquirerElement : ConfigurationElement
    {
        #region [Propertoes]
        /// <summary>
        /// Gets the URL.
        /// </summary>
        [ConfigurationProperty("url", IsRequired = true)]
        public String Url { get { return (String)this["url"]; } }
        #endregion
    }


    /// <summary>
    /// Class AcceptantCertificateElement is a kind of xml in iDeal section.
    /// </summary>
    public class AcceptantCertificateElement : ConfigurationElement
    {
        #region [Propertoes]
        /// <summary>
        /// Gets the thumbprint.
        /// </summary>
        [ConfigurationProperty("thumbprint", IsRequired = false)]
        public String Thumbprint { get { return (String)this["thumbprint"]; } }

        /// <summary>
        /// Gets the store location.
        /// </summary>
        [ConfigurationProperty("storeLocation", IsRequired = false)]
        public StoreLocation? StoreLocation { get { return (StoreLocation?)this["storeLocation"]; } }

        /// <summary>
        /// Gets the name of the store.
        /// </summary>
        [ConfigurationProperty("storeName", IsRequired = false)]
        public String StoreName { get { return (String)this["storeName"]; } }

        /// <summary>
        /// Gets the filename.
        /// </summary>
        [ConfigurationProperty("filename", IsRequired = false)]
        public String Filename { get { return (String)this["filename"]; } }

        /// <summary>
        /// Gets the password.
        /// </summary>
        [ConfigurationProperty("password", IsRequired = false)]
        public String Password { get { return (String)this["password"]; } }
        #endregion
    }
}