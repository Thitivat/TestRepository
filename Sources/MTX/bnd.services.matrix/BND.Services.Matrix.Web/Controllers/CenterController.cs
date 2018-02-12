using System;
using System.Configuration;
using System.Web.Http;
using System.Web.Http.Description;

using BND.Services.Infrastructure.ErrorHandling;
using BND.Services.Infrastructure.WebAPI.Controllers;
using BND.Services.Infrastructure.WebAPI.Helpers;
using BND.Services.Matrix.Business.Interfaces;
using BND.Services.Matrix.Entities;

namespace BND.Services.Matrix.Web.Controllers
{
   


    /// <summary>
    /// The <see cref="CenterController"/> class
    /// </summary>
    [RoutePrefix("v1")]
    public class CenterController : BaseApiController<EnumErrorCodes>
    {
          /// <summary>
        /// The <see cref="IMatrixManager"/>
        /// </summary>
        private readonly IMatrixManager _matrixManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CenterController"/> class. 
        /// </summary>
        /// <param name="manager">
        /// The <see cref="IMatrixManager"/>. 
        /// </param>
        public CenterController(IMatrixManager manager)
        {
            _matrixManager = manager;
        }

        /// <summary>
        /// The get current center.
        /// </summary>
        /// <returns>
        /// The <see cref="IHttpActionResult"/>.
        /// </returns>
        /// <exception cref="ServiceLayerException">
        /// </exception>
        [Route("centers/current")]
        [HttpGet]
        [ResponseType(typeof(string))]
        public IHttpActionResult GetCurrentCenter()
        {
            try
            {
                var currentCenter = ConfigurationManager.AppSettings[Business.Constants.MatrixCenterName];
                return Ok(currentCenter);
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