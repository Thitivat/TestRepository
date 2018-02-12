using Newtonsoft.Json;

namespace BND.Services.Matrix.Entities
{
    /// <summary>
    /// The operation type item entity
    /// </summary>
    [JsonObject("OperationType")]
    public class OperationType
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [JsonProperty("Id", DefaultValueHandling = DefaultValueHandling.Include)]
        public short Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether id specified.
        /// </summary>
        [JsonProperty("IdSpecified", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool IdSpecified { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [JsonProperty("Name", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [JsonProperty("Description", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string Description { get; set; }
    }
}
