using Newtonsoft.Json;

namespace BND.Services.Matrix.Entities
{
    /// <summary>
    /// The ServiceInfo entity
    /// </summary>
    [JsonObject("ServiceInfo")]
    public class ServiceInfo
    {
        /// <summary>
        /// Gets or sets the request uri.
        /// </summary>
        [JsonProperty("RequestUri", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string RequestUri { get; set; }

        /// <summary>
        /// Gets or sets the method.
        /// </summary>
        [JsonProperty("Method", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string Method { get; set; }

        /// <summary>
        /// Gets or sets the host name.
        /// </summary>
        [JsonProperty("HostName", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string HostName { get; set; }

        /// <summary>
        /// Gets or sets the host ip.
        /// </summary>
        [JsonProperty("HostIP", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string HostIP { get; set; }

        /// <summary>
        /// Gets or sets the client ip.
        /// </summary>
        [JsonProperty("ClientIP", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string ClientIP { get; set; }
    }
}
