using BND.Services.Payments.PaymentIdInfo.Entities;
using System;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Security.Authentication;
using System.Web.Http.Filters;

namespace BND.Services.Payments.PaymentIdInfo.WebService.App_Start
{
    /// <summary>
    /// Class ErrorHandlerConfigAttribute is an Attribute of exception filter
    /// </summary>
    public class ErrorHandlerConfigAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// The error_code_format
        /// </summary>
        private const string ERROR_CODE_FORMAT = "BND{0}";
        /// <summary>
        /// Raises the exception event for each possible exception.
        /// </summary>
        /// <param name="actionExecutedContext">The context for the action.</param>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            ApiErrorModel errorModel = new ApiErrorModel { StatusCode = HttpStatusCode.InternalServerError };

            errorModel.Message = actionExecutedContext.Exception.Message;

            //Exception type is UnauthorizedAccessException
            if (actionExecutedContext.Exception is UnauthorizedAccessException)
            {
                errorModel.StatusCode = HttpStatusCode.Unauthorized;
                errorModel.ErrorCode = String.Format(ERROR_CODE_FORMAT, (int)errorModel.StatusCode);
            }
            //Exception type is InvalidCredentialException or InvalidOperationException
            else if (actionExecutedContext.Exception is InvalidCredentialException || actionExecutedContext.Exception is InvalidOperationException)
            {
                errorModel.StatusCode = HttpStatusCode.Forbidden;
                errorModel.ErrorCode = String.Format(ERROR_CODE_FORMAT, (int)errorModel.StatusCode);
            }
            //Exception type is ArgumentException
            else if (actionExecutedContext.Exception is ArgumentException)
            {
                errorModel.StatusCode = HttpStatusCode.BadRequest;
                errorModel.ErrorCode = String.Format(ERROR_CODE_FORMAT, (int)errorModel.StatusCode);
            }
            //Exception type is ObjectNotFoundException, high priority than DataException
            else if (actionExecutedContext.Exception is ObjectNotFoundException)
            {
                errorModel.StatusCode = HttpStatusCode.NotFound;
                errorModel.ErrorCode = String.Format(ERROR_CODE_FORMAT, (int)errorModel.StatusCode);
            }
            //Exception type is DataException
            else if (actionExecutedContext.Exception is DataException)
            {
                errorModel.StatusCode = HttpStatusCode.Conflict;
                errorModel.ErrorCode = String.Format(ERROR_CODE_FORMAT, (int)errorModel.StatusCode);
            }
            //Other exception type
            else
            {
                errorModel.ErrorCode = String.Format(ERROR_CODE_FORMAT, (int)errorModel.StatusCode); ;
            }

            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(errorModel.StatusCode);
            actionExecutedContext.Response.Content = new ObjectContent<ApiErrorModel>(errorModel, new JsonMediaTypeFormatter(), "application/json");

        }
    }
}