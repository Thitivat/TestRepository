using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BND.Websites.BackOffice.SanctionListsManagement.Models
{
    /// <summary>
    /// Class p_Citizenship is a poco entity representing sl.Citizenships table in sanction lists database.
    /// </summary>
    [Table("sl.Citizenships")]
    public partial class p_Citizenship
    {
        /// <summary>
        /// Gets or sets the citizenship identifier.
        /// </summary>
        /// <value>The citizenship identifier.</value>
        [Key]
        public int CitizenshipId { get; set; }

        /// <summary>
        /// Gets or sets the original citizenship identifier.
        /// </summary>
        /// <value>The original citizenship identifier.</value>
        public int? OriginalCitizenshipId { get; set; }

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
