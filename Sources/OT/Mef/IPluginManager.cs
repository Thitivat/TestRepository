using System;
using System.Collections.Generic;

namespace BND.Services.Security.OTP.Mef
{
    /// <summary>
    /// Interface IPluginManager is the contract that provides the property and functions for manipulate plugin using by 
    /// <see>
    ///     <cref>PluginManager</cref>
    /// </see>
    /// </summary>
    /// <typeparam name="TPluginContract">The type of the t plugin contract.</typeparam>
    public interface IPluginManager<TPluginContract> : IDisposable where TPluginContract : class
    {
        /// <summary>
        /// Gets the current plugin directory of extensions.
        /// </summary>
        /// <value>The plugin directory.</value>
        string PluginDirectory { get; }

        /// <summary>
        /// Loads a plugin that implements a given interface and matches the given name.
        /// The PluginObject will contains the actual plugin object as well as the relevant metadata.
        /// </summary>
        /// <param name="pluginName">Name of the plugin.</param>
        /// <returns>PluginObject&lt;TPluginContract&gt;.</returns>
        PluginObject<TPluginContract> LoadPlugin(string pluginName);

        /// <summary>
        /// Loads all the extensions for all the extensions of type TPluginContract.
        /// Loads all the plugins for all the extensions of type TPluginContract following the pluginName parameters,
        /// if parameters is null method will return all <see>
        ///         <cref>BND.Services.Security.OTP.Mef.PluginObject</cref>
        ///     </see>
        ///     from current plugin directory.
        /// </summary>
        /// <param name="pluginName">Name of the plugin.</param>
        /// <returns>IEnumerable&lt;PluginObject&lt;TPluginContract&gt;&gt;.</returns>
        IEnumerable<PluginObject<TPluginContract>> LoadPlugins(params string[] pluginName);

        /// <summary>
        /// Loads the names of all the extensions from current plugin directory.
        /// </summary>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        IEnumerable<string> LoadPluginNames();
    }
}
