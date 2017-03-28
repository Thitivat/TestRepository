using BND.Services.Payments.eMandates.Business.Exceptions;
using BND.Services.Payments.eMandates.Business.Interfaces;
using BND.Services.Payments.eMandates.Entities;
using System;
using System.Net.Http;
using System.Web.Http;

namespace BND.Services.Payments.eMandates.WebService.Controllers
{
    /// <summary>
    /// Class TransactionsController.
    /// </summary>
    [RoutePrefix("v1/transactions")]
    public class TransactionsController : ApiController
    {

        #region [Fields]
        /// <summary>
        /// eMandates manager class.
        /// </summary>
        private readonly IEMandatesManager _eMandatesManager;
        ///// <summary>
        ///// The logger service
        ///// </summary>
        //private readonly ILogger _logger;
        #endregion


        #region [Constructors]
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionsController" /> class.
        /// </summary>
        /// <param name="eMandatesManager">The e mandates manager.</param>
        public TransactionsController(IEMandatesManager eMandatesManager)
        {
            _eMandatesManager = eMandatesManager;
        }
        #endregion

        /// <summary>
        /// Creates the transaction.
        /// </summary>
        /// <param name="newTransactionModel">The new transaction model.</param>
        /// <returns>IHttpActionResult.</returns>
        [Route("")]
        [HttpPost]
        public IHttpActionResult CreateTransaction(NewTransactionModel newTransactionModel)
        {
            IHttpActionResult result;

            try
            {
                TransactionResponseModel trm = _eMandatesManager.CreateTransaction(newTransactionModel);

                // 200 OK
                result = Ok(trm);
            }
            // 400 Bad Request
            catch (ValidationException ex)
            {
                if (ex.InnerException is ArgumentNullException)
                {
                    string paramName = ((ArgumentNullException)ex.InnerException).ParamName;
                    BadRequest("There is a problem with request parameter: " + paramName);
                }
                else if (ex.InnerException is ArgumentException)
                {
                    string paramName = ((ArgumentException)ex.InnerException).ParamName;
                    BadRequest("There is a problem with request parameter: " + paramName);
                }
                result = BadRequest("There wasa validation error.");
            }
            // 500 Internal Server Error
            catch (Exception ex)
            {
                result = InternalServerError(ex);
            }

            return result;
        }

        [Route("{transactionId}/status")]
        [HttpGet]
        public HttpResponseMessage GetTransactionStatus()
        {
            //RequestServiceInfo serviceInfo = null;

            //try
            //{
            //    serviceInfo = new RequestServiceInfo(Request);
            //}
            //catch (BaseException ex)
            //{
            //    throw CreateWrapperException(ex, EnumErrorCodes.ServiceLayerError, GetHelpLink());
            //}
            //catch (Exception ex)
            //{
            //    throw CreateWrapperException(ex, EnumErrorCodes.ServiceLayerError, GetHelpLink());
            //}

            //return Request.CreateResponse(HttpStatusCode.OK, serviceInfo);

            throw new NotImplementedException();
        }
    }
}