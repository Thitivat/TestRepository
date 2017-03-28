namespace BND.Websites.BackOffice.SanctionListsManagement.Entities
{
    /// <summary>
    /// Represents information about bank and bank accounts.
    /// </summary>
    public class Bank
    {
        /// <summary>
        /// Bank identifier.
        /// </summary>
        public int BankId { get; set; }

        /// <summary>
        /// Bank identifier from source (while importing from external source). 
        /// If it is not provided then it is set to be same as Bank Id.
        /// </summary>
        public int? OriginalBankId { get; set; }

        /// <summary>
        /// Name of the Bank.
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// Bank's SWIFT code. <a href="https://en.wikipedia.org/wiki/ISO_9362">IBAN</a>  
        /// </summary>
        public string Swift { get; set; }

        /// <summary>
        /// Account Holder's name.
        /// </summary>
        public string AccountHolderName { get; set; }

        /// <summary>
        /// Account number.
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// IBAN (International Bank Account Number). See <a href="https://en.wikipedia.org/wiki/International_Bank_Account_Number">IBAN</a>  
        /// </summary>
        public string Iban { get; set; }

        /// <summary>
        /// <see cref="Entities.Remark"/>.
        /// </summary>
        public Remark Remark { get; set; }

        /// <summary>
        /// Country associated with Bank.
        /// </summary>
        public Country Country { get; set; }

        /// <summary>
        /// Id of the Entity which Bank belongs to.
        /// </summary>
        public int EntityId { get; set; }
    }
}
