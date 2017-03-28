
namespace BND.Services.MailMan.Enums
{
    /// <summary>
    /// The  sending modes enumeration.
    /// </summary>
    public enum EnumSendingMode
    {
        /// <summary>
        /// Mode not set.
        /// </summary>
        NotSet = 0,

        /// <summary>
        /// Send immediate.
        /// </summary>
        Immediate = 1,

        /// <summary>
        /// Send in background using job queue.
        /// </summary>
        Background = 2
    }
}
