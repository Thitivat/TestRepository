using System.Collections.Generic;
using System.Linq;

using BND.Services.Infrastructure.Common.Extensions;
using BND.Services.Matrix.Enums;

namespace BND.Services.Matrix.Business.Extensions
{
    /// <summary>
    /// The message detail extensions.
    /// </summary>
    internal static class MessageDetailExtensions
    {
        /// <summary>
        /// Converts a <see cref="FiveDegrees.PaymentService.MessageItemDetails"/> to a <see cref="Entities.MessageDetail"/>
        /// </summary>
        /// <param name="item"> The <see cref="FiveDegrees.PaymentService.MessageItemDetails"/>. </param>
        /// <param name="currency"> The currency. </param>
        /// <returns>
        /// The <see cref="Entities.MessageDetail"/>.
        /// </returns>
        internal static Entities.MessageDetail ToEntity(this FiveDegrees.PaymentService.MessageItemDetails item, string currency)
        {
            return new Entities.MessageDetail()
            {
                Id = item.Id,
                OriginalMessageId = item.OriginalMessageId,
                Direction = !item.Direction.HasValue ? null : (EnumMessageDirections?)item.Direction.ToString().ParseEnum<EnumMessageDirections>(),
                Type = !item.MessageType.HasValue ? null : (EnumMessageTypes?)item.MessageType.ToString().ParseEnum<EnumMessageTypes>(),
                Status = item.MessageStatus.ToString().ParseEnum<EnumMessageStatuses>(),
                StatusDetails = item.MessageStatusDetails,
                SepaFileLevelCode = !item.SepaFileLevelCode.HasValue ? null : item.SepaFileLevelCode.Value.ToString(),
                SepaFileLevelName = item.SepaFileLevelName,
                Amount = item.Amount,
                NumberOfTransactions = item.NumberOfTransactions,
                FileName = item.FileName,
                FileRefId = item.FileRefId,
                FileSize = item.FileSize,
                Currency = currency,
                EntityInfo = item.EntityInfo.ToEntityInfoItemEntity(),

                CenterId = item.CenterId,
                FileContent = item.FileContent == null ? null : item.FileContent.ToEntity(),
                Bulks = item.Bulks.ToEntities()
            };
        }

        /// <summary>
        /// Converts a <see cref="IEnumerable{T}"/> where T is <see cref="FiveDegrees.PaymentService.MessageItemDetails"/>
        /// to a <see cref="List{T}"/> where T is <see cref="Entities.MessageDetail"/>
        /// </summary>
        /// <param name="items"> The <see cref="IEnumerable{T}"/> where T is <see cref="FiveDegrees.PaymentService.MessageItemDetails"/>. </param>
        /// <param name="currency"> The currency. </param>
        /// <returns>
        /// The <see cref="List{T}"/> where T is <see cref="Entities.MessageDetail"/>.
        /// </returns>
        internal static List<Entities.MessageDetail> ToEntities(this IEnumerable<FiveDegrees.PaymentService.MessageItemDetails> items, string currency)
        {
            return items.Select(item => item.ToEntity(currency)).ToList();
        }
    }
}
