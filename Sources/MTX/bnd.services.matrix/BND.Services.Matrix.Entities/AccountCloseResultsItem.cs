using System;

using Newtonsoft.Json;

namespace BND.Services.Matrix.Entities
{
    /// <summary>
    /// The account close results item.
    /// </summary>
    [JsonObject("AccountCloseResultsItem")]
    public class AccountCloseResultsItem
    {
        /// <summary>
        /// Gets or sets the closing balance
        /// </summary>
        [JsonProperty("ClosingBalance", DefaultValueHandling = DefaultValueHandling.Include)]
        public decimal ClosingBalance { get; set; }

        /// <summary>
        /// Gets or sets the closing interest
        /// </summary>
        [JsonProperty("ClosingInterest", DefaultValueHandling = DefaultValueHandling.Include)]
        public decimal ClosingInterest { get; set; }

        /// <summary>
        /// Gets or sets the closing fees
        /// </summary>
        [JsonProperty("ClosingFees", DefaultValueHandling = DefaultValueHandling.Include)]
        public decimal ClosingFees { get; set; }

        /// <summary>
        /// Gets or sets the closing payment amount
        /// </summary>
        [JsonProperty("ClosingPaymentAmount", DefaultValueHandling = DefaultValueHandling.Include)]
        public decimal ClosingPaymentAmount { get; set; }

        /// <summary>
        /// Gets or sets the closing payment value date
        /// </summary>
        [JsonProperty("ClosingPaymentValueDate", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public DateTime? ClosingPaymentValueDate { get; set; }

        /// <summary>
        /// Gets or sets the closing payment interest date
        /// </summary>
        [JsonProperty("ClosingPaymentInterestDate", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public DateTime? ClosingPaymentInterestDate { get; set; }

        /// <summary>
        /// Gets or sets the closing payment clearing date
        /// </summary>
        [JsonProperty("ClosingPaymentClearingDate", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public DateTime? ClosingPaymentClearingDate { get; set; }

        /// <summary>
        /// Gets or sets the payment bucket id
        /// </summary>
        [JsonProperty("PaymentBucketId", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string PaymentBucketId { get; set; }
    }
}
