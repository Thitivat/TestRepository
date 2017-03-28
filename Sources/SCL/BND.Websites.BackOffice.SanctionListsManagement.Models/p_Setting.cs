using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BND.Websites.BackOffice.SanctionListsManagement.Models
{
    /// <summary>
    /// Class p_Setting is a poco entity representing sl.Settings table in sanction lists database.
    /// </summary>
    [Table("sl.Settings")]
    public partial class p_Setting
    {
        /// <summary>
        /// Gets or sets the setting identifier.
        /// </summary>
        /// <value>The setting identifier.</value>
        [Key]
        public int SettingId { get; set; }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        [Required]
        [StringLength(50)]
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        [Column(TypeName = "text")]
        [Required]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the list type identifier.
        /// </summary>
        /// <value>The list type identifier.</value>
        public int? ListTypeId { get; set; }

        /// <summary>
        /// Gets or sets the type of the enum list.
        /// </summary>
        /// <value>The type of the enum list.</value>
        public virtual p_EnumListType EnumListType { get; set; }
    }
}
