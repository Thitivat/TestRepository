using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

using BND.Services.Infrastructure.ErrorHandling;
using BND.Services.Infrastructure.WebAPI.Controllers;
using BND.Services.Matrix.Business.Interfaces;
using BND.Services.Matrix.Entities;
using BND.Services.Matrix.Entities.QueryStringModels;

namespace BND.Services.Matrix.Web.Controllers
{
    /// <summary>
    /// The <see cref="AccountsController"/> class
    /// </summary>
    [RoutePrefix("v1/accounts")]
    public class AccountsController : BaseApiController<EnumErrorCodes>
    {
        /// <summary>
        /// The <see cref="IMatrixManager"/>
        /// </summary>
        private readonly IMatrixManager _matrixManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountsController"/> class.
        /// </summary>
        /// <param name="manager"> The <see cref="IMatrixManager"/>. </param>
        public AccountsController(IMatrixManager manager)
        {
            _matrixManager = manager;
        }

        /// <summary>
        /// Creates a savings account in Matrix
        /// </summary>
        /// <param name="savingsFree"> The <see cref="SavingsFree"/> entity</param>
        /// <returns>
        /// The <see cref="IHttpActionResult"/>.
        /// </returns>
        /// <response code="201">Created</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        /// <exception cref="ServiceLayerException"> A wrapper exception  </exception>
        [Route("savings")]
        [HttpPost]
        public IHttpActionResult CreateSavingsAccount(SavingsFree savingsFree)
        {
            try
            {
                _matrixManager.CreateSavingsAccount(savingsFree);

                // TODO/REVIEW : Created url does not exist yet!
                return Created(new Uri(string.Format("{0}/{1}", Request.RequestUri, savingsFree.Iban)), savingsFree.Iban);
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
        /// Closes an account.
        /// </summary>
        /// <param name="iban"> The iban. </param>
        /// <param name="closingPaymentItem"> The <see cref="ClosingPaymentItem"/> </param>
        /// <returns>
        /// The <see cref="IHttpActionResult"/>.
        /// </returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        /// <exception cref="ServiceLayerException"> A wrapper exception  </exception>
        [Route("{iban}/close")]
        [HttpPut]
        [ResponseType(typeof(Entities.AccountCloseResultsItem))]
        public IHttpActionResult CloseAccount(string iban, ClosingPaymentItem closingPaymentItem)
        {
            try
            {
                var result = _matrixManager.CloseAccount(iban, closingPaymentItem);
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
        /// The get interest rate.
        /// </summary>
        /// <param name="iban">
        /// The iban number.
        /// </param>
        /// <param name="options">
        /// The options.
        /// </param>
        /// <returns>
        /// The <see cref="IHttpActionResult"/>.
        /// </returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        /// <exception cref="ServiceLayerException"> A wrapper exception  </exception>
        [Route("{iban}/interestrate")]
        [HttpGet]
        [ResponseType(typeof(List<Entities.InterestRate>))]
        public IHttpActionResult GetInterestRate(string iban, [FromUri] InterestRateOptions options)
        {
            try
            {
                var interestRateOverrideItems = _matrixManager.GetInterestRate(iban, options);
                return Ok(interestRateOverrideItems);
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
        /// The unblock saving accounts.
        /// </summary>
        /// <param name="iban">
        /// The iban.
        /// </param>
        /// <returns>
        /// The <see cref="IHttpActionResult"/>.
        /// </returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        /// <exception cref="ServiceLayerException"> A wrapper exception  </exception>
        [Route("{iban}/unblock")]
        [HttpPut]
        public IHttpActionResult UnblockSavingAccounts(string iban)
        {
            try
            {
                _matrixManager.UnblockSavingAccounts(iban);
                return NoContent();
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
        /// The get accrued interest.
        /// </summary>
        /// <param name="iban">
        /// The iban.
        /// </param>
        /// <param name="valueDate">
        /// The value date.
        /// </param>
        /// <param name="calculateTaxAction">
        /// The calculate tax action.
        /// </param>
        /// <returns>
        /// The <see cref="IHttpActionResult"/>.
        /// </returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        /// <exception cref="ServiceLayerException"> A wrapper exception  </exception>
        [Route("{iban}/accruedinterest")]
        [HttpGet]
        [ResponseType(typeof(decimal))]
        public IHttpActionResult GetAccruedInterest(string iban, DateTime valueDate, bool calculateTaxAction)
        {
            try
            {
                var accruedInterest = _matrixManager.GetAccruedInterest(iban, valueDate, calculateTaxAction);
                return Ok(accruedInterest);
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
        /// The get overview balance.
        /// </summary>
        /// <param name="iban">
        /// The iban.
        /// </param>
        /// <param name="valueDate">
        /// The value date.
        /// </param>
        /// <returns>
        /// The <see cref="IHttpActionResult"/>.
        /// </returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        /// <exception cref="ServiceLayerException"> A wrapper exception  </exception>
        [Route("{iban}/balance")]
        [HttpGet]
        [ResponseType(typeof(System.Decimal))]
        public IHttpActionResult GetBalanceOverview(string iban, DateTime valueDate)
        {
            try
            {
                var balance = _matrixManager.GetBalanceOverview(iban, valueDate);
                return Ok(balance);
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
        /// Gets an overview of the transactions for specified <paramref name="iban"/> between dates specified
        /// </summary>
        /// <param name="iban"> The iban. </param>
        /// <param name="startDate"> The start date. </param>
        /// <param name="endDate"> The end date. </param>
        /// <returns>
        /// The <see cref="IHttpActionResult"/>.
        /// </returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        /// <exception cref="ServiceLayerException"> A wrapper exception  </exception>
        [Route("{iban}/movements")]
        [HttpGet]
        [ResponseType(typeof(List<Entities.MovementItem>))]
        public IHttpActionResult GetMovements(string iban, DateTime? startDate = null, DateTime? endDate = null)
        {
            try
            {
                var movements = _matrixManager.GetMovements(iban, startDate, endDate);
                return Ok(movements);
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
        /// get cash account overview.
        /// </summary>
        /// <param name="iban">
        /// The iban.
        /// </param>
        /// <param name="valueDate">
        /// The value date.
        /// </param>
        /// <returns>
        /// The <see cref="IHttpActionResult"/>.
        /// </returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        /// <exception cref="ServiceLayerException"> A wrapper exception  </exception>
        [Route("{iban}/overview")]
        [HttpGet]
        [ResponseType(typeof(AccountOverview))]
        public IHttpActionResult GetAccountOverview(string iban, DateTime valueDate)
        {
            try
            {
                var savingsAccountOverview = _matrixManager.GetAccountOverview(iban, valueDate);
                return Ok(savingsAccountOverview);
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
        /// The block saving accounts.
        /// </summary>
        /// <param name="iban">
        /// The iban.
        /// </param>
        /// <returns>
        /// The <see cref="IHttpActionResult"/>.
        /// </returns>
        [Route("{iban}/block")]
        [HttpPut]
        public IHttpActionResult BlockSavingAccounts(string iban)
        {
            try
            {
                var result = _matrixManager.BlockSavingAccounts(iban);
                if (result)
                {
                    return NoContent();
                }

                return BadRequest();
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
        /// The create outgoing payment.
        /// </summary>
        /// <param name="iban">
        /// The iban.
        /// </param>
        /// <param name="payment">
        /// Payment given.
        /// </param>
        /// <returns>
        /// The <see cref="IHttpActionResult"/>.
        /// </returns>
        [Route("{iban}/PaymentsOut")]
        [HttpPost]
        public IHttpActionResult CreateOutgoingPayment(string iban, Payment payment)
        {
            try
            {
                var result = _matrixManager.CreateOutgoingPayment(iban, payment);

                // TODO/REVIEW : Created url does not exist yet!
                return Created(new Uri(string.Format("{0}/{1}", Request.RequestUri, result)), result);
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
