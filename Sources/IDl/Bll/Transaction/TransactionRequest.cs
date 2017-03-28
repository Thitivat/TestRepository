﻿using BND.Services.Payments.iDeal.iDealClients.Base;
using BND.Services.Payments.iDeal.iDealClients.Interfaces;
using System;
using System.Xml;
using System.Xml.Linq;

namespace BND.Services.Payments.iDeal.iDealClients.Transaction
{
    /// <summary>
    /// Class TransactionRequest is a request for transaction.
    /// </summary>
    public class TransactionRequest : iDealRequestBase
    {
        #region [Fields]
        /// <summary>
        /// The merchant return URL
        /// </summary>
        private string _merchantReturnUrl;
        /// <summary>
        /// The purchase identifier
        /// </summary>
        private string _purchaseId;
        /// <summary>
        /// The expiration period
        /// </summary>
        private TimeSpan? _expirationPeriod;
        /// <summary>
        /// The description
        /// </summary>
        private string _description;
        /// <summary>
        /// The entrance code
        /// </summary>
        private string _entranceCode;
        #endregion


        #region [Properties]
        /// <summary>
        /// Unique identifier of issuer
        /// </summary>
        public string IssuerCode { get; private set; }
        /// <summary>
        /// Url to which consumer is redirected after authorizing the payment
        /// </summary>
        /// <value>The merchant return URL.</value>
        /// <exception cref="System.ArgumentNullException">Merchant url is required</exception>
        public string MerchantReturnUrl
        {
            get { return _merchantReturnUrl; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Merchant url is required");
                }
                _merchantReturnUrl = value.Trim();
            }
        }
        /// <summary>
        ///  Unique id determined by the acceptant, which will eventuelly show on the bank account
        /// </summary>
        /// <value>The purchase identifier.</value>
        /// <exception cref="System.ArgumentNullException">Purchase id is required</exception>
        /// <exception cref="System.ArgumentException">Purchase id cannot contain more than 35 characters</exception>
        public string PurchaseId
        {
            get { return _purchaseId; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Purchase id is required");
                }
                if (value.Length > 35)
                {
                    throw new ArgumentException("Purchase id cannot contain more than 35 characters");
                }
                _purchaseId = value;
            }
        }
        /// <summary>
        /// Amount measured
        /// </summary>
        public decimal Amount { get; private set; }
        /// <summary>
        /// Time until consumer has to have paid, otherwise the transaction is marked as expired by the issuer (consumer's bank)
        /// </summary>
        /// <value>The expiration period.</value>
        /// <exception cref="System.ArgumentException">
        /// Minimum expiration period is one minute
        /// or
        /// Maximum expiration period is 1 hour
        /// </exception>
        public TimeSpan? ExpirationPeriod
        {
            get { return _expirationPeriod; }
            set
            {
                if (value.HasValue)
                {
                    if (value.Value.TotalMinutes < 1)
                    {
                        throw new ArgumentException("Minimum expiration period is one minute");
                    }
                    if (value.Value.TotalMinutes > 60)
                    {
                        throw new ArgumentException("Maximum expiration period is 1 hour");
                    }
                }
                _expirationPeriod = value;
            }
        }
        /// <summary>
        /// Description ordered product (no html tags!)
        /// </summary>
        /// <value>The description.</value>
        /// <exception cref="System.ArgumentException">Description cannot contain more than 35 characters</exception>
        public string Description
        {
            get { return _description; }
            set
            {
                if (value.Trim().Length > 35) { throw new ArgumentException("Description cannot contain more than 35 characters"); }
                _description = value.Trim();
            }
        }
        /// <summary>
        /// Unique code generated by acceptant by which consumer can be identified
        /// </summary>
        /// <value>The entrance code.</value>
        /// <exception cref="System.ArgumentNullException">Entrance code is required</exception>
        /// <exception cref="System.ArgumentException">Entrance code cannot contain more than 40 characters</exception>
        public string EntranceCode
        {
            get { return _entranceCode; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Entrance code is required");
                }
                if (value.Length > 40)
                {
                    throw new ArgumentException("Entrance code cannot contain more than 40 characters");
                }
                _entranceCode = value;
            }
        }
        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        public string Language { get; set; }
        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        public string Currency { get; set; }
        #endregion


        #region [Constructor]
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionRequest"/> class.
        /// </summary>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="subId">The sub identifier.</param>
        /// <param name="issuerCode">The issuer code.</param>
        /// <param name="merchantReturnUrl">The merchant return URL.</param>
        /// <param name="purchaseId">The purchase identifier.</param>
        /// <param name="amount">The amount.</param>
        /// <param name="expirationPeriod">The expiration period.</param>
        /// <param name="description">The description.</param>
        /// <param name="entranceCode">The entrance code.</param>
        /// <param name="language">The language.</param>
        /// <param name="currency">The currency.</param>
        public TransactionRequest(string merchantId, int? subId, string issuerCode, string merchantReturnUrl, string purchaseId, decimal amount,
                                  TimeSpan? expirationPeriod, string description, string entranceCode, string language, string currency)
        {
            MerchantId = merchantId;
            MerchantSubId = subId ?? 0; // If no sub id is specified, sub id should be 0
            IssuerCode = issuerCode;
            MerchantReturnUrl = merchantReturnUrl;
            PurchaseId = purchaseId;
            Amount = amount;
            ExpirationPeriod = expirationPeriod ?? TimeSpan.FromMinutes(30); // Default 30 minutes expiration
            Description = description;
            EntranceCode = entranceCode;
            Language = language;
            Currency = currency;
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

            var requestXmlMessage =
                new XDocument(
                    new XDeclaration("1.0", "UTF-8", null),
                    new XElement(xmlNamespace + "AcquirerTrxReq",
                        new XAttribute("version", "3.3.1"),
                        new XElement(xmlNamespace + "createDateTimestamp", CreateDateTimeStamp),
                        new XElement(xmlNamespace + "Issuer",
                            new XElement(xmlNamespace + "issuerID", IssuerCode.ToString().PadLeft(4, '0'))),
                        new XElement(xmlNamespace + "Merchant",
                            new XElement(xmlNamespace + "merchantID", MerchantId.PadLeft(9, '0')),
                            new XElement(xmlNamespace + "subID", MerchantSubId),
                            new XElement(xmlNamespace + "merchantReturnURL", MerchantReturnUrl)),
                        new XElement(xmlNamespace + "Transaction",
                            new XElement(xmlNamespace + "purchaseID", PurchaseId),
                            new XElement(xmlNamespace + "amount", Amount),
                            new XElement(xmlNamespace + "currency", Currency),
                            new XElement(xmlNamespace + "expirationPeriod", "PT" +
                                         Convert.ToInt32(Math.Floor(ExpirationPeriod.Value.TotalSeconds)) + "S"),
                            new XElement(xmlNamespace + "language", Language),
                            new XElement(xmlNamespace + "description", Description),
                            new XElement(xmlNamespace + "entranceCode", EntranceCode))));

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
