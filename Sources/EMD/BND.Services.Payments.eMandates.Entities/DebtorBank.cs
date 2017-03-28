
namespace BND.Services.Payments.eMandates.Entities
{
    /// <summary>
    /// Class DebtorBank.
    /// </summary>
    public class DebtorBank
    {
        /// <summary>
        /// Country name.
        /// </summary>
        public string DebtorBankCountry { get; set; }
        /// <summary>
        /// BIC.
        /// </summary>
        public string DebtorBankId { get; set; }
        /// <summary>
        /// Bank name.
        /// </summary>
        public string DebtorBankName { get; set; }
    }
}
