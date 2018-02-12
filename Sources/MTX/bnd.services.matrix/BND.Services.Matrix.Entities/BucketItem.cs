
using System;
using System.Collections.Generic;

using BND.Services.Matrix.Enums;

using Newtonsoft.Json;

namespace BND.Services.Matrix.Entities
{
    /// <summary>
    /// The bucket item
    /// </summary>
    [JsonObject("BucketItem")]
    public class BucketItem
    {
        /// <summary>
        /// Gets or sets the created.
        /// </summary>
        [JsonProperty("Created", DefaultValueHandling = DefaultValueHandling.Include)]
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the bucket type.
        /// </summary>
        [JsonProperty("BucketType", DefaultValueHandling = DefaultValueHandling.Include)]
        public EnumBucketItemTypes BucketType { get; set; }

        /// <summary>
        /// Gets or sets the group status.
        /// </summary>
        [JsonProperty("GroupStatus", DefaultValueHandling = DefaultValueHandling.Include)]
        public EnumBucketItemStatuses GroupStatus { get; set; }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        [JsonProperty("Source", DefaultValueHandling = DefaultValueHandling.Include)]
        public EnumBucketItemSources Source { get; set; }

        /// <summary>
        /// Gets or sets the operation code.
        /// </summary>
        [JsonProperty("OperationCode", DefaultValueHandling = DefaultValueHandling.Include)]
        public EnumBucketItemOperations OperationCode { get; set; }

        /// <summary>
        /// Gets or sets the bic.
        /// </summary>
        [JsonProperty("Bic", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string Bic { get; set; }

        /// <summary>
        /// Gets or sets the value date.
        /// </summary>
        [JsonProperty("ValueDate", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public DateTime? ValueDate { get; set; }

        /// <summary>
        /// Gets or sets the return id.
        /// </summary>
        [JsonProperty("Id", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the payment buckets.
        /// </summary>
        [JsonProperty("PaymentBuckets", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public List<PaymentBucketItem> PaymentBuckets { get; set; }
    }
}
