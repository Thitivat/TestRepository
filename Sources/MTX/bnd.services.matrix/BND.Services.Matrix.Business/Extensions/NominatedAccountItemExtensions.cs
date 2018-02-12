using System.Collections.Generic;
using System.Linq;

namespace BND.Services.Matrix.Business.Extensions
{
    /// <summary>
    /// The nominated account item extensions.
    /// </summary>
    internal static class NominatedAccountItemExtensions
    {
        /// <summary>
        /// Converts a <see cref="FiveDegrees.CashAccount.NominatedAccountItem"/> to a <see cref="Entities.NominatedAccountItem"/>
        /// </summary>
        /// <param name="item"> The <see cref="FiveDegrees.CashAccount.NominatedAccountItem"/>. </param>
        /// <returns>
        /// The <see cref="Entities.NominatedAccountItem"/>.
        /// </returns>
        internal static Entities.NominatedAccountItem ToEntity(this FiveDegrees.CashAccount.NominatedAccountItem item)
        {
            return new Entities.NominatedAccountItem
            {
                NominatedAccountNumber = item.NominatedAccountNumber,
                IsInternal = item.IsInternal
            };
        }

        /// <summary>
        /// Converts a <see cref="IEnumerable{T}"/> where T is <see cref="FiveDegrees.CashAccount.NominatedAccountItem"/>
        /// to a <see cref="List{T}"/> where T is <see cref="Entities.NominatedAccountItem"/>
        /// </summary>
        /// <param name="items"> The <see cref="IEnumerable{T}"/> where T is <see cref="FiveDegrees.CashAccount.NominatedAccountItem"/>. </param>
        /// <returns>
        /// The <see cref="List{T}"/> where T is <see cref="Entities.NominatedAccountItem"/>.
        /// </returns>
        internal static List<Entities.NominatedAccountItem> ToEntities(this IEnumerable<FiveDegrees.CashAccount.NominatedAccountItem> items)
        {
            return items.Select(x => x.ToEntity()).ToList();
        }
    }
}
