using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BND.Websites.BackOffice.SanctionListsManagement.Models
{
    /// <summary>
    /// Class p_Entity is a poco entity representing sl.Entities table in sanction lists database.
    /// </summary>
    [Table("sl.Entities")]
    public partial class p_Entity
    {
        public p_Entity()
        {
            Addresses = new HashSet<p_Address>();
            Banks = new HashSet<p_Bank>();
            Births = new HashSet<p_Birth>();
            Citizenships = new HashSet<p_Citizenship>();
            ContactInfo = new HashSet<p_ContactInfo>();
            Identifications = new HashSet<p_Identification>();
            NameAliases = new HashSet<p_NameAlias>();
        }

        /// <summary>
        /// Gets or sets the entity identifier.
        /// </summary>
        /// <value>The entity identifier.</value>
        [Key]
        public int EntityId { get; set; }

        /// <summary>
        /// Gets or sets the original entity identifier.
        /// </summary>
        /// <value>The original entity identifier.</value>
        public int? OriginalEntityId { get; set; }

        /// <summary>
        /// Gets or sets the regulation identifier.
        /// </summary>
        /// <value>The regulation identifier.</value>
        public int RegulationId { get; set; }

        /// <summary>
        /// Gets or sets the subject type identifier.
        /// </summary>
        /// <value>The subject type identifier.</value>
        public int SubjectTypeId { get; set; }

        /// <summary>
        /// Gets or sets the status identifier.
        /// </summary>
        /// <value>The status identifier.</value>
        public int StatusId { get; set; }

        /// <summary>
        /// Gets or sets the remark identifier.
        /// </summary>
        /// <value>The remark identifier.</value>
        public int? RemarkId { get; set; }

        /// <summary>
        /// Gets or sets the list type identifier.
        /// </summary>
        /// <value>The list type identifier.</value>
        public int ListTypeId { get; set; }

        /// <summary>
        /// Gets or sets the list archive identifier.
        /// </summary>
        /// <value>The list archive identifier.</value>
        public int? ListArchiveId { get; set; }

        /// <summary>
        /// Gets or sets the addresses.
        /// </summary>
        /// <value>The addresses.</value>
        public virtual ICollection<p_Address> Addresses { get; set; }

        /// <summary>
        /// Gets or sets the banks.
        /// </summary>
        /// <value>The banks.</value>
        public virtual ICollection<p_Bank> Banks { get; set; }

        /// <summary>
        /// Gets or sets the births.
        /// </summary>
        /// <value>The births.</value>
        public virtual ICollection<p_Birth> Births { get; set; }

        /// <summary>
        /// Gets or sets the citizenships.
        /// </summary>
        /// <value>The citizenships.</value>
        public virtual ICollection<p_Citizenship> Citizenships { get; set; }

        /// <summary>
        /// Gets or sets the contact information.
        /// </summary>
        /// <value>The contact information.</value>
        public virtual ICollection<p_ContactInfo> ContactInfo { get; set; }

        /// <summary>
        /// Gets or sets the list archive.
        /// </summary>
        /// <value>The list archive.</value>
        [NotMapped]
        public virtual p_ListArchive ListArchive { get; set; }

        /// <summary>
        /// Gets or sets the type of the enum list.
        /// </summary>
        /// <value>The type of the enum list.</value>
        public virtual p_EnumListType EnumListType { get; set; }

        /// <summary>
        /// Gets or sets the regulation.
        /// </summary>
        /// <value>The regulation.</value>
        public virtual p_Regulation Regulation { get; set; }

        /// <summary>
        /// Gets or sets the remark.
        /// </summary>
        /// <value>The remark.</value>
        public virtual p_Remark Remark { get; set; }

        /// <summary>
        /// Gets or sets the enum status.
        /// </summary>
        /// <value>The enum status.</value>
        public virtual p_EnumStatus EnumStatus { get; set; }

        /// <summary>
        /// Gets or sets the type of the enum subject.
        /// </summary>
        /// <value>The type of the enum subject.</value>
        public virtual p_EnumSubjectType EnumSubjectType { get; set; }

        /// <summary>
        /// Gets or sets the identifications.
        /// </summary>
        /// <value>The identifications.</value>
        public virtual ICollection<p_Identification> Identifications { get; set; }

        /// <summary>
        /// Gets or sets the name aliases.
        /// </summary>
        /// <value>The name aliases.</value>
        public virtual ICollection<p_NameAlias> NameAliases { get; set; }
    }
}
