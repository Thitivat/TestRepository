using System;

namespace BND.Websites.BackOffice.SanctionListsManagement.Web.Common
{
    /// <summary>
    /// This class provides the property about the status of updating process, for front end to display 
    /// the information to show the current status of processing.
    /// </summary>
    public class AutoUpdateStatus
    {
        /// <summary>
        /// Gets or sets the progressing value for working.
        /// </summary>
        /// <value>The progress.</value>
        public decimal Progress { get; set; }

        /// <summary>
        /// Gets or sets the status of eu updating progress, it can be "Updating", "Finised" and empty.
        /// </summary>
        /// <value>The status.</value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the Time that update process start.
        /// </summary>
        /// <value>The start update.</value>
        public DateTime StartUpdate { get; set; }

        /// <summary>
        /// Gets or sets the Time that update process is finished.
        /// </summary>
        /// <value>The finished update.</value>
        public DateTime FinishedUpdate { get; set; }

        /// <summary>
        /// Gets or sets the response code, this status following HTTP status code.
        /// 200 - OK
        /// 304 - Not Modified
        /// 400 - Bad Request
        /// 404 - Not Found
        /// 500 - Internal Server Error
        /// </summary>
        /// <value>The response code.</value>
        public int ResponseCode { get; set; }

        /// <summary>
        /// Gets or sets the message that com from the 
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }
    }
}