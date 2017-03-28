using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;

namespace BND.Services.Security.OTP.Mef
{
    /// <summary>
    /// Class PluginManager is a generic class for manage extensions by wraps the MEF framework.
    /// The MEF framework it allows to developer to build extensible components in the same contracts (interface).
    /// This class provies methods and properties for all nessesary that developer need to use the plugin.
    /// </summary>
    /// <typeparam name="TPluginContract">The type of the t plugin contract.</typeparam>
    public class PluginManager<TPluginContract> : IPluginManager<TPluginContract> where TPluginContract : class
    {
        #region [Fields]

        /// <summary>
        /// The extensions container of specified type (in this case TPluginContract).
        /// </summary>
        private CompositionContainer _catalog;

        /// <summary>
        /// Track whether Dispose has been called.
        /// </summary>
        private bool _disposed;

        #endregion

        #region [Properties]

        /// <summary>
        /// Gets the current plugin directory of extensions.
        /// </summary>
        /// <value>The plugin directory.</value>
        public string PluginDirectory { get; private set; }

        #endregion

        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginManager{TPluginContract}" /> class.
        /// </summary>
        /// <param name="pluginDirectory">The plugin directory.</param>
        /// <exception cref="System.ArgumentException">directory</exception>
        public PluginManager(string pluginDirectory)
        {
            if (String.IsNullOrEmpty(pluginDirectory))
            {
                throw new ArgumentException(Properties.Resources.Directory_Null, "pluginDirectory");
            }

            PluginDirectory = pluginDirectory;
            _catalog = GetCatalog(PluginDirectory);
        }

        #endregion

        #region [Public Methods]

        /// <summary>
        /// Loads a plugin that implements a given interface and matches the given name.
        /// The PluginObject will contains the actual plugin object as well as the relevant metadata.
        /// </summary>
        /// <param name="pluginName">Name of the plugin.</param>
        /// <returns>PluginObject&lt;TPluginContract&gt;.</returns>
        public PluginObject<TPluginContract> LoadPlugin(string pluginName)
        {
            return LoadPlugins(pluginName).SingleOrDefault();
        }

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
        /// <exception cref="System.ObjectDisposedException"></exception>
        /// <exception cref="BND.Services.Security.OTP.Mef.PluginLoadException"></exception>
        public IEnumerable<PluginObject<TPluginContract>> LoadPlugins(params string[] pluginName)
        {

            if (_disposed) throw new ObjectDisposedException(GetType().FullName);
            string nameForAll = "";
            try
            {
                List<Lazy<TPluginContract, IPluginMetaData>> lazyParts = new List<Lazy<TPluginContract, IPluginMetaData>>();

                if (pluginName == null || pluginName.Length == 0)
                {
                    // get all plugin object.
                    lazyParts = _catalog.GetExports<TPluginContract, IPluginMetaData>().ToList();
                }
                else
                {
                    nameForAll = String.Join(",", pluginName.Select(p => p));

                    foreach (string name in pluginName)
                    {
                        lazyParts.Add(_catalog.GetExports<TPluginContract, IPluginMetaData>().Single(part => part.Metadata.Name.Equals(name)));
                    }
                }

                return lazyParts.Select(lazyPart => new PluginObject<TPluginContract>(lazyPart));
            }
            catch (InvalidOperationException innerException)
            {
                throw new PluginLoadException(PluginDirectory,
                                              typeof(TPluginContract),
                                              String.Format(Properties.Resources.Extension_CannotLoad,
                                                             nameForAll, typeof(TPluginContract).Name),
                                              innerException);
            }
        }

        /// <summary>
        /// Loads the names of all the extensions from current plugin directory.
        /// </summary>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        /// <exception cref="System.ObjectDisposedException"></exception>
        public IEnumerable<string> LoadPluginNames()
        {
            if (_disposed) throw new ObjectDisposedException(GetType().FullName);

            try
            {
                IEnumerable<Lazy<TPluginContract, IPluginMetaData>> lazyParts = _catalog.GetExports<TPluginContract, IPluginMetaData>();
                return lazyParts.Select(lazyPart => lazyPart.Metadata.Name);
            }
            catch (Exception exception)
            {
                throw new PluginLoadException(PluginDirectory,
                                              typeof(TPluginContract),
                                              Properties.Resources.Plugin_NotSingle.Replace(" '{0}'", ""),
                                              exception);
            }
        }

        #endregion

        #region [Private Methods]

        /// <summary>
        /// Get the catalog from current directory.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <returns>CompositionContainer.</returns>
        /// <exception cref="System.ArgumentException">directory</exception>
        /// <exception cref="PluginLoadException">
        /// </exception>
        private CompositionContainer GetCatalog(string directory)
        {
            try
            {
                DirectoryCatalog directoryCatalog = new DirectoryCatalog(directory);

                //Create the CompositionContainer with the parts in the catalog
                return new CompositionContainer(directoryCatalog);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException(Properties.Resources.Directory_Invalid, "directory");
            }
            catch (DirectoryNotFoundException exDirNotFound)
            {
                throw new PluginLoadException(PluginDirectory,
                                              typeof(TPluginContract),
                                              String.Format(Properties.Resources.Directory_NotFound, directory),
                                              exDirNotFound);
            }
            catch (PathTooLongException exPathTooLong)
            {
                throw new PluginLoadException(PluginDirectory,
                                              typeof(TPluginContract),
                                              String.Format(Properties.Resources.Directory_PathTooLong, directory),
                                              exPathTooLong);
            }
            catch (UnauthorizedAccessException exUnauthorized)
            {
                throw new PluginLoadException(PluginDirectory,
                                              typeof(TPluginContract),
                                              String.Format(Properties.Resources.Directory_CannotAccess, directory),
                                              exUnauthorized);
            }
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                // free managed resources
                if (_catalog != null)
                {
                    _catalog.Catalog.Dispose();
                    _catalog.Dispose();
                    _catalog = null;
                }
                _disposed = true;
            }
        }
        #endregion
    }
}
