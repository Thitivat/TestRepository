using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BND.Services.Payments.iDeal.Models
{
    /// <summary>
    /// Class MessageModel that contains the message properties when we need to use the in <see cref="BND.Services.Payments.iDeal.ErrorMessages"/> class.
    /// </summary>
    public class MessageModel
    {
        /// <summary>
        /// Gets or sets the code of message.
        /// </summary>
        /// <value>The code.</value>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }
    }
}
