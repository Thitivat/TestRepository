namespace BND.Services.Payments.iDeal.Dal.Pocos
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Class p_Setting representing Issuer table in iDeal database.
    /// </summary>
    [Table("ideal.Issuer")]
    public partial class p_Issuer
    {
        /// <summary>
        /// Gets or sets the acquirer identifier.
        /// </summary>
        /// <value>The acquirer identifier.</value>
        [Key]
        [Column(Order = 0)]
        [StringLength(4)]
        public string AcquirerID { get; set; }

        /// <summary>
        /// Gets or sets the country names.
        /// </summary>
        /// <value>The country names.</value>
        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string CountryNames { get; set; }

        /// <summary>
        /// Gets or sets the issuer identifier.
        /// </summary>
        /// <value>The issuer identifier.</value>
        [Key]
        [Column(Order = 2)]
        [StringLength(11)]
        public string IssuerID { get; set; }

        /// <summary>
        /// Gets or sets the name of the issuer.
        /// </summary>
        /// <value>The name of the issuer.</value>
        [StringLength(35)]
        public string IssuerName { get; set; }

        /// <summary>
        /// Gets or sets the directory.
        /// </summary>
        /// <value>The directory.</value>
        public virtual p_Directory Directory { get; set; }
    }
}
