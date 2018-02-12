using System.Collections.Generic;
using System.Linq;

namespace BND.Services.Matrix.Business.Extensions
{
    /// <summary>
    /// The service state overview extensions.
    /// </summary>
    internal static class ServiceStateItemExtension
    {
        /// <summary>
        /// Converts a <see cref="FiveDegrees.CashAccount.ServiceStateItem"/> to a <see cref="Entities.ServiceStateItem"/>
        /// </summary>
        /// <param name="item"> The <see cref="FiveDegrees.CashAccount.ServiceStateItem"/>. </param>
        /// <returns>
        /// The <see cref="Entities.ServiceStateItem"/>.
        /// </returns>
        internal static Entities.ServiceStateItem ToEntity(this FiveDegrees.CashAccount.ServiceStateItem item)
        {
            return new Entities.ServiceStateItem
                       {
                           ValidFromDate = item.ValidFromDate,
                           ServiceState = item.ServiceState.ToString(),
                           ServiceStateName = item.ServiceStateName,
                           ServiceStateSpecified = item.ServiceStateSpecified,
                           ValidFromDateSpecified = item.ValidFromDateSpecified
                       };
        }

        /// <summary>
        /// Converts a <see cref="IEnumerable{T}"/> where T is <see cref="FiveDegrees.CashAccount.ServiceStateItem"/>
        /// to a <see cref="List{T}"/> where T is <see cref="Entities.ServiceStateItem"/>
        /// </summary>
        /// <param name="items"> The <see cref="IEnumerable{T}"/> where T is <see cref="FiveDegrees.CashAccount.ServiceStateItem"/>. </param>
        /// <returns>
        /// The <see cref="List{T}"/> where T is <see cref="Entities.ServiceStateItem"/>.
        /// </returns>
        internal static List<Entities.ServiceStateItem> ToEntities(this IEnumerable<FiveDegrees.CashAccount.ServiceStateItem> items)
        {
            return items.Select(x => x.ToEntity()).ToList();
        }
    }
}
