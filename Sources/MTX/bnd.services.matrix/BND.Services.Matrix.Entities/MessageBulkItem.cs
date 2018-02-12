using System.Collections.Generic;

using Newtonsoft.Json;

namespace BND.Services.Matrix.Entities
{
    /// <summary>
    /// The message bulk item entity
    /// </summary>
    [JsonObject("MessageBulkItem")]
    public class MessageBulkItem
    {
        /// <summary>
        /// Gets or sets the message bulk item id
        /// </summary>
        [JsonProperty("Id", DefaultValueHandling = DefaultValueHandling.Include)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the message bulk item message id
        /// </summary>
        [JsonProperty("MsgId", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string MsgId { get; set; }

        /// <summary>
        /// Gets or sets the message bulk item Sepa Bulk Error Code
        /// </summary>
        [JsonProperty("SepaBulkErrorCode", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string SepaBulkErrorCode { get; set; }

        /// <summary>
        /// Gets or sets the message bulk item Sepa Bulk Error Name
        /// </summary>
        [JsonProperty("SepaBulkErrorName", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string SepaBulkErrorName { get; set; }

        /// <summary>
        /// Gets or sets the message bulk item amount
        /// </summary>
        [JsonProperty("Amount", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public decimal? Amount { get; set; }

        /// <summary>
        /// Gets or sets the message bulk item number of transactions
        /// </summary>
        [JsonProperty("NumberOfTransactions", DefaultValueHandling = DefaultValueHandling.Include)]
        public int NumberOfTransactions { get; set; }

        /// <summary>
        /// Gets or sets the message bulk item transactions
        /// </summary>
        [JsonProperty("Transactions", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public List<MessageBulkTransactionItem> Transactions { get; set; }

        /// <summary>
        /// Gets or sets the message bulk item entity info
        /// </summary>
        [JsonProperty("EntityInfo", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public EntityInfoItem EntityInfo { get; set; }
    }
}
