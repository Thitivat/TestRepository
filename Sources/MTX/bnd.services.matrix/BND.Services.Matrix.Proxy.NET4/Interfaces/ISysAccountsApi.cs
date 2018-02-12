using System;
using System.Collections.Generic;

using BND.Services.Matrix.Entities;

namespace BND.Services.Matrix.Proxy.NET4.Interfaces
{
    /// <summary>
    /// The SysAccountsApi interface.
    /// </summary>
    public interface ISysAccountsApi
    {
        /// <summary>
        /// Gets system accounts overviews.
        /// </summary>
        /// <param name="sysId">
        /// The sys Ids.
        /// </param>
        /// <param name="valueDate">
        /// The value date.
        /// </param>
        /// <param name="accessToken">
        /// The access token.
        /// </param>
        /// <returns>
        /// A <see cref="List{T}"/> where T is <see cref="AccountOverview"/>.
        /// </returns>
        AccountOverview GetAccountOverview(string sysId, DateTime valueDate, string accessToken);

        /// <summary>
        /// Gets system accounts.
        /// </summary>
        /// <param name="accessToken">The access token</param>
        /// <returns>
        /// A <see cref="List{T}"/> where T is <see cref="SystemAccount"/>..
        /// </returns>
        List<SystemAccount> GetSystemAccounts(string accessToken);

        /// <summary>
        /// Gets movements.
        /// </summary>
        /// <param name="sysId">
        /// The sys Id.
        /// </param>
        /// <param name="startDate">
        /// The start date.
        /// </param>
        /// <param name="endDate">
        /// The end date.
        /// </param>
        /// <param name="accessToken">
        /// The access Token.
        /// </param>
        /// <returns>
        /// A <see cref="List{T}"/> where T is of type <see cref="MovementItem"/>
        /// </returns>
        List<MovementItem> GetMovements(string sysId, DateTime? startDate, DateTime? endDate, string accessToken);

        /// <summary>
        /// The sweep.
        /// </summary>
        /// <param name="sweep"> The sweep parameter. </param>
        /// <param name="accessToken">The access token</param>
        /// <returns>
        /// A decimal
        /// </returns>
        decimal Sweep(Sweep sweep, string accessToken);

        /// <summary>
        /// Gets system accounts balance.
        /// </summary>
        /// <param name="sysId">
        /// The sys Id.
        /// </param>
        /// <param name="valueDate">
        /// The value date.
        /// </param>
        /// <param name="accessToken">
        /// The access token.
        /// </param>
        /// <returns>
        /// The balance.
        /// </returns>
        decimal GetBalanceOverview(string sysId, DateTime valueDate, string accessToken);

        /// <summary>
        /// Returns a payment.
        /// </summary>
        /// <param name="returnPayment">
        /// The return payment.
        /// </param>
        /// <param name="accessToken"> The access token. </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool ReturnPayment(ReturnPayment returnPayment, string accessToken);

        /// <summary>
        /// Create Outgoing Return bucket
        /// </summary>
        /// <param name="returnBucketItem">
        /// The return bucket item
        /// </param>
        /// <param name="accessToken">
        /// The access token. 
        /// </param>
        /// <returns></returns>
        string CreateOutgoingReturnBucket(ReturnBucketItem returnBucketItem, string accessToken);
    }
}
