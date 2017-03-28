
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// The Bndb.IBanStore.Dal.Pocos namespace contains all poco entities about IBanStore.
/// </summary>
namespace Bndb.IBanStore.Dal.Pocos
{
    /// <summary>
    /// Class p_BbanFile is a poco entity representing ib.BbanFile table in the IBanStore database.
    /// </summary>
    [Table("ib.BbanFile")]
    public partial class p_BbanFile
    {
        #region [Properties]
        /// <summary>
        /// Gets or sets the bban file identifier.
        /// </summary>
        /// <value>The bban file identifier.</value>
        public int BbanFileId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [StringLength(255)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the raw file.
        /// </summary>
        /// <value>The raw file.</value>
        public byte[] RawFile { get; set; }

        /// <summary>
        /// Gets or sets the hash.
        /// </summary>
        /// <value>The hash.</value>
        [StringLength(64)]
        public string Hash { get; set; }

        /// <summary>
        /// Gets or sets the current status history.
        /// </summary>
        /// <value>The current status history.</value>
        public int? CurrentStatusHistory { get; set; }

        /// <summary>
        /// Gets or sets the bban file history.
        /// </summary>
        /// <value>The bban file history.</value>
        public virtual ICollection<p_BbanFileHistory> BbanFileHistory { get; set; }

        /// <summary>
        /// Gets or sets the bban file import.
        /// </summary>
        /// <value>The bban file import.</value>
        public virtual ICollection<p_BbanFileImport> BbanFileImport { get; set; }

        /// <summary>
        /// Gets or sets the i ban.
        /// </summary>
        /// <value>The i ban.</value>
        public virtual ICollection<p_Iban> IBan { get; set; }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Initializes a new instance of the <see cref="p_BbanFile"/> class.
        /// </summary>
        public p_BbanFile()
        {
            BbanFileHistory = new HashSet<p_BbanFileHistory>();
            BbanFileImport = new HashSet<p_BbanFileImport>();
            IBan = new HashSet<p_Iban>();
        }
        #endregion
    }
}
