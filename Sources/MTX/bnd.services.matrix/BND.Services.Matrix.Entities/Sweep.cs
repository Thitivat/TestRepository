using System;

using Newtonsoft.Json;

namespace BND.Services.Matrix.Entities
{
    /// <summary>
    /// The sweep.
    /// </summary>
    [JsonObject("Sweep")]
    public class Sweep
    {
        /// <summary>
        /// Gets or sets the Source field.
        /// </summary>
        [JsonProperty("Source", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string Source { get; set; }

        /// <summary>
        /// Gets or sets the Destination field.
        /// </summary>
        [JsonProperty("Destination", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string Destination { get; set; }

        /// <summary>
        /// Gets or sets the amount field.
        /// </summary>
        [JsonProperty("Amount", DefaultValueHandling = DefaultValueHandling.Include)]
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the ValueDate field.
        /// </summary>
        [JsonProperty("ValueDate", DefaultValueHandling = DefaultValueHandling.Include)]
        public DateTime ValueDate { get; set; }

        /// <summary>
        /// Gets or sets the Description field.
        /// </summary>
        [JsonProperty("Description", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string Description { get; set; }
    }
}
