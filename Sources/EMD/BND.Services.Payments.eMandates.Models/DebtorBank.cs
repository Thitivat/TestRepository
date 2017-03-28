namespace BND.Services.Payments.eMandates.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("emandates.DebtorBank")]
    public partial class DebtorBank
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DirectoryID { get; set; }

        [Key]
        [Column(Order = 1)]
        public string DebtorBankCountry { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(11)]
        public string DebtorBankId { get; set; }

        [StringLength(35)]
        public string DebtorBankName { get; set; }

        public virtual Directory Directory { get; set; }
    }
}
