using BND.Services.Security.OTP.Models;

namespace BND.Services.Security.OTP.ApiTest.Models
{
    /// <summary>
    /// Class VerifyOtpModel contains all needed parameters for verify otp page. This object will be use send to verify
    /// otp code.
    /// </summary>
    public class VerifyOtpModel
    {
        /// <summary>
        /// The API key for validate the api user.
        /// </summary>
        public string ApiKey { get; set; }
        /// <summary>
        /// The otp identifier is the id that represent the otp object for verify otp code.
        /// </summary>
        public string OtpId { get; set; }
        /// <summary>
        /// The account identifier for verify the api user  with api key.
        /// </summary>
        public string AccountId { get; set; }
        /// <summary>
        /// The otp code for verify.
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// The otp message.
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// The reference code that represent otp code.
        /// </summary>
        public string RefCode { get; set; }
        /// <summary>
        /// The error model for show in verify otp view.
        /// </summary>
        public ApiErrorModel Error { get; set; }
    }
}