namespace BND.Websites.BackOffice.SanctionListsManagement.Entities
{
    /// <summary>
    /// Represents information about births.
    /// </summary>
    public class Birth
    {
        /// <summary>
        /// Birth Id.
        /// </summary>
        public int BirthId { get; set; }

        /// <summary>
        /// Birth identifier from source (while importing from external source). 
        /// If it is not provided then it is set to be same as Birth Id.
        /// </summary>
        public int? OriginalBirthId { get; set; }

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
        /// Country of birth.
        /// </summary>
        public Country Country { get; set; }

        /// <summary>
        /// Regulation that includes birth information.
        /// </summary>
        public Regulation Regulation { get; set; }

        /// <summary>
        /// <see cref="Entities.Remark"/>.
        /// </summary>
        public Remark Remark { get; set; }

        /// <summary>
        /// Id of the Entity that Brith belongs to.
        /// </summary>
        public int EntityId { get; set; }
    }
}
