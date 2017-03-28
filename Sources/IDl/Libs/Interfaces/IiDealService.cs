﻿using BND.Services.Payments.iDeal.Models;
using System.Collections.Generic;

/// <summary>
/// The BND.Services.Payments.iDeal.Interfaces namespace contains all shared interfaces about iDeal project.
/// </summary>
namespace BND.Services.Payments.iDeal.Interfaces
{
    /// <summary>
    /// Interface IiDealService that provides the methods to get Directory data.
    /// </summary>
    public interface IiDealService
    {
        /// <summary>
        /// Gets the issueing bank list that stored in database the data will updated one time per day from iDeal,
        /// that will execute at first request of the day.
        /// </summary>
        /// <returns>List of directories which retrieve from iDeal.</returns>
        IEnumerable<DirectoryModel> GetDirectories();

        /// <summary>
        /// Creates a transaction to request a payment to iDeal service for transfering money from customer to BND bank account.
        /// </summary>
        /// <param name="transactionRequest">The number consists of the acquirerID (first four positions) and a unique number 
        /// generated by the Acquirer (12 positions). Ultimately appears on payment confirmation (bank statement or account
        /// overview of the Consumer and Merchant).</param>
        /// <returns>TransactionResponseModel.</returns>
        TransactionResponseModel CreateTransaction(TransactionRequestModel transactionRequest);

        /// <summary>
        /// Gets the status of specific Transaction to check what going on in processing of payment.
        /// </summary>
        /// <param name="entranceCode">The entranceCode this got from transaction response after called CreateTransaction</param>
        /// <param name="transactionID">The transactionID this got from transaction response after called CreateTransaction</param>
        /// <returns>EnumQueryStatus.</returns>
        EnumQueryStatus GetStatus(string entranceCode, string transactionID);

    }
}
