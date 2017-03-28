using BND.Services.Payments.PaymentIdInfo.Entities;
using System.Collections.Generic;

namespace BND.Services.Payments.PaymentIdInfo.Data.Interfaces
{
    public interface IPaymentIdInfoRepository<T> where T : class
    {
        /// <summary>
        /// Gets iDealTransaction data the by BND iban.
        /// </summary>
        /// <param name="bndIban">The BND iban.</param>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        IEnumerable<T> GetByBndIban(string bndIban);

        /// <summary>
        /// Gets iDealTransaction data  by source iban.
        /// </summary>
        /// <param name="bndIban">The BND iban.</param>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        IEnumerable<T> GetBySourceIban(string bndIban);

        /// <summary>
        /// Gets iDealTransaction data  by transaction identifier.
        /// </summary>
        /// <param name="bndIban">The BND iban.</param>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        IEnumerable<T> GetByTransactionId(string bndIban);
    }
}
