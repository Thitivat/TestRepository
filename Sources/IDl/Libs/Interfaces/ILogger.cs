using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BND.Services.Payments.iDeal.Interfaces
{
    /// <summary>
    /// Interface ILogger that provides the methods to logged activities or error of system.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Log normal behavior of application.
        /// </summary>
        /// <param name="message">The message.</param>
        void Info(string message);

        /// <summary>
        /// Log when system got an incorrect behavior but the application can continue.
        /// </summary>
        /// <param name="message">The message.</param>
        void Warning(string message);

        /// <summary>
        /// Log when system got an error from application.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="msgId">The MSG identifier.</param>
        void Error(Exception exception, string msgId = null);
    }
}
