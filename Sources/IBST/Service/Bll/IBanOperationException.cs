using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BND.Services.IbanStore.Service
{
    public class IbanOperationException : Exception
    {
        /// <summary>
        /// Gets the error code.
        /// </summary>
        /// <value>The error code.</value>
        public int ErrorCode { get; private set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="IbanOperationException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        public IbanOperationException(int errorCode)
            : this(errorCode, null, null)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IbanOperationException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="message">The message.</param>
        public IbanOperationException(int errorCode, string message)
            : this(errorCode, message, null)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IbanOperationException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public IbanOperationException(int errorCode, string message, Exception innerException)
            : base(message, innerException)
        {
            ErrorCode = errorCode;
        }
    }
}
