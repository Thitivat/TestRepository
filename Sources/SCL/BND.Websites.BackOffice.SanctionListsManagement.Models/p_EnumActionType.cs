using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BND.Websites.BackOffice.SanctionListsManagement.Models
{
    /// <summary>
    /// Class p_EnumActionType is a poco entity representing sl.EnumActionTypes table in sanction lists database.
    /// </summary>
    [Table("sl.EnumActionTypes")]
    public partial class p_EnumActionType
    {
        public p_EnumActionType()
        {
            Logs = new HashSet<p_Log>();
        }

        /// <summary>
        /// Gets or sets the action type identifier.
        /// </summary>
        /// <value>The action type identifier.</value>
        [Key]
        public int ActionTypeId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [Column(TypeName = "text")]
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the logs.
        /// </summary>
        /// <value>The logs.</value>
        public virtual ICollection<p_Log> Logs { get; set; }
    }
}
