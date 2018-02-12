using System;

namespace BND.Services.Matrix.Enums
{
    /// <summary>
    /// The enum bucket item type.
    /// </summary>
    [Flags]
    public enum EnumBucketItemTypes
    {
        /// <summary> The incoming.</summary>
        Incoming = 1,

        /// <summary> The outgoing.</summary>
        Outgoing = 2,

        /// <summary> The withdrawal notice.</summary>
        WithdrawalNotice = 4,

        /// <summary> The in and outgoing.</summary>
        InAndOutgoing = 8,
    }
}
