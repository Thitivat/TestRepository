using BND.Services.Infrastructure.Common.Extensions;

namespace BND.Services.Matrix.Business.Extensions
{
    /// <summary>
    /// The closing payment item extensions.
    /// </summary>
    internal static class ClosePaymentItemExtensions
    {
        /// <summary>
        /// Converts a <see cref="Entities.ClosingPaymentItem"/> to a <see cref="FiveDegrees.CashAccount.ClosingPaymentItem"/>
        /// </summary>
        /// <param name="item"> The <see cref="Entities.ClosingPaymentItem"/>. </param>
        /// <returns>
        /// The <see cref="FiveDegrees.CashAccount.ClosingPaymentItem"/>.
        /// </returns>
        internal static FiveDegrees.CashAccount.ClosingPaymentItem ToMatrixModel(this Entities.ClosingPaymentItem item)
        {
            return new FiveDegrees.CashAccount.ClosingPaymentItem()
            {
                Clarification = item.Clarification,
                Reference = item.Reference,
                BypassNominatedAccountValidation = item.BypassNominatedAccountValidation,
                BypassNominatedAccountValidationSpecified = item.BypassNominatedAccountValidation.HasValue,
                CounterpartyAccountNumber = item.CounterpartyAccountNumber,
                CounterpartyBIC = item.CounterpartyBIC,
                IsExternal = item.IsExternal,
                PaymentSource = string.IsNullOrWhiteSpace(item.PaymentSource)
                    ? null
                    : (FiveDegrees.CashAccount.PaymentSources?)item.PaymentSource.ParseEnum<FiveDegrees.CashAccount.PaymentSources>(),
                PaymentSourceSpecified = !string.IsNullOrWhiteSpace(item.PaymentSource),
                CreditorDetails = item.CreditorDetails == null ? null : item.CreditorDetails.ToMatrixModel(),
                DebetorDetails = item.DebetorDetails == null ? null : item.DebetorDetails.ToMatrixModel()
            };
        }
    }
}
