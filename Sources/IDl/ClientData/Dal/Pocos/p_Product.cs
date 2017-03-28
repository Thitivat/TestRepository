using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BND.Services.Payments.iDeal.ClientData.Dal.Pocos
{
    [Table("dbo.Products")]
    public partial class p_Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public p_Product()
        {
            ProductBankAccounts = new HashSet<p_ProductBankAccount>();
            ProductClients = new HashSet<p_ProductClient>();
            Products1 = new HashSet<p_Product>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(64)]
        public string ProductGroup { get; set; }

        public DateTime? Inserted { get; set; }

        [StringLength(128)]
        public string InsertedBy { get; set; }

        public DateTime? Updated { get; set; }

        [StringLength(128)]
        public string UpdatedBy { get; set; }

        [StringLength(64)]
        public string ServiceChannelType { get; set; }

        public long? BankAccountPayments { get; set; }

        [StringLength(256)]
        public string BankAccountPaymentsName { get; set; }

        public long? BankAccountPayout { get; set; }

        [StringLength(34)]
        public string BankIBAN { get; set; }

        [StringLength(8)]
        public string BankBIC { get; set; }

        [Required]
        [StringLength(32)]
        public string ClientType { get; set; }

        public int? Referrer { get; set; }

        public int? PortfolioId { get; set; }

        [StringLength(50)]
        public string AppointmentUser { get; set; }

        [Required]
        [StringLength(256)]
        public string ProductFlowStatus { get; set; }

        [Required]
        [StringLength(50)]
        public string ProductType { get; set; }

        public int? ParentProductId { get; set; }

        [Required]
        [StringLength(512)]
        public string EncryptedPublicKey { get; set; }

        public bool? HasPaidCosts { get; set; }

        [StringLength(34)]
        public string IBANPayments { get; set; }

        [StringLength(11)]
        public string BICPayments { get; set; }

        [StringLength(34)]
        public string IBANPayout { get; set; }

        [StringLength(11)]
        public string BICPayout { get; set; }

        [StringLength(64)]
        public string IncomingChannelType { get; set; }

        public DateTime? HasPaidCostsOn { get; set; }

        public Guid? OnBoardingToken { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<p_ProductBankAccount> ProductBankAccounts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<p_ProductClient> ProductClients { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<p_Product> Products1 { get; set; }

        public virtual p_Product Product1 { get; set; }
    }
}
