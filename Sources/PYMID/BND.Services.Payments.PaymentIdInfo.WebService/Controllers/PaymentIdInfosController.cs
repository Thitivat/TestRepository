using BND.Services.Payments.PaymentIdInfo.Business;
using BND.Services.Payments.PaymentIdInfo.Entities.Helpers;
using BND.Services.Payments.PaymentIdInfo.WebService.Helpers;
using System.Linq;
using System.Web.Http;

namespace BND.Services.Payments.PaymentIdInfo.WebService.Controllers
{
    /// <summary>
    /// Class PaymentIdInfoController is controller to get data from iDeal, eMandate and Matrix.
    /// </summary>
    public class PaymentIdInfosController : ApiController
    {
        #region [Fields]
        /// <summary>
        /// The PaymentIdInfo manager
        /// </summary>
        private readonly IPaymentIdInfoManager _paymentIdInfoManager;
        #endregion

        #region [Constructors]
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentIdInfosController"/> class.
        /// </summary>
        /// <param name="paymentIdInfoManager">The payment identifier information manager.</param>
        public PaymentIdInfosController(IPaymentIdInfoManager paymentIdInfoManager)
        {
            _paymentIdInfoManager = paymentIdInfoManager;
        }
        #endregion

        #region [Methods]
        /// <summary>
        /// Gets the filter types.
        /// </summary>
        /// <returns>IHttpActionResult.</returns>
        [Route("GetFilterTypes")]
        [HttpGet]
        public IHttpActionResult GetFilterTypes()
        {
            return Ok(_paymentIdInfoManager.GetFilterTypes());
        }

        /// <summary>
        /// Gets the payment information by BND iban.
        /// </summary>
        /// <param name="bndIban">The BND iban.</param>
        /// <param name="filterType">Type of the filter.</param>
        /// <returns>IHttpActionResult.</returns>
        [Route("BndIban/{bndIban}")]
        [HttpGet]
        public IHttpActionResult GetPaymentInfoByBndIban(string bndIban, string filterType = "Unknown")
        {
            var filterList = EnumFilterTypeHelper.ConvertToEnumFilterTypes(filterType);
            var result = _paymentIdInfoManager.GetPaymentIdByBndIban(bndIban, filterList.ToList());
            return Ok(result);
        }

        /// <summary>
        /// Gets the payment information by source iban.
        /// </summary>
        /// <param name="sourceIban">The source iban.</param>
        /// <param name="filterType">Type of the filter.</param>
        /// <returns>IHttpActionResult.</returns>
        [Route("SourceIban/{sourceIban}")]
        [HttpGet]
        public IHttpActionResult GetPaymentInfoBySourceIban(string sourceIban, string filterType = "Unknown")
        {
            var filterList = EnumFilterTypeHelper.ConvertToEnumFilterTypes(filterType);
            return Ok(_paymentIdInfoManager.GetPaymentIdBySourceIban(sourceIban, filterList.ToList()));
        }

        /// <summary>
        /// Gets the payment information by transaction.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="filterType">Type of the filter.</param>
        /// <returns>IHttpActionResult.</returns>
        [Route("Transaction/{transactionId}")]
        [HttpGet]
        public IHttpActionResult GetPaymentInfoByTransaction(string transactionId, string filterType = "Unknown")
        {
            var filterList = EnumFilterTypeHelper.ConvertToEnumFilterTypes(filterType);
            return Ok(_paymentIdInfoManager.GetPaymentIdByTransaction(transactionId, filterList.ToList()));
        }
        #endregion
    }
}
