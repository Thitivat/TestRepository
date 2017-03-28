using BND.Services.Payments.iDeal.Dal;
using BND.Services.Payments.iDeal.Dal.Pocos;
using BND.Services.Payments.iDeal.iDealClients.Base;
using BND.Services.Payments.iDeal.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BND.Services.Payments.iDeal
{
    /// <summary>
    /// Class Logger for create log when the operation have been done. 
    /// </summary>
    public class Logger : ILogger
    {
        /// <summary>
        /// Enum Severity
        /// </summary>
        public enum Severity
        {
            /// <summary>
            /// system is unusable
            /// </summary>
            Emergency = 0,

            /// <summary>
            /// action must be taken immediately
            /// </summary>
            Alert = 1,

            /// <summary>
            /// critical conditions
            /// </summary>
            Critical = 2,

            /// <summary>
            /// error conditions
            /// </summary>
            Error = 3,

            /// <summary>
            /// warning conditions
            /// </summary>
            Warning = 4,

            /// <summary>
            /// normal but significant condition
            /// </summary>
            Notice = 5,

            /// <summary>
            /// informational messages
            /// </summary>
            Informational = 6,

            /// <summary>
            /// debug-level messages
            /// </summary>
            Debug = 7
        }

        /// <summary>
        /// The facility
        /// </summary>
        protected const int FACILITY = 16;
        /// <summary>
        /// The version
        /// </summary>
        protected const int VERSION = 1;

        /// <summary>
        /// The host name
        /// </summary>
        protected string HostName { get; private set; }
        /// <summary>
        /// The app name
        /// </summary>
        protected string AppName { get; private set; }

        /// <summary>
        /// The log repository
        /// </summary>
        private ILogRepository _logRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// </summary>
        /// <param name="logRepository">The log repository for database communication.</param>
        /// <param name="appName">Name of the application.</param>
        public Logger(ILogRepository logRepository, string appName)
        {
            _logRepository = logRepository;
            AppName = appName;
            HostName = System.Environment.MachineName;
        }

        /// <summary>
        /// Gets the prival, calculated by severity and facility.
        /// </summary>
        /// <param name="severity">The severity.</param>
        /// <returns>Prival.</returns>
        protected virtual byte GetPrival(int severity)
        {
            return (byte)(FACILITY * 8 + severity);
        }

        /// <summary>
        /// Generates the log.
        /// </summary>
        /// <param name="severity">Type of the log.</param>
        /// <param name="procId">The procecure identifier.</param>
        /// <param name="msgId">The MSG identifier.</param>
        /// <param name="message">The message.</param>
        /// <returns>log object.</returns>
        protected virtual p_Log GenerateLog(int severity, string procId, string msgId, string message)
        {
            return new p_Log
            {
                Prival = GetPrival(severity),
                Version = VERSION,
                Timestamp = DateTime.Now,
                Hostname = HostName,
                AppName = AppName,
                ProcId = procId,
                MsgId = msgId,
                Msg = message,
            };
        }

        /// <summary>
        /// Generates the log by exception.
        /// </summary>
        /// <param name="severity">The severity.</param>
        /// <param name="procId">The procedure identifier.</param>
        /// <param name="msgId">The message identifier.</param>
        /// <param name="exception">The exception.</param>
        /// <returns>log object.</returns>
        protected virtual p_Log GenerateLog(int severity, string procId, string msgId, Exception exception)
        {
            if (exception is iDealException && String.IsNullOrEmpty(msgId))
            {
                msgId = ((iDealException)exception).ErrorCode;
            }

            List<string> messages = new List<string>();
            do
            {
                messages.Add(exception.Message);
                exception = exception.InnerException;
            } while (exception != null);
            string message = String.Join(Environment.NewLine, messages);

            return GenerateLog(severity, procId, msgId, message);
        }


        /// <summary>
        /// Log normal behavior of application.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <exception cref="System.ArgumentNullException">message</exception>
        public void Info(string message)
        {
            if (String.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException("message");
            }

            string msgId = "INFO";
            string procId = new StackFrame(1).GetMethod().Name;
            p_Log log = GenerateLog((int)Severity.Informational, procId, msgId, message);
            _logRepository.Insert(log);
        }

        /// <summary>
        /// Log when system got an incorrect behavior but the application can continue.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <exception cref="System.ArgumentNullException">message</exception>
        public void Warning(string message)
        {
            if (String.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException("message");
            }

            string msgId = "WARNING";
            string procId = new StackFrame(1).GetMethod().Name;
            p_Log log = GenerateLog((int)Severity.Warning, procId, msgId, message);
            _logRepository.Insert(log);
        }

        /// <summary>
        /// Log when system got an error from application.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="msgId">The MSG identifier.</param>
        /// <exception cref="System.ArgumentNullException">
        /// exception
        /// or
        /// msgId;msgId is required when exception is not iDealException.
        /// </exception>
        public void Error(Exception exception, string msgId = null)
        {
            if (exception == null)
            {
                throw new ArgumentNullException("exception");
            }
            if (!(exception is iDealException) && !(exception is iDealOperationException) && String.IsNullOrEmpty(msgId))
            {
                throw new ArgumentNullException("msgId", "msgId is required when exception is not iDealException.");
            }

            if (String.IsNullOrEmpty(msgId))
            {
                msgId = (string)((dynamic)exception).ErrorCode;
            }

            string procId = new StackFrame(1).GetMethod().Name;
            p_Log log = GenerateLog((int)Severity.Error, procId, msgId, exception);
            _logRepository.Insert(log);
        }
    }
}
