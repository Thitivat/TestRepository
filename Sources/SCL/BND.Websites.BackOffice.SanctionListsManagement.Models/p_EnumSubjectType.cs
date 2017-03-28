using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BND.Websites.BackOffice.SanctionListsManagement.Models
{
    /// <summary>
    /// Class p_EnumSubjectType is a poco entity representing sl.EnumSubjectTypes table in sanction lists database.
    /// </summary>
    [Table("sl.EnumSubjectTypes")]
    public partial class p_EnumSubjectType
    {
        public p_EnumSubjectType()
        {
            Entities = new HashSet<p_Entity>();
        }

        /// <summary>
        /// Gets or sets the subject type identifier.
        /// </summary>
        /// <value>The subject type identifier.</value>
        [Key]
        public int SubjectTypeId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the entities.
        /// </summary>
        /// <value>The entities.</value>
        public virtual ICollection<p_Entity> Entities { get; set; }
    }
}
