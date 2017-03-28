using BND.Services.Payments.PaymentIdInfo.WebService.Helpers;
using System;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace BND.Services.Payments.PaymentIdInfo.WebService.App_Start
{
    /// <summary>
    /// Class SecurityConfigAttribute is an Attribute of action filter
    /// </summary>
    public class SecurityConfigAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// The security service
        /// </summary>
        private ISecurity _security;
        /// <summary>
        /// Call before the action method executes.
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        /// <exception cref="System.ArgumentNullException">Authorization;Authorization header is required, cannot be null.</exception>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            _security = WindsorConfig._container.Resolve<ISecurity>();
            if (actionContext.Request.Headers.Authorization == null)
            {
                throw new ArgumentNullException("Authorization", "Authorization header is required, cannot be null.");
            }
            else
            {
                _security.ValidateToken(actionContext.Request.Headers.Authorization.ToString());
            }
        }
    }
}