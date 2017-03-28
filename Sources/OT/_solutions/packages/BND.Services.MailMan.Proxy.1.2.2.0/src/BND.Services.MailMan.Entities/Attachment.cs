using Newtonsoft.Json;

namespace BND.Services.MailMan.Entities
{
    /// <summary>
    /// The attachment entity.
    /// </summary>
    [JsonObject("Attachment entity")]
    public class Attachment
    {
        /// <summary>
        /// Gets or sets the file name.
        /// </summary>
        [JsonProperty("Name", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        [JsonProperty("Data", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string Data { get; set; }
    }
}
