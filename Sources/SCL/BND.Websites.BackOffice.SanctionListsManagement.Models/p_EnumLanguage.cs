using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BND.Websites.BackOffice.SanctionListsManagement.Models
{
    /// <summary>
    /// Class p_EnumLanguage is a poco entity representing sl.EnumLanguages table in sanction lists database.
    /// </summary>
    [Table("sl.EnumLanguages")]
    public partial class p_EnumLanguage
    {
        public p_EnumLanguage()
        {
            NameAliases = new HashSet<p_NameAlias>();
        }

        /// <summary>
        /// Gets or sets the iso3 standard code.
        /// </summary>
        /// <value>The iso3 standard code.</value>
        [Key]
        [StringLength(3)]
        public string Iso3 { get; set; }

        /// <summary>
        /// Gets or sets the iso2 standard code.
        /// </summary>
        /// <value>The iso2 standard code.</value>
        [Required]
        [StringLength(2)]
        public string Iso2 { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name aliases.
        /// </summary>
        /// <value>The name aliases.</value>
        public virtual ICollection<p_NameAlias> NameAliases { get; set; }
    }
}
