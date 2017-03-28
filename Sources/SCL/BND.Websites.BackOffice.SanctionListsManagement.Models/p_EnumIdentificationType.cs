using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BND.Websites.BackOffice.SanctionListsManagement.Models
{
    /// <summary>
    /// Class p_EnumIdentificationType is a poco entity representing sl.EnumIdentificationTypes table in sanction lists database.
    /// </summary>
    [Table("sl.EnumIdentificationTypes")]
    public partial class p_EnumIdentificationType
    {
        public p_EnumIdentificationType()
        {
            Identifications = new HashSet<p_Identification>();
        }

        /// <summary>
        /// Gets or sets the identification type identifier.
        /// </summary>
        /// <value>The identification type identifier.</value>
        [Key]
        public int IdentificationTypeId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [Column(TypeName = "text")]
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the identifications.
        /// </summary>
        /// <value>The identifications.</value>
        public virtual ICollection<p_Identification> Identifications { get; set; }
    }
}
