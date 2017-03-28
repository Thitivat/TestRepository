using BND.Services.Payments.iDeal.Models;
using BND.Services.Payments.iDeal.Proxy.NET4.Interfaces;
using System;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace BND.Services.Payments.iDeal.Proxy.NET4
{
    /// <summary>
    /// Class Transaction provides the function for send transaction request to IDeal service by verify token and request to the resource from iDeal
    /// rest api.
    /// </summary>
    public class Transaction : ResourceBase, ITransaction
    {
        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceBase" /> class.
        /// </summary>
        /// <param name="baseAddress">The base address of api.</param>
        /// <param name="token">The token string that implement the authentication key .</param>
        public Transaction(string baseAddress, string token)
            : base(baseAddress, token)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceBase" /> class.
        /// </summary>
        /// <param name="baseAddress">The base address of api.</param>
        /// <param name="token">The token string that implement the authentication key .</param>
        /// <param name="httpClient">The HTTP client.</param>
        public Transaction(string baseAddress, string token, HttpClient httpClient)
            : base(baseAddress, token, httpClient)
        { }
        #endregion


        #region [Public Methods]

        /// <summary>
        /// Creates the transaction model and request to iDeal service.
        /// </summary>
        /// <param name="transactionRequest">The transaction request.</param>
        /// <returns>BND.Services.Payments.iDeal.Models.TransactionResponseModel.</returns>
        /// <exception cref="System.ObjectDisposedException">The instance has been disposed.</exception>
        /// <exception cref="System.ArgumentNullException">
        /// When TransactionRequestModel is null.
        /// </exception>
        public TransactionResponseModel CreateTransaction(TransactionRequestModel transactionRequest)
        {
            // Checks required parameters.
            if (transactionRequest == null)
            {
                throw new ArgumentNullException("transactionRequest");
            }

            // Calls api with post method to send transaction request.
            HttpResponseMessage result = base._httpClient.PostAsync(Uri.EscapeUriString(Properties.Resources.URL_RES_TRANSACTION), 
																	base.MapModelToFormUrlEncodedContent(transactionRequest)).Result;

            // Returns TransactionResponseModel.
            return base.SetResult<TransactionResponseModel>(result);
        }

        /// <summary>
        /// Gets the status of specific Transaction to check what going on in processing of payment.
        /// </summary>
        /// <param name="entranceCode">The entranceCode this got from transaction response after called CreateTransaction</param>
        /// <param name="transactionID">The transactionID this got from transaction response after called CreateTransaction</param>
        /// <returns>StatusResponseModel.</returns>
        /// <exception cref="System.ArgumentNullException">entranceCode
        /// or
        /// transactionID</exception>
        /// <exception cref="System.ArgumentException">
        /// entranceCode can be only number or letter.
        /// or
        /// transactionID can be only number or letter.
        /// </exception>
        /// <exception cref="iDealOperationException"></exception>
        public EnumQueryStatus GetStatus(string entranceCode, string transactionID)
        {
            // Check parameters
            if(String.IsNullOrEmpty(entranceCode))
            {
                throw new ArgumentNullException("entranceCode");
            }
            if (String.IsNullOrEmpty(transactionID))
            {
                throw new ArgumentNullException("transactionID");
            }

            Match m = Regex.Match(entranceCode, @"^[a-zA-Z0-9]+$");
            if (!m.Success)
            {
                throw new ArgumentException("entranceCode can be only number or letter.");
            }
            m = Regex.Match(transactionID, @"^[a-zA-Z0-9]+$");
            if (!m.Success)
            {
                throw new ArgumentException("transactionID can be only number or letter.");
            }

            // Calls api with post method to send query request.
            HttpResponseMessage result = base._httpClient.GetAsync(Uri.EscapeUriString(
                                                                        String.Format(Properties.Resources.URL_RES_TRANSACTION_STATUS,
                                                                                      transactionID, entranceCode))).Result;

            // Returns result from api.
            string statusResponse = base.SetResult<string>(result);

            EnumQueryStatus queryStatus;
            if (Enum.TryParse<EnumQueryStatus>(statusResponse, out queryStatus))
            {
                // return the sattus result.
                return queryStatus;
            }
            else
            {
                throw new iDealOperationException("", String.Format("Cannot parse the status {0}", statusResponse));
            }
        }
        #endregion
        
    }
}
