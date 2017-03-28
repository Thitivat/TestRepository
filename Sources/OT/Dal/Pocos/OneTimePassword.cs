namespace BND.Services.Security.OTP.Dal.Pocos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("otp.OneTimePassword")]
    public sealed class OneTimePassword
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OneTimePassword()
        {
            Attempt = new HashSet<Attempt>();
        }

        [Key]
        public string OtpId { get; set; }

        [Required]
        public Guid AccountId { get; set; }

        [Required]
        [StringLength(128)]
        public string Suid { get; set; }

        [Required]
        [StringLength(16)]
        public string ChannelType { get; set; }

        [Required]
        [StringLength(64)]
        public string ChannelSender { get; set; }

        [Required]
        [StringLength(256)]
        public string ChannelAddress { get; set; }

        [Required]
        public string ChannelMessage { get; set; }

        public DateTime ExpiryDate { get; set; }

        public string Payload { get; set; }

        [StringLength(8)]
        public string RefCode { get; set; }

        [StringLength(8)]
        public string Code { get; set; }

        [Required]
        [StringLength(16)]
        public string Status { get; set; }

        public Account Account { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Attempt> Attempt { get; set; }

        public EnumChannelType EnumChannelType { get; set; }

        public EnumStatus EnumStatus { get; set; }
    }
}
