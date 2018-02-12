namespace BND.Services.Matrix.Business.Extensions
{
    /// <summary>
    /// The movement overview item extensions.
    /// </summary>
    internal static class MovementOverviewItemExtensions
    {
        /// <summary>
        /// Converts a <see cref="FiveDegrees.CashAccount.MovementOverviewItem"/> to a <see cref="Entities.MovementOverviewItem"/>
        /// </summary>
        /// <param name="item"> The <see cref="FiveDegrees.CashAccount.MovementOverviewItem"/>. </param>
        /// <returns>
        /// The <see cref="Entities.MovementOverviewItem"/>.
        /// </returns>
        internal static Entities.MovementOverviewItem ToEntity(this FiveDegrees.CashAccount.MovementOverviewItem item)
        {
            return new Entities.MovementOverviewItem()
                       {
                           FromDate = item.FromDate,
                           ToDate = item.ToDate,
                           Payments = item.Payments.ToEntities()
                       };
        }

    }
}
