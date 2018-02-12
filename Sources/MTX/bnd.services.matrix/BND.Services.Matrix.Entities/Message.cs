using BND.Services.Matrix.Enums;

using Newtonsoft.Json;

namespace BND.Services.Matrix.Entities
{
    /// <summary>
    /// The message item entity
    /// </summary>
    [JsonObject("Message")]
    public class Message
    {
        /// <summary>
        /// Gets or sets the message item id
        /// </summary>
        [JsonProperty("Id", DefaultValueHandling = DefaultValueHandling.Include)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the message item file size
        /// </summary>
        [JsonProperty("OriginalMessageId", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public int? OriginalMessageId { get; set; }

        /// <summary>
        /// Gets or sets the message item direction.
        /// </summary>
        [JsonProperty("Direction", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public EnumMessageDirections? Direction { get; set; }

        /// <summary>
        /// Gets or sets the message item type.
        /// </summary>
        [JsonProperty("Type", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public EnumMessageTypes? Type { get; set; }

        /// <summary>
        /// Gets or sets the message item status.
        /// </summary>
        [JsonProperty("Status", DefaultValueHandling = DefaultValueHandling.Include)]
        public EnumMessageStatuses Status { get; set; }

        /// <summary>
        /// Gets or sets the message item status details
        /// </summary>
        [JsonProperty("StatusDetails", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string StatusDetails { get; set; }

        /// <summary>
        /// Gets or sets the message item sepa file level code
        /// </summary>
        [JsonProperty("SepaFileLevelCode", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string SepaFileLevelCode { get; set; }

        /// <summary>
        /// Gets or sets the message item sepa file level name
        /// </summary>
        [JsonProperty("SepaFileLevelName", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string SepaFileLevelName { get; set; }

        /// <summary>
        /// Gets or sets the message item amount
        /// </summary>
        [JsonProperty("Amount", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public decimal? Amount { get; set; }

        /// <summary>
        /// Gets or sets the message item number of transactions
        /// </summary>
        [JsonProperty("NumberOfTransactions", DefaultValueHandling = DefaultValueHandling.Include)]
        public int NumberOfTransactions { get; set; }

        /// <summary>
        /// Gets or sets the message item file ref id
        /// </summary>
        [JsonProperty("FileRefId", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string FileRefId { get; set; }

        /// <summary>
        /// Gets or sets the message item file name
        /// </summary>
        [JsonProperty("FileName", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the message item file size
        /// </summary>
        [JsonProperty("FileSize", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public int? FileSize { get; set; }

        /// <summary>
        /// Gets or sets the message item currency
        /// </summary>
        [JsonProperty("Currency", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the message item entity info.
        /// </summary>
        [JsonProperty("EntityInfo", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public EntityInfoItem EntityInfo { get; set; }
    }
}
