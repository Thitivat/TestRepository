
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// The Bndb.IBanStore.Dal.Pocos namespace contains all poco entities about IBanStore.
/// </summary>
namespace Bndb.IBanStore.Dal.Pocos
{
    /// <summary>
    /// Class p_BbanFileHistory is a poco entity representing ib.BbanFileHistory table in IBanStore database.
    /// </summary>
    [Table("ib.BbanFileHistory")]
    public partial class p_BbanFileHistory
    {
        /// <summary>
        /// Gets or sets the history identifier.
        /// </summary>
        /// <value>The history identifier.</value>
        [Key]
        public int HistoryId { get; set; }

        /// <summary>
        /// Gets or sets the bban file identifier.
        /// </summary>
        /// <value>The bban file identifier.</value>
        public int BbanFileId { get; set; }

        /// <summary>
        /// Gets or sets the bban file status identifier.
        /// </summary>
        /// <value>The bban file status identifier.</value>
        public int BbanFileStatusId { get; set; }

        /// <summary>
        /// Gets or sets the remark.
        /// </summary>
        /// <value>The remark.</value>
        public string Remark { get; set; }

        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>The context.</value>
        [StringLength(50)]
        public string Context { get; set; }

        /// <summary>
        /// Gets or sets the changed date.
        /// </summary>
        /// <value>The changed date.</value>
        public DateTime ChangedDate { get; set; }

        /// <summary>
        /// Gets or sets the changed by.
        /// </summary>
        /// <value>The changed by.</value>
        [Required]
        [StringLength(50)]
        public string ChangedBy { get; set; }

        /// <summary>
        /// Gets or sets the bban file.
        /// </summary>
        /// <value>The bban file.</value>
        public virtual p_BbanFile BbanFile { get; set; }

        /// <summary>
        /// Gets or sets the enum bban file status.
        /// </summary>
        /// <value>The enum bban file status.</value>
        public virtual p_EnumBbanFileStatus EnumBbanFileStatus { get; set; }
    }
}
