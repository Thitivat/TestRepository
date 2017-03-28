using System;

namespace BND.Services.Payments.iDeal.Utilities
{
    /// <summary>
    /// Class EntranceCode for generating entrance code what uses with iDeal service when you want to send a transaction request.
    /// </summary>
    public class EntranceCode
    {
        /// <summary>
        /// Generates a new unique code.
        /// </summary>
        /// <returns>A new entrance code.</returns>
        public static string GenerateCode()
        {
            return Guid.NewGuid().ToString("n");
        }
    }
}
