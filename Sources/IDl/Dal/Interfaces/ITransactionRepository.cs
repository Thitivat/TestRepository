using System;
using BND.Services.Payments.iDeal.Dal.Pocos;
using BND.Services.Payments.iDeal.Dal.Enums;

namespace BND.Services.Payments.iDeal.Dal
{
    /// <summary>
    /// Interface ITransactionRepository performing transaction table in database.
    /// </summary>
    public interface ITransactionRepository
    {
        /// <summary>
        /// Inserts the specified transaction.
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        /// <returns>total number of affected row(s)</returns>
        int Insert(p_Transaction transaction);

        /// <summary>
        /// Gets the specified transaction identifier with the latest status.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="entranceCode">The entrance code.</param>
        /// <returns>p_Transaction.</returns>
        p_Transaction GetTransactionWithLatestStatus(string transactionId, string entranceCode);

        /// <summary>
        /// Updates the status.
        /// </summary>
        /// <param name="consumeName">Consumer Name.</param>
        /// <param name="consumerIban">Consumer IBAN.</param>
        /// <param name="consumerBic">Consumer BIC.</param>
        /// <param name="isSystemFail">Is System Fail.</param>
        /// <param name="transactionStatusHistory">The transaction status history.</param>
        /// <returns>total number of affected row(s)</returns>
        int UpdateStatus(string consumeName, string consumerIban, string consumerBic, bool isSystemFail, p_TransactionStatusHistory transactionStatusHistory);

        /// <summary>
        /// Update transaction attempts.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="entranceCode">The entrance code.</param>
        /// <param name="attempts">The attempts.</param>
        /// <returns>total number of affected row(s)</returns>
        int UpdateTransactionAttempts(string transactionId, string entranceCode, int attempts);

        /// <summary>
        /// Updates the booking status.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="entranceCode">The entrance code.</param>
        /// <param name="newStatus">The new status.</param>
        /// <returns>p_Transaction.</returns>
        p_Transaction UpdateBookingStatus(string transactionId, string entranceCode, EnumBookingStatus newStatus);

        // <summary>
        /// Updates the booking data after booking to matrix.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="entranceCode">The entrance code.</param>
        /// <param name="movementId">The movement identifier.</param>
        /// <param name="status">The status.</param>
        /// <param name="bookingDate">The booking date.</param>
        /// <returns>p_Transaction.</returns>
        p_Transaction UpdateBookingData(string transactionId, string entranceCode, int movementId, EnumBookingStatus status, DateTime bookingDate);
    }
}
