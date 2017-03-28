using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// The Bndb.Kyc.SanctionLists.SanctionListsDal.Pocos namespace contains all poco entities about sanction lists.
/// </summary>
namespace BND.Websites.BackOffice.SanctionListsManagement.Models
{
    /// <summary>
    /// Class p_Address is a poco entity representing sl.Addresses table in sanction lists database.
    /// </summary>
    [Table("sl.Addresses")]
    public partial class p_Address
    {
        /// <summary>
        /// Gets or sets the address identifier.
        /// </summary>
        /// <value>The address identifier.</value>
        [Key]
        public int AddressId { get; set; }

        /// <summary>
        /// Gets or sets the original address identifier.
        /// </summary>
        /// <value>The original address identifier.</value>
        public int? OriginalAddressId { get; set; }

        /// <summary>
        /// Gets or sets the entity identifier.
        /// </summary>
        /// <value>The entity identifier.</value>
        public int EntityId { get; set; }

        /// <summary>
        /// Gets or sets the regulation identifier.
        /// </summary>
        /// <value>The regulation identifier.</value>
        public int RegulationId { get; set; }

        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        /// <value>The number.</value>
        [StringLength(20)]
        public string Number { get; set; }

        /// <summary>
        /// Gets or sets the street.
        /// </summary>
        /// <value>The street.</value>
        [StringLength(256)]
        public string Street { get; set; }

        /// <summary>
        /// Gets or sets the zipcode.
        /// </summary>
        /// <value>The zipcode.</value>
        [StringLength(20)]
        public string Zipcode { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>The city.</value>
        [StringLength(128)]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the country following iso3 standard.
        /// </summary>
        /// <value>The country following iso3 standard.</value>
        [StringLength(3)]
        public string CountryIso3 { get; set; }

        /// <summary>
        /// Gets or sets the remark identifier.
        /// </summary>
        /// <value>The remark identifier.</value>
        public int? RemarkId { get; set; }

        /// <summary>
        /// Gets or sets the other.
        /// </summary>
        /// <value>The other.</value>
        public string Other { get; set; }

        /// <summary>
        /// Gets or sets the enum country.
        /// </summary>
        /// <value>The enum country.</value>
        public virtual p_EnumCountry EnumCountry { get; set; }

        /// <summary>
        /// Gets or sets the entity.
        /// </summary>
        /// <value>The entity.</value>
        public virtual p_Entity Entity { get; set; }

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
    }
}
