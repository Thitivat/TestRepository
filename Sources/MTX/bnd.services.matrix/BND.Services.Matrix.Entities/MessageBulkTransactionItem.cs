using Newtonsoft.Json;

namespace BND.Services.Matrix.Entities
{
    /// <summary>
    /// The message bulk transaction item entity
    /// </summary>
    [JsonObject("MessageBulkTransactionItem")]
    public class MessageBulkTransactionItem
    {
        /// <summary>
        /// Gets or sets the message bulk transaction item id
        /// </summary>
        [JsonProperty("Id", DefaultValueHandling = DefaultValueHandling.Include)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the message bulk transaction item message bulk id
        /// </summary>
        [JsonProperty("MessageBulkId", DefaultValueHandling = DefaultValueHandling.Include)]
        public int MessageBulkId { get; set; }

        /// <summary>
        /// Gets or sets the message bulk transaction item movement id
        /// </summary>
        [JsonProperty("MovementId", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public int? MovementId { get; set; }

        /// <summary>
        /// Gets or sets the message bulk transaction item bucket id
        /// </summary>
        [JsonProperty("BucketId", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string BucketId { get; set; }

        /// <summary>
        /// Gets or sets the message bulk transaction item bucket row id
        /// </summary>
        [JsonProperty("BucketRowId", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public int? BucketRowId { get; set; }

        /// <summary>
        /// Gets or sets the message bulk transaction item transaction id
        /// </summary>
        [JsonProperty("TrxId", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string TrxId { get; set; }

        /// <summary>
        /// Gets or sets the message bulk transaction item return transaction id
        /// </summary>
        [JsonProperty("ReturnTrxId", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string ReturnTrxId { get; set; }

        /// <summary>
        /// Gets or sets the message bulk transaction item return message id
        /// </summary>
        [JsonProperty("ReturnMessageId", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public int? ReturnMessageId { get; set; }

        /// <summary>
        /// Gets or sets the message bulk transaction item return file size
        /// </summary>
        [JsonProperty("ReturnFileSize", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public int? ReturnFileSize { get; set; }

        /// <summary>
        /// Gets or sets the message bulk transaction item return file ref id
        /// </summary>
        [JsonProperty("ReturnFileRefId", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string ReturnFileRefId { get; set; }

        /// <summary>
        /// Gets or sets the message bulk transaction item return file name
        /// </summary>
        [JsonProperty("ReturnFileName", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string ReturnFileName { get; set; }

        /// <summary>
        /// Gets or sets the message bulk transaction item amount
        /// </summary>
        [JsonProperty("Amount", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public decimal? Amount { get; set; }

        /// <summary>
        /// Gets or sets the message bulk transaction item sepa transaction level error code
        /// </summary>
        [JsonProperty("SepaTransactionLevelErrorCode", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string SepaTransactionLevelErrorCode { get; set; }

        /// <summary>
        /// Gets or sets the message bulk transaction item sepa transaction level error name
        /// </summary>
        [JsonProperty("SepaTransactionLevelErrorName", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string SepaTransactionLevelErrorName { get; set; }

        /// <summary>
        /// Gets or sets the message bulk transaction item entity info
        /// </summary>
        [JsonProperty("EntityInfo", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public EntityInfoItem EntityInfo { get; set; }
    }
}
