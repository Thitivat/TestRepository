using BND.Services.Payments.eMandates.Entities;
using BND.Services.Payments.eMandates.Enums;
using System;

namespace BND.Services.Payments.eMandates.Proxy.NET4.Interfaces
{
    /// <summary>
    /// Interface ITransaction
    /// </summary>
    public interface ITransaction : IDisposable
    {
        /// <summary>
        /// Creates the emandate transaction.
        /// </summary>
        /// <param name="newTransaction">The new transaction model.</param>
        /// <returns>TransactionResponseModel.</returns>
        TransactionResponseModel CreateTransaction(NewTransactionModel newTransaction);

        /// <summary>
        /// Gets the emandate transaction status.
        /// </summary>
        /// <param name="transactionId">The transaction id.</param>
        /// <returns>EnumQueryStatus.</returns>
        EnumQueryStatus GetTransactionStatus(string transactionId);
    }
}
