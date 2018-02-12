using BND.Services.Infrastructure.Common.Extensions;

namespace BND.Services.Matrix.Business.Extensions
{
    /// <summary>
    /// The payment customer extensions.
    /// </summary>
    internal static class PaymentCustomerItemExtensions
    {
        /// <summary>
        /// Converts a <see cref="Entities.PaymentCustomerItem"/> to a <see cref="FiveDegrees.CashAccount.PaymentCustomerItem"/>
        /// </summary>
        /// <param name="item"> The <see cref="Entities.PaymentCustomerItem"/>. </param>
        /// <returns>
        /// The <see cref="FiveDegrees.CashAccount.PaymentCustomerItem"/>.
        /// </returns>
        internal static FiveDegrees.CashAccount.PaymentCustomerItem ToMatrixModel(this Entities.PaymentCustomerItem item)
        {
            return new FiveDegrees.CashAccount.PaymentCustomerItem()
            {
                CustomerName = item.CustomerName,
                AddressLine1 = item.AddressLine1,
                AddressLine2 = item.AddressLine2,
                AddressLine3 = item.AddressLine3,
                CountryCode = string.IsNullOrWhiteSpace(item.CountryCode)
                    ? null
                    : (FiveDegrees.CashAccount.CountryCodes?)item.CountryCode.ParseEnum<FiveDegrees.CashAccount.CountryCodes>(),
                CountryCodeSpecified = !string.IsNullOrWhiteSpace(item.CountryCode)
            };
        }
    }
}
