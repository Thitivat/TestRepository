using BND.Services.Security.OTP.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace BND.Services.Security.OTP.Api.Utils
{
    /// <summary>
    /// Interface IApiManager represents all actions what api needs.
    /// </summary>
    public interface IApiManager : IDisposable
    {
        /// <summary>
        /// Verifies an account by using api key and account id.
        /// </summary>
        void VerifyAccount();
        /// <summary>
        /// Adds a new <a href="https://en.wikipedia.org/wiki/One-time_password" target="_blank">OTP</a> code to database.
        /// </summary>
        /// <param name="otpRequest">The <a href="https://en.wikipedia.org/wiki/One-time_password" target="_blank">OTP</a> request.</param>
        /// <returns>
        /// An <a href="https://en.wikipedia.org/wiki/One-time_password" target="_blank">OTP</a>
        /// object representing output of api which response back to client.
        /// </returns>
        OtpResultModel AddOtp(OtpRequestModel otpRequest);
        /// <summary>
        /// Verifies the <a href="https://en.wikipedia.org/wiki/One-time_password" target="_blank">OTP</a> code.
        /// </summary>
        /// <param name="otpId">The <a href="https://en.wikipedia.org/wiki/One-time_password" target="_blank">OTP</a> identifier.</param>
        /// <param name="enteredOtpCode">
        /// The entered <a href="https://en.wikipedia.org/wiki/One-time_password" target="_blank">OTP</a>
        /// code as JObject which retrieved from client.
        /// </param>
        /// <returns>An <a href="https://en.wikipedia.org/wiki/One-time_password" target="_blank">OTP</a> object as OtpModel.</returns>
        OtpModel VerifyOtp(string otpId, JObject enteredOtpCode);
        /// <summary>
        /// Gets all channel names.
        /// </summary>
        /// <returns>A collection of channel names.</returns>
        IEnumerable<string> GetChannelNames();
    }
}
