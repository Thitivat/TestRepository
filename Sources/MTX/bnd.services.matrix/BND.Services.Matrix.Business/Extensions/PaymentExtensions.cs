using System.Configuration;

namespace BND.Services.Matrix.Business.Extensions
{
    /// <summary>
    /// The payment extensions.
    /// </summary>
    internal static class PaymentExtensions
    {
        /// <summary>
        /// Converts a <see cref="Entities.Payment"/> to a <see cref="FiveDegrees.CashAccount.PaymentItem"/>
        /// </summary>
        /// <param name="item"> The <see cref="Entities.Payment"/>. </param>
        /// <returns>
        /// The <see cref="FiveDegrees.CashAccount.PaymentItem"/>.
        /// </returns>
        internal static FiveDegrees.CashAccount.PaymentItem ToMatrixModel(this Entities.Payment item)
        {
            return new FiveDegrees.CashAccount.PaymentItem()
                       {  
                           CounterpartyAccountNumber = item.CounterpartyIBAN,
                           CounterpartyBIC = item.CounterpartyBIC,
                           Amount = item.Amount,
                           ValueDate = item.ValueDate,
                           Reference = item.Reference,
                           Clarification = item.Clarification,
                           DebetorDetails = item.DebtorDetails.ToMatrixModel(),
                           CreditorDetails = item.CreditorDetails.ToMatrixModel(),
                           Currency = ConfigurationManager.AppSettings[Constants.MatrixCurrency],
                           PaymentType = FiveDegrees.CashAccount.PaymentTypes.Principal,
                           PaymentSource = FiveDegrees.CashAccount.PaymentSources.SEPA,
                           ClearingDate = item.ValueDate,
                           InterestDate = item.ValueDate,
                           ExchangeRate = 1M,
                           IsExternal = true,
                           IsExternalSpecified = true
                       };
        }
    }
}
