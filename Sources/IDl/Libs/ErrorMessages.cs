using BND.Services.Payments.iDeal.Models;
using System;

namespace BND.Services.Payments.iDeal
{
    /// <summary>
    /// Class ErrorMessages this is a static class contains all error message that will return to the client.
    /// </summary>
    public static class ErrorMessages
    {
        /// <summary>
        /// The prefix name of error code for category the message that are from our system.
        /// </summary>
        private const string ERROR_CODE_PREFIX = "BND";

        /// <summary>
        /// ID: 001
        /// MESSAGE: Unfortunately, it is not possible to pay using iDEAL at this time. Please try again later or use an alternative method of payment.
        /// </summary>
        public static readonly MessageModel Error = new MessageModel
        {
            Code = String.Concat(ERROR_CODE_PREFIX, "001"),
            Message = "Unfortunately, it is not possible to pay using iDEAL at this time. Please try again later or use an alternative method of payment."
        };
    }
}
