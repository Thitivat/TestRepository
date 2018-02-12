using Newtonsoft.Json;

namespace BND.Services.Matrix.Entities
{
    /// <summary>
    /// The closing payment item.
    /// </summary>
    [JsonObject("ClosingPaymentItem")]
    public class ClosingPaymentItem
    {
        /// <summary>
        /// Gets or sets the clarification
        /// </summary>
        [JsonProperty("Clarification", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string Clarification { get; set; }

        /// <summary>
        /// Gets or sets the reference
        /// </summary>
        [JsonProperty("Reference", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string Reference { get; set; }

        /// <summary>
        /// Gets or sets the payment source
        /// </summary>
        [JsonProperty("PaymentSource", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string PaymentSource { get; set; }

        /// <summary>
        /// Gets or sets the counterparty account number
        /// </summary>
        [JsonProperty("CounterpartyAccountNumber", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string CounterpartyAccountNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is external
        /// </summary>
        [JsonProperty("IsExternal", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public bool? IsExternal { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to bypass nominated account validation
        /// </summary>
        [JsonProperty("BypassNominatedAccountValidation", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public bool? BypassNominatedAccountValidation { get; set; }

        /// <summary>
        /// Gets or sets the counterparty BIC
        /// </summary>
        [JsonProperty("CounterpartyBIC", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string CounterpartyBIC { get; set; }

        /// <summary>
        /// Gets or sets the debtor details
        /// </summary>
        [JsonProperty("DebetorDetails", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public PaymentCustomerItem DebetorDetails { get; set; }

        /// <summary>
        /// Gets or sets the creditor details
        /// </summary>
        [JsonProperty("CreditorDetails", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public PaymentCustomerItem CreditorDetails { get; set; }
    }
}
