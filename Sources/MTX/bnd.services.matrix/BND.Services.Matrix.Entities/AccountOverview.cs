using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace BND.Services.Matrix.Entities
{
    /// <summary>
    /// The account overview item.
    /// </summary>
    [JsonObject("AccountOverview")]
    public class AccountOverview
    {
        /// <summary>
        /// Gets or sets the balance
        /// </summary>
        [JsonProperty("Balance", DefaultValueHandling = DefaultValueHandling.Include)]
        public decimal Balance { get; set; }

        /// <summary>
        /// Gets or sets the available balance
        /// </summary>
        [JsonProperty("AvailableBalance", DefaultValueHandling = DefaultValueHandling.Include)]
        public decimal AvailableBalance { get; set; }

        /// <summary>
        /// Gets or sets the product id.
        /// </summary>
        [JsonProperty("ProductId", DefaultValueHandling = DefaultValueHandling.Include)]
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the product name.
        /// </summary>
        [JsonProperty("ProductName", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets the gross interest.
        /// </summary>
        [JsonProperty("GrossInterest", DefaultValueHandling = DefaultValueHandling.Include)]
        public decimal GrossInterest { get; set; }

        /// <summary>
        /// Gets or sets the gross interest rate.
        /// </summary>
        [JsonProperty("GrossInterestRate", DefaultValueHandling = DefaultValueHandling.Include)]
        public decimal GrossInterestRate { get; set; }

        /// <summary>
        /// Gets or sets the total balance.
        /// </summary>
        [JsonProperty("TotalBalance", DefaultValueHandling = DefaultValueHandling.Include)]
        public decimal TotalBalance { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        [JsonProperty("CreatedDate", DefaultValueHandling = DefaultValueHandling.Include)]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the first movement date.
        /// </summary>
        [JsonProperty("FirstMovementDate", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public DateTime? FirstMovementDate { get; set; }

        /// <summary>
        /// Gets or sets the Next Compounding Date.
        /// </summary>
        [JsonProperty("NextCompoundingDate", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public DateTime? NextCompoundingDate { get; set; }

        /// <summary>
        /// Gets or sets the Interest Period Start Date.
        /// </summary>
        [JsonProperty("InterestPeriodStartDate", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public DateTime? InterestPeriodStartDate { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        [JsonProperty("Currency", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the Service State.
        /// </summary>
        [JsonProperty("ServiceState", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string ServiceState { get; set; }

        /// <summary>
        /// Gets or sets the Fee Handling.
        /// </summary>
        [JsonProperty("FeeHandling", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string FeeHandling { get; set; }

        /// <summary>
        /// Gets or sets the Tax Exempt State.
        /// </summary>
        [JsonProperty("TaxExemptState", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string TaxExemptState { get; set; }

        /// <summary>
        /// Gets or sets the Due Fee Amount.
        /// </summary>
        [JsonProperty("DueFeeAmount", DefaultValueHandling = DefaultValueHandling.Include)]
        public decimal DueFeeAmount { get; set; }

        /// <summary>
        /// Gets or sets the Portfolio Id.
        /// </summary>
        [JsonProperty("PortfolioId", DefaultValueHandling = DefaultValueHandling.Include)]
        public int PortfolioId { get; set; }

        /// <summary>
        /// Gets or sets the External Portfolio Id.
        /// </summary>
        [JsonProperty("ExternalPortfolioId", DefaultValueHandling = DefaultValueHandling.Include)]
        public int ExternalPortfolioId { get; set; }

        /// <summary> 
        /// Gets or sets the Is First Payment Processed
        /// </summary>
        [JsonProperty("IsFirstPaymentProcessed", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool IsFirstPaymentProcessed { get; set; }

        /// <summary>
        /// Gets or sets the service state history.
        /// </summary>
        [JsonProperty("ServiceStateHistory", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public List<ServiceStateItem> ServiceStateHistory { get; set; }

        /// <summary>
        /// Gets or sets the nominated accounts.
        /// </summary>
        [JsonProperty("NominatedAccounts", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public List<NominatedAccountItem> NominatedAccounts { get; set; }

        /// <summary>
        /// Gets or sets the Account Identifiers.
        /// </summary>
        [JsonProperty("AccountIdentifiers", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public List<AccountIdentifierItem> AccountIdentifiers { get; set; }

        /// <summary>
        /// Gets or sets the Linked Account Identifiers.
        /// </summary>
        [JsonProperty("LinkedAccountIdentifiers", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public List<AccountIdentifierItem> LinkedAccountIdentifiers { get; set; }

        /// <summary>
        /// Gets or sets the Product History.
        /// </summary>
        [JsonProperty("ProductHistory", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public List<ProductHistoryItem> ProductHistory { get; set; }
    }
}
