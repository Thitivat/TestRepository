
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// The Bndb.IBanStore.Dal.Pocos namespace contains all poco entities about IBanStore.
/// </summary>
namespace Bndb.IBanStore.Dal.Pocos
{
    /// <summary>
    /// Class p_EnumIbanStatus is a poco entity representing ib.EnumIBanStatus table in the database.
    /// </summary>
    [Table("ib.EnumIBanStatus")]
    public partial class p_EnumIbanStatus
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
        /// Gets or sets the i ban status.
        /// </summary>
        /// <value>The i ban status.</value>
        [Required]
        [StringLength(50)]
        public string IBanStatus { get; set; }

        /// <summary>
        /// Gets or sets the i ban history.
        /// </summary>
        /// <value>The i ban history.</value>
        public virtual ICollection<p_IbanHistory> IBanHistory { get; set; }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Initializes a new instance of the <see cref="p_EnumIbanStatus"/> class.
        /// </summary>
        public p_EnumIbanStatus()
        {
            IBanHistory = new HashSet<p_IbanHistory>();
        }
        #endregion
    }
}
