using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BND.Websites.BackOffice.SanctionListsManagement.Models
{
    /// <summary>
    /// Class p_ContactInfo is a poco entity representing sl.ContactInfo table in sanction lists database.
    /// </summary>
    [Table("sl.ContactInfo")]
    public partial class p_ContactInfo
    {
        /// <summary>
        /// Gets or sets the contact information identifier.
        /// </summary>
        /// <value>The contact information identifier.</value>
        [Key]
        public int ContactInfoId { get; set; }

        /// <summary>
        /// Gets or sets the original contact information identifier.
        /// </summary>
        /// <value>The original contact information identifier.</value>
        public int? OriginalContactInfoId { get; set; }

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
        /// Gets or sets the contact information type identifier.
        /// </summary>
        /// <value>The contact information type identifier.</value>
        public int ContactInfoTypeId { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        [Required]
        [StringLength(256)]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the remark identifier.
        /// </summary>
        /// <value>The remark identifier.</value>
        public int? RemarkId { get; set; }

        /// <summary>
        /// Gets or sets the type of the enum contact information.
        /// </summary>
        /// <value>The type of the enum contact information.</value>
        public virtual p_EnumContactInfoType EnumContactInfoType { get; set; }

        /// <summary>
        /// Gets or sets the entity.
        /// </summary>
        /// <value>The entity.</value>
        public virtual p_Entity Entity { get; set; }

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
