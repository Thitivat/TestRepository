using System.Net;

namespace BND.Services.IbanStore.Models
{
    /// <summary>
    /// Class ApiErrorModel.
    /// </summary>
    public class ApiErrorModel
    {
        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        /// <value>The status code.</value>
        public HttpStatusCode StatusCode { get; set; }
        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        /// <value>The error code.</value>
        public int ErrorCode { get; set; }
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }
    }
}
