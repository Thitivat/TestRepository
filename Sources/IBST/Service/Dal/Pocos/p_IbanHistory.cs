
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// The Bndb.IBanStore.Dal.Pocos namespace contains all poco entities about IBanStore.
/// </summary>
namespace BND.Services.IbanStore.Service.Dal.Pocos
{
    /// <summary>
    /// Class p_IbanHistory is a poco entity representing ib.IBanHistory table in the database.
    /// </summary>
    [Table("ib.IbanHistory")]
    public partial class p_IbanHistory
    {
        /// <summary>
        /// Gets or sets the history identifier.
        /// </summary>
        /// <value>The history identifier.</value>
        [Key]
        public int HistoryId { get; set; }

        /// <summary>
        /// Gets or sets the i ban identifier.
        /// </summary>
        /// <value>The i ban identifier.</value>
        public int IbanId { get; set; }

        /// <summary>
        /// Gets or sets the i ban status identifier.
        /// </summary>
        /// <value>The i ban status identifier.</value>
        public p_EnumIbanStatus IbanStatusId { get; set; }

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
        /// Gets or sets the i ban.
        /// </summary>
        /// <value>The i ban.</value>
        public virtual p_Iban Iban { get; set; }
    }
}
