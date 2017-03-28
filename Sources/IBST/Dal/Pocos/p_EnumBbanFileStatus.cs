
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// The Bndb.IBanStore.Dal.Pocos namespace contains all poco entities about IBanStore.
/// </summary>
namespace Bndb.IBanStore.Dal.Pocos
{
    /// <summary>
    /// Class p_EnumBbanFileStatus is a poco entity representing ib.EnumBbanFileStatus table in the IBanStore database.
    /// </summary>
    [Table("ib.EnumBbanFileStatus")]
    public partial class p_EnumBbanFileStatus
    {
        #region [Properties]
        /// <summary>
        /// Gets or sets the status identifier.
        /// </summary>
        /// <value>The status identifier.</value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StatusId { get; set; }

        /// <summary>
        /// Gets or sets the bban file status.
        /// </summary>
        /// <value>The bban file status.</value>
        [Required]
        [StringLength(50)]
        public string BbanFileStatus { get; set; }

        /// <summary>
        /// Gets or sets the bban file history.
        /// </summary>
        /// <value>The bban file history.</value>
        public virtual ICollection<p_BbanFileHistory> BbanFileHistory { get; set; }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Initializes a new instance of the <see cref="p_EnumBbanFileStatus"/> class.
        /// </summary>
        public p_EnumBbanFileStatus()
        {
            BbanFileHistory = new HashSet<p_BbanFileHistory>();
        }
        #endregion
    }
}
