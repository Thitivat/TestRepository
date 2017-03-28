namespace BND.Services.Security.OTP.Plugins
{
    /// <summary>
    /// Class ChannelParams.
    /// </summary>
    public class ChannelParams
    {
        #region [Properties]
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public string Type { get; set; }
        /// <summary>
        /// Gets or sets the sender.
        /// </summary>
        /// <value>The sender.</value>
        public string Sender { get; set; }
        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>The address.</value>
        public string Address { get; set; }
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }
        /// <summary>
        /// Gets or sets the reference code.
        /// </summary>
        /// <value>The reference code.</value>
        public string RefCode { get; set; }
        /// <summary>
        /// Gets or sets the otp code.
        /// </summary>
        /// <value>The otp code.</value>
        public string OtpCode { get; set; }
        /// <summary>
        /// Gets or sets the expiry period as second.
        /// </summary>
        /// <value>The expiry period.</value>
        public double ExpiryPeriod { get; set; }
        #endregion
    }
}
