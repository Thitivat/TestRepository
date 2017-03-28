using System;

namespace BND.Services.Security.OTP.Mef
{
    /// <summary>
    /// The PluginLoadException class is a custom exception class for used with extension manager class
    /// This class inherit from <see cref="System.Exception"/> class.
    /// </summary>
    public class PluginLoadException : Exception
    {
        #region [Properties]

        /// <summary>
        /// Gets the plugin directory.
        /// </summary>
        /// <value>The plugin directory.</value>
        public string PluginDirectory { get; private set; }

        /// <summary>
        /// Gets the type of the plugin contract.
        /// </summary>
        /// <value>The type of the plugin contract.</value>
        public Type PluginContractType { get; private set; }

        #endregion

        #region [Constructor]

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginLoadException"/> class.
        /// </summary>
        /// <param name="pluginDirectory">The plugin directory.</param>
        /// <param name="pluginContractType">Type of the plugin contract.</param>
        public PluginLoadException(string pluginDirectory, Type pluginContractType)
            : this(pluginDirectory, pluginContractType, null, null)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginLoadException"/> class.
        /// </summary>
        /// <param name="pluginDirectory">The plugin directory.</param>
        /// <param name="pluginContractType">Type of the plugin contract.</param>
        /// <param name="message">The message.</param>
        public PluginLoadException(string pluginDirectory, Type pluginContractType, string message)
            : this(pluginDirectory, pluginContractType, message, null)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginLoadException"/> class.
        /// </summary>
        /// <param name="pluginDirectory">The plugin directory.</param>
        /// <param name="pluginContractType">Type of the plugin contract.</param>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public PluginLoadException(string pluginDirectory, Type pluginContractType, string message, Exception innerException)
            : base(message, innerException)
        {
            PluginDirectory = pluginDirectory;
            PluginContractType = pluginContractType;
        }
        #endregion
    }
}
