using BND.Services.Payments.iDeal.iDealClients.Interfaces;
using System;
using System.Xml;

namespace BND.Services.Payments.iDeal.iDealClients.Base
{
    /// <summary>
    /// Class iDealRequestBase is a request prototype.
    /// </summary>
    public abstract class iDealRequestBase
    {
        #region [Fields]
        /// <summary>
        /// The merchant identifier
        /// </summary>
        protected string _merchantId;

        /// <summary>
        /// The sub identifier
        /// </summary>
        protected int _subId;
        #endregion


        #region [Properties]
        /// <summary>
        /// MerchantID as supplied to the Merchant by the Acquirer.
        /// If the MerchantID has less than 9 digits, leading zeros must be used to fill out the field.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">
        /// MerchantId does not contain a value or whitespaces
        /// or
        /// MerchantId cannot contain more than 9 characters.
        /// </exception>
        /// <exception cref="InvalidOperationException">MerchantId does not contain a value
        /// or
        /// MerchantId cannot contain whitespaces
        /// or
        /// MerchantId cannot contain more than 9 characters.</exception>
        public string MerchantId
        {
            get { return _merchantId; }
            protected set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("MerchantId does not contain a value or whitespaces");
                }
                if (value.Length > 9)
                {
                    throw new ArgumentException("MerchantId cannot contain more than 9 characters.");
                }
                _merchantId = value;
            }
        }

        /// <summary>
        /// Merchant subID, as supplied to the Merchant by the Acquirer, if the Merchanthas requested to use this.A Merchant can state a request to
        /// the Acquirer to use one or more subIDs. In this way apart from the Legal Name the Trade name will also be shown on the bank statements
        /// for each subID used. Unless agreed otherwise with the Acquirer, the Merchanthas to use 0 (zero) as subID by default(if no subIDs are used).
        /// </summary>
        /// <exception cref="System.InvalidOperationException">SubId must contain a value ranging from 0 to 6</exception>
        /// <exception cref="InvalidOperationException">SubId must contain a value ranging from 0 to 6</exception>
        public int MerchantSubId
        {
            get { return _subId; }
            protected set
            {
                if (value < 0 || value > 6)
                {
                    throw new ArgumentException("SubId must contain a value ranging from 0 to 6");
                }
                _subId = value;
            }
        }

        /// <summary>
        /// Gets the create date time stamp.
        /// </summary>
        public DateTime CreateDateTimeStamp { get; private set; }
        #endregion


        #region [Constructor]
        /// <summary>
        /// Initializes a new instance of the <see cref="iDealRequestBase" /> class.
        /// </summary>
        protected iDealRequestBase()
        {
            // This CreateDateTimeStamp need to use UTC Time follow iDeal Document P.19 Section 4.2 DirectoryRequest if it isn't UTC it will throw 
            // error from abn amro.
            CreateDateTimeStamp = DateTime.UtcNow;
        }
        #endregion


        #region [Method]
        /// <summary>
        /// To the XML.
        /// </summary>
        /// <param name="signatureProvider">The signature provider.</param>
        /// <returns>XmlDocument.</returns>
        public abstract XmlDocument ToXml(ISignatureProvider signatureProvider);
        #endregion
    }
}
