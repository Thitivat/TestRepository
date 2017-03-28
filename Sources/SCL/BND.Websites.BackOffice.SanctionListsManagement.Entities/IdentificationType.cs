namespace BND.Websites.BackOffice.SanctionListsManagement.Entities
{
    /// <summary>
    /// Represents types of <see cref="Identification"/> documents.
    /// </summary>
    public class IdentificationType
    {
        /// <summary>
        /// IdentificationType id.
        /// </summary>
        public int IdentificationTypeId { get; set; }

        /// <summary>
        /// Type name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Type description.
        /// </summary>
        public string Description { get; set; }
    }
}
