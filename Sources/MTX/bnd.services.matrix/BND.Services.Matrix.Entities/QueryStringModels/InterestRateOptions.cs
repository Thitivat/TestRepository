using System;

using BND.Services.Matrix.Entities.Interfaces;

using Newtonsoft.Json;

namespace BND.Services.Matrix.Entities.QueryStringModels
{
    /// <summary>
    /// The interest rate options.
    /// </summary>
    [JsonObject("InterestRateOptions")]
    public class InterestRateOptions : IQueryStringModel
    {
        /// <summary>
        /// Gets or sets the from date.
        /// </summary>
        [JsonProperty("FromDate", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// Gets or sets the end override date.
        /// </summary>
        [JsonProperty("EndOverrideDate", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public DateTime? EndOverrideDate { get; set; }
    }
}
