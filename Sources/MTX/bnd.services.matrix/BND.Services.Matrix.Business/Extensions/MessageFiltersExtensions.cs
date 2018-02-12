using BND.Services.Infrastructure.Common.Extensions;
using BND.Services.Matrix.Business.FiveDegrees.PaymentService;

namespace BND.Services.Matrix.Business.Extensions
{
    /// <summary>
    /// The message filters extensions.
    /// </summary>
    internal static class MessageFiltersExtensions
    {
        /// <summary>
        /// Converts a <see cref="Entities.QueryStringModels.MessageFilters"/> to a <see cref="FiveDegrees.PaymentService.BulkFilterItem"/>
        /// </summary>
        /// <param name="filters"> The <see cref="Entities.QueryStringModels.MessageFilters"/> </param>
        /// <returns>
        /// The <see cref="FiveDegrees.PaymentService.BulkFilterItem"/>.
        /// </returns>
        internal static BulkFilterItem ToMatrixModel(this Entities.QueryStringModels.MessageFilters filters)
        {
            return new BulkFilterItem()
            {
                From = filters.From,
                To = filters.To,
                MessageType = (MessageTypes?)filters.Type.ToString().ParseEnum<MessageTypes>(),
                Direction = (DirectionLevels?)filters.Direction,
                MessageStatus = (MessageStatuses?)filters.Status
            };
        }
    }
}
