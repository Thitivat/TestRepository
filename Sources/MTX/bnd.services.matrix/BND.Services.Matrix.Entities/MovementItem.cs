using System;

using Newtonsoft.Json;

namespace BND.Services.Matrix.Entities
{
    /// <summary>
    /// The movement item entity
    /// </summary>
    [JsonObject("MovementItem")]
    public class MovementItem
    {
        /// <summary>
        /// Gets or sets the movement id.
        /// </summary>
        [JsonProperty("MovementId",  DefaultValueHandling = DefaultValueHandling.Include)]
        public int MovementId { get; set; }

        /* /// <summary>
        /// Gets or sets a value indicating whether movement id specified.
        /// </summary>
        [JsonProperty("MovementIdSpecified", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public bool MovementIdSpecified { get; set; }*/

        /// <summary>
        /// Gets or sets the system date.
        /// </summary>
        [JsonProperty("SystemDate", DefaultValueHandling = DefaultValueHandling.Include)]
        public DateTime SystemDate { get; set; }

        /* /// <summary>
        /// Gets or sets a value indicating whether system date specified.
        /// </summary>
        [JsonProperty("SystemDateSpecified", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public bool SystemDateSpecified { get; set; }*/

        /// <summary>
        /// Gets or sets the value date.
        /// </summary>
        [JsonProperty("ValueDate", DefaultValueHandling = DefaultValueHandling.Include)]
        public DateTime ValueDate { get; set; }

     /*   /// <summary>
        /// Gets or sets a value indicating whether value date specified.
        /// </summary>
        [JsonProperty("ValueDateSpecified", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public bool ValueDateSpecified { get; set; }*/

        /// <summary>
        /// Gets or sets the account from.
        /// </summary>
        [JsonProperty("AccountFrom", DefaultValueHandling = DefaultValueHandling.Include)]
        public string AccountFrom { get; set; }

        /// <summary>
        /// Gets or sets the account to.
        /// </summary>
        [JsonProperty("AccountTo", DefaultValueHandling = DefaultValueHandling.Include)]
        public string AccountTo { get; set; }

        /// <summary>
        /// Gets or sets the payment type.
        /// </summary>
        [JsonProperty("PaymentType", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string PaymentType { get; set; }

        /// <summary>
        /// Gets or sets the reference.
        /// </summary>
        [JsonProperty("Reference", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string Reference { get; set; }

        /// <summary>
        /// Gets or sets the clarification.
        /// </summary>
        [JsonProperty("Clarification", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string Clarification { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        [JsonProperty("Amount", DefaultValueHandling = DefaultValueHandling.Include)]
        public decimal Amount { get; set; }

      /*  /// <summary>
        /// Gets or sets a value indicating whether amount specified.
        /// </summary>
        [JsonProperty("AmountSpecified", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public bool AmountSpecified { get; set; }*/

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        [JsonProperty("Currency", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the balance after.
        /// </summary>
        [JsonProperty("BalanceAfter", DefaultValueHandling = DefaultValueHandling.Include)]
        public decimal BalanceAfter { get; set; }

     /*   /// <summary>
        /// Gets or sets a value indicating whether balance after specified.
        /// </summary>
        [JsonProperty("BalanceAfterSpecified", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public bool BalanceAfterSpecified { get; set; }*/

        /// <summary>
        /// Gets or sets the cash flow item id.
        /// </summary>
        [JsonProperty("CashFlowItemId", DefaultValueHandling = DefaultValueHandling.Include)]
        public int CashFlowItemId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether cash flow item id specified.
        /// </summary>
        [JsonProperty("CashFlowItemIdSpecified", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool CashFlowItemIdSpecified { get; set; }

        /// <summary>
        /// Gets or sets the transaction id.
        /// </summary>
        [JsonProperty("TransactionId", DefaultValueHandling = DefaultValueHandling.Include)]
        public long TransactionId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether transaction id specified.
        /// </summary>
        [JsonProperty("TransactionIdSpecified", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool TransactionIdSpecified { get; set; }

        /// <summary>
        /// Gets or sets the operation id.
        /// </summary>
        [JsonProperty("OperationId", DefaultValueHandling = DefaultValueHandling.Include)]
        public long OperationId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether operation id specified.
        /// </summary>
        [JsonProperty("OperationIdSpecified", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool OperationIdSpecified { get; set; }

        /// <summary>
        /// Gets or sets the operation type.
        /// </summary>
        [JsonProperty("OperationType", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public OperationType OperationType { get; set; }

        /// <summary>
        /// Gets or sets the origin.
        /// </summary>
        [JsonProperty("Origin", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public Origin Origin { get; set; }

        /// <summary>
        /// Gets or sets the movement state.
        /// </summary>
        [JsonProperty("MovementState", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string MovementState { get; set; } // For MovementStates enum

        /// <summary>
        /// Gets or sets a value indicating whether movement state specified.
        /// </summary>
        [JsonProperty("MovementStateSpecified", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool MovementStateSpecified { get; set; }

        /// <summary>
        /// Gets or sets the movement state comment.
        /// </summary>
        [JsonProperty("MovementStateComment", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string MovementStateComment { get; set; }

        /// <summary>
        /// Gets or sets the movement state operation id.
        /// </summary>
        [JsonProperty("MovementStateOperationId", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public long? MovementStateOperationId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether movement state operation id specified.
        /// </summary>
        [JsonProperty("MovementStateOperationIdSpecified", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool MovementStateOperationIdSpecified { get; set; }

        /// <summary>
        /// Gets or sets the movement state system date.
        /// </summary>
        [JsonProperty("MovementStateSystemDate", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public DateTime? MovementStateSystemDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether movement state system date specified.
        /// </summary>
        [JsonProperty("MovementStateSystemDateSpecified", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool MovementStateSystemDateSpecified { get; set; }

        /// <summary>
        /// Gets or sets the Debtor.
        /// </summary>
        [JsonProperty("Debitor", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public MovementCustomerItem Debitor { get; set; }

        /// <summary>
        /// Gets or sets the origin.
        /// </summary>
        [JsonProperty("Creditor", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public MovementCustomerItem Creditor { get; set; }
    }
}
