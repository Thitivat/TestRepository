using System;

namespace BND.Services.Payments.iDeal.iDealClients.Base
{
    /// <summary>
    /// Class iDealResponseBase is a response prototype.
    /// </summary>
    public abstract class iDealResponseBase
    {
        #region [Properties]
        /// <summary>
        /// Unique four-digit identifier of the Acquirer within iDEAL.
        /// </summary>
        public int AcquirerId { get; protected set; }

        /// <summary>
        /// Date and time at which the response message was created.
        /// </summary>
        public DateTime CreateDateTimeStamp { get; protected set; }
        #endregion
    }
}
