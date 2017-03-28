using System;

namespace BND.Services.Payments.eMandates.Entities
{
    public class eMandateOperationException : Exception
    {
        /// <summary>
        /// Gets the error code.
        /// </summary>
        /// <value>The error code.</value>
        public string ErrorCode { get; private set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="iDealOperationException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        public eMandateOperationException(string errorCode)
            : this(errorCode, null, null)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="iDealOperationException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="message">The message.</param>
        public eMandateOperationException(string errorCode, string message)
            : this(errorCode, message, null)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="iDealOperationException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public eMandateOperationException(string errorCode, string message, Exception innerException)
            : base(message, innerException)
        {
            ErrorCode = errorCode;
        }
    }
}
