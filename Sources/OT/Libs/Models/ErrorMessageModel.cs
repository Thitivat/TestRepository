using System;

namespace BND.Services.Security.OTP.Models
{
    /// <summary>
    /// Class ErrorMessageModel is a model representing error message which returns from <see>
    ///         <cref>BND.Services.Security.OTP.Api</cref>
    ///     </see>
    ///     api.
    /// </summary>
    public class ErrorMessageModel
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        public string Key { get; set; }
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public int Type { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return String.Format("{0}: {1}", Key, Message);
        }
    }
}
