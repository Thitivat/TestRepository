
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// The Bndb.IBanStore.Dal.Pocos namespace contains all poco entities about IBanStore.
/// </summary>
namespace Bndb.IBanStore.Dal.Pocos
{
    /// <summary>
    /// Class p_BbanFileImport is a poco entity representing ib.BbanFileImport table in the IBanStore database.
    /// </summary>
    [Table("ib.BbanFileImport")]
    public partial class p_BbanFileImport
    {
        /// <summary>
        /// Gets or sets the bban import identifier.
        /// </summary>
        /// <value>The bban import identifier.</value>
        [Key]
        public int BbanImportId { get; set; }

        /// <summary>
        /// Gets or sets the bban file identifier.
        /// </summary>
        /// <value>The bban file identifier.</value>
        public int BbanFileId { get; set; }

        /// <summary>
        /// Gets or sets the bban.
        /// </summary>
        /// <value>The bban.</value>
        [StringLength(10)]
        public string Bban { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is imported.
        /// </summary>
        /// <value><c>null</c> if [is imported] contains no value, <c>true</c> if [is imported]; otherwise, <c>false</c>.</value>
        public bool? IsImported { get; set; }

        /// <summary>
        /// Gets or sets the bban file.
        /// </summary>
        /// <value>The bban file.</value>
        public virtual p_BbanFile BbanFile { get; set; }
    }
}
