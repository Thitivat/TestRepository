using BND.Services.Payments.iDeal.iDealClients.Directory;
using BND.Services.Payments.iDeal.iDealClients.Interfaces;
using BND.Services.Payments.iDeal.iDealClients.Status;
using BND.Services.Payments.iDeal.iDealClients.Transaction;
using System;

namespace BND.Services.Payments.iDeal.Interfaces
{
    /// <summary>
    /// Interface IiDealClient that provides method to manage the iDeal.
    /// </summary>
    public interface IiDealClient
    {
        #region [Properties]
        IConfiguration Configuration { get; }
        #endregion


        #region [Method]
        /// <summary>
        /// Sends the directory request.
        /// </summary>
        /// <returns>DirectoryResponse.</returns>
        DirectoryResponse SendDirectoryRequest();

        /// <summary>
        /// Sends the transaction request.
        /// </summary>
        /// <param name="issuerCode">The issuer code.</param>
        /// <param name="merchantReturnUrl">The merchant return URL.</param>
        /// <param name="purchaseId">The purchase identifier.</param>
        /// <param name="amount">The amount.</param>
        /// <param name="expirationPeriod">The expiration period.</param>
        /// <param name="description">The description.</param>
        /// <param name="entranceCode">The entrance code.</param>
        /// <returns>TransactionResponse.</returns>
        TransactionResponse SendTransactionRequest(string issuerCode, string merchantReturnUrl, string purchaseId, decimal amount,
                                                         TimeSpan? expirationPeriod, string description, string entranceCode, string language,
                                                         string currency);

        /// <summary>
        /// Retrieve status of transaction (Success, Cancelled Failure, Expired, Open)
        /// </summary>
        /// <param name="transactionId">Unique identifier of transaction obtained when the transaction was issued</param>
        /// <returns>StatusResponse.</returns>
        StatusResponse SendStatusRequest(string transactionId);
        #endregion
    }
}