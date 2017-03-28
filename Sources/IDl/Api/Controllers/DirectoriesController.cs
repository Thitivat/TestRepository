using System;
using BND.Services.Payments.iDeal.Interfaces;
using System.Web.Http;

namespace BND.Services.Payments.iDeal.Api.Controllers
{
    /// <summary>
    /// Class DirectoriesController is a controller representing directories resource for performing directories
    /// </summary>
    public class DirectoriesController : ApiController
    {
        /// <summary>
        /// The ideal service
        /// </summary>
        private readonly IiDealService _idealService;
        /// <summary>
        /// The logger service
        /// </summary>
        private readonly ILogger _logger;
        /// <summary>
        /// Initializes a new instance of the <see cref="DirectoriesController" /> class.
        /// </summary>
        /// <param name="iDealService">The ideal service.</param>
        /// <param name="logger">The logger.</param>
        public DirectoriesController(IiDealService iDealService, ILogger logger)
        {
            _logger = logger;
            _idealService = iDealService;
        }

        // GET api/<controller>
        /// <summary>
        /// Gets all available issuer bank list and return
        /// </summary>
        /// <returns>IHttpActionResult.</returns>
        public IHttpActionResult Get()
        {
            try
            {
                // Locks to prevent multiple requests to iDeal service.
                lock (Helpers.Concurrence.lockObj)
                {
                    return Ok(_idealService.GetDirectories());
                }
            }
            catch (Exception ex)
            {
               _logger.Error(ex, ex.Message); 
                throw;
            }
        }
    }
}