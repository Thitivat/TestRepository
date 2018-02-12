using System.Collections.Generic;
using System.Linq;

using BND.Services.Matrix.Business.FiveDegrees.PaymentService;

namespace BND.Services.Matrix.Business.Extensions
{
    /// <summary>
    /// The payment bucket items extensions.
    /// </summary>
    internal static class PaymentBucketItemsExtensions
    {
        /// <summary>
        /// The to entity.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <returns>
        /// The <see cref="PaymentBucketItem"/>.
        /// </returns>
        internal static Entities.PaymentBucketItem ToEntity(this FiveDegrees.PaymentService.PaymentBucketItem item)
        {
            return new Entities.PaymentBucketItem
            {
               ValueDate = item.ValueDate,
               Amount = item.Amount,
               Currency = item.Currency.ToString(),
               ErrorMessage = item.ErrorMessage,
               RowId = item.RowId,
               CreditorAccountItemDetailsAgent = item.CreditorAccountItemDetailsAgent,
               CreditorAccountItemDetailsAgentAccount = item.CreditorAccountItemDetailsAgentAccount,
               CreditorAccountNumber = item.CreditorAccountNumber,
               CreditorAddressLine1 = item.CreditorAddressLine1,
               CreditorAddressLine2 = item.CreditorAddressLine2,
               CreditorAddressLine3 = item.CreditorAddressLine3,
               CreditorCountryCode = item.CreditorCountryCode.ToString(),
               CreditorCustomerName = item.CreditorCustomerName,
               DebetorAccountItemDetailsAgent = item.DebetorAccountItemDetailsAgent,
               DebetorAccountItemDetailsAgentAccount = item.DebetorAccountItemDetailsAgentAccount,
               DebetorAccountNumber = item.DebetorAccountNumber,
               DebetorAddressLine1 = item.DebetorAddressLine1,
               DebetorAddressLine2 = item.DebetorAddressLine2,
               DebetorAddressLine3 = item.DebetorAddressLine3,
               DebetorCountryCode = item.DebetorCountryCode.ToString(),
               DebetorCustomerName = item.DebetorCustomerName,
               FromAccount = item.FromAccount,
               FromBic = item.FromBIC,
               InterestDate = item.InterestDate,
               IsReturn = item.IsReturn,
               ItemStatus = item.ItemStatus.ToString(),
               Reference = item.Reference,
               PaymentOrder = item.PaymentOrder,
               ProcessingTransactionId = item.ProcessingTransactionId,
               ToAccount = item.ToAccount,
               ToBic = item.ToBIC
            };
        }

        /// <summary>
        /// The to entities.
        /// </summary>
        /// <param name="items">
        /// The items.
        /// </param>
        /// <returns>
        /// The <see cref="List{PaymentBucketItem}"/>.
        /// </returns>
        internal static List<Entities.PaymentBucketItem> ToEntities(this IEnumerable<FiveDegrees.PaymentService.PaymentBucketItem> items)
        {
            return items.Select(item => item.ToEntity()).ToList();
        }
    }
}
