namespace BND.Services.Payments.eMandates.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("emandates.Transaction")]
    public partial class Transaction
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Transaction()
        {
            TransactionStatusHistories = new HashSet<TransactionStatusHistory>();
        }
        [Key]
        [StringLength(16)]
        public string TransactionID { get; set; }

        [Required]
        [StringLength(11)]
        public string DebtorBankID { get; set; }

        [StringLength(35)]
        public string DebtorReference { get; set; }

        [Required]
        [StringLength(35)]
        public string EMandateID { get; set; }

        [StringLength(70)]
        public string EMandateReason { get; set; }

        [Required]
        [StringLength(40)]
        public string EntranceCode { get; set; }

        public long? ExpirationPeriod { get; set; }

        [Required]
        [StringLength(2)]
        public string Language { get; set; }

        public decimal? MaxAmount { get; set; }

        [Required]
        [StringLength(35)]
        public string MessageID { get; set; }

        [StringLength(35)]
        public string PurchaseID { get; set; }

        [Required]
        [StringLength(11)]
        public string OriginalDebtorBankID { get; set; }

        [Required]
        [StringLength(34)]
        public string OriginalIban { get; set; }

        [Required]
        [StringLength(50)]
        public string TransactionType { get; set; }

        [Required]
        [StringLength(4)]
        public string SequenceType { get; set; }

        public DateTime TransactionCreateDateTimestamp { get; set; }

        [Required]
        [StringLength(512)]
        public string IssuerAuthenticationUrl { get; set; }

        [Required]
        [StringLength(512)]
        public string MerchantReturnUrl { get; set; }

        public int? RawMessageID { get; set; }

        public int TodayAttempts { get; set; }

        public DateTime? LatestAttemptsDateTimestamp { get; set; }

        public bool IsSystemFail { get; set; }

        public virtual EnumSequenceType EnumSequenceType { get; set; }

        public virtual EnumTransactionType EnumTransactionType { get; set; }

        public virtual RawMessage RawMessage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionStatusHistory> TransactionStatusHistories { get; set; }
    }
}
