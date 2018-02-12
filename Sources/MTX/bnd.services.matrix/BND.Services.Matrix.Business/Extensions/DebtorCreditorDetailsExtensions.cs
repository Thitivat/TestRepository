using BND.Services.Infrastructure.Common.Extensions;

namespace BND.Services.Matrix.Business.Extensions
{
    /// <summary>
    /// The debtor creditor details extensions.
    /// </summary>
    internal static class DebtorCreditorDetailsExtensions
    {
        /// <summary>
        /// Converts a <see cref="Entities.DebtorCreditorDetails"/> to a <see cref="FiveDegrees.CashAccount.PaymentCustomerItem"/>
        /// </summary>
        /// <param name="item"> The <see cref="Entities.DebtorCreditorDetails"/>. </param>
        /// <returns>
        /// The <see cref="FiveDegrees.CashAccount.PaymentCustomerItem"/>.
        /// </returns>
        internal static FiveDegrees.CashAccount.PaymentCustomerItem ToMatrixModel(this Entities.DebtorCreditorDetails item)
        {
            FiveDegrees.CashAccount.CountryCodes? countryCode = null;

            if (!item.CountryCode.IsNullOrEmpty())
            {
                countryCode = item.CountryCode.ParseEnum<FiveDegrees.CashAccount.CountryCodes>();
            }

            return new FiveDegrees.CashAccount.PaymentCustomerItem()
                       {
                           AddressLine1 = item.Street,
                           AddressLine2 = item.Postcode,
                           AddressLine3 = item.City,
                           CountryCode = countryCode,
                           CustomerName = item.Name,
                           CountryCodeSpecified = countryCode.HasValue
                       };
        }
    }
}
