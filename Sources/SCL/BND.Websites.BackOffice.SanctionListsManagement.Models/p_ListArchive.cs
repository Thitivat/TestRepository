using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BND.Websites.BackOffice.SanctionListsManagement.Models
{
    /// <summary>
    /// Class p_ListArchive is a poco entity representing sl.ListArchive table in sanction lists database.
    /// </summary>
    [Table("sl.ListArchive")]
    public partial class p_ListArchive
    {
        public p_ListArchive()
        {
            Entities = new HashSet<p_Entity>();
            Updates = new HashSet<p_Update>();
        }

        /// <summary>
        /// Gets or sets the list archive identifier.
        /// </summary>
        /// <value>The list archive identifier.</value>
        [Key]
        public int ListArchiveId { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>The date.</value>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the file.
        /// </summary>
        /// <value>The file.</value>
        [Required]
        public byte[] File { get; set; }

        /// <summary>
        /// Gets or sets the entities.
        /// </summary>
        /// <value>The entities.</value>
        public virtual ICollection<p_Entity> Entities { get; set; }

        /// <summary>
        /// Gets or sets the updates.
        /// </summary>
        /// <value>The updates.</value>
        public virtual ICollection<p_Update> Updates { get; set; }
    }
}
