using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BND.Services.Payments.eMandates.Business.Models
{
    /// <summary>
    /// A debtor bank contained in a directory response
    /// 
    /// </summary>
    public class DebtorBankModel
    {
        /// <summary>
        /// Country name
        /// 
        /// </summary>
        public string DebtorBankCountry { get; set; }
        /// <summary>
        /// BIC
        /// 
        /// </summary>
        public string DebtorBankId { get; set; }
        /// <summary>
        /// Bank name
        /// 
        /// </summary>
        public string DebtorBankName { get; set; }
    }
}
