using BND.Services.Security.OTP.Dal.Attributes;

namespace BND.Services.Security.OTP.Dal.Pocos
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("otp.Log")]
    public class Log
    {
        public int LogId { get; set; }

        public byte Prival { get; set; }

        public byte Version { get; set; }

        [SqlColumnType("datetime2")]
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
