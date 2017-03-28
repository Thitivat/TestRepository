using System;

namespace BND.Services.Payments.iDeal
{
    /// <summary>
    /// Class iDealOperationException is a custom exception class for using when something goes wrong in iDeal service project. it provides error code.
    /// </summary>
    public class iDealOperationException : Exception
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
        public iDealOperationException(string errorCode)
            : this(errorCode, null, null)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="iDealOperationException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="message">The message.</param>
        public iDealOperationException(string errorCode, string message)
            : this(errorCode, message, null)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="iDealOperationException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public iDealOperationException(string errorCode, string message, Exception innerException)
            : base(message, innerException)
        {
            ErrorCode = errorCode;
        }

    }
}
