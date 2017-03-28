namespace BND.Websites.BackOffice.SanctionListsManagement.Entities
{
    /// <summary>
    /// Represents citizenship information.
    /// </summary>
    public class Citizenship
    {
        /// <summary>
        /// Id of citizenship.
        /// </summary>
        public int CitizenshipId { get; set; }

        /// <summary>
        /// Citizenship identifier from source (while importing from external source). 
        /// If it is not provided then it is set to be same as Citizenship Id.
        /// </summary>
        public int? OriginalCitizenshipId { get; set; }

        /// <summary>
        /// Country of citizenship.
        /// </summary>
        public Country Country { get; set; }

        /// <summary>
        /// Regulation that includes information about citizenship.
        /// </summary>
        public Regulation Regulation { get; set; }

        /// <summary>
        /// <see cref="Entities.Remark"/>.
        /// </summary>
        public Remark Remark { get; set; }

        /// <summary>
        /// Id of the entity that Citizenship belongs to.
        /// </summary>
        public int EntityId { get; set; }
    }
}
