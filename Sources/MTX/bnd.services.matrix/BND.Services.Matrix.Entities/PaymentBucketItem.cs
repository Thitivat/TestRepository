using System;

using Newtonsoft.Json;

namespace BND.Services.Matrix.Entities
{
    /// <summary>
    /// The payment bucket item.
    /// </summary>
    [JsonObject("PaymentBucketItem")]
    public class PaymentBucketItem
    {
        /// <summary>
        /// Gets or sets the row id.
        /// </summary>
        [JsonProperty("RowId", DefaultValueHandling = DefaultValueHandling.Include)]
        public int RowId { get; set; }

        /// <summary>
        /// Gets or sets the payment order.
        /// </summary>
        [JsonProperty("PaymentOrder", DefaultValueHandling = DefaultValueHandling.Include)]
        public int PaymentOrder { get; set; }

        /// <summary>
        /// Gets or sets the from account.
        /// </summary>
        [JsonProperty("FromAccount", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string FromAccount { get; set; }

        /// <summary>
        /// Gets or sets the from bic.
        /// </summary>
        [JsonProperty("FromBic", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string FromBic { get; set; }

        /// <summary>
        /// Gets or sets the to account.
        /// </summary>
        [JsonProperty("ToAccount", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string ToAccount { get; set; }

        /// <summary>
        /// Gets or sets the to bic.
        /// </summary>
        [JsonProperty("ToBic", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string ToBic { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        [JsonProperty("Currency", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        [JsonProperty("Amount", DefaultValueHandling = DefaultValueHandling.Include)]
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the value date.
        /// </summary>
        [JsonProperty("ValueDate", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public DateTime? ValueDate { get; set; }

        /// <summary>
        /// Gets or sets the reference.
        /// </summary>
        [JsonProperty("Reference", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string Reference { get; set; }

        /// <summary>
        /// Gets or sets the item status.
        /// </summary>
        [JsonProperty("ItemStatus", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string ItemStatus { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        [JsonProperty("ErrorMessage", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets the processing transaction id.
        /// </summary>
        [JsonProperty("ProcessingTransactionId", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public long? ProcessingTransactionId { get; set; }

        /// <summary>
        /// Gets or sets the debetor account number.
        /// </summary>
        [JsonProperty("DebetorAccountNumber", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string DebetorAccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the debetor address line 1.
        /// </summary>
        [JsonProperty("DebetorAddressLine1", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string DebetorAddressLine1 { get; set; }

        /// <summary>
        /// Gets or sets the debetor address line 2.
        /// </summary>
        [JsonProperty("DebetorAddressLine2", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string DebetorAddressLine2 { get; set; }

        /// <summary>
        /// Gets or sets the debetor address line 3.
        /// </summary>
        [JsonProperty("DebetorAddressLine3", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string DebetorAddressLine3 { get; set; }

        /// <summary>
        /// Gets or sets the debetor country code.
        /// </summary>
        [JsonProperty("DebetorCountryCode", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string DebetorCountryCode { get; set; }

        /// <summary>
        /// Gets or sets the debetor customer name.
        /// </summary>
        [JsonProperty("DebetorCustomerName", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string DebetorCustomerName { get; set; }

        /// <summary>
        /// Gets or sets the debetor account item details agent.
        /// </summary>
        [JsonProperty("DebetorAccountItemDetailsAgent", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string DebetorAccountItemDetailsAgent { get; set; }

        /// <summary>
        /// Gets or sets the debetor account item details agent account.
        /// </summary>
        [JsonProperty("DebetorAccountItemDetailsAgentAccount", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string DebetorAccountItemDetailsAgentAccount { get; set; }

        /// <summary>
        /// Gets or sets the creditor account number.
        /// </summary>
        [JsonProperty("CreditorAccountNumber", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string CreditorAccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the creditor address line 1.
        /// </summary>
        [JsonProperty("CreditorAddressLine1", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string CreditorAddressLine1 { get; set; }

        /// <summary>
        /// Gets or sets the creditor address line 2.
        /// </summary>
        [JsonProperty("CreditorAddressLine2", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string CreditorAddressLine2 { get; set; }

        /// <summary>
        /// Gets or sets the creditor address line 3.
        /// </summary>
        [JsonProperty("CreditorAddressLine3", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string CreditorAddressLine3 { get; set; }

        /// <summary>
        /// Gets or sets the creditor country code.
        /// </summary>
        [JsonProperty("CreditorCountryCode", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string CreditorCountryCode { get; set; }

        /// <summary>
        /// Gets or sets the creditor customer name.
        /// </summary>
        [JsonProperty("CreditorCustomerName", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string CreditorCustomerName { get; set; }

        /// <summary>
        /// Gets or sets the creditor account item details agent.
        /// </summary>
        [JsonProperty("CreditorAccountItemDetailsAgent", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string CreditorAccountItemDetailsAgent { get; set; }

        /// <summary>
        /// Gets or sets the creditor account item details agent account.
        /// </summary>
        [JsonProperty("CreditorAccountItemDetailsAgentAccount", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string CreditorAccountItemDetailsAgentAccount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is return.
        /// </summary>
        [JsonProperty("IsReturn", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool IsReturn { get; set; }

        /// <summary>
        /// Gets or sets the interest date.
        /// </summary>
        [JsonProperty("InterestDate", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public DateTime? InterestDate { get; set; }
    }
}
