using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

using BND.Services.Infrastructure.ErrorHandling;
using BND.Services.Infrastructure.WebAPI.Controllers;
using BND.Services.Matrix.Business.Interfaces;
using BND.Services.Matrix.Entities;

namespace BND.Services.Matrix.Web.Controllers
{
    /// <summary>
    /// The sys accounts controller.
    /// </summary>
    [RoutePrefix("v1/sysaccounts")]
    public class SysAccountsController : BaseApiController<EnumErrorCodes>
    {
        /// <summary>
        /// The _matrix manager.
        /// </summary>
        private readonly IMatrixManager _matrixManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="SysAccountsController"/> class.
        /// </summary>
        /// <param name="matrixManager">
        /// The matrix manager.
        /// </param>
        public SysAccountsController(IMatrixManager matrixManager)
        {
            _matrixManager = matrixManager;
        }

        /// <summary>
        /// The create transaction.
        /// </summary>
        /// <param name="sweep"> The sweep. </param>
        /// <returns>
        /// The <see cref="IHttpActionResult"/>.
        /// </returns>
        [Route("sweep")]
        [HttpPost]
        public IHttpActionResult Sweep(Sweep sweep)
        {
            try
            {
                var result = _matrixManager.Sweep(sweep);
                return Created(string.Empty, result);
            }
            catch (BaseException ex)
            {
                throw CreateWrapperException(ex, EnumErrorCodes.ServiceLayerError, GetHelpLink());
            }
            catch (Exception ex)
            {
                throw CreateWrapperException(ex, EnumErrorCodes.ServiceLayerError, GetHelpLink());
            }
        }

        /// <summary>
        /// The get movements.
        /// </summary>
        /// <param name="sysId">
        /// The sys Id.
        /// </param>
        /// <param name="startDate">
        /// The start date.
        /// </param>
        /// <param name="endDate">
        /// The end date.
        /// </param>
        /// <returns>
        /// The <see cref="IHttpActionResult"/>.
        /// </returns>
        [Route("{sysId}/movements")]
        [HttpGet]
        [ResponseType(typeof(List<Entities.MovementItem>))]
        public IHttpActionResult GetMovements(string sysId, DateTime? startDate = null, DateTime? endDate = null)
        {
            try
            {
                var result = _matrixManager.GetMovements(sysId, startDate, endDate);
                return Ok(result);
            }
            catch (BaseException ex)
            {
                throw CreateWrapperException(ex, EnumErrorCodes.ServiceLayerError, GetHelpLink());
            }
            catch (Exception ex)
            {
                throw CreateWrapperException(ex, EnumErrorCodes.ServiceLayerError, GetHelpLink());
            }
        }

        /// <summary>
        /// The get balance.
        /// </summary>
        /// <param name="sysId">
        /// The sys Id.
        /// </param>
        /// <param name="valueDate">
        /// The value date.
        /// </param>
        /// <returns>
        /// The <see cref="IHttpActionResult"/>.
        /// </returns>
        [Route("{sysId}/balance")]
        [HttpGet]
        [ResponseType(typeof(decimal))]
        public IHttpActionResult GetBalanceOverview(string sysId, DateTime valueDate)
        {
            try
            {
                var result = _matrixManager.GetBalanceOverview(sysId, valueDate);
                return Ok(result);
            }
            catch (BaseException ex)
            {
                throw CreateWrapperException(ex, EnumErrorCodes.ServiceLayerError, GetHelpLink());
            }
            catch (Exception ex)
            {
                throw CreateWrapperException(ex, EnumErrorCodes.ServiceLayerError, GetHelpLink());
            }
        }

        /// <summary>
        /// The get balance.
        /// </summary>
        /// <param name="sysId">
        /// The sys Id.
        /// </param>
        /// <param name="valueDate">
        /// The value date.
        /// </param>
        /// <returns>
        /// The <see cref="IHttpActionResult"/>.
        /// </returns>
        [Route("{sysId}/overview")]
        [HttpGet]
        [ResponseType(typeof(AccountOverview))]
        public IHttpActionResult GetAccountOverview(string sysId, DateTime valueDate)
        {
            try
            {
                var result = _matrixManager.GetAccountOverview(sysId, valueDate);
                return Ok(result);
            }
            catch (BaseException ex)
            {
                throw CreateWrapperException(ex, EnumErrorCodes.ServiceLayerError, GetHelpLink());
            }
            catch (Exception ex)
            {
                throw CreateWrapperException(ex, EnumErrorCodes.ServiceLayerError, GetHelpLink());
            }
        }

        /// <summary>
        /// Get a <see cref="List{SystemAccount}"/> from store.
        /// </summary>
        /// <returns>
        /// The <see cref="IHttpActionResult"/>.
        /// </returns>
        /// <remarks>
        /// A <see cref="List{SystemAccount}"/> entities is returned.
        /// On success the <see cref="List{T}"/> and a response status code of 200 (OK) is returned.
        /// On failure, it can return a number of different status codes depending on the type error, e.g 400 (Bad request), 500 (Internal server error), 422 (Unprocessable entity)
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [Route("")]
        [HttpGet]
        [ResponseType(typeof(List<SystemAccount>))]
        public IHttpActionResult GetSystemAccounts()
        {
            try
            {
                var result = _matrixManager.GetSystemAccounts();
                return Ok(result);
            }
            catch (BaseException ex)
            {
                throw CreateWrapperException(ex, EnumErrorCodes.ServiceLayerError, GetHelpLink());
            }
            catch (Exception ex)
            {
                throw CreateWrapperException(ex, EnumErrorCodes.ServiceLayerError, GetHelpLink());
            }
        }

        /// <summary>
        /// Returns a payment
        /// </summary>
        /// <returns>
        /// The <see cref="IHttpActionResult"/>.
        /// </returns>
        /// <remarks>
        /// A <see cref="bool"/>  is returned.
        /// On success the bool and a response status code of created is returned.
        /// On failure, it can return a number of different status codes depending on the type error, e.g 400 (Bad request), 500 (Internal server error), 422 (Unprocessable entity)
        /// </remarks>
        /// <response code="201">Created</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [Route("returnPayment")]
        [HttpPost]
        public IHttpActionResult ReturnPayment(ReturnPayment returnPayment)
        {
            try
            {
                var result = _matrixManager.ReturnPayment(returnPayment);

                return Created(string.Empty, result);
            }
            catch (BaseException ex)
            {
                throw CreateWrapperException(ex, EnumErrorCodes.ServiceLayerError, GetHelpLink());
            }
            catch (Exception ex)
            {
                throw CreateWrapperException(ex, EnumErrorCodes.ServiceLayerError, GetHelpLink());
            }
        }

        /// <summary>
        /// Creates a return outgoing bucket
        /// </summary>
        /// <returns>
        /// The <see cref="IHttpActionResult"/>.
        /// </returns>
        /// <remarks>
        /// A <see cref="string"/>  is returned.
        /// On success a string and a response status code of created is returned.
        /// On failure, it can return a number of different status codes depending on the type error, e.g 400 (Bad request), 500 (Internal server error), 422 (Unprocessable entity)
        /// </remarks>
        /// <response code="201">Created</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [Route("CreateOutgoingReturnBucket")]
        [HttpPost]
        public IHttpActionResult CreateOutgoingReturnBucket(ReturnBucketItem createReturnBucketItem)
        {
            try
            {
                var result = _matrixManager.CreateOutgoingReturnBucket(createReturnBucketItem);

                return Created(string.Empty, result);
            }
            catch (BaseException ex)
            {
                throw CreateWrapperException(ex, EnumErrorCodes.ServiceLayerError, GetHelpLink());
            }
            catch (Exception ex)
            {
                throw CreateWrapperException(ex, EnumErrorCodes.ServiceLayerError, GetHelpLink());
            }
        }
    }
}
