using BND.Services.Payments.eMandates.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BND.Services.Payments.eMandates.Domain.Interfaces
{
    public interface ITransactionRepository : IDisposable
    {
        /// <summary>
        /// Inserts the specified transaction.
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        /// <returns>System.Int32.</returns>
        int Insert(Transaction transaction);

        /// <summary>
        /// Gets the transaction with latest status.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <returns>Transaction.</returns>
        Transaction GetTransactionWithLatestStatus(string transactionId);
        
        /// <summary>
        /// Updates the transaction attempts.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="newAttempts">The new attempts.</param>
        /// <returns>System.Int32.</returns>
        int UpdateTransactionAttempts(string transactionId, int newAttempts);

        /// <summary>
        /// Updates the status.
        /// </summary>
        /// <param name="isSystemFail">if set to <c>true</c> [is system fail].</param>
        /// <param name="transactionStatusHistory">The transaction status history.</param>
        /// <returns>System.Int32.</returns>
        int UpdateStatus(bool isSystemFail, TransactionStatusHistory transactionStatusHistory);

    }
}
