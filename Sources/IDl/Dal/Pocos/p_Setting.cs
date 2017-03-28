namespace BND.Services.Payments.iDeal.Dal.Pocos
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Class p_Setting representing Setting table in iDeal database.
    /// </summary>
    [Table("ideal.Setting")]
    public partial class p_Setting
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        [Key]
        [StringLength(32)]
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        [Required]
        [StringLength(32)]
        public string Value { get; set; }
    }
}
