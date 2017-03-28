using System.Collections.Generic;

namespace BND.Websites.BackOffice.SanctionListsManagement.Entities
{
    /// <summary>
    /// Represents entity inforation.
    /// </summary>
    public class Entity
    {
        /// <summary>
        /// Entity Id.
        /// </summary>
        public int EntityId { get; set; }

        /// <summary>
        /// Entity identifier from source (while importing from external source). 
        /// If it is not provided then it is set to be same as Entity Id.
        /// </summary>
        public int? OriginalEntityId { get; set; }

        /// <summary>
        /// An Id of archived file from which entity was imported.
        /// </summary>
        public int? ListArchiveId { get; set; }

        /// <summary>
        /// Type of the list entity belongs to.
        /// </summary>
        public ListType ListType { get; set; }

        /// <summary>
        /// Regulation that includes information about the entity.
        /// </summary>
        public Regulation Regulation { get; set; }

        /// <summary>
        /// <see cref="Entities.Remark"/>.
        /// </summary>
        public Remark Remark { get; set; }

        /// <summary>
        /// Status of the entity.
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// Type of the entity.
        /// </summary>
        public SubjectType SubjectType { get; set; }

        /// <summary>
        /// List of <see cref="Address"/> assigned to the entity.
        /// </summary>
        public List<Address> Addresses { get; set; }

        /// <summary>
        /// List of <see cref="Bank"/> assigned to the entity.
        /// </summary>
        public List<Bank> Banks { get; set; }

        /// <summary>
        /// List of <see cref="Birth"/> assigned to the entity.
        /// </summary>
        public List<Birth> Births { get; set; }

        /// <summary>
        /// List of <see cref="Citizenship"/> assigned to the entity.
        /// </summary>
        public List<Citizenship> Citizenships { get; set; }

        /// <summary>
        /// List of <see cref="Entities.ContactInfo"/> assigned to the entity.
        /// </summary>
        public List<ContactInfo> ContactInfo { get; set; }

        /// <summary>
        /// List of <see cref="Identification"/> assigned to the entity.
        /// </summary>
        public List<Identification> Identifications { get; set; }

        /// <summary>
        /// List of <see cref="NameAlias"/> assigned to the entity. 
        /// </summary>
        public List<NameAlias> NameAliases { get; set; }
    }
}
