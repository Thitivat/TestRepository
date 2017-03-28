using System.Web.Http;
using BND.Services.Payments.iDeal.Interfaces;
using BND.Services.Payments.iDeal.Models;

namespace BND.Services.Payments.iDeal.Api.Controllers
{
    /// <summary>
    /// Class TransactionsController is a controller representing transactions resource for performing transactions
    /// </summary>
    public class TransactionsController : ApiController
    {
        #region [Fields]
        /// <summary>
        /// The ideal service
        /// </summary>
        private readonly IiDealService _idealService;
        /// <summary>
        /// The logger service
        /// </summary>
        private readonly ILogger _logger;
        #endregion


        #region [Constructors]
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionsController"/> class.
        /// </summary>
        /// <param name="iDealService">The i deal service.</param>
        /// <param name="logger">The logger.</param>
        public TransactionsController(IiDealService iDealService, ILogger logger)
        {
            _logger = logger;
            _idealService = iDealService;
        }
        #endregion


        #region [Methods]
        /// <summary>
        /// Posts the specified transaction request model.
        /// </summary>
        /// <param name="transactionRequestModel">The transaction request model.</param>
        /// <returns>IHttpActionResult.</returns>
        public IHttpActionResult Post(TransactionRequestModel transactionRequestModel)
        {
            return Ok(_idealService.CreateTransaction(transactionRequestModel));
        }

        /// <summary>
        /// Gets transaction status.
        /// </summary>
        /// <param name="transactionID">The transaction identifier.</param>
        /// <param name="entranceCode">The entrance code.</param>
        /// <returns>IHttpActionResult.</returns>
        [Route("{transactionID}/Status")]
        [HttpGet]
        public IHttpActionResult GetStatus(string entranceCode, string transactionID)
        {
            return Ok(_idealService.GetStatus(entranceCode, transactionID).ToString());
        }
        #endregion
    }
}