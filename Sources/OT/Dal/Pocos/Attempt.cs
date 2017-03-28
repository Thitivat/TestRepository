using BND.Services.Security.OTP.Dal.Attributes;

namespace BND.Services.Security.OTP.Dal.Pocos
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("otp.Attempt")]
    public class Attempt
    {
        public int AttemptId { get; set; }

        [Required]
        [StringLength(128)]
        public string OtpId { get; set; }

        [SqlColumnType("datetime2")]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(64)]
        public string IpAddress { get; set; }

        [Required]
        [StringLength(64)]
        public string UserAgent { get; set; }

        public virtual OneTimePassword OneTimePassword { get; set; }
    }
}
