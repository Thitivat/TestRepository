using BND.Services.Security.OTP.Models;
using Newtonsoft.Json.Linq;
using System.Web.Http;

namespace BND.Services.Security.OTP.Api.Controllers
{
    /// <summary>
    /// OneTimePasswordsController class providing one time password resource to client.
    /// </summary>
    public class OneTimePasswordsController : ApiControllerBase
    {
        /// <summary>
        /// The action to creates a new <a href="https://en.wikipedia.org/wiki/One-time_password" target="_blank">OTP</a> code to system.
        /// </summary>
        /// <param name="otpRequest">A one time password request object.</param>
        /// <returns>IHttpActionResult.</returns>
        public IHttpActionResult Post([FromBody]OtpRequestModel otpRequest)
        {
            // Calls Process method from base class to perform work properly.
            return Process(() => Ok(ApiManager.AddOtp(otpRequest)));
        }

        /// <summary>
        /// The action to verifies the specified <a href="https://en.wikipedia.org/wiki/One-time_password" target="_blank">OTP</a> code.
        /// </summary>
        /// <param name="otpId">The <a href="https://en.wikipedia.org/wiki/One-time_password" target="_blank">OTP</a> identifier.</param>
        /// <param name="otpCode">The entered code.</param>
        /// <returns>IHttpActionResult.</returns>
        [HttpPut]
        [Route("{otpId}/Verify")]
        public IHttpActionResult Verify(string otpId, [FromBody]JObject otpCode)
        {
            // Calls Process method from base class to perform work properly.
            return Process(() => Ok(ApiManager.VerifyOtp(otpId, otpCode)));
        }
    }
}
