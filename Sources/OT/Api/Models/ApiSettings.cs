namespace BND.Services.Security.OTP.Api.Models
{
    /// <summary>
    /// Class ApiSettings is a setting class for setting whole api.
    /// </summary>
    public class ApiSettings
    {
        /// <summary>
        /// Gets or sets the API key.
        /// </summary>
        /// <value>The API key.</value>
        public string ApiKey { get; set; }
        /// <summary>
        /// Gets or sets the account identifier.
        /// </summary>
        /// <value>The account identifier.</value>
        public string AccountId { get; set; }
        /// <summary>
        /// Gets or sets the client ip.
        /// </summary>
        /// <value>The client ip.</value>
        public string ClientIp { get; set; }
        /// <summary>
        /// Gets or sets the user agent.
        /// </summary>
        /// <value>The user agent.</value>
        public string UserAgent { get; set; }
        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        /// <value>The connection string.</value>
        public string ConnectionString { get; set; }
        /// <summary>
        /// Gets or sets the channel plugins path.
        /// </summary>
        /// <value>The channel plugins path.</value>
        public string ChannelPluginsPath { get; set; }
        /// <summary>
        /// Gets or sets the length of the otp identifier.
        /// </summary>
        /// <value>The length of the otp identifier.</value>
        public int OtpIdLength { get; set; }
        /// <summary>
        /// Gets or sets the length of the reference code.
        /// </summary>
        /// <value>The length of the reference code.</value>
        public int RefCodeLength { get; set; }
        /// <summary>
        /// Gets or sets the length of the otp code.
        /// </summary>
        /// <value>The length of the otp code.</value>
        public int OtpCodeLength { get; set; }
    }
}