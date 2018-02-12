using Newtonsoft.Json;

namespace BND.Services.Matrix.Entities
{
    /// <summary>
    /// The origin item entity
    /// </summary>
    [JsonObject("Origin")]
    public class Origin
    {
        /// <summary>
        /// Gets or sets the account number.
        /// </summary>
        [JsonProperty("AccountNumber", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string AccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [JsonProperty("Name", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        [JsonProperty("CountryCode", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string CountryCode { get; set; } // For CountryCodes enum

        /// <summary>
        /// Gets or sets a value indicating whether country code specified.
        /// </summary>
        [JsonProperty("CountryCodeSpecified", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool CountryCodeSpecified { get; set; }
    }
}
