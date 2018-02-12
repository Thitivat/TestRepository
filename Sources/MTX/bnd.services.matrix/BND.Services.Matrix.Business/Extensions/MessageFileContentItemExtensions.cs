using System.Collections.Generic;
using System.Linq;

namespace BND.Services.Matrix.Business.Extensions
{
    /// <summary>
    /// The message file content item extensions.
    /// </summary>
    internal static class MessageFileContentItemExtensions
    {
        /// <summary>
        /// Converts a <see cref="FiveDegrees.PaymentService.MessageFileContentItem"/> to a <see cref="Entities.MessageFileContentItem"/>
        /// </summary>
        /// <param name="item"> The <see cref="FiveDegrees.PaymentService.MessageFileContentItem"/>. </param>
        /// <returns>
        /// The <see cref="Entities.MessageFileContentItem"/>.
        /// </returns>
        internal static Entities.MessageFileContentItem ToEntity(this FiveDegrees.PaymentService.MessageFileContentItem item)
        {
            return new Entities.MessageFileContentItem()
            {
                MessageId = item.MessageId,
                RefId = item.RefId,
                Name = item.Name,
                Size = item.Size,
                Content = item.Content
            };
        }

        /// <summary>
        /// Converts a <see cref="IEnumerable{T}"/> where T is <see cref="FiveDegrees.PaymentService.MessageFileContentItem"/>
        /// to a <see cref="List{T}"/> where T is <see cref="Entities.MessageFileContentItem"/>
        /// </summary>
        /// <param name="items"> The <see cref="IEnumerable{T}"/> where T is <see cref="FiveDegrees.PaymentService.MessageFileContentItem"/>. </param>
        /// <returns>
        /// The <see cref="List{T}"/> where T is <see cref="Entities.MessageFileContentItem"/>.
        /// </returns>
        internal static List<Entities.MessageFileContentItem> ToEntities(this IEnumerable<FiveDegrees.PaymentService.MessageFileContentItem> items)
        {
            return items.Select(item => item.ToEntity()).ToList();
        }
    }
}
