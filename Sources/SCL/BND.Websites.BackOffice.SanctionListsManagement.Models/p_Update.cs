using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BND.Websites.BackOffice.SanctionListsManagement.Models
{
    /// <summary>
    /// Class p_Update is a poco entity representing sl.Updates table in sanction lists database.
    /// </summary>
    [Table("sl.Updates")]
    public partial class p_Update
    {
        /// <summary>
        /// Gets or sets the update identifier.
        /// </summary>
        /// <value>The update identifier.</value>
        [Key]
        public int UpdateId { get; set; }

        /// <summary>
        /// Gets or sets the updated date.
        /// </summary>
        /// <value>The updated date.</value>
        public DateTime UpdatedDate { get; set; }

        /// <summary>
        /// Gets or sets the public date.
        /// </summary>
        /// <value>The public date.</value>
        public DateTime PublicDate { get; set; }

        /// <summary>
        /// Gets or sets the list type identifier.
        /// </summary>
        /// <value>The list type identifier.</value>
        public int ListTypeId { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        [Required]
        [StringLength(96)]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the list archive identifier.
        /// </summary>
        /// <value>The list archive identifier.</value>
        public int? ListArchiveId { get; set; }

        /// <summary>
        /// Gets or sets the type of the enum list.
        /// </summary>
        /// <value>The type of the enum list.</value>
        public virtual p_EnumListType EnumListType { get; set; }

        /// <summary>
        /// Gets or sets the list archive.
        /// </summary>
        /// <value>The list archive.</value>
        [NotMapped]
        public virtual p_ListArchive ListArchive { get; set; }
    }
}
