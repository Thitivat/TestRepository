
using System.Collections.Generic;

using BND.Services.Matrix.Enums;

using Newtonsoft.Json;

namespace BND.Services.Matrix.Entities
{
    /// <summary>
    /// The return bucket item
    /// </summary>
    [JsonObject("ReturnBucketItem")]
    public class ReturnBucketItem
    {
        /// <summary>
        /// The source.
        /// </summary>
        [JsonProperty("Source", DefaultValueHandling = DefaultValueHandling.Include)]
        public EnumBucketItemSources Source { get; set; }

        /// <summary>
        /// The bank operation code.
        /// </summary>
        [JsonProperty("OperationCode", DefaultValueHandling = DefaultValueHandling.Include)]
        public EnumBucketItemOperations OperationCode { get; set; }

        /// <summary>
        /// The Return Bucket Id.
        /// </summary>
        [JsonProperty("ReturnBucketId", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string ReturnBucketId { get; set; }

        /// <summary>
        /// The Return Bucket Row Items
        /// </summary>
        [JsonProperty("ReturnBucketRowItems", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public List<ReturnBucketRowItem> ReturnBucketRowItems { get; set; }
    }
}
