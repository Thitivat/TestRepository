using BND.Services.Security.OTP.Mef;
using BND.Services.Security.OTP.Interfaces;
using BND.Services.Security.OTP.Plugins;
using System;
using System.Collections.Generic;

namespace BND.Services.Security.OTP
{
    /// <summary>
    /// Class ChannelFactory is a class which implements <see cref="BND.Services.Security.OTP.Interfaces.IChannelFactory"/> interface to provides channels
    /// for sending <a href="https://en.wikipedia.org/wiki/One-time_password" target="_blank">OTP</a> code.
    /// </summary>
    public class ChannelFactory : IChannelFactory
    {
        #region [Fields]

        /// <summary>
        /// The disposed flag.
        /// </summary>
        public bool Disposed;
        /// <summary>
        /// The _pluginmanager field as IPluginManager interface for loading plugin.
        /// </summary>
        IPluginManager<IPluginChannel> _pluginManager;

        #endregion


        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelFactory"/> class.
        /// </summary>
        /// <param name="pluginPath">The plugin path.</param>
        /// <exception cref="BND.Services.Security.OTP.ChannelOperationException">When could not initialize plugin. Error code = 1</exception>
        public ChannelFactory(string pluginPath)
        {
            try
            {
                // Creates new instance of PluginManager.
                _pluginManager = new PluginManager<IPluginChannel>(pluginPath);
            }
            catch (Exception ex)
            {
                // Throws custom exception with error code.
                throw new ChannelOperationException(1, Properties.Resources.ErrorInit, ex);
            }
        }

        #endregion


        #region [Methods]

        /// <summary>
        /// Gets the channel object by using channel name.
        /// </summary>
        /// <param name="channel">A channel name to get channel.</param>
        /// <returns>A channel object as IPluginChannel.</returns>
        /// <exception cref="BND.Services.Security.OTP.ChannelOperationException">When could not retrieve plugin. Error code = 2</exception>
        public IPluginChannel GetChannel(string channel)
        {
            try
            {
                // Loads plugin by passing channel type name then returns plugin.
                return _pluginManager.LoadPlugin(channel).Plugin;
            }
            catch (Exception ex)
            {
                // Throws custom exception with error code.
                throw new ChannelOperationException(2, String.Format(Properties.Resources.ErrorLoadPlugin, channel), ex);
            }
        }

        /// <summary>
        /// Gets all channel type names from plugin folder.
        /// </summary>
        /// <returns>The collection of all channel type names.</returns>
        /// <exception cref="BND.Services.Security.OTP.ChannelOperationException">When could not retrieve plugin type names. Error code = 3</exception>
        public IEnumerable<string> GetAllChannelTypeNames()
        {
            try
            {
                // Loads all plugin type names then returns as channel type names.
                return _pluginManager.LoadPluginNames();
            }
            catch (Exception ex)
            {
                // Throws custom exception with error code.
                throw new ChannelOperationException(3, Properties.Resources.ErrorLoadName, ex);
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Dispose()
        {
            Dispose(true);
            // Clears garbage collector.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Releases all resources.
                if (_pluginManager != null)
                {
                    _pluginManager.Dispose();
                    _pluginManager = null;
                }

                // Sets dispose flag.
                Disposed = true;
            }
        }

        #endregion
    }
}
