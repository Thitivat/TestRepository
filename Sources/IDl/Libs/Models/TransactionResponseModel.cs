﻿using System;

namespace BND.Services.Payments.iDeal.Models
{
    /// <summary>
    /// Class TransactionResponseModel is a class representing response object from our iDeal service.
    /// </summary>
    public class TransactionResponseModel
    {
        /// <summary>
        /// Unique 16-digit number within iDEAL. The number consists of the acquirerID (first four positions) and a unique number generated by the 
        /// Acquirer(12 positions). Ultimately appears on payment confirmation (bank statement or account overview of the Consumer and Merchant).
        /// </summary>
        /// <value>The transaction identifier.</value>
        public string TransactionID { get; set; }
        /// <summary>
        /// The Transaction.entranceCode is an 'authentication identifier' to facilitate continuation of the session between Merchant and Consumer, 
        /// even if the existing session has been lost. It enables the Merchantto recognise the Consumerassociated with a (completed) transaction.
        /// The Transaction.entranceCode is sent to the Merchantin the Redirect.The Transaction.entranceCode must have a minimum variation of
        /// 1 millionand shouldcomprise letters and/or figures (maximum 40 positions).The Transaction.entranceCode is created by the Merchant and 
        /// passed to the Issuer.
        /// </summary>
        /// <value>The entrance code.</value>
        public string EntranceCode { get; set; }
        /// <summary>
        /// Unique identification of the order within the Merchant’s system. Ultimately appears on the payment confirmation (Bank statement / account
        /// overview of the Consumer and Merchant).
        /// </summary>
        /// <value>The purchase identifier.</value>
        public string PurchaseID { get; set; }
        /// <summary>
        /// The complete Issuer URL to which the Consumershall be redirected by the Merchantfor authenticationand authorisation of the transaction.
        /// </summary>
        /// <value>The issuer authentication URL.</value>
        public Uri IssuerAuthenticationURL { get; set; }
    }
}
