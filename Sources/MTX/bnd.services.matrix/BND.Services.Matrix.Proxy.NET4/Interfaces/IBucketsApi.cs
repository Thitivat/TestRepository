using System.Collections.Generic;

using BND.Services.Matrix.Entities;
using BND.Services.Matrix.Entities.QueryStringModels;

namespace BND.Services.Matrix.Proxy.NET4.Interfaces
{
    /// <summary>
    /// The BucketsApi interface.
    /// </summary>
    public interface IBucketsApi
    {
        /// <summary>
        /// The get buckets.
        /// </summary>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <param name="field">
        /// The field
        /// </param>
        /// <param name="accessToken">The access token</param>
        /// <returns>
        /// The <see cref="List{BucketItem}"/>.
        /// </returns>
        List<BucketItem> GetBuckets(BucketItemFilters filter, BucketExtraFields field,  string accessToken);

        /// <summary>
        /// The get bucket.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="accessToken">
        /// The access token.
        /// </param>
        /// <returns>
        /// The <see cref="BucketItem"/>.
        /// </returns>
        BucketItem GetBucket(string id, string accessToken);
    }
}
