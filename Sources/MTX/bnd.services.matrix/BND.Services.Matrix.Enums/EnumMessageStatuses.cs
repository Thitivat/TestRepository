using System;

namespace BND.Services.Matrix.Enums
{
    /// <summary>
    /// The message status enum.
    /// </summary>
    [Flags]
    public enum EnumMessageStatuses
    {
        /// <summary>
        /// The xsd validation error message status.
        /// </summary>
        XsdValidationError = 1,

        /// <summary>
        /// The step 2 validation error message status.
        /// </summary>
        Step2ValidationError = 2,

        /// <summary>
        /// The duplication error message status.
        /// </summary>
        DuplicationError = 4,

        /// <summary>
        /// The completed.
        /// </summary>
        Completed = 8,

        /// <summary>
        /// The unknown center error message status.
        /// </summary>
        UnknownCenterError = 16,

        /// <summary>
        /// The processing message status.
        /// </summary>
        Processing = 32,

        /// <summary>
        /// The pending message status.
        /// </summary>
        Pending = 64,

        /// <summary>
        /// The link bucket to row id error message status.
        /// </summary>
        LinkBucketToRowIdError = 128
    }
}
