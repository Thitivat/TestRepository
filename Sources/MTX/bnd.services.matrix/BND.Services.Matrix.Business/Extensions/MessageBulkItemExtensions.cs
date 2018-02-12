using System.Collections.Generic;
using System.Linq;

namespace BND.Services.Matrix.Business.Extensions
{
    /// <summary>
    /// The message bulk item extensions.
    /// </summary>
    internal static class MessageBulkItemExtensions
    {
        /// <summary>
        /// Converts a <see cref="FiveDegrees.PaymentService.MessageBulkItem"/> to a <see cref="Entities.MessageBulkItem"/>
        /// </summary>
        /// <param name="item"> The <see cref="FiveDegrees.PaymentService.MessageBulkItem"/>. </param>
        /// <returns>
        /// The <see cref="Entities.MessageBulkItem"/>.
        /// </returns>
        internal static Entities.MessageBulkItem ToEntity(this FiveDegrees.PaymentService.MessageBulkItem item)
        {
            return new Entities.MessageBulkItem()
            {
                Id = item.Id,
                MsgId = item.MsgId,
                SepaBulkErrorCode = !item.SepaBulkErrorCode.HasValue ? null : item.SepaBulkErrorCode.Value.ToString(),
                SepaBulkErrorName = item.SepaBulkErrorName,
                Amount = item.Amount,
                NumberOfTransactions = item.NumberOfTransactions,
                Transactions = item.Transactions.ToEntities(),
                EntityInfo = item.EntityInfo.ToEntityInfoItemEntity()
           };
        }

        /// <summary>
        /// Converts a <see cref="IEnumerable{T}"/> where T is <see cref="FiveDegrees.PaymentService.MessageBulkItem"/>
        /// to a <see cref="List{T}"/> where T is <see cref="Entities.MessageBulkItem"/>
        /// </summary>
        /// <param name="items"> The <see cref="IEnumerable{T}"/> where T is <see cref="FiveDegrees.PaymentService.MessageBulkItem"/>. </param>
        /// <returns>
        /// The <see cref="List{T}"/> where T is <see cref="Entities.MessageBulkItem"/>.
        /// </returns>
        internal static List<Entities.MessageBulkItem> ToEntities(this IEnumerable<FiveDegrees.PaymentService.MessageBulkItem> items)
        {
            return items.Select(item => item.ToEntity()).ToList();
        }
    }
}
