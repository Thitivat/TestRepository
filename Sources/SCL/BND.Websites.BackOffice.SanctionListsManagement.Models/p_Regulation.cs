using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BND.Websites.BackOffice.SanctionListsManagement.Models
{
    /// <summary>
    /// Class p_Regulation is a poco entity representing sl.Regulations table in sanction lists database.
    /// </summary>
    [Table("sl.Regulations")]
    public partial class p_Regulation
    {
        p_Entity pEntity = new p_Entity();
        public p_Regulation()
        {
            Addresses = new HashSet<p_Address>();
            Births = new HashSet<p_Birth>();
            Citizenships = new HashSet<p_Citizenship>();
            ContactInfo = new HashSet<p_ContactInfo>();
            Entities = new HashSet<p_Entity>();
            Identifications = new HashSet<p_Identification>();
            NameAliases = new HashSet<p_NameAlias>();
        }

        /// <summary>
        /// Gets or sets the regulation identifier.
        /// </summary>
        /// <value>The regulation identifier.</value>
        [Key]
        public int RegulationId { get; set; }

        /// <summary>
        /// Gets or sets the regulation title.
        /// </summary>
        /// <value>The regulation title.</value>
        [Required]
        [StringLength(128)]
        public string RegulationTitle { get; set; }

        /// <summary>
        /// Gets or sets the regulation date.
        /// </summary>
        /// <value>The regulation date.</value>
        [Column(TypeName = "date")]
        public DateTime? RegulationDate { get; set; }

        /// <summary>
        /// Gets or sets the publication date.
        /// </summary>
        /// <value>The publication date.</value>
        [Column(TypeName = "date")]
        public DateTime PublicationDate { get; set; }

        /// <summary>
        /// Gets or sets the publication title.
        /// </summary>
        /// <value>The publication title.</value>
        [Required]
        [StringLength(128)]
        public string PublicationTitle { get; set; }

        /// <summary>
        /// Gets or sets the publication URL.
        /// </summary>
        /// <value>The publication URL.</value>
        [StringLength(256)]
        public string PublicationUrl { get; set; }

        /// <summary>
        /// Gets or sets the remark identifier.
        /// </summary>
        /// <value>The remark identifier.</value>
        public int? RemarkId { get; set; }

        /// <summary>
        /// Gets or sets the programme.
        /// </summary>
        /// <value>The programme.</value>
        [Required]
        [StringLength(20)]
        public string Programme { get; set; }

        /// <summary>
        /// Gets or sets the list type identifier.
        /// </summary>
        /// <value>The list type identifier.</value>
        public int ListTypeId { get; set; }

        /// <summary>
        /// Gets or sets the addresses.
        /// </summary>
        /// <value>The addresses.</value>
        public virtual ICollection<p_Address> Addresses { get; set; }

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
        /// Gets or sets the entities.
        /// </summary>
        /// <value>The entities.</value>
        public virtual ICollection<p_Entity> Entities { get; set; }

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

        /// <summary>
        /// Gets or sets the remark.
        /// </summary>
        /// <value>The remark.</value>
        public virtual p_Remark Remark { get; set; }

        /// <summary>
        /// Gets or sets the type of the enum list.
        /// </summary>
        /// <value>The type of the enum list.</value>
        public virtual p_EnumListType EnumListType { get; set; }
    }
}
