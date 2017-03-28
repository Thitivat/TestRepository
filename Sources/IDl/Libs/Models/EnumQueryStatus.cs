using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BND.Services.Payments.iDeal.Models
{
    /// <summary>
    /// Enumerable of query status they're all status that iDeal define for each Transaction.
    /// </summary>
    public enum EnumQueryStatus
    {
        /// <summary>
        /// Final result not yet known. A new status request is necessary to obtain the status.
        /// </summary>
        Open = 0,
        /// <summary>
        /// Positive result, the payment is guaranteed.
        /// </summary>
        Success = 3,
        /// <summary>
        /// Negative result due to cancellation by Consumer, no payment has been made.
        /// </summary>
        Cancelled = 4,
        /// <summary>
        /// Negative result due to expiration of the transaction, no payment has been made.
        /// </summary>
        Expired = 5,
        /// <summary>
        /// Negative result due to other reasons, no payment has been made.
        /// </summary>
        Failure = 6,
        /// <summary>
        /// The status indicates that iDeal transaction was successfull, but there was an error while booking to matrix.
        /// </summary>
        IdealSuccessBookingFailed = 7
    }
}
