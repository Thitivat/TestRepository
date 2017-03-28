using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace BND.Services.IbanStore.Service
{
    public static class MessageLibs
    {
        /// <summary>
        /// ID: 1001
        /// MESSAGE: {0} already existing in the system.
        /// PLACEHOLDERS: {0} = Object name
        /// </summary>
        public static readonly LogMessage MSG_ALREADY_EXIST_SYSTEM = new LogMessage
        {
            Code = 1001,
            Message = "{0} already existing in the system."
        };

        /// <summary>
        /// ID: 1002
        /// MESSAGE: {0} cannot {1} to the database.
        /// PLACEHOLDERS: {0} = Object name
        ///               {1} = Process
        /// </summary>
        public static readonly LogMessage MSG_CANNOT_PROCESS_DATABASE = new LogMessage
        {
            Code = 1002,
            Message = "{0} cannot {1} to the database."
        };

        /// <summary>
        /// ID: 1003
        /// MESSAGE: {0} cannot be approved.
        /// PLACEHOLDERS: {0} = Object name
        /// </summary>
        public static readonly LogMessage MSG_CANNOT_PROCESS_FUNCTION = new LogMessage
        {
            Code = 1003,
            Message = "{0} cannot be {1}."
        };

        /// <summary>
        /// ID: 1004
        /// MESSAGE: {0} already existing in the database.
        /// PLACEHOLDERS: {0} = Object name
        /// </summary>
        public static readonly LogMessage MSG_ALREADY_EXIST_DATABASE = new LogMessage
        {
            Code = 1004,
            Message = "{0} already existing in the database."
        };

        /// <summary>
        /// ID: 1005
        /// MESSAGE: The system cannot be found {0} ready to approve.
        /// PLACEHOLDERS: {0} = Object name
        ///               {1} = Activity
        /// </summary>
        public static readonly LogMessage MSG_CANNOT_FOUND = new LogMessage
        {
            Code = 1005,
            Message = "The system cannot be found {0} that {1}"
        };

        /// <summary>
        /// ID: 1006
        /// MESSAGE: The system cannot be found {0} ready to approve.
        /// PLACEHOLDERS: {0} = Object name
        ///               {1} = Activity
        /// </summary>
        public static readonly LogMessage MSG_COULD_NOT_BE_FOUND = new LogMessage
        {
            Code = 1006,
            Message = "{0} could not be found."
        };

        /// <summary>
        /// ID: 1007
        /// MESSAGE: The system cannot sent an email.
        /// </summary>
        public static readonly LogMessage ERR_SEND_EMAIL = new LogMessage
        {
            Code = 1007,
            Message = "The system cannot sent an email."
        };

        /// <summary>
        /// ID: 1008
        /// MESSAGE: {0} is invalid.
        /// PLACEHOLDERS: {0} = Object name
        /// </summary>
        public static readonly LogMessage MSG_INVALID = new LogMessage
        {
            Code = 1008,
            Message = "{0} is invalid."
        };

        /// <summary>
        /// ID: 1009
        /// MESSAGE: The Uploader cannot approve BBAN File that your uploaded.
        /// </summary>
        public static readonly LogMessage MSG_UPLOADER_CANNOT_APPROVE = new LogMessage
        {
            Code = 1009,
            Message = "The Uploader cannot approve BBAN File that your uploaded."
        };

        /// <summary>
        /// ID: 1010
        /// MESSAGE: {0} {1} records.
        /// PLACEHOLDERS: {0} = Status
        ///               {1} = Total records
        /// </summary>
        public static readonly LogMessage MSG_RECORDS = new LogMessage
        {
            Code = 1010,
            Message = "{0} {1} records." 
        };

        /// <summary>
        /// ID: 1011
        /// MESSAGE: {0} cannot be null or empty.
        /// PLACEHOLDERS: {0} = object
        /// </summary>
        public static readonly LogMessage MSG_CANNOT_BE_NULL = new LogMessage
        {
            Code = 1011,
            Message = "{0} cannot be null or empty."
        };

        /// <summary>
        /// ID: 1012
        /// MESSAGE: {0} is expired.
        /// PLACEHOLDERS: {0} = object
        /// </summary>
        public static readonly LogMessage MSG_EXPIRE_DATE = new LogMessage
        {
            Code = 1012,
            Message = "{0} is expired."
        };

        /// <summary>
        /// ID: 1013
        /// MESSAGE: {0} not available.
        /// PLACEHOLDERS: {0} = object
        /// </summary>
        public static readonly LogMessage MSG_NOT_AVAILABLE = new LogMessage
        {
            Code = 1013,
            Message = "{0} not available."
        };

        /// <summary>
        /// ID: 1014
        /// MESSAGE: The system cannot be found {0} ready to approve.
        /// PLACEHOLDERS: {0} = Object name
        /// </summary>
        public static readonly LogMessage MSG_CANNOT_BE_FOUND = new LogMessage
        {
            Code = 1014,
            Message = "The system cannot be found {0}."
        };

        /// <summary>
        /// ID: 1015
        /// MESSAGE: The system cannot be found {0} ready to approve.
        /// PLACEHOLDERS: {0} = Object name
        /// PLACEHOLDERS: {1} = Time
        /// </summary>
        public static readonly LogMessage MSG_RESERVED_HISTORY = new LogMessage
        {
            Code = 1015,
            Message = "Reserved by {0} at time: {1}."
        };

        /// <summary>
        /// ID: 1016
        /// MESSAGE: The system cannot be found {0} ready to approve.
        /// </summary>
        public static readonly LogMessage MSG_IBAN_ALREADY_ASSIGNED = new LogMessage
        {
            Code = 1016,
            Message = "Cannot reserve IBAN that already assigned."
        };

        /// <summary>
        /// ID: 1017
        /// MESSAGE: BBAN file has duplicate BBAN {0}.
        /// </summary>
        public static readonly LogMessage MSG_BBAN_FILE_DUPLICATE = new LogMessage
        {
            Code = 1017,
            Message = "BBAN file has duplicate BBAN {0}."
        };

        /// <summary>
        /// ID: 1018
        /// MESSAGE: Assigned IBAN.
        /// </summary>
        /// PLACEHOLDERS: {0} = Object name
        /// PLACEHOLDERS: {1} = Time
        public static readonly LogMessage MSG_ASSIGNED_IBAN = new LogMessage()
        {
            Code = 1018,
            Message = "Assigned by {0} at time: {1}."
        };
    }

    public class LogMessage
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }
}
