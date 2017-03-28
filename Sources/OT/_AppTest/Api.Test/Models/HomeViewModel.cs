using BND.Services.Security.OTP.Models;
using System.Web.Mvc;

namespace BND.Services.Security.OTP.ApiTest.Models
{
    /// <summary>
    /// Class HomeViewModel contains all properties to manipulate data in the view page and these data  will be use for
    /// comunicate with otp api.
    /// </summary>
    public class HomeViewModel
    {
        /// <summary>
        /// The API key for validate the api user.
        /// </summary>
        public string ApiKey { get; set; }
        /// <summary>
        /// The account identifier for verify the api user  with api key.
        /// </summary>
        public string AccountId { get; set; }
        /// <summary>
        /// The recipient mobile number.
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// The receipient email address
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// The otp request object for request otp.
        /// </summary>
        [AllowHtml]
        public OtpRequestViewModel OtpRequest { get; set; }
        /// <summary>
        /// The api error model for contains the error object.
        /// </summary>
        public ApiErrorModel Error { get; set; }
    }
}