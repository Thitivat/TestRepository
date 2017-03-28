namespace BND.Websites.BackOffice.SanctionListsManagement.Entities.EuEntities
{
    /// <summary>
    /// Represents citizenship information of xml.
    /// </summary>
    public class EuCitizenship
    {
        /// <summary>
        /// Id of citizenship.
        /// </summary>
        public int CitizenshipId { get; set; }

        /// <summary>
        /// The Entity Identifier of this citizenship.
        /// </summary>
        public int EntityId { get; set; }

        /// <summary>
        /// Country of citizenship.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Regulation that includes information about citizenship.
        /// </summary>
        public EuRegulation Regulation { get; set; }

        /// <summary>
        /// Remark of the citizenship.
        /// </summary>
        public string Remark { get; set; }
    }
}
