
namespace BND.Services.Matrix.Entities
{
    using System;

    using Newtonsoft.Json;

    /// <summary>
    /// The product history item.
    /// </summary>
    [JsonObject("ProductHistoryItem")]
    public class ProductHistoryItem
    {
        /// <summary>
        /// Gets or sets the Valid From date.
        /// </summary>
        [JsonProperty("ValidFromDate", DefaultValueHandling = DefaultValueHandling.Include)]
        public DateTime ValidFromDate { get; set; }

        /// <summary>
        /// Gets or sets the Product Id.
        /// </summary>
        [JsonProperty("ProductId", DefaultValueHandling = DefaultValueHandling.Include)]
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the Product name.
        /// </summary>
        [JsonProperty("ProductName", DefaultValueHandling = DefaultValueHandling.Include, NullValueHandling = NullValueHandling.Include)]
        public string ProductName { get; set; }
    }
}
