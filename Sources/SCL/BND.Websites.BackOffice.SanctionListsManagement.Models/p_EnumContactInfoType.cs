using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BND.Websites.BackOffice.SanctionListsManagement.Models
{
    /// <summary>
    /// Class p_EnumContactInfoType is a poco entity representing sl.EnumContactInfoTypes table in sanction lists database.
    /// </summary>
    [Table("sl.EnumContactInfoTypes")]
    public partial class p_EnumContactInfoType
    {
        public p_EnumContactInfoType()
        {
            ContactInfo = new HashSet<p_ContactInfo>();
        }

        /// <summary>
        /// Gets or sets the contact information type identifier.
        /// </summary>
        /// <value>The contact information type identifier.</value>
        [Key]
        public int ContactInfoTypeId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the contact information.
        /// </summary>
        /// <value>The contact information.</value>
        public virtual ICollection<p_ContactInfo> ContactInfo { get; set; }
    }
}
