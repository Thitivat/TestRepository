
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// The Bndb.IBanStore.Dal.Pocos namespace contains all poco entities about IBanStore.
/// </summary>
namespace Bndb.IBanStore.Dal.Pocos
{
    /// <summary>
    /// Class p_Iban is a poco entity representing ib.IBan table in the database.
    /// </summary>
    [Table("ib.IBan")]
    public partial class p_Iban
    {
        #region [Properties]
        /// <summary>
        /// Gets or sets the i ban identifier.
        /// </summary>
        /// <value>The i ban identifier.</value>
        public int IBanId { get; set; }

        /// <summary>
        /// Gets or sets the b ban file identifier.
        /// </summary>
        /// <value>The b ban file identifier.</value>
        public int BBanFileId { get; set; }

        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        /// <value>The country code.</value>
        [StringLength(2)]
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the bank code.
        /// </summary>
        /// <value>The bank code.</value>
        [StringLength(4)]
        public string BankCode { get; set; }

        /// <summary>
        /// Gets or sets the check sum.
        /// </summary>
        /// <value>The check sum.</value>
        public int? CheckSum { get; set; }

        /// <summary>
        /// Gets or sets the bban.
        /// </summary>
        /// <value>The bban.</value>
        [StringLength(10)]
        public string Bban { get; set; }

        /// <summary>
        /// Gets or sets the current status history identifier.
        /// </summary>
        /// <value>The current status history identifier.</value>
        public int? CurrentStatusHistoryId { get; set; }

        /// <summary>
        /// Gets or sets the bban file.
        /// </summary>
        /// <value>The bban file.</value>
        public virtual p_BbanFile BbanFile { get; set; }

        /// <summary>
        /// Gets or sets the i ban history.
        /// </summary>
        /// <value>The i ban history.</value>
        public virtual ICollection<p_IbanHistory> IBanHistory { get; set; }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Initializes a new instance of the <see cref="p_Iban"/> class.
        /// </summary>
        public p_Iban()
        {
            IBanHistory = new HashSet<p_IbanHistory>();
        }
        #endregion
    }
}
