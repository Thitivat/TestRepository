using System.Collections.Generic;
using System.Linq;

using BND.Services.Matrix.Business.FiveDegrees.PortfolioService;
using BND.Services.Matrix.Entities;

namespace BND.Services.Matrix.Business.Extensions
{
    /// <summary>
    /// The system account extensions.
    /// </summary>
    internal static class SystemAccountExtensions
    {
        /// <summary>
        /// Converts a <see cref="FiveDegrees.PortfolioService.ServiceListItem"/> to a <see cref="Entities.SystemAccount"/>
        /// </summary>
        /// <param name="item"> The <see cref="FiveDegrees.PortfolioService.ServiceListItem"/>. </param>
        /// <returns>
        /// The <see cref="Entities.SystemAccount"/>.
        /// </returns>
        internal static Entities.SystemAccount ToEntity(this FiveDegrees.PortfolioService.ServiceListItem item)
        {
            var systemAccounts = new Entities.SystemAccount()
            {
                UnitId = item.UnitId,
                GlAccountId = item.GlAccountId,
                ProductId = item.Product.Id,
                AccruedInterestGlAccountId = item.AccruedInterestGlAccountId,
                AccruedInterestGlAccountName = item.AccruedInterestGlAccountName,
                DepartmentId = item.DepartmentId,
                DepartmentName = item.DepartmentName,
                FeatureId = item.FeatureId,
                GlAccountName = item.GlAccountName,
                PortfolioId = item.PortfolioId,
                ProductName = item.Product.Name,
                UnitName = item.UnitName,
                Created = item.EntityInfo.Created.EventDate,
                Currency = item.Currency.ToString(),
                AccountNumbers = new Dictionary<string, string>()
            };

            foreach (var serviceIdentificationItem in item.ServiceIdentifications)
            {
                systemAccounts.AccountNumbers.Add(serviceIdentificationItem.AccountNumber, serviceIdentificationItem.Standard.ToString());
            }

            return systemAccounts;
        }

        /// <summary>
        /// Converts a <see cref="IEnumerable{T}"/> where T is <see cref="FiveDegrees.PortfolioService.ServiceListItem"/>
        /// to a <see cref="List{T}"/> where T is <see cref="Entities.SystemAccount"/>
        /// </summary>
        /// <param name="items"> The <see cref="IEnumerable{T}"/> where T is <see cref="FiveDegrees.PortfolioService.ServiceListItem"/>. </param>
        /// <returns>
        /// The <see cref="List{T}"/> where T is <see cref="Entities.SystemAccount"/>.
        /// </returns>
        internal static List<Entities.SystemAccount> ToEntities(this IEnumerable<FiveDegrees.PortfolioService.ServiceListItem> items)
        {
            return items.Select(s => s.ToEntity()).ToList();
        }

        /// <summary>
        /// Populates additional properties of a SystemAccount.
        /// </summary>
        /// <param name="account">
        /// The account.
        /// </param>
        /// <param name="serviceItem">
        /// The service item.
        /// </param>
        internal static void AddAccountInfo(this SystemAccount account, FiveDegrees.PortfolioService.ServiceItem serviceItem)
        {
            account.ValueDate = serviceItem.ValueDate;
            account.AvailableBalance = serviceItem.AvailableBalance;
            account.Balance = serviceItem.Balance;
            account.DueFeeAmount = serviceItem.DueFeeAmount;
            account.FeeHandling = serviceItem.FeeHandling.ToString();
            account.ServiceState = serviceItem.ServiceState.ToString();
            account.StatementInterval = serviceItem.StatementInterval;
            account.TaxExemptState = serviceItem.TaxExemptState.ToString();
            account.TransactionId = serviceItem.TransactionId;
        }
    }
}
