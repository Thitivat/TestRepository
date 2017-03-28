using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BND.Services.Payments.eMandates.Business.Models
{
    /// <summary>
    /// Describes a directory response
    /// 
    /// </summary>
    public class DirectoryResponseModel
    {
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
        /// DateTime set to when this directory was last updated
        /// 
        /// </summary>
        public DateTime DirectoryDateTimestamp { get; set; }
        /// <summary>
        /// The response XML
        /// 
        /// </summary>
        public string RawMessage { get; set; }
        /// <summary>
        /// List of available debtor banks
        /// 
        /// </summary>
        public List<DebtorBankModel> DebtorBanks { get; set; }
    }
}
