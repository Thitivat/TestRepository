using Newtonsoft.Json;

namespace BND.Services.Matrix.Entities
{
    /// <summary>
    /// The payment customer item.
    /// </summary>
    [JsonObject("PaymentCustomerItem")]
    public class PaymentCustomerItem
    {
        /// <summary>
        /// Gets or sets the customer name.
        /// </summary>
        [JsonProperty("CustomerName", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string CustomerName { get; set; }

        /// <summary>
        /// Gets or sets the address line 1.
        /// </summary>
        [JsonProperty("AddressLine1", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string AddressLine1 { get; set; }

        /// <summary>
        /// Gets or sets the address line 2.
        /// </summary>
        [JsonProperty("AddressLine2", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string AddressLine2 { get; set; }

        /// <summary>
        /// Gets or sets the address line 3.
        /// </summary>
        [JsonProperty("AddressLine3", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string AddressLine3 { get; set; }

        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        [JsonProperty("CountryCode", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string CountryCode { get; set; }
    }
}
