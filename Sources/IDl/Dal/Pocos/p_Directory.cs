namespace BND.Services.Payments.iDeal.Dal.Pocos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Class p_Setting representing Directory table in iDeal database.
    /// </summary>
    [Table("ideal.Directory")]
    public partial class p_Directory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="p_Directory"/> class.
        /// </summary>
        public p_Directory()
        {
            Issuers = new HashSet<p_Issuer>();
        }

        /// <summary>
        /// Gets or sets the acquirer identifier.
        /// </summary>
        /// <value>The acquirer identifier.</value>
        [Key]
        [StringLength(4)]
        public string AcquirerID { get; set; }

        /// <summary>
        /// Gets or sets the directory date timestamp.
        /// </summary>
        /// <value>The directory date timestamp.</value>
        public DateTime DirectoryDateTimestamp { get; set; }

        /// <summary>
        /// Gets or sets the last directory request date timestamp.
        /// </summary>
        /// <value>The last directory request date timestamp.</value>
        public DateTime LastDirectoryRequestDateTimestamp { get; set; }

        /// <summary>
        /// Gets or sets the issuers.
        /// </summary>
        /// <value>The issuers.</value>
        public virtual ICollection<p_Issuer> Issuers { get; set; }
    }
}
