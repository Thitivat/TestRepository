using BND.Services.Security.OTP.Models;
using System.Web.Mvc;

namespace BND.Services.Security.OTP.ApiTest.Models
{
    public class OtpRequestViewModel
    {
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
        /// Gets or sets the payload as any object to forwards back to your system when you verify
        /// <a href="https://en.wikipedia.org/wiki/One-time_password" target="_blank">OTP</a> code.
        /// </summary>
        /// <value>The payload.</value>
        [AllowHtml]
        public string Payload { get; set; }
    }
}