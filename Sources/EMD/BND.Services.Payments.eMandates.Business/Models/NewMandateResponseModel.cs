using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eMandates.Merchant.Library;

namespace BND.Services.Payments.eMandates.Business.Models
{
    /// <summary>
    /// Describes a new mandate response
    /// 
    /// </summary>
    public class NewMandateResponseModel
    {
        /// <summary>
        /// true if an error occured, or false when no errors were encountered
        /// 
        /// </summary>
        public bool IsError { get; set; }
        /// <summary>
        /// Object that holds the error if one occurs; when there are no errors, this is set to null
        /// 
        /// </summary>
        public ErrorResponseModel Error { get; set; }
        /// <summary>
        /// The URL to which to redirect the creditor so they can authorize the transaction
        /// 
        /// </summary>
        public string IssuerAuthenticationUrl { get; set; }
        /// <summary>
        /// The transaction ID
        /// 
        /// </summary>
        public string TransactionId { get; set; }
        /// <summary>
        /// DateTime set to when this transaction was created
        /// 
        /// </summary>
        public DateTime TransactionCreateDateTimestamp { get; set; }
        /// <summary>
        /// The response XML
        /// 
        /// </summary>
        public string RawMessage { get; set; }
    }
}
