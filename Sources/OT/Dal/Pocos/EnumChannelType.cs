namespace BND.Services.Security.OTP.Dal.Pocos
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("otp.EnumChannelType")]
    public sealed class EnumChannelType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EnumChannelType()
        {
            OneTimePassword = new HashSet<OneTimePassword>();
        }

        [Key]
        [StringLength(16)]
        public string ChannelType { get; set; }

        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<OneTimePassword> OneTimePassword { get; set; }
    }
}
