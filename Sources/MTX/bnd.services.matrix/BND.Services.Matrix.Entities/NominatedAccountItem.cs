
using Newtonsoft.Json;

namespace BND.Services.Matrix.Entities
{
    /// <summary>
    /// The nominated account.
    /// </summary>
    [JsonObject("NominatedAccountItem")]
    public class NominatedAccountItem
    {
        /// <summary>
        /// Gets or sets the nominated account number.
        /// </summary>
        [JsonProperty("NominatedAccountNumber", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string NominatedAccountNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is internal.
        /// </summary>
        [JsonProperty("IsInternal", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool IsInternal { get; set; }
    }
}
