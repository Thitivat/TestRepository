using System.Net;

namespace BND.Services.Security.OTP.Models
{
    /// <summary>
    /// Class ApiErrorModel is a model representing error which returns from <see>
    ///         <cref>BND.Services.Security.OTP.Api</cref>
    ///     </see>
    ///     api.
    /// </summary>
    public class ApiErrorModel
    {
        /// <summary>
        /// Gets or sets the http status code following HTTP protocol.
        /// </summary>
        /// <value>The http status code.</value>
        public HttpStatusCode StatusCode { get; set; }
        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        /// <value>The error code.</value>
        public int ErrorCode { get; set; }
        /// <summary>
        /// Gets or sets the messages.
        /// </summary>
        /// <value>The collection of messages.</value>
        public ErrorMessageModel[] Messages { get; set; }
    }
}
