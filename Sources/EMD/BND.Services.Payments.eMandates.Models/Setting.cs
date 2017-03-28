namespace BND.Services.Payments.eMandates.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("emandates.Setting")]
    public partial class Setting
    {
        [Key]
        [StringLength(32)]
        public string Key { get; set; }

        [Required]
        [StringLength(2048)]
        public string Value { get; set; }
    }
}
