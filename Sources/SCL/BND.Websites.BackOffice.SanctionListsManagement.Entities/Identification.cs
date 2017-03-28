using System;

namespace BND.Websites.BackOffice.SanctionListsManagement.Entities
{
    /// <summary>
    /// Represents Identifications.
    /// </summary>
    public class Identification
    {
        /// <summary>
        /// Identification id.
        /// </summary>
        public int IdentificationId { get; set; }

        /// <summary>
        /// Identification identifier from source (while importing from external source). 
        /// If it is not provided then it is set to be same as Identification Id.
        /// </summary>
        public int? OriginalIdentificationId { get; set; }

        /// <summary>
        /// Identification document number.
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// City of issue of the identification document.
        /// </summary>
        public string IssueCity { get; set; }

        /// <summary>
        /// Date of issue of the identification document.
        /// </summary>
        public DateTime? IssueDate { get; set; }

        /// <summary>
        /// Country of issue of the identification document.
        /// </summary>
        public Country IssueCountry { get; set; }

        /// <summary>
        /// Type of Identification document. See <see cref="Entities.IdentificationType"/>
        /// </summary>
        public IdentificationType IdentificationType { get; set; }

        /// <summary>
        /// <see cref="Entities.Regulation"/> that includes identification.
        /// </summary>
        public Regulation Regulation { get; set; }

        /// <summary>
        /// <see cref="Entities.Remark"/>.
        /// </summary>
        public Remark Remark { get; set; }

        /// <summary>
        /// Id of <see cref="Entity"/> that identification belongs to.
        /// </summary>
        public int EntityId { get; set; }

        /// <summary>
        /// Identification document expiry date.
        /// </summary>
        public DateTime? ExpiryDate { get; set; }
    }
}
