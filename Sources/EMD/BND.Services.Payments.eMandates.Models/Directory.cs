namespace BND.Services.Payments.eMandates.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("emandates.Directory")]
    public partial class Directory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Directory()
        {
            DebtorBanks = new HashSet<DebtorBank>();
        }
        [Key]        
        public int DirectoryID { get; set; }

        public DateTime DirectoryDateTimestamp { get; set; }

        public DateTime LastDirectoryRequestDateTimestamp { get; set; }

        public int? RawMessageID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DebtorBank> DebtorBanks { get; set; }

        public virtual RawMessage RawMessage { get; set; }
    }
}
