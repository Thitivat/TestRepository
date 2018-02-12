
namespace BND.Services.Matrix.Business.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    internal static class ProductHistoryItemExtensions
    {
        /// <summary>
        /// Converts a <see cref="FiveDegrees.CashAccount.ProductHistoryItem"/> to a <see cref="Entities.ProductHistoryItem"/>
        /// </summary>
        /// <param name="item"> The <see cref="FiveDegrees.CashAccount.ProductHistoryItem"/>. </param>
        /// <returns>
        /// The <see cref="Entities.ProductHistoryItem"/>.
        /// </returns>
        internal static Entities.ProductHistoryItem ToEntity(this FiveDegrees.CashAccount.ProductHistoryItem item)
        {
            return new Entities.ProductHistoryItem()
            {
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                ValidFromDate = item.ValidFromDate
            };
        }

        /// <summary>
        /// Converts a <see cref="IEnumerable{T}"/> where T is <see cref="FiveDegrees.CashAccount.ProductHistoryItem"/>
        /// to a <see cref="List{T}"/> where T is <see cref="Entities.ProductHistoryItem"/>
        /// </summary>
        /// <param name="items"> The <see cref="IEnumerable{T}"/> where T is <see cref="FiveDegrees.CashAccount.ProductHistoryItem"/>. </param>
        /// <returns>
        /// The <see cref="List{T}"/> where T is <see cref="Entities.ProductHistoryItem"/>.
        /// </returns>
        internal static List<Entities.ProductHistoryItem> ToEntities(this IEnumerable<FiveDegrees.CashAccount.ProductHistoryItem> items)
        {
            return items.Select(x => x.ToEntity()).ToList();
        }
    }
}
