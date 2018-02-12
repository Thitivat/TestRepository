using System;
using System.Web.Http;
using System.Web.Http.Description;

using BND.Services.Infrastructure.ErrorHandling;
using BND.Services.Infrastructure.WebAPI.Controllers;
using BND.Services.Infrastructure.WebAPI.Helpers;
using BND.Services.Matrix.Entities;

namespace BND.Services.Matrix.Web.Controllers
{
    /// <summary>
    /// The <see cref="CoreController"/> class
    /// </summary>
    [RoutePrefix("v1")]
    public class CoreController : BaseApiController<EnumErrorCodes>
    {
        /// <summary>
        /// Pings the service.
        /// </summary>
        /// <returns>
        /// The <see cref="IHttpActionResult"/>.
        /// </returns>
        [Route("ping")]
        [HttpGet]
        [ResponseType(typeof(ServiceInfo))]
        public IHttpActionResult Ping()
        {
            try
            {
                var serviceInfo = new RequestServiceInfo(Request);
                return Ok(serviceInfo);
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