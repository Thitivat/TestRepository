namespace BND.Services.Payments.eMandates.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("emandates.Log")]
    public partial class Log
    {
        [Key]
        public int LogID { get; set; }

        public byte? Prival { get; set; }

        public byte? Version { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Timestamp { get; set; }

        [StringLength(255)]
        public string Hostname { get; set; }

        [StringLength(48)]
        public string AppName { get; set; }

        [StringLength(128)]
        public string ProcId { get; set; }

        [StringLength(32)]
        public string MsgId { get; set; }

        public string StructuredData { get; set; }

        public string Msg { get; set; }
    }
}
