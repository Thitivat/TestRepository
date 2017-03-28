namespace BND.Services.Security.OTP.Dal.Pocos
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("otp.Setting")]
    public class Setting
    {
        [Key]
        [StringLength(16)]
        public string Key { get; set; }

        [Required]
        [StringLength(32)]
        public string Value { get; set; }
    }
}
