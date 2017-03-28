namespace BND.Services.Security.OTP.Models
{
    /// <summary>
    /// Class ChannelModel is a model representing channel which you want to use for sending
    /// <a href="https://en.wikipedia.org/wiki/One-time_password" target="_blank">OTP</a> code by.
    /// </summary>
    public class ChannelModel
    {
        /// <summary>
        /// Gets or sets the channel type name.
        /// </summary>
        /// <value>The channel type name.</value>
        public string Type { get; set; }
        /// <summary>
        /// Gets or sets the sender name.
        /// </summary>
        /// <value>The sender name.</value>
        public string Sender { get; set; }
        /// <summary>
        /// Gets or sets the endpoint address for sending.
        /// </summary>
        /// <value>The endpoint address for sending.</value>
        public string Address { get; set; }
        /// <summary>
        /// Gets or sets the message what you want to send.
        /// </summary>
        /// <value>The message what you want to send.</value>
        public string Message { get; set; }
    }
}
