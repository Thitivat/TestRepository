using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace BND.Services.Matrix.Entities
{
    /// <summary>
    /// The system account.
    /// </summary>
    [JsonObject("SystemAccount")]
    public class SystemAccount
    {
        /// <summary>
        /// Gets or sets the department id.
        /// </summary>
        [JsonProperty("DepartmentId", DefaultValueHandling = DefaultValueHandling.Include)]
        public int DepartmentId { get; set; }

        /// <summary>
        /// Gets or sets the department name.
        /// </summary>
        [JsonProperty("DepartmentName", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string DepartmentName { get; set; }

        /// <summary>
        /// Gets or sets the unit id.
        /// </summary>
        [JsonProperty("UnitId", DefaultValueHandling = DefaultValueHandling.Include)]
        public int UnitId { get; set; }

        /// <summary>
        /// Gets or sets the unit name.
        /// </summary>
        [JsonProperty("UnitName", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string UnitName { get; set; }

        /// <summary>
        /// Gets or sets the portfolioId.
        /// </summary>
        [JsonProperty("PortfolioId", DefaultValueHandling = DefaultValueHandling.Include)]
        public int PortfolioId { get; set; }

        /// <summary>
        /// Gets or sets the account numbers.
        /// </summary>
        [JsonProperty("AccountNumbers", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public Dictionary<string, string> AccountNumbers { get; set; }

        /// <summary>
        /// Gets or sets the feature id.
        /// </summary>
        [JsonProperty("FeatureId", DefaultValueHandling = DefaultValueHandling.Include)]
        public short FeatureId { get; set; }

        /// <summary>
        /// Gets or sets the ProductId.
        /// </summary>
        [JsonProperty("ProductId", DefaultValueHandling = DefaultValueHandling.Include)]
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the product name.
        /// </summary>
        [JsonProperty("ProductName", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets the gl account id.
        /// </summary>
        [JsonProperty("GlAccountId", DefaultValueHandling = DefaultValueHandling.Include)]
        public int GlAccountId { get; set; }

        /// <summary>
        /// Gets or sets the gl account name.
        /// </summary>
        [JsonProperty("GlAccountName", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string GlAccountName { get; set; }

        /// <summary>
        /// Gets or sets the accrued interest gl account id.
        /// </summary>
        [JsonProperty("AccruedInterestGlAccountId", DefaultValueHandling = DefaultValueHandling.Include)]
        public int AccruedInterestGlAccountId { get; set; }

        /// <summary>
        /// Gets or sets the accrued interest gl account name.
        /// </summary>
        [JsonProperty("AccruedInterestGlAccountName", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string AccruedInterestGlAccountName { get; set; }

        //ServiceItem properties

        /// <summary>
        /// Gets or sets the statement interval.
        /// </summary>
        [JsonProperty("StatementInterval", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string StatementInterval { get; set; }

        /// <summary>
        /// Gets or sets the transaction id.
        /// </summary>
        [JsonProperty("TransactionId", DefaultValueHandling = DefaultValueHandling.Include)]
        public long TransactionId { get; set; }

        /// <summary>
        /// Gets or sets the fee handling.
        /// </summary>
        [JsonProperty("FeeHandling", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string FeeHandling { get; set; }

        /// <summary>
        /// Gets or sets the value date.
        /// </summary>
        [JsonProperty("ValueDate", DefaultValueHandling = DefaultValueHandling.Include)]
        public DateTime ValueDate { get; set; }

        /// <summary>
        /// Gets or sets the tax exempt state.
        /// </summary>
        [JsonProperty("TaxExemptState", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string TaxExemptState { get; set; }

        //serviceLimit???

        /// <summary>
        /// Gets or sets the balance.
        /// </summary>
        [JsonProperty("Balance", DefaultValueHandling = DefaultValueHandling.Include)]
        public decimal Balance { get; set; }

        /// <summary>
        /// Gets or sets the available balance.
        /// </summary>
        [JsonProperty("AvailableBalance", DefaultValueHandling = DefaultValueHandling.Include)]
        public decimal AvailableBalance { get; set; }

        /// <summary>
        /// Gets or sets the due fee amount.
        /// </summary>
        [JsonProperty("DueFeeAmount", DefaultValueHandling = DefaultValueHandling.Include)]
        public decimal DueFeeAmount { get; set; }

        /// <summary>
        /// Gets or sets the service state.
        /// </summary>
        [JsonProperty("ServiceState", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string ServiceState { get; set; }

        /// <summary>
        /// Gets or sets the created.
        /// </summary>
        [JsonProperty("Created", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public DateTime? Created { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        [JsonProperty("Currency", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string Currency { get; set; }
    }
}
