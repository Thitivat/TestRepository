
using System;

using Newtonsoft.Json;

namespace BND.Services.Matrix.Entities
{
    /// <summary>
    /// The service state.
    /// </summary>
    [JsonObject("ServiceStateItem")]
    public class ServiceStateItem
    {
        /// <summary>
        /// Gets or sets the valid from date
        /// </summary>
        [JsonProperty("ValidFromDate", DefaultValueHandling = DefaultValueHandling.Include)]
        public DateTime ValidFromDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether valid from date specified
        /// </summary>
        [JsonProperty("ValidFromDateSpecified", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool ValidFromDateSpecified { get; set; }

        /// <summary>
        /// Gets or sets the service state
        /// </summary>
        [JsonProperty("ServiceState", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string ServiceState { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the service state is specified
        /// </summary>
        [JsonProperty("ServiceStateSpecified", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool ServiceStateSpecified { get; set; }

        /// <summary>
        /// Gets or sets the service state name
        /// </summary>
        [JsonProperty("ServiceStateName", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string ServiceStateName { get; set; }
    }
}
