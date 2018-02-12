using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace BND.Services.Matrix.Entities
{
    /// <summary>
    /// The interest rate override item.
    /// </summary>
    [JsonObject("InterestRate")]
    public class InterestRate
    {
        /// <summary>
        /// Gets or sets the interest rate override id field.
        /// </summary>
        [JsonProperty("InterestRateOverrideIdField", DefaultValueHandling = DefaultValueHandling.Include)]
        public int InterestRateOverrideIdField { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether interest rate override id field specified.
        /// </summary>
        [JsonProperty("InterestRateOverrideIdFieldSpecified", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool InterestRateOverrideIdFieldSpecified { get; set; }

        /// <summary>
        /// Gets or sets the account identifiers field.
        /// </summary>
        [JsonProperty("AccountIdentifiersField", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public List<AccountIdentifierItem> AccountIdentifiersField { get; set; }

        /// <summary>
        /// Gets or sets the gross rate field.
        /// </summary>
        [JsonProperty("GrossRateField", DefaultValueHandling = DefaultValueHandling.Include)]
        public decimal GrossRateField { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gross rate field specified.
        /// </summary>
        [JsonProperty("GrossRateFieldSpecified", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool GrossRateFieldSpecified { get; set; }

        /// <summary>
        /// Gets or sets the valid from date field.
        /// </summary>
        [JsonProperty("ValidFromDateField", DefaultValueHandling = DefaultValueHandling.Include)]
        public DateTime ValidFromDateField { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether valid from date field specified.
        /// </summary>
        [JsonProperty("ValidFromDateFieldSpecified", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool ValidFromDateFieldSpecified { get; set; }

        /// <summary>
        /// Gets or sets the end override date field.
        /// </summary>
        [JsonProperty("EndOverrideDateField", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public DateTime? EndOverrideDateField { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether end override date field specified.
        /// </summary>
        [JsonProperty("EndOverrideDateFieldSpecified", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool EndOverrideDateFieldSpecified { get; set; }

        /// <summary>
        /// Gets or sets the entity info field.
        /// </summary>
        [JsonProperty("EntityInfoField", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public EntityInfoItem EntityInfoField { get; set; }
    }
}
