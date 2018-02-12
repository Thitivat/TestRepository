using BND.Services.Matrix.Business.FiveDegrees.PaymentService;

namespace BND.Services.Matrix.Business.Extensions
{
    /// <summary>
    /// The bucket filter extensions.
    /// </summary>
    internal static class BucketFiltersExtensions
    {
        /// <summary>
        /// Converts a <see cref="Entities.QueryStringModels.BucketItemFilters"/> to a
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <returns>
        /// The <see cref="BucketFilterItem"/>.
        /// </returns>
        internal static BucketFilterItem ToMatrixModel(this Entities.QueryStringModels.BucketItemFilters item)
        {
            return new BucketFilterItem()
            {
                From = item.From,
                To = item.To,
                Operation = (BankOperationCodes?)item.Operation,
                Source = (PaymentSources?)item.Source,
                Status = (PaymentBucketStatuses?)item.Status,
                Type = (PaymentBucketTypes?)item.Type
            };
        }
    }
}
