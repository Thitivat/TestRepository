using System.Collections.Generic;
using System.Linq;

using BND.Services.Infrastructure.Common.Extensions;

namespace BND.Services.Matrix.Business.Extensions
{
    /// <summary>
    /// The movement item extensions.
    /// </summary>
    internal static class MovementsItemExtensions
    {
        /// <summary>
        /// Converts a <see cref="IEnumerable{T}"/> where T is <see cref="FiveDegrees.CashAccount.MovementItem"/>
        /// to a <see cref="List{T}"/> where T is <see cref="Entities.MovementItem"/>
        /// </summary>
        /// <param name="cashAccountMovementItems"> The <see cref="IEnumerable{T}"/> where T is <see cref="FiveDegrees.CashAccount.MovementItem"/>. </param>
        /// <returns>
        /// The <see cref="List{T}"/> where T is <see cref="Entities.MovementItem"/>.
        /// </returns>
        internal static List<Entities.MovementItem> ToEntities(this IEnumerable<FiveDegrees.CashAccount.MovementItem> cashAccountMovementItems)
        {
            return cashAccountMovementItems.Select(x => x.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts a <see cref="FiveDegrees.CashAccount.MovementItem"/> to a <see cref="Entities.MovementItem"/>
        /// </summary>
        /// <param name="cashAccountMovementItem"> The <see cref="FiveDegrees.CashAccount.MovementItem"/>. </param>
        /// <returns>
        /// The <see cref="Entities.MovementItem"/>.
        /// </returns>
        internal static Entities.MovementItem ToEntity(this FiveDegrees.CashAccount.MovementItem cashAccountMovementItem)
        {
            return new Entities.MovementItem()
            {
                Amount = cashAccountMovementItem.Amount,
                BalanceAfter = cashAccountMovementItem.BalanceAfter,
                CashFlowItemId = cashAccountMovementItem.CashFlowItemId,
                Clarification = cashAccountMovementItem.Clarification,
                Currency = cashAccountMovementItem.Currency,
                MovementId = cashAccountMovementItem.MovementId,
                MovementState = cashAccountMovementItem.MovementState.HasValue ? cashAccountMovementItem.MovementState.ToString() : null,
                MovementStateComment = cashAccountMovementItem.MovementStateComment,
                MovementStateOperationId = cashAccountMovementItem.MovementStateOperationId,
                MovementStateSystemDate = cashAccountMovementItem.MovementStateSystemDate,
                OperationId = cashAccountMovementItem.OperationId,
                OperationType = cashAccountMovementItem.OperationType.ToEntity(),
                Origin = cashAccountMovementItem.Origin.ToEntity(),
                PaymentType = cashAccountMovementItem.PaymentType.ToString(),
                Reference = cashAccountMovementItem.Reference,
                SystemDate = cashAccountMovementItem.SystemDate,
                TransactionId = cashAccountMovementItem.TransactionId,
                TransactionIdSpecified = true,
                ValueDate = cashAccountMovementItem.ValueDate,
                AccountTo = cashAccountMovementItem.Creditor.Accountnumber,
                AccountFrom = cashAccountMovementItem.Debetor.Accountnumber,
                Creditor = cashAccountMovementItem.Creditor.ToEntity(),
                Debitor = cashAccountMovementItem.Debetor.ToEntity()
            };
        }

        /// <summary>
        /// Converts a <see cref="Entities.MovementItem"/> to a <see cref="FiveDegrees.CashAccount.MovementItem"/>
        /// </summary>
        /// <param name="entityMovementItem"> The <see cref="Entities.MovementItem"/>. </param>
        /// <returns>
        /// The <see cref="FiveDegrees.CashAccount.MovementItem"/>.
        /// </returns>
        internal static FiveDegrees.CashAccount.MovementItem ToMatrixModel(this Entities.MovementItem entityMovementItem)
        {
            return new FiveDegrees.CashAccount.MovementItem()
            {
                Amount = entityMovementItem.Amount,
                BalanceAfter = entityMovementItem.BalanceAfter,
                CashFlowItemId = entityMovementItem.CashFlowItemId,
                CashFlowItemIdSpecified = entityMovementItem.CashFlowItemIdSpecified,
                Clarification = entityMovementItem.Clarification,
                Currency = entityMovementItem.Currency,
                MovementId = entityMovementItem.MovementId,
                MovementState = string.IsNullOrWhiteSpace(entityMovementItem.MovementState)
                                    ? null
                                    : (FiveDegrees.CashAccount.MovementStates?)entityMovementItem.MovementState.ParseEnum<FiveDegrees.CashAccount.MovementStates>(),
                MovementStateSpecified = entityMovementItem.MovementStateSpecified,
                MovementStateComment = entityMovementItem.MovementStateComment,
                MovementStateOperationId = entityMovementItem.MovementStateOperationId,
                MovementStateOperationIdSpecified = entityMovementItem.MovementStateOperationIdSpecified,
                MovementStateSystemDate = entityMovementItem.MovementStateSystemDate,
                MovementStateSystemDateSpecified = entityMovementItem.MovementStateSystemDateSpecified,
                OperationId = entityMovementItem.OperationId,
                OperationType = entityMovementItem.OperationType.ToMatrixModel(),
                Origin = entityMovementItem.Origin.ToMatrixModel(),
                PaymentType = entityMovementItem.PaymentType.ParseEnum<FiveDegrees.CashAccount.PaymentTypes>(),
                Reference = entityMovementItem.Reference,
                SystemDate = entityMovementItem.SystemDate,
                TransactionId = entityMovementItem.TransactionId,
                ValueDate = entityMovementItem.ValueDate,
                Creditor = entityMovementItem.Creditor.ToMatrixModel(),
                Debetor = entityMovementItem.Debitor.ToMatrixModel()
            };
        }
    }
}
