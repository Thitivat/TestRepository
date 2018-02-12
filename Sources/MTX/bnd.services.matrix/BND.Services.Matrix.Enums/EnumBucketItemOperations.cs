using System;

namespace BND.Services.Matrix.Enums
{
    /// <summary>
    /// The Bucket Item Operation
    /// </summary>
    [Flags]
    public enum EnumBucketItemOperations
    {
        /// <summary> The cred.</summary>
        CRED = 1,

        /// <summary>The crts.</summary>
        CRTS = 2,

        /// <summary>The spay.</summary>
        SPAY = 4,

        /// <summary>The spri.</summary>
        SPRI = 8,

        /// <summary>The sstd.</summary>
        SSTD = 16,
    }
}
