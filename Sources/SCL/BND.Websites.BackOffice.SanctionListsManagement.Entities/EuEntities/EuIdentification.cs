using System;

namespace BND.Websites.BackOffice.SanctionListsManagement.Entities.EuEntities
{
    /// <summary>
    /// Represents information about identification from xml.
    /// </summary>
    public class EuIdentification
    {
        /// <summary>
        /// Identification Id.
        /// </summary>
        public int IdentificationId { get; set; }

        /// <summary>
        /// The Entity Identifier of this identification.
        /// </summary>
        public int EntityId { get; set; }

        /// <summary>
        /// The document number of the identification.
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// The city of the identification.
        /// </summary>
        public string IssueCity { get; set; }

        /// <summary>
        /// The date of the identification.
        /// </summary>
        public DateTime? IssueDate { get; set; }

        /// <summary>
        /// The country of the identification.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Type of the identification.
        /// </summary>
        public string IdentificationType { get; set; }

        /// <summary>
        /// Regulation that includes information about identification.
        /// </summary>
        public EuRegulation Regulation { get; set; }

        /// <summary>
        /// Remark of the identification.
        /// </summary>
        public string Remark { get; set; }
    }
}
