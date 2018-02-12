using Newtonsoft.Json;

namespace BND.Services.Matrix.Entities
{
    /// <summary>
    /// The savings free.
    /// </summary>
    [JsonObject("SavingsFree")]
    public class SavingsFree
    {
        /*/// <summary>
        /// Gets or sets the product id.
        /// </summary>
        [JsonProperty("ProductId", DefaultValueHandling = DefaultValueHandling.Include)]
        public int ProductId { get; set; }*/

        /// <summary>
        /// Gets or sets the department name.
        /// </summary>
        [JsonProperty("DepartmentName", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string DepartmentName { get; set; }

        /// <summary>
        /// Gets or sets the unit name.
        /// </summary>
        [JsonProperty("UnitName", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string UnitName { get; set; }

        /// <summary>
        /// Gets or sets the iban.
        /// </summary>
        [JsonProperty("Iban", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string Iban { get; set; }

        /// <summary>
        /// Gets or sets the nominated iban.
        /// </summary>
        [JsonProperty("NominatedIban", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string NominatedIban { get; set; }
    }
}
