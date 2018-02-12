using System.Collections.Generic;
using System.Linq;

namespace BND.Services.Matrix.Business.Extensions
{
    /// <summary>
    /// The account identifier item extensions.
    /// </summary>
    internal static class AccountIdentifierItemExtensions
    {
        /// <summary>
        /// Converts a <see cref="FiveDegrees.CashAccount.AccountIdentifierItem"/> to a <see cref="Entities.AccountIdentifierItem"/>
        /// </summary>
        /// <param name="item"> The <see cref="FiveDegrees.CashAccount.AccountIdentifierItem"/>. </param>
        /// <returns>
        /// The <see cref="Entities.AccountIdentifierItem"/>.
        /// </returns>
        internal static Entities.AccountIdentifierItem ToEntity(this FiveDegrees.CashAccount.AccountIdentifierItem item)
        {
            return new Entities.AccountIdentifierItem()
                       {
                           IdentificationStandardField = item.IdentificationStandard.ToString(),
                           IdentificationStandardFieldSpecified = item.IdentificationStandardSpecified,
                           IdentifierField = item.Identifier
                       };
        }

        /// <summary>
        /// Converts a <see cref="IEnumerable{T}"/> where T is <see cref="FiveDegrees.CashAccount.AccountIdentifierItem"/>
        /// to a <see cref="List{T}"/> where T is <see cref="Entities.AccountIdentifierItem"/>
        /// </summary>
        /// <param name="items"> The <see cref="IEnumerable{T}"/> where T is <see cref="FiveDegrees.CashAccount.AccountIdentifierItem"/>. </param>
        /// <returns>
        /// The <see cref="List{T}"/> where T is <see cref="Entities.AccountIdentifierItem"/>.
        /// </returns>
        internal static List<Entities.AccountIdentifierItem> ToEntities(this IEnumerable<FiveDegrees.CashAccount.AccountIdentifierItem> items)
        {
            return items.Select(item => item.ToEntity()).ToList();
        }
    }
}
