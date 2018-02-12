using System.Collections.Generic;
using System.Linq;

using BND.Services.Matrix.Entities;

namespace BND.Services.Matrix.Business.Extensions
{
    /// <summary>
    /// The interest rate extensions.
    /// </summary>
    internal static class InterestRateExtensions
    {
        /// <summary>
        /// Converts a <see cref="FiveDegrees.CashAccount.GetInterestRateOverrideItem"/> to a <see cref="Entities.InterestRate"/>
        /// </summary>
        /// <param name="item"> The <see cref="FiveDegrees.CashAccount.GetInterestRateOverrideItem"/>. </param>
        /// <returns>
        /// The <see cref="Entities.InterestRate"/>.
        /// </returns>
        internal static Entities.InterestRate ToEntity(this FiveDegrees.CashAccount.GetInterestRateOverrideItem item)
        {
            return new Entities.InterestRate()
                       {
                           AccountIdentifiersField = item.AccountIdentifiers.ToEntities(),
                           EndOverrideDateField = item.EndOverrideDate,
                           EndOverrideDateFieldSpecified = item.EndOverrideDateSpecified,
                           EntityInfoField = item.EntityInfo.ToEntityInfoItemEntity(),
                           GrossRateField = item.GrossRate,
                           GrossRateFieldSpecified = item.GrossRateSpecified,
                           InterestRateOverrideIdField = item.InterestRateOverrideId,
                           InterestRateOverrideIdFieldSpecified = item.InterestRateOverrideIdSpecified,
                           ValidFromDateField = item.ValidFromDate,
                           ValidFromDateFieldSpecified = item.ValidFromDateSpecified
                       };
        }

        /// <summary>
        /// Converts a <see cref="IEnumerable{T}"/> where T is <see cref="FiveDegrees.CashAccount.GetInterestRateOverrideItem"/>
        /// to a <see cref="List{T}"/> where T is <see cref="Entities.InterestRate"/>
        /// </summary>
        /// <param name="items"> The <see cref="IEnumerable{T}"/> where T is <see cref="FiveDegrees.CashAccount.GetInterestRateOverrideItem"/>. </param>
        /// <returns>
        /// The <see cref="List{T}"/> where T is <see cref="Entities.InterestRate"/>.
        /// </returns>
        internal static List<Entities.InterestRate> ToEntities(this IEnumerable<FiveDegrees.CashAccount.GetInterestRateOverrideItem> items)
        {
            return items.Select(item => item.ToEntity()).ToList();
        }
    }
}
