using BND.Services.Payments.iDeal.iDealClients.Base;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace BND.Services.Payments.iDeal.iDealClients.Directory
{
    /// <summary>
    /// Class DirectoryResponse for response a list of bank from iDeal.
    /// </summary>
    public class DirectoryResponse : iDealResponseBase
    {
        #region [Field]
        /// <summary>
        /// The _issuers
        /// </summary>
        private readonly IList<IssuerModel> _issuers = new List<IssuerModel>();
        #endregion


        #region [Properties]
        /// <summary>
        /// Gets the directory date time stamp.
        /// </summary>
        public string DirectoryDateTimeStamp { get; private set; }

        /// <summary>
        /// Gets the directory date time stamp local time.
        /// </summary>
        public DateTime DirectoryDateTimeStampLocalTime { get { return DateTime.Parse(DirectoryDateTimeStamp); } }

        /// <summary>
        /// Gets the issuers.
        /// </summary>
        public IEnumerable<IssuerModel> Issuers { get { return new List<IssuerModel>(_issuers); } }
        #endregion


        #region [Constructor]
        /// <summary>
        /// Initializes a new instance of the <see cref="DirectoryResponse"/> class.
        /// </summary>
        /// <param name="xmlDirectoryResponse">The XML directory response.</param>
        public DirectoryResponse(string xmlDirectoryResponse)
        {
            // Parse document
            var xDocument = XElement.Parse(xmlDirectoryResponse);
            XNamespace xmlNamespace = Properties.Resources.XML_NAMESPACE;

            // Create datetimestamp
            CreateDateTimeStamp = Convert.ToDateTime(xDocument.Element(xmlNamespace + "createDateTimestamp").Value);

            // Acquirer id
            AcquirerId = (int)xDocument.Element(xmlNamespace + "Acquirer").Element(xmlNamespace + "acquirerID");

            // Directory datetimestamp
            DirectoryDateTimeStamp = xDocument.Element(xmlNamespace + "Directory").Element(xmlNamespace + "directoryDateTimestamp").Value;

            // Get list of issuers
            foreach (var country in xDocument.Element(xmlNamespace + "Directory").Elements(xmlNamespace + "Country"))
            {
                foreach (var issuer in country.Elements(xmlNamespace + "Issuer"))
                {
                    _issuers.Add(
                            new IssuerModel
                            {
                                IssuerID = issuer.Element(xmlNamespace + "issuerID").Value,
                                IssuerName = issuer.Element(xmlNamespace + "issuerName").Value,
                                CountryNames = country.Element(xmlNamespace + "countryNames").Value
                            });
                }

            }
        }
        #endregion
    }
}
