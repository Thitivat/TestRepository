using System;

namespace BND.Websites.BackOffice.SanctionListsManagement.Entities
{
    /// <summary>
    /// Contains information about SancionList's update.
    /// </summary>
    public class Update
    {
        /// <summary>
        /// Update Id.
        /// </summary>
        public int UpdateId { get; set; }

        /// <summary>
        /// Date of update.
        /// </summary>
        public DateTime UpdatedDate { get; set; }

        /// <summary>
        /// Date of publication.
        /// </summary>
        public DateTime PublicDate { get; set; }
        
        /// <summary>
        /// Name of the user who performed update.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// <see cref="Entities.ListType"/> that was updated.
        /// </summary>
        public ListType ListType { get; set; }

        /// <summary>
        /// Id of <see cref="ListArchive"/> that contains original import file.
        /// </summary>
        public int? ListArchiveId { get; set; }
    }
}
