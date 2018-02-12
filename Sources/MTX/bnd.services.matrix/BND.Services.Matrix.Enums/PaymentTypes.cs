using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BND.Services.Matrix.Enums
{
    /// <summary>
    /// The payment types.
    /// </summary>
    public enum PaymentTypes
    {
        /// <summary>
        /// The fee.
        /// </summary>
        Fee,

        /// <summary>
        /// The interest.
        /// </summary>
        Interest,

        /// <summary>
        /// The principal.
        /// </summary>
        Principal,

        /// <summary>
        /// The tax.
        /// </summary>
        Tax,

        /// <summary>
        /// The penalty interest.
        /// </summary>
        PenaltyInterest,

        /// <summary>
        /// The other.
        /// </summary>
        Other,
    }
}
