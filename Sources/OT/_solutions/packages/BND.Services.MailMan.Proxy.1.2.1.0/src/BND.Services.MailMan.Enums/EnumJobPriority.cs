namespace BND.Services.MailMan.Enums
{
    /// <summary>
    /// The enum job priority.
    /// </summary>
    public enum EnumJobPriority
    {
        /// <summary>
        /// direct.
        /// </summary>
        Direct = 0,

        /// <summary>
        /// as soon as possible.
        /// </summary>
        AsSoonAsPossible = 9,

        /// <summary>
        ///  first in first out.
        /// </summary>
        FirstInFirstOut = 99,

        /// <summary>
        /// as late as possible.
        /// </summary>
        AsLateAsPossible = 999
    }
}
