using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BND.Services.Payments.eMandates.Business.Models
{
    /// <summary>
    /// Describes a new mandate response
    /// 
    /// </summary>
    public class ErrorResponseModel
    {
        /// <summary>
        /// Unique identification of the error occurring within the iDx transaction
        /// 
        /// </summary>
        public string ErrorCode { get; set; }
        /// <summary>
        /// Descriptive text accompanying Error.errorCode
        /// 
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// Details of the error
        /// 
        /// </summary>
        public string ErrorDetails { get; set; }
        /// <summary>
        /// Suggestions aimed at resolving the problem
        /// 
        /// </summary>
        public string SuggestedAction { get; set; }
        /// <summary>
        /// A (standardised) message that the merchant should show to the consumer
        /// 
        /// </summary>
        public string ConsumerMessage { get; set; }
    }
}
