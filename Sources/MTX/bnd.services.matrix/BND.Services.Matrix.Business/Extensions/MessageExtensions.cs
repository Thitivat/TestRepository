using System.Collections.Generic;
using System.Linq;

using BND.Services.Infrastructure.Common.Extensions;
using BND.Services.Matrix.Enums;

namespace BND.Services.Matrix.Business.Extensions
{
    /// <summary>
    /// The message extensions.
    /// </summary>
    internal static class MessageExtensions
    {
        /// <summary>
        /// Converts a <see cref="FiveDegrees.PaymentService.MessageItem"/> to a <see cref="Entities.Message"/>
        /// </summary>
        /// <param name="item"> The <see cref="FiveDegrees.PaymentService.MessageItem"/>. </param>
        /// <param name="currency"> The currency. </param>
        /// <returns>
        /// The <see cref="Entities.Message"/>.
        /// </returns>
        internal static Entities.Message ToEntity(this FiveDegrees.PaymentService.MessageItem item, string currency)
        {
            return new Entities.Message()
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
                EntityInfo = item.EntityInfo.ToEntityInfoItemEntity()
            };
        }

        /// <summary>
        /// Converts a <see cref="IEnumerable{T}"/> where T is <see cref="FiveDegrees.PaymentService.MessageItem"/>
        /// to a <see cref="List{T}"/> where T is <see cref="Entities.Message"/>
        /// </summary>
        /// <param name="items"> The <see cref="IEnumerable{T}"/> where T is <see cref="FiveDegrees.PaymentService.MessageItem"/>.  </param>
        /// <param name="currency"> The currency. </param>
        /// <returns>
        /// The <see cref="List{T}"/> where T is <see cref="Entities.Message"/>.
        /// </returns>
        internal static List<Entities.Message> ToEntities(this IEnumerable<FiveDegrees.PaymentService.MessageItem> items, string currency)
        {
            return items.Select(item => item.ToEntity(currency)).ToList();
        }
    }
}
