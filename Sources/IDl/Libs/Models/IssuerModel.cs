using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BND.Services.Payments.iDeal.Models
{
    /// <summary>
    /// Class IssuerModel that contains the issuer bank properties of Directory data from iDeal.
    /// </summary>
    public class IssuerModel
    {
        /// <summary>
        /// Gets or sets the Bank Identifier Code (BIC) of the iDEAL Issuer
        /// </summary>
        /// <value>The issuer identifier.</value>
        public string IssuerID { get; set; }

        /// <summary>
        /// Gets or sets the name of the Issuer (as this should be displayed to the Consumer in the Merchant's Issuer list).
        /// </summary>
        /// <value>The name of the issuer.</value>
        public string IssuerName { get; set; }
    }
}
