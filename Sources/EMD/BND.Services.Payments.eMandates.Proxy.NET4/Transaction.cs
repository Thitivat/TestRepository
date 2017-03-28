using BND.Services.Payments.eMandates.Entities;
using BND.Services.Payments.eMandates.Enums;
using BND.Services.Payments.eMandates.Proxy.NET4.Interfaces;
using System;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace BND.Services.Payments.eMandates.Proxy.NET4
{
    /// <summary>
    /// Class Transaction.
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

        /// <summary>
        /// Creates the emandate transaction.
        /// </summary>
        /// <param name="newTransaction">The new transaction model.</param>
        /// <returns>TransactionResponseModel.</returns>
        /// <exception cref="System.ArgumentNullException">newTransaction</exception>
        public Entities.TransactionResponseModel CreateTransaction(NewTransactionModel newTransaction)
        {
            // Checks required parameters.
            if (newTransaction == null)
            {
                throw new ArgumentNullException("newTransaction");
            }

            // Calls api with post method to send transaction request.
            HttpResponseMessage result = base._httpClient.PostAsync(Uri.EscapeUriString(Properties.Resources.URL_RES_TRANSACTION),
                                                                    base.MapModelToFormUrlEncodedContent(newTransaction)).Result;

            // Returns TransactionResponseModel.
            return base.SetResult<TransactionResponseModel>(result);
        }

        /// <summary>
        /// Gets the emandate transaction status.
        /// </summary>
        /// <param name="transactionId">The transaction id.</param>
        /// <returns>EnumQueryStatus.</returns>
        /// <exception cref="System.ArgumentNullException">transactionId</exception>
        /// <exception cref="System.ArgumentException">transactionId can be number only.</exception>
        /// <exception cref="BND.Services.Payments.eMandates.Entities.eMandateOperationException"></exception>
        public Enums.EnumQueryStatus GetTransactionStatus(string transactionId)
        {
            if (String.IsNullOrEmpty(transactionId))
            {
                throw new ArgumentNullException("transactionId");
            }

            Match m = Regex.Match(transactionId, @"^[0-9]+$");
            if (!m.Success)
            {
                throw new ArgumentException("transactionId can be number only.");
            }

            // Calls api with post method to send query request.
            HttpResponseMessage result = base._httpClient.GetAsync(Uri.EscapeUriString(
                                                                        String.Format(Properties.Resources.URL_RES_TRANSACTION_STATUS,
                                                                                      transactionId))).Result;

            // Returns result from api.
            string statusResponse = base.SetResult<string>(result);

            EnumQueryStatus queryStatus;
            if (Enum.TryParse<EnumQueryStatus>(statusResponse, out queryStatus))
            {
                // return the status result.
                return queryStatus;
            }
            else
            {
                throw new eMandateOperationException("", String.Format("Cannot parse the status {0}", statusResponse));
            }
        }
    }
}
