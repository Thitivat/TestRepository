
namespace BND.Services.Payments.iDeal.IntegrationTests.ViewModels
{
    /// <summary>
    /// Class TransactionRequestViewModel represent data that be mapped with UI in Payment page.
    /// </summary>
    public class TransactionRequestViewModel
    {
        /// <summary>
        /// Gets or sets the purchase identifier.
        /// </summary>
        /// <value>The purchase identifier.</value>
        public string PurchaseID { get; set; }
        /// <summary>
        /// Gets or sets the issuer identifier.
        /// </summary>
        /// <value>The issuer identifier.</value>
        public string IssuerID { get; set; }
        /// <summary>
        /// Gets or sets the BND IBAN of customer.
        /// </summary>
        /// <value>The BND IBAN.</value>
        public string BNDIBAN { get; set; }
        /// <summary>
        /// Gets or sets the customer IBAN which is used transfer money via iDeal.
        /// </summary>
        /// <value>The customer IBAN.</value>
        public string CustomerIBAN { get; set; }
        /// <summary>
        /// Gets or sets the type of the payment.
        /// </summary>
        /// <value>The type of the payment.</value>
        public string PaymentType { get; set; }
        /// <summary>
        /// Gets or sets the ALPHA-3 currency code, the default value is EUR.
        /// </summary>
        /// <value>The ALPHA-3 currency.</value>
        public string Currency { get; set; }
        /// <summary>
        /// Gets or sets the ALPHA-2 language code, the default value is NL.
        /// </summary>
        /// <value>The ALPHA-2 language.</value>
        public string Language { get; set; }
        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>The amount.</value>
        public decimal Amount { get; set; }
        /// <summary>
        /// Gets or sets the expiration period (Seconds).
        /// </summary>
        /// <value>The expiration period (Seconds).</value>
        public int ExpirationPeriod { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the merchant return URL, Issueing bank will return back to you when customer has confirmed.
        /// </summary>
        /// <value>The merchant return URL.</value>
        public string MerchantReturnURL { get; set; }

        /// <summary>
        /// Gets or sets the api token, used to authorization when connect to api.
        /// </summary>
        public string ApiToken { get; set; }
    }
}