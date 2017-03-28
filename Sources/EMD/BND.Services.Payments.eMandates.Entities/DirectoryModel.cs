using System.Collections.Generic;

namespace BND.Services.Payments.eMandates.Entities
{
    /// <summary>
    /// Class DirectoryModel.
    /// </summary>
    public class DirectoryModel
    {
        /// <summary>
        /// The debtor banks.
        /// </summary>
        public List<DebtorBank> DebtorBanks { get; set; }
    }
}
