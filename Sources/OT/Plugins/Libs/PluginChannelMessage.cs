namespace BND.Services.Security.OTP.Plugins
{
    /// <summary>
    /// Class PluginChannelMessage.
    /// </summary>
    public class PluginChannelMessage
    {
        #region [Properties]
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        public string Key { get; set; }
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public int Type { get; set; }
        #endregion
    }
}
