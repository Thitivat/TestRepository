using BND.Services.IbanStore.Api.Helpers;
using System.Web.Http.Filters;

namespace BND.Services.IbanStore.Api.App_Start
{
    /// <summary>
    /// Class ErrorHandlerConfigAttribute is a class for manage with error exception that can occur in the system. We can intercept system after
    /// exception occur here in OnException method.
    /// </summary>
    public class ErrorHandlerConfigAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// Raises the exception event.
        /// </summary>
        /// <param name="actionExecutedContext">The context for the action.</param>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            // call helper for create response message.
            actionExecutedContext.Response = Errors.CreateErrorResponseMessage(actionExecutedContext.Exception);
        }
    }
}