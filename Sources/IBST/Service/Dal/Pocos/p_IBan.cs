
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// The Bndb.IBanStore.Dal.Pocos namespace contains all poco entities about IBanStore.
/// </summary>
namespace BND.Services.IbanStore.Service.Dal.Pocos
{
    /// <summary>
    /// Class p_Iban is a poco entity representing ib.IBan table in the database.
    /// </summary>
    [Table("ib.Iban")]
    public partial class p_Iban
    {
        #region [Properties]
        /// <summary>
        /// Gets or sets the i ban identifier.
        /// </summary>
        /// <value>The i ban identifier.</value>
        [Key]
        public int IbanId { get; set; }

        /// <summary>
        /// Gets or sets the b ban file identifier.
        /// </summary>
        /// <value>The b ban file identifier.</value>
        public int BbanFileId { get; set; }

        /// <summary>
        /// Gets or sets the uid.
        /// </summary>
        /// <value>The uid.</value>
        public string Uid { get; set; }

        /// <summary>
        /// Gets or sets the uid prefix.
        /// </summary>
        /// <value>The uid prefix.</value>
        public string UidPrefix { get; set; }

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
        public string CheckSum { get; set; }

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
        public virtual ICollection<p_IbanHistory> IbanHistory { get; set; }

        /// <summary>
        /// Gets or sets the time stamp.
        /// </summary>
        /// <value>The time stamp.</value>
        public DateTime? ReservedTime { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Initializes a new instance of the <see cref="p_Iban"/> class.
        /// </summary>
        public p_Iban()
        {
            IbanHistory = new HashSet<p_IbanHistory>();
        }
        #endregion
    }
}
