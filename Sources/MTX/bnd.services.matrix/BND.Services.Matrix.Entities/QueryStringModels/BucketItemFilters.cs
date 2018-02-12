using System;

using BND.Services.Matrix.Entities.Interfaces;
using BND.Services.Matrix.Enums;

using Newtonsoft.Json;

namespace BND.Services.Matrix.Entities.QueryStringModels
{
    /// <summary>
    /// The bucket item filter.
    /// </summary>
    public class BucketItemFilters : IQueryStringModel
    {
        /// <summary>
        /// Gets or sets the created.
        /// </summary>
        [JsonProperty("From", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public DateTime? From { get; set; }

        /// <summary>
        /// Gets or sets the created.
        /// </summary>
        [JsonProperty("To", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public DateTime? To { get; set; }

        /// <summary>
        /// Gets or sets the bucket type.
        /// </summary>
        [JsonProperty("Type", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public EnumBucketItemTypes? Type { get; set; }

        /// <summary>
        /// Gets or sets the group status.
        /// </summary>
        [JsonProperty("Status", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public EnumBucketItemStatuses? Status { get; set; }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        [JsonProperty("Source", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public EnumBucketItemSources? Source { get; set; }

        /// <summary>
        /// Gets or sets the operation code.
        /// </summary>
        [JsonProperty("Operation", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public EnumBucketItemOperations? Operation { get; set; }
    }
}
