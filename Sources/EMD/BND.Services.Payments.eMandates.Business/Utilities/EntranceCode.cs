using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BND.Services.Payments.eMandates.Business.Utilities
{
    public class EntranceCode
    {
        /// <summary>
        /// Generates a new unique code.
        /// </summary>
        /// <returns>A new entrance code.</returns>
        public static string Generate()
        {
            return Guid.NewGuid().ToString("n");
        }
    }
}
