namespace BND.Services.Matrix.Business.Extensions
{
    /// <summary>
    /// The account overview extensions.
    /// </summary>
    internal static class AccountOverviewExtensions
    {
        /// <summary>
        /// Converts a <see cref="FiveDegrees.CashAccount.CashAccountOverviewItem"/> to a <see cref="Entities.AccountOverview"/>
        /// </summary>
        /// <param name="item"> The <see cref="FiveDegrees.CashAccount.CashAccountOverviewItem"/>. </param>
        /// <returns>
        /// The <see cref="Entities.AccountOverview"/>.
        /// </returns>
        internal static Entities.AccountOverview ToEntity(this FiveDegrees.CashAccount.CashAccountOverviewItem item)
        {
            return new Entities.AccountOverview()
            {
               TotalBalance = item.TotalBalance,
               GrossInterestRate = item.GrossInterestRate,
               Balance = item.Balance,
               AvailableBalance = item.AvailableBalance,
               CreatedDate = item.CreatedDate,
               Currency = item.Currency.ToString(),
               FirstMovementDate = item.FirstMovementDate,
               GrossInterest = item.GrossInterest,
               ProductId = item.ProductId,
               ProductName = item.ProductName,
               ServiceStateHistory = item.ServiceStateHistory.ToEntities(),
               NominatedAccounts = item.NominatedAccounts.ToEntities(),
               AccountIdentifiers = item.AccountIdentifiers.ToEntities(),
               DueFeeAmount = item.DueFeeAmount,
               ServiceState = item.ServiceState.ToString(),
               NextCompoundingDate = item.NextCompoundingDate,
               InterestPeriodStartDate = item.InterestPeriodStartDate,
               FeeHandling = item.FeeHandling.ToString(),
               TaxExemptState = item.TaxExemptState.ToString(),
               ExternalPortfolioId = item.ExternalPortfolioId,
               ProductHistory = item.ProductHistory.ToEntities(),
               LinkedAccountIdentifiers = item.LinkedAccountIdentifiers.ToEntities(),
               IsFirstPaymentProcessed = item.IsFirstPaymentProcessed,
               PortfolioId = item.PortfolioId
            };
        }
    }
}
