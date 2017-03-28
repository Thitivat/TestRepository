using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BND.Websites.BackOffice.SanctionListsManagement.Models
{
    /// <summary>
    /// Class p_Identification is a poco entity representing sl.Identifications table in sanction lists database.
    /// </summary>
    [Table("sl.Identifications")]
    public partial class p_Identification
    {
        /// <summary>
        /// Gets or sets the identification identifier.
        /// </summary>
        /// <value>The identification identifier.</value>
        [Key]
        public int IdentificationId { get; set; }

        /// <summary>
        /// Gets or sets the original identification identifier.
        /// </summary>
        /// <value>The original identification identifier.</value>
        public int? OriginalIdentificationId { get; set; }

        /// <summary>
        /// Gets or sets the entity identifier.
        /// </summary>
        /// <value>The entity identifier.</value>
        public int EntityId { get; set; }

        /// <summary>
        /// Gets or sets the regulation identifier.
        /// </summary>
        /// <value>The regulation identifier.</value>
        public int RegulationId { get; set; }

        /// <summary>
        /// Gets or sets the identification type identifier.
        /// </summary>
        /// <value>The identification type identifier.</value>
        public int IdentificationTypeId { get; set; }

        /// <summary>
        /// Gets or sets the document number.
        /// </summary>
        /// <value>The document number.</value>
        [Required]
        [StringLength(2048)]
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Gets or sets the issue city.
        /// </summary>
        /// <value>The issue city.</value>
        [StringLength(256)]
        public string IssueCity { get; set; }

        /// <summary>
        /// Gets or sets the issue country following iso3 standard.
        /// </summary>
        /// <value>The issue country following iso3 standard.</value>
        [StringLength(3)]
        public string IssueCountryIso3 { get; set; }

        /// <summary>
        /// Gets or sets the issue date.
        /// </summary>
        /// <value>The issue date.</value>
        [Column(TypeName = "date")]
        public DateTime? IssueDate { get; set; }

        /// <summary>
        /// Gets or sets the expiry date.
        /// </summary>
        /// <value>The expiry date.</value>
        [Column(TypeName = "date")]
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// Gets or sets the remark identifier.
        /// </summary>
        /// <value>The remark identifier.</value>
        public int? RemarkId { get; set; }

        /// <summary>
        /// Gets or sets the entity.
        /// </summary>
        /// <value>The entity.</value>
        public virtual p_Entity Entity { get; set; }

        /// <summary>
        /// Gets or sets the enum country.
        /// </summary>
        /// <value>The enum country.</value>
        public virtual p_EnumCountry EnumCountry { get; set; }

        /// <summary>
        /// Gets or sets the type of the enum identification.
        /// </summary>
        /// <value>The type of the enum identification.</value>
        public virtual p_EnumIdentificationType EnumIdentificationType { get; set; }

        /// <summary>
        /// Gets or sets the regulation.
        /// </summary>
        /// <value>The regulation.</value>
        public virtual p_Regulation Regulation { get; set; }

        /// <summary>
        /// Gets or sets the remark.
        /// </summary>
        /// <value>The remark.</value>
        public virtual p_Remark Remark { get; set; }
    }
}
