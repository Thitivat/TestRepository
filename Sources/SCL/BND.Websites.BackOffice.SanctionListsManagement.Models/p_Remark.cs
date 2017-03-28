using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BND.Websites.BackOffice.SanctionListsManagement.Models
{
    /// <summary>
    /// Class p_Remark is a poco entity representing sl.Remarks table in sanction lists database.
    /// </summary>
    [Table("sl.Remarks")]
    public partial class p_Remark
    {
        public p_Remark()
        {
            Addresses = new HashSet<p_Address>();
            Banks = new HashSet<p_Bank>();
            Births = new HashSet<p_Birth>();
            Citizenships = new HashSet<p_Citizenship>();
            ContactInfo = new HashSet<p_ContactInfo>();
            Entities = new HashSet<p_Entity>();
            Identifications = new HashSet<p_Identification>();
            NameAliases = new HashSet<p_NameAlias>();
            Regulations = new HashSet<p_Regulation>();
        }

        /// <summary>
        /// Gets or sets the remark identifier.
        /// </summary>
        /// <value>The remark identifier.</value>
        [Key]
        public int RemarkId { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        [Column(TypeName = "text")]
        [Required]
        public string Value { get; set; }

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
        /// Gets or sets the regulations.
        /// </summary>
        /// <value>The regulations.</value>
        public virtual ICollection<p_Regulation> Regulations { get; set; }
    }
}
