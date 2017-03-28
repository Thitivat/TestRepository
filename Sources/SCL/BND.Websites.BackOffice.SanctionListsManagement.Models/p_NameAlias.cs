using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BND.Websites.BackOffice.SanctionListsManagement.Models
{
    /// <summary>
    /// Class p_NameAlias is a poco entity representing sl.NameAliases table in sanction lists database.
    /// </summary>
    [Table("sl.NameAliases")]
    public partial class p_NameAlias
    {
        /// <summary>
        /// Gets or sets the name alias identifier.
        /// </summary>
        /// <value>The name alias identifier.</value>
        [Key]
        public int NameAliasId { get; set; }

        /// <summary>
        /// Gets or sets the original name alias identifier.
        /// </summary>
        /// <value>The original name alias identifier.</value>
        public int? OriginalNameAliasId { get; set; }

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
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        [StringLength(256)]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        [StringLength(256)]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the name of the middle.
        /// </summary>
        /// <value>The name of the middle.</value>
        [StringLength(256)]
        public string MiddleName { get; set; }

        /// <summary>
        /// Gets or sets the name of the whole.
        /// </summary>
        /// <value>The name of the whole.</value>
        [Required]
        [StringLength(512)]
        public string WholeName { get; set; }

        /// <summary>
        /// Gets or sets the name of the prefix.
        /// </summary>
        /// <value>The name of the prefix.</value>
        [StringLength(20)]
        public string PrefixName { get; set; }

        /// <summary>
        /// Gets or sets the gender identifier.
        /// </summary>
        /// <value>The gender identifier.</value>
        public int GenderId { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [StringLength(2048)]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the language following iso3 standard.
        /// </summary>
        /// <value>The language following iso3 standard.</value>
        [StringLength(3)]
        public string LanguageIso3 { get; set; }

        /// <summary>
        /// Gets or sets the remark identifier.
        /// </summary>
        /// <value>The remark identifier.</value>
        public int? RemarkId { get; set; }

        /// <summary>
        /// Gets or sets the quality.
        /// </summary>
        /// <value>The quality.</value>
        public short? Quality { get; set; }

        /// <summary>
        /// Gets or sets the function.
        /// </summary>
        /// <value>The function.</value>
        [StringLength(2048)]
        public string Function { get; set; }

        /// <summary>
        /// Gets or sets the entity.
        /// </summary>
        /// <value>The entity.</value>
        public virtual p_Entity Entity { get; set; }

        /// <summary>
        /// Gets or sets the enum gender.
        /// </summary>
        /// <value>The enum gender.</value>
        public virtual p_EnumGender EnumGender { get; set; }

        /// <summary>
        /// Gets or sets the enum language.
        /// </summary>
        /// <value>The enum language.</value>
        public virtual p_EnumLanguage EnumLanguage { get; set; }

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
