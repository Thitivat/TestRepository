using BND.Services.Payments.iDeal.Models;
using System;

namespace BND.Services.Payments.iDeal.Proxy.Interfaces
{
    /// <summary>
    /// Class Transaction provides the function for send transaction request to IDeal service by verify token and request to the resource from iDeal 
    /// rest api.
    /// </summary>
    public interface ITransaction : IDisposable
    {
        /// <summary>
        /// Creates the transaction model and request to iDeal service.
        /// </summary>
        /// <param name="transactionReq">The transaction request model.</param>
        /// <returns>TransactionResponseModel.</returns>
        TransactionResponseModel CreateTransaction(TransactionRequestModel transactionReq);

        /// <summary>
        /// Gets the status of specific Transaction to check what going on in processing of payment.
        /// </summary>
        /// <param name="entranceCode">The entranceCode this got from transaction response after called CreateTransaction</param>
        /// <param name="transactionID">The transactionID this got from transaction response after called CreateTransaction</param>
        /// <returns>EnumQueryStatus.</returns>
        EnumQueryStatus GetStatus(string entranceCode, string transactionID);
    }
}
