using BND.Services.Payments.PaymentIdInfo.Entities;
using System.Collections.Generic;

namespace BND.Services.Payments.PaymentIdInfo.Business
{
    /// <summary>
    /// Interface IPaymentIdInfoManager
    /// </summary>
    public interface IPaymentIdInfoManager
    {
        /// <summary>
        /// Gets the filter types.
        /// </summary>
        /// <returns>IEnumerable{EnumFilterType}.</returns>
        IEnumerable<EnumFilterType> GetFilterTypes();
        /// <summary>
        /// Gets the payment identifier by BND iban.
        /// </summary>
        /// <param name="bndIban">The BND iban.</param>
        /// <param name="filterTypes">The filter types.</param>
        /// <returns>IEnumerable{PaymentIdInfoModel}.</returns>
        IEnumerable<PaymentIdInfoModel> GetPaymentIdByBndIban(string bndIban, List<EnumFilterType> filterTypes);
        /// <summary>
        /// Gets the payment identifier by source iban.
        /// </summary>
        /// <param name="sourceIban">The source iban.</param>
        /// <param name="filterTypes">The filter types.</param>
        /// <returns>IEnumerable{PaymentIdInfoModel}.</returns>
        IEnumerable<PaymentIdInfoModel> GetPaymentIdBySourceIban(string sourceIban, List<EnumFilterType> filterTypes);
        /// <summary>
        /// Gets the payment identifier by transaction.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="filterTypes">The filter types.</param>
        /// <returns>IEnumerable{PaymentIdInfoModel}.</returns>
        IEnumerable<PaymentIdInfoModel> GetPaymentIdByTransaction(string transactionId, List<EnumFilterType> filterTypes);
    }
}
