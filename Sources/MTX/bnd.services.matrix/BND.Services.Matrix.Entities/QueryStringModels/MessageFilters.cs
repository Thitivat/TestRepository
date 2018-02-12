using System;

using BND.Services.Matrix.Entities.Interfaces;
using BND.Services.Matrix.Enums;

using Newtonsoft.Json;

namespace BND.Services.Matrix.Entities.QueryStringModels
{
    /// <summary>
    /// The message filters.
    /// </summary>
    [JsonObject("MessageFilters")]
    public class MessageFilters : IQueryStringModel
    {
        /*/// <summary>
        /// Initializes a new instance of the <see cref="MessageFilters"/> class.
        /// </summary>
        public MessageFilters()
        {
            Direction = new List<EnumMessageItemDirection>();
            Status = new List<EnumMessageItemStatus>();
            Type = new List<EnumMessageItemType>();
        }*/

        /// <summary>
        /// Gets or sets the from date.
        /// </summary>
        [JsonProperty("From", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public DateTime? From { get; set; }

        /// <summary>
        /// Gets or sets the to date.
        /// </summary>
        [JsonProperty("To", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public DateTime? To { get; set; }

        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        [JsonProperty("Direction", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public EnumMessageDirections? Direction { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        [JsonProperty("Status", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public EnumMessageStatuses? Status { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        [JsonProperty("Type", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public EnumMessageTypes? Type { get; set; }
    }
}
