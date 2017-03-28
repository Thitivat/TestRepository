using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BND.Websites.BackOffice.SanctionListsManagement.Models
{
    /// <summary>
    /// Class p_EnumStatus is a poco entity representing sl.EnumStatuses table in sanction lists database.
    /// </summary>
    [Table("sl.EnumStatuses")]
    public partial class p_EnumStatus
    {
        public p_EnumStatus()
        {
            Entities = new HashSet<p_Entity>();
        }

        /// <summary>
        /// Gets or sets the status identifier.
        /// </summary>
        /// <value>The status identifier.</value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StatusId { get; set; }

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
