using System.Collections.Generic;
using System.Linq;

using BND.Services.Infrastructure.Common.Extensions;
using BND.Services.Matrix.Enums;

namespace BND.Services.Matrix.Business.Extensions
{
    /// <summary>
    /// The bucket items extensions.
    /// </summary>
    internal static class BucketItemsExtensions
    {
        /// <summary>
        /// The to entity.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <returns>
        /// The <see cref="Entities.BucketItem"/>.
        /// </returns>
        internal static Entities.BucketItem ToEntity(this FiveDegrees.PaymentService.BucketItem item)
        {
            return new Entities.BucketItem()
            {
                Id = item.Id,
                ValueDate = item.ValueDate,
                Created = item.Created,
                Bic = item.Bic,
                BucketType = item.BucketType.ToString().ParseEnum<EnumBucketItemTypes>(),
                GroupStatus = item.GroupStatus.ToString().ParseEnum<EnumBucketItemStatuses>(),
                OperationCode = item.OperationCode.ToString().ParseEnum<EnumBucketItemOperations>(),
                Source = item.Source.ToString().ParseEnum<EnumBucketItemSources>(),
                PaymentBuckets = item.PaymentBucketItems.ToEntities()
            };
        }

        /// <summary>
        /// The to entities.
        /// </summary>
        /// <param name="items">
        /// The items.
        /// </param>
        /// <returns>
        /// The <see cref="List{BucketItem}"/>.
        /// </returns>
        internal static List<Entities.BucketItem> ToEntities(this IEnumerable<FiveDegrees.PaymentService.BucketItem> items)
        {
            return items.Select(item => item.ToEntity()).ToList();
        }
    }
}
