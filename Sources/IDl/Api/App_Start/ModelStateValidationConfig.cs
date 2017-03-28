using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using BND.Services.Payments.iDeal.Api.Helpers;

namespace BND.Services.Payments.iDeal.Api
{
    /// <summary>
    /// Class ModelStateValidationConfig is an Attribute of action filter
    /// </summary>
    public class ModelStateValidationConfigAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Occurs before the action method is invoked.
        /// </summary>
        /// <param name="actionContext">The context for the action.</param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext != null && actionContext.ActionArguments != null && actionContext.ActionArguments.Any())
            {
                foreach (var x in actionContext.ActionArguments)
                {
                    actionContext.ModelState.Validate(x.Key);
                }
            }
        }
    }
}