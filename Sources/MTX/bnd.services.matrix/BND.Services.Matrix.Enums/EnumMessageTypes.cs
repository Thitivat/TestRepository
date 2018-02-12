using System;

namespace BND.Services.Matrix.Enums
{
    /// <summary>
    /// The message type enum.
    /// </summary>
    [Flags]
    public enum EnumMessageTypes
    {
        /// <summary>
        /// The Pacs008 message type.
        /// </summary>
        Pacs008 = 1,

        /// <summary>
        /// The Pacs004 message type.
        /// </summary>
        Pacs004 = 2,

        /// <summary>
        /// The MT940 message type.
        /// </summary>
        Mt940 = 4,

        /// <summary>
        /// The MT940Auto message type.
        /// </summary>
        Mt940Auto = 8
    }
}
