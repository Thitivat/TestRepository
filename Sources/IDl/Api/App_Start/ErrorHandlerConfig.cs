using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using BND.Services.Payments.iDeal.Models;
using System.Net.Http.Formatting;
using System.Security.Authentication;
using System.Data;
using BND.Services.Payments.iDeal.iDealClients.Base;
using BND.Services.Payments.iDeal.Interfaces;

namespace BND.Services.Payments.iDeal.Api
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
            ILogger logger = WindsorConfig._container.Resolve<ILogger>();

            ApiErrorModel errorModel = new ApiErrorModel { StatusCode = HttpStatusCode.InternalServerError };

            errorModel.Message = actionExecutedContext.Exception.Message;

            //Exception type is UnauthorizedAccessException
            if (actionExecutedContext.Exception is UnauthorizedAccessException)
            {
                errorModel.StatusCode = HttpStatusCode.Unauthorized;
                errorModel.ErrorCode = String.Format(ERROR_CODE_FORMAT, (int)errorModel.StatusCode);
            }
            //Exception type is InvalidCredentialException or InvalidOperationException
            else if(actionExecutedContext.Exception is InvalidCredentialException || actionExecutedContext.Exception is InvalidOperationException)
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
            //Exception type is iDealOperationException
            else if (actionExecutedContext.Exception is iDealOperationException)
            {
                errorModel.ErrorCode = ((iDealOperationException)actionExecutedContext.Exception).ErrorCode;
            }
            //Exception type is iDealException
            else if (actionExecutedContext.Exception is iDealException)
            {
                errorModel.ErrorCode = ((iDealException)actionExecutedContext.Exception).ErrorCode;
            }
            //Other exception type
            else
            {
                errorModel.ErrorCode = String.Format(ERROR_CODE_FORMAT, (int)errorModel.StatusCode); ;
                errorModel.Message = ErrorMessages.Error.Message;
            }

            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(errorModel.StatusCode);
            actionExecutedContext.Response.Content = new ObjectContent<ApiErrorModel>(errorModel, new JsonMediaTypeFormatter(), "application/json");
            
            //Add Error log
            logger.Error(actionExecutedContext.Exception, errorModel.ErrorCode);
        }
    }
}