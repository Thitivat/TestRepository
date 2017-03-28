namespace BND.Services.Payments.eMandates.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("emandates.EMandate")]
    public partial class EMandate
    {
        [Key]        
        public int AcceptanceReportID { get; set; }

        public bool AcceptedResult { get; set; }

        [Required]
        [StringLength(255)]
        public string CreditorAddressLine { get; set; }

        [Required]
        [StringLength(100)]
        public string CreditorCountry { get; set; }

        [Required]
        [StringLength(35)]
        public string CreditorID { get; set; }

        [Required]
        [StringLength(70)]
        public string CreditorName { get; set; }

        [StringLength(70)]
        public string CreditorTradeName { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(70)]
        public string DebtorAccountName { get; set; }

        [Required]
        [StringLength(11)]
        public string DebtorBankID { get; set; }

        [Required]
        [StringLength(34)]
        public string DebtorIban { get; set; }

        [StringLength(35)]
        public string DebtorReference { get; set; }

        [Required]
        [StringLength(70)]
        public string DebtorSignerName { get; set; }

        [StringLength(70)]
        public string EMandateReason { get; set; }

        [Required]
        [StringLength(4)]
        public string LocalInstrumentCode { get; set; }

        [Required]
        [StringLength(16)]
        public string MandateRequestID { get; set; }

        public decimal? MaxAmount { get; set; }

        [Required]
        [StringLength(35)]
        public string MessageID { get; set; }

        [Required]
        [StringLength(9)]
        public string MessageNameID { get; set; }

        [Required]
        [StringLength(35)]
        public string OriginalMandateID { get; set; }

        [Required]
        [StringLength(16)]
        public string OriginalMessageID { get; set; }

        public int? RawMessageID { get; set; }

        [Required]
        [StringLength(4)]
        public string SchemeName { get; set; }

        [Required]
        [StringLength(4)]
        public string SequenceType { get; set; }

        [Required]
        [StringLength(4)]
        public string ServiceLevelCode { get; set; }

        [Required]
        [StringLength(128)]
        public string ValidationReference { get; set; }

        public int TransactionStatusHistoryID { get; set; }

        public long? FrequencyCount { get; set; }

        [StringLength(4)]
        public string FrequencyPeriod { get; set; }

        [StringLength(70)]
        public string AmendmentReason { get; set; }

        public virtual EnumSequenceType EnumSequenceType { get; set; }

        public virtual RawMessage RawMessage { get; set; }

        public virtual TransactionStatusHistory TransactionStatusHistory { get; set; }
    }
}
