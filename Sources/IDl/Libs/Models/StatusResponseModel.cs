using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BND.Services.Payments.iDeal.Models
{
    /// <summary>
    /// Class StatusResponseModel is a class representing response object from our iDeal service.
    /// </summary>
    public class StatusResponseModel
    {
        /// <summary>
        /// The status of transaction true is payment finished, false will be something worng in processing.
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// The code for each status.
        /// </summary>
        public string StatusCode { get; set; }

        /// <summary>
        /// The descrtion for each status.
        /// </summary>
        public string Description { get; set; }
    }
}
