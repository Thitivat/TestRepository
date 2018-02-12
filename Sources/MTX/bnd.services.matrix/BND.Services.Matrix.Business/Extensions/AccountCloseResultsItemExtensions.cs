namespace BND.Services.Matrix.Business.Extensions
{
    /// <summary>
    /// The account close results item extensions.
    /// </summary>
    internal static class AccountCloseResultsItemExtensions
    {
        /// <summary>
        /// The to entity.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <returns>
        /// The <see cref="Entities.AccountCloseResultsItem"/>.
        /// </returns>
        internal static Entities.AccountCloseResultsItem ToEntity(this FiveDegrees.CashAccount.AccountCloseResultsItem item)
        {
            return new Entities.AccountCloseResultsItem()
            {
                PaymentBucketId = item.PaymentBucketId,
                ClosingBalance = item.ClosingBalance,
                ClosingInterest = item.ClosingInterest,
                ClosingFees = item.ClosingFees,
                ClosingPaymentAmount = item.ClosingPaymentAmount,
                ClosingPaymentClearingDate = item.ClosingPaymentClearingDate,
                ClosingPaymentInterestDate = item.ClosingPaymentInterestDate,
                ClosingPaymentValueDate = item.ClosingPaymentValueDate
            };
        }
    }
}
