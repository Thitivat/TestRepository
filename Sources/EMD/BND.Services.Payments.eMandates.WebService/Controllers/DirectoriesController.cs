using BND.Services.Payments.eMandates.Business.Interfaces;
using BND.Services.Payments.eMandates.Entities;
using System;
using System.Web.Http;

namespace BND.Services.Payments.eMandates.WebService.Controllers
{
    /// <summary>
    /// Class DirectoriesController.
    /// </summary>
    [RoutePrefix("v1/directories")]
    public class DirectoriesController : ApiController
    {
        #region [Fields]
        /// <summary>
        /// The _e mandates manager
        /// </summary>
        private readonly IEMandatesManager _eMandatesManager;
        #endregion


        #region [Constructor]
        /// <summary>
        /// Initializes a new instance of the <see cref="DirectoriesController"/> class.
        /// </summary>
        /// <param name="eMandatesManager">The e mandates manager.</param>
        public DirectoriesController(IEMandatesManager eMandatesManager)
        {
            _eMandatesManager = eMandatesManager;
        }
        #endregion

        /// <summary>
        /// Gets the current.
        /// </summary>
        /// <returns>IHttpActionResult.</returns>
        [Route("current")]
        [HttpGet]
        public IHttpActionResult GetCurrent()
        {
            IHttpActionResult result;

            try
            {
                DirectoryModel directory = _eMandatesManager.GetDirectory();

                // 200 OK
                result = Ok(directory);
            }
            // 500 Internal Server Error
            catch (Exception ex)
            {
                result = InternalServerError(ex);
            }

            return result;
        }
    }
}