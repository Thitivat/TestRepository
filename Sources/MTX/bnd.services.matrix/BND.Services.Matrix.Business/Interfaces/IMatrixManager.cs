using System;
using System.Collections.Generic;

using BND.Services.Matrix.Entities;
using BND.Services.Matrix.Entities.QueryStringModels;
using BND.Services.Matrix.Enums;

namespace BND.Services.Matrix.Business.Interfaces
{
    /// <summary>
    /// The matrix manager <see langword="interface"/>
    /// </summary>
    public interface IMatrixManager
    {
        /// <summary>
        /// This method creates a savings account (This method requires access authentication)
        /// </summary>
        /// <param name="savingsFree">
        /// The <see cref="Entities.SavingsFree"/> entity
        /// </param>
        void CreateSavingsAccount(Entities.SavingsFree savingsFree);

        /// <summary>
        /// Closes an account.
        /// </summary>
        /// <param name="iban"> The iban. </param>
        /// <param name="closingPaymentItem"> The <see cref="Entities.ClosingPaymentItem"/> </param>
        /// <returns>
        /// The <see cref="AccountCloseResultsItem"/>.
        /// </returns>
        Entities.AccountCloseResultsItem CloseAccount(string iban, Entities.ClosingPaymentItem closingPaymentItem);

        /// <summary>
        /// The block saving accounts.
        /// </summary>
        /// <param name="iban"> The iban. </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool BlockSavingAccounts(string iban);

        /// <summary>
        /// The unblock saving accounts.
        /// </summary>
        /// <param name="iban"> The iban. </param>
        void UnblockSavingAccounts(string iban);

        /// <summary>
        /// The get accrued interest.
        /// </summary>
        /// <param name="iban"> The iban. </param>
        /// <param name="valueDate"> The value date. </param>
        /// <param name="calculateTaxAction"> The calculate tax action. </param>
        /// <returns>
        /// The <see cref="decimal"/>.
        /// </returns>
        decimal GetAccruedInterest(string iban, DateTime valueDate, bool calculateTaxAction);

        /// <summary>
        /// The get overview balance.
        /// </summary>
        /// <param name="iban"> The iban. </param>
        /// <param name="valueDate"> The value date. </param>
        /// <returns>
        /// The <see cref="decimal"/>.
        /// </returns>
        decimal GetBalanceOverview(string iban, DateTime valueDate);

        /// <summary>
        /// Gets an overview of the movements for specified <paramref name="iban"/> between dates specified
        /// </summary>
        /// <param name="iban"> The iban. </param>
        /// <param name="startDate"> The start date. </param>
        /// <param name="endDate"> The end date. </param>
        /// <returns>
        /// The <see cref="List{T}"/> where T is <see cref="Entities.MovementItem"/> .
        /// </returns>
        List<Entities.MovementItem> GetMovements(string iban, DateTime? startDate, DateTime? endDate = null);

        /// <summary>
        /// The get interest rate override.
        /// </summary>
        /// <param name="iban"> The iban. </param>
        /// <param name="options"> The options. </param>
        /// <returns>
        /// The <see cref="List{T}"/> where T is of type <see cref="Entities.InterestRate"/>.
        /// </returns>
        List<Entities.InterestRate> GetInterestRate(string iban, InterestRateOptions options);

        /// <summary>
        /// The get overview account.
        /// </summary>
        /// <param name="iban"> The iban. </param>
        /// <param name="valueDate"> The value date. </param>
        /// <returns>
        /// The <see cref="Entities.AccountOverview"/>.
        /// </returns>
        Entities.AccountOverview GetAccountOverview(string iban, DateTime valueDate);

        /// <summary>
        /// The sweep.
        /// </summary>
        /// <param name="sweep"> The sweep entity. </param>
        /// <returns>
        /// The <see cref="decimal"/>.
        /// </returns>
        decimal Sweep(Entities.Sweep sweep);

        /// <summary>
        /// The create outgoing payment.
        /// </summary>
        /// <param name="iban"> The iban. </param>
        /// <param name="payment"> The payment. </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string CreateOutgoingPayment(string iban, Entities.Payment payment);

        /// <summary>
        /// The get system accounts.
        /// </summary>
        /// <returns>
        /// The <see cref="List{T}"/> where T is of type <see cref="Entities.SystemAccount"/>
        /// </returns>
        List<Entities.SystemAccount> GetSystemAccounts();

        /// <summary>
        /// Gets messages from Matrix.
        /// </summary>
        /// <param name="filters"> The <see cref="MessageFilters"/>.</param>
        /// <returns>
        /// The <see cref="List{T}"/> where T is of type <see cref="Entities.Message"/>.
        /// </returns>
        List<Entities.Message> GetMessages(MessageFilters filters);

        /// <summary>
        /// Gets a message's details from Matrix.
        /// </summary>
        /// <param name="id"> The message id.  </param>
        /// <param name="msgType"> The <see cref="EnumMessageTypes"/> </param>
        /// <returns>
        /// The <see cref="Entities.MessageDetail"/>.
        /// </returns>
        Entities.MessageDetail GetMessageDetails(int id, EnumMessageTypes msgType);

        /// <summary>
        /// The get buckets.
        /// </summary>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <param name="fields">
        /// The fields.
        /// </param>
        /// <returns>
        /// The <see cref="Entities.BucketItem"/>.
        /// </returns>
        List<Entities.BucketItem> GetBuckets(Entities.QueryStringModels.BucketItemFilters filter, BucketExtraFields fields);

        /// <summary>
        /// The get bucket.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Entities.BucketItem"/>.
        /// </returns>
        Entities.BucketItem GetBucket(string id);

        /// <summary>
        /// the return payment.
        /// </summary>
        /// <param name="returnPayment">
        /// The return payment
        /// </param>
        /// <returns></returns>
        bool ReturnPayment(ReturnPayment returnPayment);

        /// <summary>
        /// Creates a return outgoing bucket
        /// </summary>
        /// <param name="returnBucketItem">
        /// The return bucket
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string CreateOutgoingReturnBucket(ReturnBucketItem returnBucketItem);
    }
}
