using BND.Services.IbanStore.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace BND.Services.IbanStore.Api.Models
{
    public class ErrorResult : IHttpActionResult
    {
        #region [Properties]
        public HttpRequestMessage Request { get; set; }
        public ApiErrorModel Error { get; set; }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorResult"/> class.
        /// </summary>
        /// <param name="request">The http request.</param>
        /// <param name="error">The error object.</param>
        /// <exception cref="System.ArgumentNullException">
        /// request
        /// or
        /// error
        /// or
        /// error.Messages
        /// </exception>
        /// <exception cref="System.ArgumentException">error.StatusCode</exception>
        public ErrorResult(HttpRequestMessage request, ApiErrorModel error)
        {
            // Checks required parameters.
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }
            if (error == null)
            {
                throw new ArgumentNullException("error");
            }
            if (error.Message == null || error.Message.Length == 0)
            {
                throw new ArgumentNullException("error.Messages");
            }
            // Http status cannot be 200.
            if (error.StatusCode == HttpStatusCode.OK)
            {
                throw new ArgumentException(String.Format(Properties.Resources.ErrorWrongHttpStatus, error.StatusCode), "error.StatusCode");
            }

            // Sets all properties.
            Request = request;
            Error = error;
        }
        #endregion

        #region [Public Method]
        /// <summary>
        /// Executes this instance.
        /// </summary>
        /// <returns>HttpResponseMessage.</returns>
        public HttpResponseMessage Execute()
        {
            HttpResponseMessage response = new HttpResponseMessage(Error.StatusCode);
            response.Content = new ObjectContent<ApiErrorModel>(Error, new JsonMediaTypeFormatter(), "application/json");
            response.RequestMessage = Request;

            return response;
        }

        /// <summary>
        /// Creates an <see cref="T:System.Net.Http.HttpResponseMessage" /> asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task that, when completed, contains the <see cref="T:System.Net.Http.HttpResponseMessage" />.</returns>
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }
        #endregion
    }
}