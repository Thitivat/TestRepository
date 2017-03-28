using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BND.Services.Payments.eMandates.Enums;

namespace BND.Services.Payments.eMandates.Business.Models
{
    public class StatusResponseModel
    {
        public static readonly string Open;
        public static readonly string Pending;
        public static readonly string Success;
        public static readonly string Failure;
        public static readonly string Expired;
        public static readonly string Cancelled;
        /// <summary>
        /// true if an error occured, or false when no errors were encountered
        /// 
        /// </summary>
        public bool IsError { get; set; }
        /// <summary>
        /// Object that holds the error if one occurs; when there are no errors, this is set to null
        /// 
        /// </summary>
        public ErrorResponseModel Error { get; set; }
        /// <summary>
        /// The transaction ID
        /// 
        /// </summary>
        public string TransactionId { get; set; }
        /// <summary>
        /// Possible values: Open, Pending, Success, Failure, Expired, Cancelled
        /// 
        /// </summary>
        public EnumQueryStatus Status { get; set; }
        /// <summary>
        /// DateTime when the status was created, or null if no such date available (for example, when mandate has expired)
        /// 
        /// </summary>
        public DateTime? StatusDateTimestamp { get; set; }
        /// <summary>
        /// The acceptance report returned in the status response
        /// 
        /// </summary>
        public AcceptanceReportModel AcceptanceReport { get; set; }
        /// <summary>
        /// The response XML
        /// 
        /// </summary>
        public string RawMessage { get; set; }
    }
}
