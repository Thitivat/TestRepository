using System;
using System.Net;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// The BND.Services.Payments.iDeal.Models namespace contains all shared models what are used in relevant iDeal projects.
/// </summary>
namespace BND.Services.Payments.iDeal.Models
{
    /// <summary>
    /// Class TransactionRequestModel is a model what is used for requesting a transaction to iDeal service.
    /// </summary>
    public class TransactionRequestModel
    {
        #region [Fields]

        /// <summary>
        /// The currency default value.
        /// </summary>
        private const string DEFAULT_CURRENCY = "EUR";
        /// <summary>
        /// The language default value.
        /// </summary>
        private const string DEFAULT_LANGUAGE = "NL";

        #endregion


        #region [Properties]

        /// <summary>
        /// Unique identification of the order within the Merchant’s system. Ultimately appears on the payment confirmation (Bank statement / 
        /// account overview of the Consumer and Merchant)
        /// </summary>
        /// <value>The purchase identifier.</value>
        [Required(ErrorMessage = "Purchase ID is required.")]
        [StringLength(35, ErrorMessage = "Purchase ID cannot exceed 35 characters.")]
        public string PurchaseID { get; set; }
        /// <summary>
        /// The ID (BIC) of the Issuer selected by the Consumer, as stated in the Issuer list.
        /// </summary>
        /// <value>The issuer identifier.</value>
        [Required(ErrorMessage = "Issuer ID is required.")]
        [StringLength(11, ErrorMessage = "Issuer ID cannot exceed 11 characters.")]
        public string IssuerID { get; set; }
        /// <summary>
        /// Gets or sets the BND IBAN of customer.
        /// </summary>
        /// <value>The BND IBAN.</value>
        [Required(ErrorMessage = "BND IBAN is required.")]
        [StringLength(34, ErrorMessage = "BND IBAN cannot exceed 34 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Characters are not allowed, Only alphanumeric can be used.")]
        public string BNDIBAN { get; set; }
        /// <summary>
        /// Gets or sets the customer IBAN which is used transfer money via iDeal.
        /// </summary>
        /// <value>The customer IBAN.</value>
        [Required(ErrorMessage = "Customer IBAN is required.")]
        [StringLength(34, ErrorMessage = "Customer IBAN cannot exceed 34 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Characters are not allowed, Only alphanumeric can be used.")]
        public string CustomerIBAN { get; set; }
        /// <summary>
        /// Gets or sets the type of the payment.
        /// </summary>
        /// <value>The type of the payment.</value>
        [Required(ErrorMessage = "Payment Type is required.")]
        public string PaymentType { get; set; }
        /// <summary>
        /// Gets or sets the ALPHA-3 currency code, the default value is EUR.
        /// </summary>
        /// <value>The ALPHA-3 currency.</value>
        [RegularExpression(@"^[a-zA-Z]{3}$", ErrorMessage = "Characters are not allowed, It has to be ALPHA-3 currency code following ISO 4217.")]
        public string Currency { get; set; }
        /// <summary>
        /// Gets or sets the ALPHA-2 language code, the default value is NL.
        /// </summary>
        /// <value>The ALPHA-2 language.</value>
        [RegularExpression(@"^[a-zA-Z]{2}$", ErrorMessage = "Characters are not allowed, It has to be ALPHA-2 language code following ISO 639-2.")]
        public string Language { get; set; }
        /// <summary>
        /// The amount payable in euro (with a period (.) used asdecimal separator).
        /// </summary>
        /// <value>The amount.</value>
        [DataType(DataType.Currency)]
        [Range(0.01, Double.MaxValue, ErrorMessage = "The Amount value could not be less than or equals zero.")]
        public decimal Amount { get; set; }
        /// <summary>
        /// Optional: the period of validity of the payment request as stated by the Merchant measured from the receipt by the Issuer. The Consumer 
        /// must authorise the payment within this period. Otherwise the Issuersets the status of the transaction to ‘Expired’.
        /// </summary>
        /// <value>The expiration period (Seconds).</value>
        public int ExpirationPeriod { get; set; }
        /// <summary>
        /// Description of the product(s) or services being paid for. This field must not contain characters that can lead to problems  (for example 
        /// those occurring in HTML editing codes). To prevent any possible errors most iDEAL systems willreject any description that contains HTML-tags 
        /// and other such code.
        /// </summary>
        /// <value>The description.</value>
        [Required(ErrorMessage = "Description is required.")]
        [StringLength(35, ErrorMessage = "Description cannot exceed 35 characters.")]
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the merchant return URL, Issueing bank will return back to you when customer has confirmed.
        /// </summary>
        /// <value>The merchant return URL.</value>
        [Url2(ErrorMessage = "Merchant return URL is wrong format.")]
        [Required(ErrorMessage = "Merchant return URL is required.")]
        [StringLength(512, ErrorMessage = "Merchant return URL cannot exceed 512 characters.")]
        public string MerchantReturnURL { get; set; }

        #endregion


        #region [Constuctors]

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionRequestModel" /> class.
        /// </summary>
        public TransactionRequestModel()
        {
            // Sets default values.
            Currency = DEFAULT_CURRENCY;
            Language = DEFAULT_LANGUAGE;
        }

        #endregion
    }
}