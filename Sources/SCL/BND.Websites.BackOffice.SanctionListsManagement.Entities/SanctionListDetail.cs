namespace BND.Websites.BackOffice.SanctionListsManagement.Entities
{
    /// <summary>
    /// Sanction list detailed information.
    /// </summary>
    public class SanctionListDetail
    {
        /// <summary>
        /// <see cref="Entities.ListType"/> that sanction list belongs to.
        /// </summary>
        public ListType ListType { get; set; }

        /// <summary>
        /// <see cref="Entities.Update"/> object with information about update.
        /// </summary>
        public Update Update { get; set; }

        /// <summary>
        /// Number of entities.
        /// </summary>
        public int EntitiesCount { get; set; }
    }
}
