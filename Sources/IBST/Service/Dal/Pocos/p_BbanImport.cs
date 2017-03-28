
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// The Bndb.IBanStore.Dal.Pocos namespace contains all poco entities about IBanStore.
/// </summary>
namespace BND.Services.IbanStore.Service.Dal.Pocos
{
    /// <summary>
    /// Class p_BbanImport is a poco entity representing ib.BbanFImport table in the IBanStore database.
    /// </summary>
    [Table("ib.BbanImport")]
    public partial class p_BbanImport
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
        public string Bban { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is imported.
        /// </summary>
        /// <value><c>null</c> if [is imported] contains no value, <c>true</c> if [is imported]; otherwise, <c>false</c>.</value>
        public bool IsImported { get; set; }

        /// <summary>
        /// Gets or sets the bban file.
        /// </summary>
        /// <value>The bban file.</value>
        public virtual p_BbanFile BbanFile { get; set; }
    }
}
