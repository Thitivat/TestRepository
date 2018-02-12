using System;
using System.Collections.Generic;

using BND.Services.Matrix.Entities;
using BND.Services.Matrix.Entities.QueryStringModels;

namespace BND.Services.Matrix.Proxy.NET4.Interfaces
{
    /// <summary>
    /// The AccountsApi interface.
    /// </summary>
    public interface IAccountsApi
    {
        /// <summary>
        /// Creates a savings account
        /// </summary>
        /// <param name="savingsFree"> The savings free. </param>
        /// <param name="accessToken">The access token</param>
        /// <returns>
        /// The created account iban
        /// </returns>
        string CreateSavingsAccount(SavingsFree savingsFree, string accessToken);

        /// <summary>
        /// Gets an interest rate.
        /// </summary>
        /// <param name="iban"> The iban. </param>
        /// <param name="options"> The options. </param>
        /// <param name="accessToken">The access token</param>
        /// <returns>
        /// A <see cref="List{T}"/> where T is of type <see cref="InterestRate"/>
        /// </returns>
        List<InterestRate> GetInterestRate(string iban, InterestRateOptions options, string accessToken);

        /// <summary>
        /// Unblock savings accounts.
        /// </summary>
        /// <param name="iban"> The iban. </param>
        /// <param name="accessToken">The access token</param>
        void UnblockSavingAccounts(string iban, string accessToken);

        /// <summary>
        /// Gets the accrued interest.
        /// </summary>
        /// <param name="iban"> The iban. </param>
        /// <param name="valueDate"> The value date. </param>
        /// <param name="calculateTaxAction"> The calculate tax action. </param>
        /// <param name="accessToken">The access token</param>
        /// <returns>
        /// The accrued interest
        /// </returns>
        decimal GetAccruedInterest(string iban, DateTime valueDate, bool calculateTaxAction, string accessToken);

        /// <summary>
        /// Gets the balance overview.
        /// </summary>
        /// <param name="iban"> The iban. </param>
        /// <param name="valueDate"> The value date. </param>
        /// <param name="accessToken">The access token</param>
        /// <returns>
        /// The balance overview
        /// </returns>
        decimal GetBalanceOverview(string iban, DateTime valueDate, string accessToken);

        /// <summary>
        /// Gets the movements overview.
        /// </summary>
        /// <param name="iban"> The iban. </param>
        /// <param name="startDate"> The start date. </param>
        /// <param name="endDate"> The end date. </param>
        /// <param name="accessToken">The access token</param>
        /// <returns>
        /// A <see cref="List{T}"/> where T is of type <see cref="MovementItem"/>
        /// </returns>
        List<MovementItem> GetMovements(string iban, DateTime? startDate, DateTime? endDate, string accessToken);

        /// <summary>
        /// Gets a saving account overview.
        /// </summary>
        /// <param name="iban"> The iban. </param>
        /// <param name="valueDate"> The value date. </param>
        /// <param name="accessToken">The access token</param>
        /// <returns>
        /// The account overview
        /// </returns>
        AccountOverview GetAccountOverview(string iban, DateTime valueDate, string accessToken);

        /// <summary>
        /// Block saving accounts.
        /// </summary>
        /// <param name="iban"> The iban. </param>
        /// <param name="accessToken">The access token</param>
        void BlockSavingAccounts(string iban, string accessToken);

        /// <summary>
        /// Create outgoing payment.
        /// </summary>
        /// <param name="iban"> The iban. </param>
        /// <param name="payment"> The payment. </param>
        /// <param name="accessToken">The access token</param>
        /// <returns>
        /// The created outgoing payment id
        /// </returns>
        string CreateOutgoingPayment(string iban, Payment payment, string accessToken);

        /// <summary>
        /// The close account.
        /// </summary>
        /// <param name="iban">
        /// The iban.
        /// </param>
        /// <param name="closingPaymentItem">
        /// The closing payment item.
        /// </param>
        /// <param name="accessToken">
        /// The access token.
        /// </param>
        /// <returns>
        /// The <see cref="AccountCloseResultsItem"/>.
        /// </returns>
        AccountCloseResultsItem CloseAccount(string iban, ClosingPaymentItem closingPaymentItem, string accessToken);

    }
}
