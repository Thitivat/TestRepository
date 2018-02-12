using System.Collections.Generic;

using Newtonsoft.Json;

namespace BND.Services.Matrix.Entities
{
    /// <summary>
    /// The message detail entity
    /// </summary>
    [JsonObject("MessageDetail")]
    public class MessageDetail : Message
    {
        /// <summary>
        /// Gets or sets the message detail center id
        /// </summary>
        [JsonProperty("CenterId", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public int? CenterId { get; set; }

        /// <summary>
        /// Gets or sets the message detail message file content item
        /// </summary>
        [JsonProperty("FileContent", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public MessageFileContentItem FileContent { get; set; }

        /// <summary>
        /// Gets or sets the message detail bulks
        /// </summary>
        [JsonProperty("Bulks", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public List<MessageBulkItem> Bulks { get; set; }
    }
}
