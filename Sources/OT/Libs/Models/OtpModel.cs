
using System;
namespace BND.Services.Security.OTP.Models
{
    /// <summary>
    /// Class OtpModel is a model representing request object for using with <see>
    ///         <cref>BND.Services.Security.OTP.Api</cref>
    ///     </see>
    ///     api.
    /// </summary>
    public class OtpModel
    {
        /// <summary>
        /// Gets or sets the identifier representing OneTimePassword table.
        /// </summary>
        /// <value>The identifier of one time password data.</value>
        public string Id { get; set; }
        /// <summary>
        /// Gets or sets the The reference number for hookup with your system.
        /// </summary>
        /// <value>The reference number for hookup with your system.</value>
        public string Suid { get; set; }
        /// <summary>
        /// Gets or sets the channel object which you want to use for sending
        /// <a href="https://en.wikipedia.org/wiki/One-time_password" target="_blank">OTP</a> code by.
        /// </summary>
        /// <value>The channel.</value>
        public ChannelModel Channel { get; set; }
        /// <summary>
        /// Gets or sets the expiry date of
        /// <a href="https://en.wikipedia.org/wiki/One-time_password" target="_blank">OTP</a> code.
        /// </summary>
        /// <value>The expiry date.</value>
        public DateTime ExpiryDate { get; set; }
        /// <summary>
        /// Gets or sets the payload as any object to forwards back to your system when you verify
        /// <a href="https://en.wikipedia.org/wiki/One-time_password" target="_blank">OTP</a> code.
        /// </summary>
        /// <value>The payload.</value>
        public string Payload { get; set; }
        /// <summary>
        /// Gets or sets the reference code for user easy to recognize which one for which
        /// <a href="https://en.wikipedia.org/wiki/One-time_password" target="_blank">OTP</a> code.
        /// </summary>
        /// <value>The reference code.</value>
        public string RefCode { get; set; }
        /// <summary>
        /// Gets or sets the status of
        /// <a href="https://en.wikipedia.org/wiki/One-time_password" target="_blank">OTP</a> code.
        /// </summary>
        /// <value>The status.</value>
        public string Status { get; set; }
    }
}
