using System;

namespace BND.Services.Matrix.Enums
{
    /// <summary>
    /// The Bucket Item Status
    /// </summary>
    [Flags]
    public enum EnumBucketItemStatuses
    {
        /// <summary> The accepted customer profile.</summary>
        AcceptedCustomerProfile = 1,

        /// <summary>The accepted settlement completed.</summary>
        AcceptedSettlementCompleted = 2,

        /// <summary>The accepted settlement in process.</summary>
        AcceptedSettlementInProcess = 4,

        /// <summary>The accepted technical validation.</summary>
        AcceptedTechnicalValidation = 8,

        /// <summary>The accepted with change.</summary>
        AcceptedWithChange = 16,

        /// <summary>The partially accepted. </summary>
        PartiallyAccepted = 32,

        /// <summary>The pending.</summary>
        Pending = 64,

        /// <summary>The received.</summary>
        Received = 128,

        /// <summary>The rejected.</summary>
        Rejected = 256,

        /// <summary>The canceled.</summary>
        Canceled = 512,

        /// <summary>The failed.</summary>
        Failed = 1024,
    }
}
