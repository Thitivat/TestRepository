using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace BND.Services.Payments.PaymentIdInfo.Entities
{
    /// <summary>
    /// Class ApiErrorModel is a class representing error for API response when something wrong.
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
        public string ErrorCode { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The error message.</value>
        public string Message { get; set; }
    }
}
