using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace BND.Services.Matrix.Entities
{
    /// <summary>
    /// The movement over view item.
    /// </summary>
    [JsonObject("MovementOverviewItem")]
    public class MovementOverviewItem
    {
        /// <summary>
        /// Gets or sets the from date.
        /// </summary>
        [JsonProperty("FromDate", DefaultValueHandling = DefaultValueHandling.Include)]
        public DateTime FromDate { get; set; }

        /// <summary>
        /// Gets or sets the to date.
        /// </summary>
        [JsonProperty("ToDate", DefaultValueHandling = DefaultValueHandling.Include)]
        public DateTime ToDate { get; set; }

        /// <summary>
        /// Gets or sets the movement items.
        /// </summary>
        [JsonProperty("Payments", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public List<MovementItem> Payments { get; set; }
    }
}
