using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BND.Websites.BackOffice.SanctionListsManagement.Models
{
    /// <summary>
    /// Class p_Log is a poco entity representing sl.Logs table in sanction lists database.
    /// </summary>
    [Table("sl.Logs")]
    public partial class p_Log
    {
        /// <summary>
        /// Gets or sets the log identifier.
        /// </summary>
        /// <value>The log identifier.</value>
        [Key]
        public int LogId { get; set; }

        /// <summary>
        /// Gets or sets the log date.
        /// </summary>
        /// <value>The log date.</value>
        public DateTime LogDate { get; set; }

        /// <summary>
        /// Gets or sets the list type identifier.
        /// </summary>
        /// <value>The list type identifier.</value>
        public int? ListTypeId { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        [Required]
        [StringLength(96)]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [Column(TypeName = "text")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the action type identifier.
        /// </summary>
        /// <value>The action type identifier.</value>
        public int ActionTypeId { get; set; }

        /// <summary>
        /// Gets or sets the type of the enum action.
        /// </summary>
        /// <value>The type of the enum action.</value>
        public virtual p_EnumActionType EnumActionType { get; set; }

        /// <summary>
        /// Gets or sets the type of the enum list.
        /// </summary>
        /// <value>The type of the enum list.</value>
        public virtual p_EnumListType EnumListType { get; set; }
    }
}
