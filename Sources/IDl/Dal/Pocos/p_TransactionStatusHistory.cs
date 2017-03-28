using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BND.Services.Payments.iDeal.Dal.Pocos
{
    /// <summary>
    /// Class p_TransactionStatusHistory representing TransactionStatusHistory table in iDeal database.
    /// </summary>
    [Table("ideal.TransactionStatusHistory")]
    public class p_TransactionStatusHistory
    {
        /// <summary>
        /// Gets or sets the transaction status history identifier.
        /// </summary>
        /// <value>The transaction status history identifier.</value>
        [Key]
        public int TransactionStatusHistoryID { get; set; }

        /// <summary>
        /// Gets or sets the transaction identifier.
        /// </summary>
        /// <value>The transaction identifier.</value>
        [Required]
        [StringLength(16)]
        public string TransactionID { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        [Required]
        [StringLength(32)]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the status request time stamp.
        /// </summary>
        /// <value>The status request time stamp.</value>
        public DateTime StatusRequestDateTimeStamp { get; set; }

        /// <summary>
        /// Gets or sets the status response time stamp.
        /// </summary>
        /// <value>The status response time stamp.</value>
        public DateTime StatusResponseDateTimeStamp { get; set; }

        /// <summary>
        /// Gets or sets the status time stamp.
        /// </summary>
        /// <value>The status time stamp.</value>
        public DateTime? StatusDateTimeStamp { get; set; }

        /// <summary>
        /// Gets or sets the transaction.
        /// </summary>
        /// <value>The transaction.</value>
        public virtual p_Transaction Transaction { get; set; }
    }
}
