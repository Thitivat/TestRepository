using BND.Services.Security.OTP.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace BND.Services.Security.OTP.Api.Utils
{
    /// <summary>
    /// Class ErrorResult is an api response class which implements IHttpActionResult for respond error to client with error and specified
    /// <a hrefe="https://msdn.microsoft.com/en-us/library/system.net.httpstatuscode(v=vs.110).aspx" target="_blank">http status code</a>.
    /// </summary>
    public class ErrorResult : IHttpActionResult
    {
        #region [Properties]

        /// <summary>
        /// Gets the http request.
        /// </summary>
        /// <value>The http request.</value>
        public HttpRequestMessage Request { get; private set; }
        /// <summary>
        /// Gets the error object.
        /// </summary>
        /// <value>The error object.</value>
        public ApiErrorModel Error { get; private set; }

        #endregion


        #region [Constructors]

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
            if (error.Messages == null || error.Messages.Length == 0)
            {
                throw new ArgumentException("error.Messages");
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
    }
}