namespace BND.Websites.BackOffice.SanctionListsManagement.Entities.EuEntities
{
    /// <summary>
    /// Represents information about births from xml.
    /// </summary>
    public class EuBirth
    {
        /// <summary>
        /// Birth Id.
        /// </summary>
        public int BirthId { get; set; }

        /// <summary>
        /// The Entity Identifier of this birth.
        /// </summary>
        public int EntityId { get; set; }

        /// <summary>
        /// Day of birth.
        /// </summary>
        public int? Day { get; set; }

        /// <summary>
        /// Month of birth.
        /// </summary>
        public int? Month { get; set; }

        /// <summary>
        /// Year of birth.
        /// </summary>
        public int? Year { get; set; }

        /// <summary>
        /// Place of birth.
        /// </summary>
        public string Place { get; set; }

        /// <summary>
        /// Country, part of the address information.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Regulation that includes birth information.
        /// </summary>
        public EuRegulation Regulation { get; set; }

        /// <summary>
        /// Remark of the birth.
        /// </summary>
        public string Remark { get; set; }
    }
}
