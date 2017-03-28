using AutoMapper;
using BND.Services.Payments.iDeal.IntegrationTests.ViewModels;
using BND.Services.Payments.iDeal.Models;
using BND.Services.Payments.iDeal.Proxy.NET4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BND.Services.Payments.iDeal.IntegrationTests.Controllers
{
    /// <summary>
    /// Class iDealController, the main webapi for consume data from iDealProxy
    /// </summary>
    public class iDealController : ApiController
    {
        #region [Public Method]
        /// <summary>
        /// Gets the directories.
        /// </summary>
        /// <returns>HttpResponseMessage.</returns>
        public HttpResponseMessage GetDirectories()
        {
            try
            {
                string apiBaseAddress = System.Configuration.ConfigurationManager.AppSettings["ApiBaseAddress"];
                string apiToken = System.Configuration.ConfigurationManager.AppSettings["ApiToken"];

                Directories directories = new Directories(apiBaseAddress, apiToken);
                IEnumerable<DirectoryModel> models = directories.GetDirectories().ToList();

                var result = new DirectoryViewModel()
                {
                    Directories = models,
                };
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (iDealOperationException ex)
            {
                string errorCode = ex.ErrorCode;
                string errorMessage = (errorCode.ToUpper().StartsWith("BND")) ? String.Format("Code: {0}, {1}", ex.ErrorCode, ex.Message)
                                                                              : ex.Message;

                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, errorMessage));
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        /// <summary>
        /// Sends the transaction request to iDeal service.
        /// </summary>
        /// <param name="transactionRequest">The transaction request model.</param>
        /// <returns>HttpResponseMessage.</returns>
        /// <exception cref="System.Web.Http.HttpResponseException"></exception>
        [HttpPost]
        public HttpResponseMessage SendTransactionRequest(TransactionRequestViewModel transactionRequestViewModel)
        {
            try
            {
                string apiBaseAddress = System.Configuration.ConfigurationManager.AppSettings["ApiBaseAddress"];
                string apiToken = transactionRequestViewModel.ApiToken;

                Transaction transaction = new Transaction(apiBaseAddress, apiToken);
                TransactionRequestModel transactionRequestModel = Mapper.Map<TransactionRequestModel>(transactionRequestViewModel);
                TransactionResponseModel model = transaction.CreateTransaction(transactionRequestModel);

                // redirect to issuer authentication page
                return Request.CreateResponse(HttpStatusCode.OK, model);
            }
            catch (iDealOperationException ex)
            {
                string errorCode = ex.ErrorCode;
                string errorMessage = String.Empty;
                if (!String.IsNullOrEmpty(errorCode) && (errorCode.ToUpper().StartsWith("BND")))
                {
                    errorMessage = String.Format("Code: {0}, {1}", ex.ErrorCode, ex.Message);
                }
                else
                {
                    errorMessage = ex.Message;
                }

                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, errorMessage));
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpPost]
        public HttpResponseMessage GetTransactionStatus(TransactionStatusRequestViewModel statusRequest)
        {
            try
            {
                string apiBaseAddress = System.Configuration.ConfigurationManager.AppSettings["ApiBaseAddress"];
                string apiToken = statusRequest.ApiToken;

                Transaction transaction = new Transaction(apiBaseAddress, apiToken);
                EnumQueryStatus queryStatus = transaction.GetStatus(statusRequest.EntranceCode, statusRequest.TransactionId);

                return Request.CreateResponse(HttpStatusCode.OK, queryStatus.ToString());
            }
            catch (iDealOperationException ex)
            {
                string errorCode = ex.ErrorCode;
                string errorMessage = String.Empty;
                if (!String.IsNullOrEmpty(errorCode) && (errorCode.ToUpper().StartsWith("BND")))
                {
                    errorMessage = String.Format("Code: {0}, {1}", ex.ErrorCode, ex.Message);
                }
                else
                {
                    errorMessage = ex.Message;
                }

                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, errorMessage));
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        #endregion
    }
}