using Newtonsoft.Json;

namespace BND.Services.Matrix.Entities
{
    /// <summary>
    /// The account identifier item.
    /// </summary>
    [JsonObject("AccountIdentifierItem")]
    public class AccountIdentifierItem
    {
        /// <summary>
        /// Gets or sets the identification standard field.
        /// </summary>
        [JsonProperty("IdentificationStandardField", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string IdentificationStandardField { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether identification standard field specified.
        /// </summary>
        [JsonProperty("IdentificationStandardFieldSpecified", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool IdentificationStandardFieldSpecified { get; set; }

        /// <summary>
        /// Gets or sets the identifier field.
        /// </summary>
        [JsonProperty("IdentifierField", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string IdentifierField { get; set; }
    }
}
