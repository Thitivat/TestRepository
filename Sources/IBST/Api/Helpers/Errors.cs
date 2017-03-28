using BND.Services.IbanStore.Models;
using BND.Services.IbanStore.Service;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace BND.Services.IbanStore.Api.Helpers
{
    /// <summary>
    /// Class Errors is a helper class for manipulate with http response.
    /// </summary>
    public class Errors
    {
        /// <summary>
        /// Creates the HttpResponseMessage by using the Exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public static HttpResponseMessage CreateErrorResponseMessage(Exception exception)
        {
            ApiErrorModel errorModel = Errors.CreateErrorModel(exception);
            HttpResponseMessage result = new HttpResponseMessage();
            result.Content = new ObjectContent<ApiErrorModel>(errorModel, new JsonMediaTypeFormatter(), "application/json");
            result.StatusCode = errorModel.StatusCode;

            return result;
        }

        /// <summary>
        /// This method intended to creates the ApiErrorModel by analyse Exception object.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public static ApiErrorModel CreateErrorModel(Exception exception)
        {
            ApiErrorModel errorModel = new ApiErrorModel { StatusCode = HttpStatusCode.InternalServerError };

            if (exception is UnauthorizedAccessException)
            {
                errorModel.StatusCode = HttpStatusCode.Unauthorized;
            }
            else if (exception is ArgumentException)
            {
                errorModel.StatusCode = HttpStatusCode.BadRequest;
            }
            else if (exception is FileNotFoundException)
            {
                errorModel.StatusCode = HttpStatusCode.NotFound;
            }
            else if (exception is IbanOperationException)
            {
                errorModel.StatusCode = HttpStatusCode.BadRequest;
                errorModel.ErrorCode = ((IbanOperationException)exception).ErrorCode;
            }

            errorModel.Message = exception.Message;
            return errorModel;
        }
    }
}