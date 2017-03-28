using BND.Services.Payments.iDeal.iDealClients.Base;
using BND.Services.Payments.iDeal.iDealClients.Interfaces;
using System;
using System.Xml;
using System.Xml.Linq;

namespace BND.Services.Payments.iDeal.iDealClients.Status
{
    /// <summary>
    /// Class StatusRequest is a request for status of transaction.
    /// </summary>
    public class StatusRequest : iDealRequestBase
    {
        #region [Field]
        /// <summary>
        /// The _transaction identifier
        /// </summary>
        private string _transactionId;
        #endregion


        #region [Property]
        /// <summary>
        /// Unique 16 digits number, assigned by the acquirer to the transaction
        /// </summary>
        /// <value>The transaction identifier.</value>
        /// <exception cref="System.ArgumentException">TransactionId must contain exactly 16 characters</exception>
        public string TransactionId
        {
            get { return _transactionId; }
            private set
            {
                if (String.IsNullOrWhiteSpace(value) || value.Length != 16)
                {
                    throw new ArgumentException("TransactionId must contain exactly 16 characters");
                }
                _transactionId = value;
            }
        }
        #endregion


        #region [Constructor]
        /// <summary>
        /// Initializes a new instance of the <see cref="StatusRequest"/> class.
        /// </summary>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="subId">The sub identifier.</param>
        /// <param name="transactionId">The transaction identifier.</param>
        public StatusRequest(string merchantId, int? subId, string transactionId)
        {
            MerchantId = merchantId;
            MerchantSubId = subId ?? 0; // If no sub id is specified, sub id should be 0
            TransactionId = transactionId;
        }
        #endregion


        #region [Method]
        /// <summary>
        /// To the XML.
        /// </summary>
        /// <param name="signatureProvider">The signature provider.</param>
        /// <returns>XmlDocument.</returns>
        public override XmlDocument ToXml(ISignatureProvider signatureProvider)
        {
            XNamespace xmlNamespace = Properties.Resources.XML_NAMESPACE;

            var requestXmlMessage = new XDocument(new XDeclaration("1.0", "UTF-8", null),
                                                    new XElement(xmlNamespace + "AcquirerStatusReq",
                                                        new XAttribute("version", "3.3.1"),
                                                        new XElement(xmlNamespace + "createDateTimestamp", CreateDateTimeStamp),
                                                        new XElement(xmlNamespace + "Merchant",
                                                            new XElement(xmlNamespace + "merchantID", MerchantId.PadLeft(9, '0')),
                                                            new XElement(xmlNamespace + "subID", MerchantSubId)),
                                                        new XElement(xmlNamespace + "Transaction",
                                                            new XElement(xmlNamespace + "transactionID", TransactionId))
                                                    ));

            var xmlDocument = new XmlDocument();
            using (var xmlReader = requestXmlMessage.CreateReader())
            {
                xmlDocument.Load(xmlReader);
            }

            return xmlDocument;
        }
        #endregion
    }
}