using System;

namespace BND.Services.Security.OTP
{
    /// <summary>
    /// Class ChannelOperationException is a custom exception class for using with <see cref="BND.Services.Security.OTP.ChannelFactory"/> class.
    /// </summary>
    public class ChannelOperationException : Exception
    {
        /// <summary>
        /// Gets the error code.
        /// </summary>
        /// <value>The error code.</value>
        public int ErrorCode { get; private set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelOperationException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        public ChannelOperationException(int errorCode)
            : this(errorCode, null, null)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelOperationException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="message">The message.</param>
        public ChannelOperationException(int errorCode, string message)
            : this(errorCode, message, null)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelOperationException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public ChannelOperationException(int errorCode, string message, Exception innerException)
            : base(message, innerException)
        {
            ErrorCode = errorCode;
        }
    }
}
