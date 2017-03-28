using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BND.Websites.BackOffice.SanctionListsManagement.Models
{
    /// <summary>
    /// Class p_EnumCountry is a poco entity representing sl.EnumCountries table in sanction lists database.
    /// </summary>
    [Table("sl.EnumCountries")]
    public partial class p_EnumCountry
    {
        public p_EnumCountry()
        {
            Addresses = new HashSet<p_Address>();
            Banks = new HashSet<p_Bank>();
            Births = new HashSet<p_Birth>();
            Citizenships = new HashSet<p_Citizenship>();
            Identifications = new HashSet<p_Identification>();
        }

        /// <summary>
        /// Gets or sets the iso3 standard code.
        /// </summary>
        /// <value>The iso3 standard code.</value>
        [Key]
        [StringLength(3)]
        public string Iso3 { get; set; }

        /// <summary>
        /// Gets or sets the iso2 standard code.
        /// </summary>
        /// <value>The iso2 standard code.</value>
        [Required]
        [StringLength(2)]
        public string Iso2 { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the nice.
        /// </summary>
        /// <value>The name of the nice.</value>
        [Required]
        [StringLength(128)]
        public string NiceName { get; set; }

        /// <summary>
        /// Gets or sets the number code.
        /// </summary>
        /// <value>The number code.</value>
        public int? NumCode { get; set; }

        /// <summary>
        /// Gets or sets the phone code.
        /// </summary>
        /// <value>The phone code.</value>
        public int? PhoneCode { get; set; }

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
        /// Gets or sets the identifications.
        /// </summary>
        /// <value>The identifications.</value>
        public virtual ICollection<p_Identification> Identifications { get; set; }
    }
}
