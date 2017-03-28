using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BND.Services.Payments.iDeal.Dal.Pocos
{
    /// <summary>
    /// Class p_Transaction representing transaction table in iDeal database.
    /// </summary>
    [Table("ideal.Transaction")]
    public partial class p_Transaction
    {
        /// <summary>
        /// Gets or sets the transaction identifier.
        /// </summary>
        /// <value>The transaction identifier.</value>
        [Key]
        [StringLength(16)]
        public string TransactionID { get; set; }

        /// <summary>
        /// Gets or sets the acquirer identifier.
        /// </summary>
        /// <value>The acquirer identifier.</value>
        [Required]
        [StringLength(4)]
        public string AcquirerID { get; set; }

        /// <summary>
        /// Gets or sets the merchant identifier.
        /// </summary>
        /// <value>The merchant identifier.</value>
        [Required]
        [StringLength(9)]
        public string MerchantID { get; set; }

        /// <summary>
        /// Gets or sets the sub identifier.
        /// </summary>
        /// <value>The sub identifier.</value>
        public int? SubID { get; set; }

        /// <summary>
        /// Gets or sets the issuer identifier.
        /// </summary>
        /// <value>The issuer identifier.</value>
        [Required]
        [StringLength(11)]
        public string IssuerID { get; set; }

        /// <summary>
        /// Gets or sets the issuer authentication URL.
        /// </summary>
        /// <value>The issuer authentication URL.</value>
        [StringLength(512)]
        public string IssuerAuthenticationURL { get; set; }

        /// <summary>
        /// Gets or sets the merchant return URL.
        /// </summary>
        /// <value>The merchant return URL.</value>
        [Required]
        [StringLength(512)]
        public string MerchantReturnURL { get; set; }

        /// <summary>
        /// Gets or sets the entrance code.
        /// </summary>
        /// <value>The entrance code.</value>
        [Required]
        [StringLength(40)]
        public string EntranceCode { get; set; }

        /// <summary>
        /// Gets or sets the purchase identifier.
        /// </summary>
        /// <value>The purchase identifier.</value>
        [Required]
        [StringLength(35)]
        public string PurchaseID { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>The amount.</value>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>The currency.</value>
        [Required]
        [StringLength(3)]
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>The language.</value>
        [Required]
        [StringLength(2)]
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [StringLength(35)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the name of the consumer.
        /// </summary>
        /// <value>The name of the consumer.</value>
        [StringLength(70)]
        public string ConsumerName { get; set; }

        /// <summary>
        /// Gets or sets the consumer iban.
        /// </summary>
        /// <value>The consumer iban.</value>
        [StringLength(34)]
        public string ConsumerIBAN { get; set; }

        /// <summary>
        /// Gets or sets the consumer bic.
        /// </summary>
        /// <value>The consumer bic.</value>
        [StringLength(11)]
        public string ConsumerBIC { get; set; }

        /// <summary>
        /// Gets or sets the transaction request date time stamp.
        /// </summary>
        /// <value>The transaction request date time stamp.</value>
        public DateTime TransactionRequestDateTimestamp { get; set; }

        /// <summary>
        /// Gets or sets the transaction response date time stamp.
        /// </summary>
        /// <value>The transaction response date time stamp.</value>
        public DateTime TransactionResponseDateTimestamp { get; set; }

        /// <summary>
        /// Gets or sets the transaction create date time stamp.
        /// </summary>
        /// <value>The transaction create date time stamp.</value>
        public DateTime TransactionCreateDateTimestamp { get; set; }

        /// <summary>
        /// Gets or sets the attempts for today.
        /// </summary>
        /// <value>The attempts for today.</value>
        public int TodayAttempts { get; set; }

        /// <summary>
        /// Gets or sets the latest attempts time stamp.
        /// </summary>
        /// <value>The latest attempts time stamp.</value>
        public DateTime? LatestAttemptsDateTimestamp { get; set; }

        /// <summary>
        /// Gets or sets the bndiban.
        /// </summary>
        /// <value>The bndiban.</value>
        [Required]
        [StringLength(34)]
        public string BNDIBAN { get; set; }

        /// <summary>
        /// Gets or sets the type of the payment.
        /// </summary>
        /// <value>The type of the payment.</value>
        [Required]
        [StringLength(64)]
        public string PaymentType { get; set; }

        /// <summary>
        /// Gets or sets the expiration second period.
        /// </summary>
        /// <value>The expiration second period.</value>
        public int ExpirationSecondPeriod { get; set; }

        /// <summary>
        /// Gets or sets the expected customer IBAN.
        /// </summary>
        /// <value>The expected customer IBAN.</value>
        [Required]
        [StringLength(34)]
        public string ExpectedCustomerIBAN { get; set; }

        /// <summary>
        /// Is system fail.
        /// </summary>
        public bool IsSystemFail { get; set; }

        /// <summary>
        /// Gets or sets the booking unique identifier.
        /// </summary>
        /// <value>The booking unique identifier.</value>
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public Guid BookingGuid { get; set; }

        /// <summary>
        /// Gets or sets the booking status.
        /// </summary>
        /// <value>The booking status.</value>
        [StringLength(32)]
        public string BookingStatus { get; set; }

        /// <summary>
        /// Gets or sets the booking date.
        /// </summary>
        /// <value>The booking date.</value>
        public DateTime? BookingDate { get; set; }

        /// <summary>
        /// Gets or sets the movement identifier.
        /// </summary>
        /// <value>The movement identifier.</value>
        public int? MovementId { get; set; }

        /// <summary>
        /// Gets or sets the transaction status histories.
        /// </summary>
        /// <value>The transaction status histories.</value>
        public virtual ICollection<p_TransactionStatusHistory> TransactionStatusHistories { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="p_Transaction"/> class.
        /// </summary>
        public p_Transaction()
        {
            TransactionStatusHistories = new HashSet<p_TransactionStatusHistory>();
        }
    }
}
