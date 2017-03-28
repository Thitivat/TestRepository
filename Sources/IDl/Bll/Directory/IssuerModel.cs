
namespace BND.Services.Payments.iDeal.iDealClients.Directory
{
    /// <summary>
    /// Class IssuerModel is a issuer object.
    /// </summary>
    public class IssuerModel
    {
        /// <summary>
        /// Gets or sets the Bank Identifier Code (BIC) of the iDEAL Issuer
        /// </summary>
        public string IssuerID { get; set; }

        /// <summary>
        /// Gets or sets the name of the Issuer (as this should be displayed to the Consumer in the Merchant's Issuer list).
        /// </summary>
        public string IssuerName { get; set; }

        /// <summary>
        /// Contains the countryNamesin the official  languages of the country.
        /// </summary>
        public string CountryNames { get; set; }
    }
}
