namespace BND.Services.Payments.eMandates.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("emandates.TransactionStatusHistory")]
    public partial class TransactionStatusHistory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TransactionStatusHistory()
        {
            Emandates = new HashSet<EMandate>();
        }
        [Key]
        public int TransactionStatusHistoryID { get; set; }

        [Required]
        [StringLength(16)]
        public string TransactionID { get; set; }

        [Required]
        [StringLength(9)]
        public string Status { get; set; }

        public DateTime? StatusDateTimestamp { get; set; }

        public int? RawMessageID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMandate> Emandates { get; set; }

        public virtual RawMessage RawMessage { get; set; }

        public virtual Transaction Transaction { get; set; }
    }
}
