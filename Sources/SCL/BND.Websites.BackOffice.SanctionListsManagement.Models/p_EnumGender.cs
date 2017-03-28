using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BND.Websites.BackOffice.SanctionListsManagement.Models
{
    /// <summary>
    /// Class p_EnumGender is a poco entity representing sl.EnumGenders table in sanction lists database.
    /// </summary>
    [Table("sl.EnumGenders")]
    public partial class p_EnumGender
    {
        public p_EnumGender()
        {
            NameAliases = new HashSet<p_NameAlias>();
        }

        /// <summary>
        /// Gets or sets the gender identifier.
        /// </summary>
        /// <value>The gender identifier.</value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GenderId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name aliases.
        /// </summary>
        /// <value>The name aliases.</value>
        public virtual ICollection<p_NameAlias> NameAliases { get; set; }
    }
}
