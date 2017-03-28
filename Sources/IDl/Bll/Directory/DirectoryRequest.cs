using BND.Services.Payments.iDeal.iDealClients.Base;
using BND.Services.Payments.iDeal.iDealClients.Interfaces;
using System.Xml;
using System.Xml.Linq;

namespace BND.Services.Payments.iDeal.iDealClients.Directory
{
    /// <summary>
    /// Class DirectoryRequest for request a list of bank from iDeal.
    /// </summary>
    public class DirectoryRequest : iDealRequestBase
    {
        #region [Properties]
        /// <summary>
        /// Creates xml representation of directory request.
        /// </summary>
        /// <param name="signatureProvider">The signature provider.</param>
        /// <returns>XmlDocument.</returns>
        public override XmlDocument ToXml(ISignatureProvider signatureProvider)
        {
            XNamespace xmlNamespace = Properties.Resources.XML_NAMESPACE;
            XNamespace xmlNamespaceSignature = Properties.Resources.XML_SIGNATURE;

            var requestXmlMessage = new XDocument(
                   new XDeclaration("1.0", "UTF-8", null),
                   new XElement(xmlNamespace + "DirectoryReq",
                       new XAttribute("version", "3.3.1"),
                       new XElement(xmlNamespace + "createDateTimestamp", CreateDateTimeStamp),
                       new XElement(xmlNamespace + "Merchant",
                           new XElement(xmlNamespace + "merchantID", MerchantId.PadLeft(9, '0')),
                           new XElement(xmlNamespace + "subID", MerchantSubId))));

            var xmlDocument = new XmlDocument();
            using (var xmlReader = requestXmlMessage.CreateReader())
            {
                xmlDocument.Load(xmlReader);
            }

            return xmlDocument;
        }
        #endregion


        #region [Constructor]
        /// <summary>
        /// Initializes a new instance of the <see cref="DirectoryRequest"/> class.
        /// </summary>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="subId">The sub identifier.</param>
        public DirectoryRequest(string merchantId, int? subId)
        {
            base.MerchantId = merchantId;
            base.MerchantSubId = subId ?? 0;
        }
        #endregion
    }
}
