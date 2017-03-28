using BND.Services.Security.OTP.Dal.Attributes;

namespace BND.Services.Security.OTP.Dal.Pocos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("otp.Account")]
    public sealed class Account
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Account()
        {
            OneTimePassword = new HashSet<OneTimePassword>();
        }

        public Guid AccountId { get; set; }

        [Required]
        [StringLength(128)]
        public string ApiKey { get; set; }

        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        [Required]
        [StringLength(64)]
        public string IpAddress { get; set; }

        public bool IsActive { get; set; }

        [SqlColumnType("text")]
        public string Description { get; set; }

        [Required]
        [StringLength(16)]
        public string Salt { get; set; }

        [Required]
        [StringLength(256)]
        public string Email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<OneTimePassword> OneTimePassword { get; set; }
    }
}
