namespace BND.Websites.BackOffice.SanctionListsManagement.Entities
{
    /// <summary>
    /// Represents contact information.
    /// </summary>
    public class ContactInfo
    {
        /// <summary>
        /// ContactInfo identifier.
        /// </summary>
        public int ContactInfoId { get; set; }

        /// <summary>
        /// ContactInfo identifier from source (while importing from external source). 
        /// If it is not provided then it is set to be same as ContactInfo Id.
        /// </summary>
        public int? OriginalContactInfoId { get; set; }

        /// <summary>
        /// Value of the contactInfo.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Type of the contactInfo.
        /// </summary>
        public ContactInfoType ContactInfoType { get; set; }

        /// <summary>
        /// Regulation that includes contactInfo.
        /// </summary>
        public Regulation Regulation { get; set; }

        /// <summary>
        /// <see cref="Entities.Remark"/>.
        /// </summary>
        public Remark Remark { get; set; }

        /// <summary>
        /// Id of the entity ContactInfo belongs to.
        /// </summary>
        public int EntityId { get; set; }
    }
}
