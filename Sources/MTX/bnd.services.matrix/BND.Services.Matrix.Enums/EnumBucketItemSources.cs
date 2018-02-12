using System;

namespace BND.Services.Matrix.Enums
{
    /// <summary>
    /// The Bucket Item Source
    /// </summary>
    [Flags]
    public enum EnumBucketItemSources
    {
        /// <summary>The default payment source.</summary>
        DefaultPaymentSource = 1,

        /// <summary>The sepa.</summary>
        SEPA = 2,

        /// <summary>The system internal.</summary>
        SystemInternal = 4,

        /// <summary>The internal interest.</summary>
        InternalInterest = 8,

        /// <summary>The swift.</summary>
        SWIFT = 16,
    }
}
