using BND.Services.Infrastructure.Common.Extensions;

namespace BND.Services.Matrix.Business.Extensions
{
    /// <summary>
    /// The origin item extensions.
    /// </summary>
    internal static class OriginItem
    {
        /// <summary>
        /// Converts a <see cref="FiveDegrees.CashAccount.OriginItem"/> to a <see cref="Entities.Origin"/>
        /// </summary>
        /// <param name="cashAccountOriginItem"> The <see cref="FiveDegrees.CashAccount.OriginItem"/>. </param>
        /// <returns>
        /// The <see cref="Entities.Origin"/>.
        /// </returns>
        internal static Entities.Origin ToEntity(this FiveDegrees.CashAccount.OriginItem cashAccountOriginItem)
        {
            return new Entities.Origin()
            {
                Name = cashAccountOriginItem.Name,
                AccountNumber = cashAccountOriginItem.AccountNumber,
                CountryCode = cashAccountOriginItem.CountryCode.ToString(),
                CountryCodeSpecified = cashAccountOriginItem.CountryCodeSpecified
            };
        }

        /// <summary>
        /// Converts a <see cref="Entities.Origin"/> to a <see cref="FiveDegrees.CashAccount.OriginItem"/>
        /// </summary>
        /// <param name="entityOriginItem"> The <see cref="Entities.Origin"/>. </param>
        /// <returns>
        /// The <see cref="FiveDegrees.CashAccount.OriginItem"/>.
        /// </returns>
        internal static FiveDegrees.CashAccount.OriginItem ToMatrixModel(this Entities.Origin entityOriginItem)
        {
            return new FiveDegrees.CashAccount.OriginItem()
            {
                Name = entityOriginItem.Name,
                AccountNumber = entityOriginItem.AccountNumber,
                CountryCode = entityOriginItem.CountryCode.ParseEnum<FiveDegrees.CashAccount.CountryCodes>(),
                CountryCodeSpecified = entityOriginItem.CountryCodeSpecified
            };
        }
    }
}
