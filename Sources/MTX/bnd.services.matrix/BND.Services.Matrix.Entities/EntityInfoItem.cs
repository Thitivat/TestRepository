using Newtonsoft.Json;

namespace BND.Services.Matrix.Entities
{
    /// <summary>
    /// The entity info item.
    /// </summary>
    [JsonObject("EntityInfoItem")]
    public class EntityInfoItem
    {
        /// <summary>
        /// Gets or sets the created field.
        /// </summary>
        [JsonProperty("Created", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public EventDateItem Created { get; set; }

        /// <summary>
        /// Gets or sets the changed field.
        /// </summary>
        [JsonProperty("Changed", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public EventDateItem Changed { get; set; }
    }
}
