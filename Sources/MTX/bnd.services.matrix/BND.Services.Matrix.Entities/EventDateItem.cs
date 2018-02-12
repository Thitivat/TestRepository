using System;

using Newtonsoft.Json;

namespace BND.Services.Matrix.Entities
{
    /// <summary>
    /// The event date item.
    /// </summary>
    [JsonObject("EventDateItem")]
    public class EventDateItem
    {
        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        [JsonProperty("UserId", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public int? UserId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether user id field specified.
        /// </summary>
        [JsonProperty("UserIdSpecified", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool UserIdSpecified { get; set; }

        /// <summary>
        /// Gets or sets the user name field.
        /// </summary>
        [JsonProperty("UserName", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the event date field.
        /// </summary>
        [JsonProperty("EventDate", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public DateTime? EventDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether event date field specified.
        /// </summary>
        [JsonProperty("EventDateSpecified", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool EventDateSpecified { get; set; }
    }
}
