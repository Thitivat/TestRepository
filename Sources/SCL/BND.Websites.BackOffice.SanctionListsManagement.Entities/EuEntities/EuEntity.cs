using System.Collections.Generic;

namespace BND.Websites.BackOffice.SanctionListsManagement.Entities.EuEntities
{
    /// <summary>
    /// Represents entity information of xml.
    /// </summary>
    public class EuEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EuEntity"/> class.
        /// </summary>
        public EuEntity()
        {
            Addresses = new List<EuAddress>();
            Births = new List<EuBirth>();
            Citizens = new List<EuCitizenship>();
            ContactInfo = new List<EuContactInfo>();
            Identifications = new List<EuIdentification>();
            NameAliases = new List<EuNameAlias>();
        }

        /// <summary>
        /// Entity Id.
        /// </summary>
        public int EntityId { get; set; }

        /// <summary>
        /// Regulation that includes information about entity.
        /// </summary>
        public EuRegulation Regulation { get; set; }

        /// <summary>
        /// Remark associated with this entity.
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// Type of the entity.
        /// </summary>
        public string SubjectType { get; set; }

        public List<EuAddress> Addresses { get; set; }

        /// <summary>
        /// List of <see cref="EuBirth"/> assigned to the entity.
        /// </summary>
        public List<EuBirth> Births { get; set; }

        /// <summary>
        /// List of <see cref="EuCitizenship"/> assigned to the entity.
        /// </summary>
        public List<EuCitizenship> Citizens { get; set; }

        /// <summary>
        /// List of <see cref="EuContactInfo"/> assigned to the entity.
        /// </summary>
        public List<EuContactInfo> ContactInfo { get; set; }

        /// <summary>
        /// List of <see cref="EuIdentification"/> assigned to the entity.
        /// </summary>
        public List<EuIdentification> Identifications { get; set; }

        /// <summary>
        /// List of <see cref="EuNameAlias"/> assigned to the entity.
        /// </summary>
        public List<EuNameAlias> NameAliases{ get; set; }
    }
}
