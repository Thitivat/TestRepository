using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BND.Websites.BackOffice.SanctionListsManagement.Models
{
    /// <summary>
    /// Class p_Bank is a poco entity representing sl.Banks table in sanction lists database.
    /// </summary>
    [Table("sl.Banks")]
    public partial class p_Bank
    {
        /// <summary>
        /// Gets or sets the bank identifier.
        /// </summary>
        /// <value>The bank identifier.</value>
        [Key]
        public int BankId { get; set; }

        /// <summary>
        /// Gets or sets the original bank identifier.
        /// </summary>
        /// <value>The original bank identifier.</value>
        public int? OriginalBankId { get; set; }

        /// <summary>
        /// Gets or sets the entity identifier.
        /// </summary>
        /// <value>The entity identifier.</value>
        public int EntityId { get; set; }

        /// <summary>
        /// Gets or sets the name of the bank.
        /// </summary>
        /// <value>The name of the bank.</value>
        [Required]
        [StringLength(256)]
        public string BankName { get; set; }

        /// <summary>
        /// Gets or sets the name of the account holder.
        /// </summary>
        /// <value>The name of the account holder.</value>
        [Required]
        [StringLength(512)]
        public string AccountHolderName { get; set; }

        /// <summary>
        /// Gets or sets the account number.
        /// </summary>
        /// <value>The account number.</value>
        [Required]
        [StringLength(20)]
        public string AccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the country following iso3 standard.
        /// </summary>
        /// <value>The country following iso3 standard.</value>
        [StringLength(3)]
        public string CountryIso3 { get; set; }

        /// <summary>
        /// Gets or sets the swift code.
        /// </summary>
        /// <value>The swift code.</value>
        [StringLength(11)]
        public string Swift { get; set; }

        /// <summary>
        /// Gets or sets the iban code.
        /// </summary>
        /// <value>The iban code.</value>
        [StringLength(40)]
        public string Iban { get; set; }

        /// <summary>
        /// Gets or sets the remark identifier.
        /// </summary>
        /// <value>The remark identifier.</value>
        public int? RemarkId { get; set; }

        /// <summary>
        /// Gets or sets the enum country.
        /// </summary>
        /// <value>The enum country.</value>
        public virtual p_EnumCountry EnumCountry { get; set; }

        /// <summary>
        /// Gets or sets the entity.
        /// </summary>
        /// <value>The entity.</value>
        public virtual p_Entity Entity { get; set; }

        /// <summary>
        /// Gets or sets the remark.
        /// </summary>
        /// <value>The remark.</value>
        public virtual p_Remark Remark { get; set; }
    }
}
