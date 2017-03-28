using System;

namespace BND.Websites.BackOffice.SanctionListsManagement.Entities
{
    /// <summary>
    /// Represents system log.
    /// </summary>
    public class Log
    {
        /// <summary>
        /// Log id.
        /// </summary>
        public int LogId { get; set; }

        /// <summary>
        /// Date of saving log.
        /// </summary>
        public DateTime LogDate { get; set; }

        /// <summary>
        /// Name of user who generated log.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Description of the log action.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Type of action. See <see cref="Entities.ActionType"/>.
        /// </summary>
        public ActionType ActionType { get; set; }

        /// <summary>
        /// Sanction list type.
        /// </summary>
        public ListType ListType { get; set; }
    }
}
