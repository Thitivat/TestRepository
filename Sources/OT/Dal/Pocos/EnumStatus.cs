using BND.Services.Security.OTP.Dal.Attributes;

namespace BND.Services.Security.OTP.Dal.Pocos
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("otp.EnumStatus")]
    public sealed class EnumStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EnumStatus()
        {
            OneTimePassword = new HashSet<OneTimePassword>();
        }

        [Key]
        [StringLength(16)]
        public string Status { get; set; }

        [SqlColumnType("text")]
        public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<OneTimePassword> OneTimePassword { get; set; }
    }
}
