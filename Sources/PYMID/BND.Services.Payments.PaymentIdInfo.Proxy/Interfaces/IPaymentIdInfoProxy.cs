using BND.Services.Payments.PaymentIdInfo.Entities;
using System;
using System.Collections.Generic;

namespace BND.Services.Payments.PaymentIdInfo.Proxy.Interfaces
{
    /// <summary>
    /// Interface IPaymentIdInfoProxy
    /// </summary>
    public interface IPaymentIdInfoProxy : IDisposable
    {
        /// <summary>
        /// Gets the filter types.
        /// </summary>
        /// <returns>IEnumerable&lt;EnumFilterType&gt;.</returns>
        IEnumerable<EnumFilterType> GetFilterTypes();
        /// <summary>
        /// Gets the by BND iban.
        /// </summary>
        /// <param name="bndIban">The BND iban.</param>
        /// <param name="filterTypes">The filter types.</param>
        /// <returns>IEnumerable&lt;PaymentIdInfoModel&gt;.</returns>
        IEnumerable<PaymentIdInfoModel> GetByBndIban(string bndIban, IEnumerable<EnumFilterType> filterTypes);
        /// <summary>
        /// Gets the by source iban.
        /// </summary>
        /// <param name="sourceIban">The source iban.</param>
        /// <param name="filterTypes">The filter types.</param>
        /// <returns>IEnumerable&lt;PaymentIdInfoModel&gt;.</returns>
        IEnumerable<PaymentIdInfoModel> GetBySourceIban(string sourceIban, IEnumerable<EnumFilterType> filterTypes);
        /// <summary>
        /// Gets the by transaction identifier.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="filterTypes">The filter types.</param>
        /// <returns>IEnumerable&lt;PaymentIdInfoModel&gt;.</returns>
        IEnumerable<PaymentIdInfoModel> GetByTransactionId(string transactionId, IEnumerable<EnumFilterType> filterTypes);
    }
}
