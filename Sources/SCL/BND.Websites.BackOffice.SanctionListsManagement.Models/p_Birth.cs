using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BND.Websites.BackOffice.SanctionListsManagement.Models
{
    /// <summary>
    /// Class p_Birth is a poco entity representing sl.Births table in sanction lists database.
    /// </summary>
    [Table("sl.Births")]
    public partial class p_Birth
    {
        /// <summary>
        /// Gets or sets the birth identifier.
        /// </summary>
        /// <value>The birth identifier.</value>
        [Key]
        public int BirthId { get; set; }

        /// <summary>
        /// Gets or sets the original birth identifier.
        /// </summary>
        /// <value>The original birth identifier.</value>
        public int? OriginalBirthId { get; set; }

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
        /// Gets or sets the year.
        /// </summary>
        /// <value>The year.</value>
        public int? Year { get; set; }

        /// <summary>
        /// Gets or sets the month.
        /// </summary>
        /// <value>The month.</value>
        public int? Month { get; set; }

        /// <summary>
        /// Gets or sets the day.
        /// </summary>
        /// <value>The day.</value>
        public int? Day { get; set; }

        /// <summary>
        /// Gets or sets the place.
        /// </summary>
        /// <value>The place.</value>
        [StringLength(2048)]
        public string Place { get; set; }

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
        /// Gets or sets the remark.
        /// </summary>
        /// <value>The remark.</value>
        public virtual p_Remark Remark { get; set; }

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
    }
}
