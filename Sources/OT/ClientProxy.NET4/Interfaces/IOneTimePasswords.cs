using BND.Services.Security.OTP.Models;

namespace BND.Services.Security.OTP.ClientProxy.NET4.Interfaces
{
    /// <summary>
    /// Interface IOneTimePasswords is an interface providing one time password resource.
    /// </summary>
    public interface IOneTimePasswords
    {
        /// <summary>
        /// Creates new <a href="https://en.wikipedia.org/wiki/One-time_password" target="_blank">OTP</a> code.
        /// </summary>
        /// <param name="request">The request object.</param>
        /// <returns>The otp result, it will has value when otp code has been created and sent to receiver following channel address.</returns>
        OtpResultModel NewCode(OtpRequestModel request);

        /// <summary>
        /// Verifies the <a href="https://en.wikipedia.org/wiki/One-time_password" target="_blank">OTP</a> code.
        /// </summary>
        /// <param name="otpId">The otp identifier which you got from creating new otp code.</param>
        /// <param name="otpCode">The entered otp code.</param>
        /// <returns>The otp result, it will has value when entered otp code is valid.</returns>
        OtpModel VerifyCode(string otpId, string otpCode);
    }
}
