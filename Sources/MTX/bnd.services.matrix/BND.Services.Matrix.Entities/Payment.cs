using System;

using Newtonsoft.Json;

namespace BND.Services.Matrix.Entities
{
    /// <summary>
    /// The payment.
    /// </summary>
    [JsonObject("Payment")]
    public class Payment
    {
        /// <summary>
        /// Gets or sets the source iban.
        /// </summary>
        [JsonProperty("SourceIBAN", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string SourceIBAN { get; set; }

        /// <summary>
        /// Gets or sets the counterparty IBAN.
        /// </summary>
        [JsonProperty("CounterpartyIBAN", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string CounterpartyIBAN { get; set; }

        /// <summary>
        /// Gets or sets the counterparty BIC.
        /// </summary>
        [JsonProperty("CounterpartyBIC", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string CounterpartyBIC { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        [JsonProperty("Amount", DefaultValueHandling = DefaultValueHandling.Include)]
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the value date.
        /// </summary>
        [JsonProperty("ValueDate", DefaultValueHandling = DefaultValueHandling.Include)]
        public DateTime ValueDate { get; set; }

        /// <summary>
        /// Gets or sets the reference.
        /// </summary>
        [JsonProperty("Reference", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string Reference { get; set; }

        /// <summary>
        /// Gets or sets the clarification.
        /// </summary>
        [JsonProperty("Clarification", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string Clarification { get; set; }

        /// <summary>
        /// Gets or sets the debtor details.
        /// </summary>
        [JsonProperty("DebtorDetails", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public DebtorCreditorDetails DebtorDetails { get; set; }

        /// <summary>
        /// Gets or sets the creditor details.
        /// </summary>
        [JsonProperty("CreditorDetails", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public DebtorCreditorDetails CreditorDetails { get; set; }
    }
}
