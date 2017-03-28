using System;
using System.Linq;

namespace BND.Services.Payments.iDeal.ClientData.Dal.Ef
{
    /// <summary>
    /// Class Page providing pagination.
    /// </summary>
    /// <typeparam name="TPocoEntity">The type of the t poco entity.</typeparam>
    public class Page<TPocoEntity>
    {
        /// <summary>
        /// Gets or sets the ordering data.
        /// </summary>
        /// <value>The ordering data.</value>
        public Func<IQueryable<TPocoEntity>, IOrderedQueryable<TPocoEntity>> OrderBy { get; set; }

        /// <summary>
        /// Gets or sets the starting record position.
        /// </summary>
        /// <value>The starting record position.</value>
        public int? Offset { get; set; }

        /// <summary>
        /// Gets or sets the amount of data you want to retrieve.
        /// </summary>
        /// <value>The amount of data you want to retrieve.</value>
        public int? Limit { get; set; }
    }
}
