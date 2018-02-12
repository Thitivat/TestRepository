using Newtonsoft.Json;

namespace BND.Services.Matrix.Entities
{
    /// <summary>
    /// The message file content item entity
    /// </summary>
    [JsonObject("MessageFileContentItem")]
    public class MessageFileContentItem
    {
        /// <summary>
        /// Gets or sets the message file content item message id
        /// </summary>
        [JsonProperty("MessageId", DefaultValueHandling = DefaultValueHandling.Include)]
        public int MessageId { get; set; }

        /// <summary>
        /// Gets or sets the message file content item ref id
        /// </summary>
        [JsonProperty("RefId", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string RefId { get; set; }

        /// <summary>
        /// Gets or sets the message file content item name
        /// </summary>
        [JsonProperty("Name", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the message file content item size
        /// </summary>
        [JsonProperty("Size", DefaultValueHandling = DefaultValueHandling.Include)]
        public int Size { get; set; }

        /// <summary>
        /// Gets or sets the message file content item content
        /// </summary>
        [JsonProperty("Content", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string Content { get; set; }
    }
}
