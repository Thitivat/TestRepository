using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BND.Services.Matrix.Enums
{
    /// <summary>
    /// The enum extra field.
    /// </summary>
    [Flags]
    public enum EnumBucketExtraField
    {
        /// <summary>
        /// The has payments buckets.
        /// </summary>
        PaymentBucketDetails = 1,
    }
}
