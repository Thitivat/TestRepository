namespace BND.Services.Security.OTP.Models
{
    /// <summary>
    /// Class OtpResultModel a model representing response object which retrieves from <see>
    ///         <cref>BND.Services.Security.OTP.Api</cref>
    ///     </see>
    ///     api.
    /// </summary>
    public class OtpResultModel
    {
        /// <summary>
        /// Gets or sets the identifier representing OneTimePassword table.
        /// </summary>
        /// <value>The identifier of one time password data.</value>
        public string OtpId { get; set; }
        /// <summary>
        /// Gets or sets the reference code for user easy to recognize which one for which
        /// <a href="https://en.wikipedia.org/wiki/One-time_password" target="_blank">OTP</a> code.
        /// </summary>
        /// <value>The reference code.</value>
        public string RefCode { get; set; }
    }
}
