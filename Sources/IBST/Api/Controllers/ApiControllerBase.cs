using BND.Services.IbanStore.Api.Helpers;
using BND.Services.IbanStore.Api.Models;
using System;
using System.Linq;
using System.Web.Http;

namespace BND.Services.IbanStore.Api.Controllers
{
    /// <summary>
    /// Class ApiControllerBase.
    /// </summary>
    public class ApiControllerBase : ApiController
    {
        /// <summary>
        /// Gets the credentials.
        /// </summary>
        /// <value>The credentials.</value>
        /// <exception cref="System.ArgumentException"></exception>
        protected CredentialsModel Credentials
        {
            get
            {
                if (Request.Headers.Authorization == null)
                {
                    throw new ArgumentException(String.Format("{0} header is required.", "Authorization"));
                }

                return new CredentialsModel(Request.Headers.Authorization.ToString());
            }
        }

        /// <summary>
        /// Gets the uid prefix.
        /// </summary>
        /// <value>The uid prefix.</value>
        protected string UidPrefix
        {
            get
            {
                return (!Request.Headers.Contains("Uid-Prefix"))
                       ? null
                       : Request.Headers.GetValues("Uid-Prefix").FirstOrDefault();
            }
        }

        /// <summary>
        /// Gets the security.
        /// </summary>
        /// <value>The security.</value>
        protected ISecurity _security;

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>The logger.</value>
        protected ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiControllerBase"/> class.
        /// </summary>
        /// <param name="security">The security.</param>
        /// <param name="logger">The logger.</param>
        public ApiControllerBase()
        {
            _security = WindsorConfig._container.Resolve<ISecurity>();
            _logger = WindsorConfig._container.Resolve<ILogger>();
        }

        /// <summary>
        /// Initializes the <see cref="T:System.Web.Http.ApiController" /> instance with the specified controllerContext.
        /// </summary>
        /// <param name="controllerContext">The <see cref="T:System.Web.Http.Controllers.HttpControllerContext" /> object that is used for the initialization.</param>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.UnauthorizedAccessException"></exception>
        protected override void Initialize(System.Web.Http.Controllers.HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            try
            {
                if (!_security.Authenticate(Credentials))
                {
                    throw new UnauthorizedAccessException();
                }
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Errors.CreateErrorResponseMessage(ex));
            }
        }
    }
}
