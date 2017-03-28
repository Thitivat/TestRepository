using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BND.Services.Payments.iDeal.Models
{
    /// <summary>
    /// Class TransactionResponseModel is a class representing response object from our iDeal service.
    /// </summary>
    public class TransactionResponseModel
    {
        /// <summary>
        /// Gets or sets the transaction identifier.
        /// </summary>
        /// <value>The transaction identifier.</value>
        public string TransactionID { get; set; }
        /// <summary>
        /// Gets or sets the entrance code.
        /// </summary>
        /// <value>The entrance code.</value>
        public string EntranceCode { get; set; }
        /// <summary>
        /// Gets or sets the purchase identifier.
        /// </summary>
        /// <value>The purchase identifier.</value>
        public string PurchaseID { get; set; }
        /// <summary>
        /// Gets or sets the issuer authentication URL that is used for redirecting customer to thier bank to confirm payment.
        /// </summary>
        /// <value>The issuer authentication URL.</value>
        public Uri IssuerAuthenticationURL { get; set; }
    }
}
