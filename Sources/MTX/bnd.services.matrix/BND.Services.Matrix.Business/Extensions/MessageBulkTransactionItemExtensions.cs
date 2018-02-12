using System.Collections.Generic;
using System.Linq;

namespace BND.Services.Matrix.Business.Extensions
{
    /// <summary>
    /// The message bulk transaction item extensions.
    /// </summary>
    internal static class MessageBulkTransactionItemExtensions
    {
        /// <summary>
        /// Converts a <see cref="FiveDegrees.PaymentService.MessageBulkTransactionItem"/> to a <see cref="Entities.MessageBulkTransactionItem"/>
        /// </summary>
        /// <param name="item"> The <see cref="FiveDegrees.PaymentService.MessageBulkTransactionItem"/>. </param>
        /// <returns>
        /// The <see cref="Entities.MessageBulkTransactionItem"/>.
        /// </returns>
        internal static Entities.MessageBulkTransactionItem ToEntity(this FiveDegrees.PaymentService.MessageBulkTransactionItem item)
        {
            return new Entities.MessageBulkTransactionItem()
            {
                Id = item.Id,
                MessageBulkId = item.MessageBulkId,
                MovementId = item.MovementId,
                BucketId = item.BucketId,
                BucketRowId = item.BucketRowId,
                TrxId = item.TrxId,
                ReturnTrxId = item.ReturnTrxId,
                ReturnMessageId = item.ReturnMessageId,
                ReturnFileSize = item.ReturnFileSize,
                ReturnFileName = item.ReturnFileName,
                ReturnFileRefId = item.ReturnFileRefId,
                Amount = item.Amount,
                SepaTransactionLevelErrorCode = !item.SepaTransactionLevelErrorCode.HasValue ? null : item.SepaTransactionLevelErrorCode.Value.ToString(),
                SepaTransactionLevelErrorName = item.SepaTransactionLevelErrorName,
                EntityInfo = item.EntityInfo.ToEntityInfoItemEntity()
           };
        }

        /// <summary>
        /// Converts a <see cref="IEnumerable{T}"/> where T is <see cref="FiveDegrees.PaymentService.MessageBulkTransactionItem"/>
        /// to a <see cref="List{T}"/> where T is <see cref="Entities.MessageBulkTransactionItem"/>
        /// </summary>
        /// <param name="items"> The <see cref="IEnumerable{T}"/> where T is <see cref="FiveDegrees.PaymentService.MessageBulkTransactionItem"/>. </param>
        /// <returns>
        /// The <see cref="List{T}"/> where T is <see cref="Entities.MessageBulkTransactionItem"/>.
        /// </returns>
        internal static List<Entities.MessageBulkTransactionItem> ToEntities(this IEnumerable<FiveDegrees.PaymentService.MessageBulkTransactionItem> items)
        {
            return items.Select(item => item.ToEntity()).ToList();
        }
    }
}
