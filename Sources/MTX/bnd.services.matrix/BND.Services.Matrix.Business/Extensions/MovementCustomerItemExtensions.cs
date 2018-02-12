using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BND.Services.Matrix.Business.Extensions
{
    using BND.Services.Infrastructure.Common.Extensions;

    /// <summary>
    /// The movement customer item extensions.
    /// </summary>
    internal static class MovementCustomerItemExtensions
    {
        /// <summary>
        /// Converts a <see cref="FiveDegrees.CashAccount.MovementCustomerItem"/> to a <see cref="Entities.DebtorCreditorDetails"/>
        /// </summary>
        /// <param name="item"> The <see cref="FiveDegrees.CashAccount.MovementCustomerItem"/>. </param>
        /// <returns>
        /// The <see cref="Entities.DebtorCreditorDetails"/>.
        /// </returns>
        internal static Entities.MovementCustomerItem ToEntity(this FiveDegrees.CashAccount.MovementCustomerItem item)
        {
            return new Entities.MovementCustomerItem()
            {
                Name = item.CustomerName,
                AccountNumber = item.Accountnumber,
                Street = item.AddressLine1,
                Postcode = item.AddressLine2,
                City = item.AddressLine3,
                CountryCode = item.CountryCode.ToString()
            };
        }

        /// <summary>
        /// Converts a <see cref="Entities.DebtorCreditorDetails"/> to a <see cref="FiveDegrees.CashAccount.PaymentCustomerItem"/>
        /// </summary>
        /// <param name="item"> The <see cref="Entities.DebtorCreditorDetails"/>. </param>
        /// <returns>
        /// The <see cref="FiveDegrees.CashAccount.PaymentCustomerItem"/>.
        /// </returns>
        internal static FiveDegrees.CashAccount.MovementCustomerItem ToMatrixModel(this Entities.MovementCustomerItem item)
        {
            FiveDegrees.CashAccount.CountryCodes? countryCode = null;

            if (!item.CountryCode.IsNullOrEmpty())
            {
                countryCode = item.CountryCode.ParseEnum<FiveDegrees.CashAccount.CountryCodes>();
            }

            return new FiveDegrees.CashAccount.MovementCustomerItem()
            {
                CustomerName = item.Name,
                Accountnumber = item.AccountNumber,
                AddressLine1 = item.Street,
                AddressLine2 = item.Postcode,
                AddressLine3 = item.City,
                CountryCode = countryCode,
                CountryCodeSpecified = countryCode.HasValue
            };
        }
    }
}
