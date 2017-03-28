
namespace BND.Services.Security.OTP.Plugins
{
    /// <summary>
    /// Interface IChannelPlugin
    /// </summary>
    public interface IPluginChannel
    {
        #region [Action]

        /// <summary>
        /// Sends the specified channel.
        /// </summary>
        /// <returns>PluginChannelResult.</returns>
        PluginChannelResult Send(ChannelParams parameters);
        #endregion
    }
}
