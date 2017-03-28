using System;

namespace BND.Websites.BackOffice.SanctionListsManagement.Entities
{
    /// <summary>
    /// Represents archive of imported sanction lists.
    /// </summary>
    public class ListArchive
    {
        /// <summary>
        /// ListArchive Id.
        /// </summary>
        public int ListArchiveId { get; set; }

        /// <summary>
        /// Date of saving sanctions list.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// File containing sanctions list.
        /// </summary>
        public byte[] File { get; set; }
    }
}
