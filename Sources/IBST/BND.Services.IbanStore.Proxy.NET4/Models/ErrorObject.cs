
namespace BND.Services.IbanStore.Proxy.NET4.Models
{
    /// <summary>
    /// Class ErrorObject.
    /// </summary>
    public class ErrorObject
    {
        /// <summary>
        /// HttpStatusCode Error.
        /// </summary>
        /// <value>The status code.</value>
        public int StatusCode { get; set; }

        /// <summary>
        /// ErrorCode.
        /// </summary>
        /// <value>The error code.</value>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Message Error.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }
    }
}
