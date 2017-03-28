using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BND.Websites.BackOffice.SanctionListsManagement.Models
{
    /// <summary>
    /// Class p_EnumListType is a poco entity representing sl.EnumListTypes table in sanction lists database.
    /// </summary>
    [Table("sl.EnumListTypes")]
    public partial class p_EnumListType
    {
        public p_EnumListType()
        {
            Entities = new HashSet<p_Entity>();
            Logs = new HashSet<p_Log>();
            Regulations = new HashSet<p_Regulation>();
            Settings = new HashSet<p_Setting>();
            Updates = new HashSet<p_Update>();
        }

        /// <summary>
        /// Gets or sets the list type identifier.
        /// </summary>
        /// <value>The list type identifier.</value>
        [Key]
        public int ListTypeId { get; set; }

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
        /// Gets or sets the entities.
        /// </summary>
        /// <value>The entities.</value>
        public virtual ICollection<p_Entity> Entities { get; set; }

        /// <summary>
        /// Gets or sets the logs.
        /// </summary>
        /// <value>The logs.</value>
        public virtual ICollection<p_Log> Logs { get; set; }

        /// <summary>
        /// Gets or sets the regulations.
        /// </summary>
        /// <value>The regulations.</value>
        public virtual ICollection<p_Regulation> Regulations { get; set; }

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>The settings.</value>
        public virtual ICollection<p_Setting> Settings { get; set; }

        /// <summary>
        /// Gets or sets the updates.
        /// </summary>
        /// <value>The updates.</value>
        public virtual ICollection<p_Update> Updates { get; set; }
    }
}
