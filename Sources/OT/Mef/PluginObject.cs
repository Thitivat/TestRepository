using System;

namespace BND.Services.Security.OTP.Mef
{
    /// <summary>
    /// Class PluginObject contains the extension object and the matching metadata for the extension.
    /// </summary>
    /// <typeparam name="TPluginContract">The type of the t plugin contract.</typeparam>
    
    public class PluginObject<TPluginContract> : IPluginMetaData where TPluginContract : class
    {
        #region [Properties]

        /// <summary>
        /// Gets or sets the name of extension.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; private set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; private set; }
        /// <summary>
        /// Gets the plugin.
        /// </summary>
        /// <value>The plugin.</value>
        public TPluginContract Plugin { get; private set; }

        #endregion

        #region [Constructor]

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginObject{TPluginContract}" /> class.
        /// </summary>
        /// <param name="lazyIndirectReference">The lazy indirect reference.</param>
        /// <exception cref="System.NotSupportedException">TPluginContract has to be only interface.</exception>
        /// <exception cref="System.ArgumentNullException">lazyIndirectReference
        /// or
        /// lazyIndirectReference.Metadata
        /// or
        /// lazyIndirectReference.Metadata.Name
        /// or
        /// lazyIndirectReference.Metadata.Description
        /// or
        /// lazyIndirectReference.Value</exception>
        internal PluginObject(Lazy<TPluginContract, IPluginMetaData> lazyIndirectReference)
        {
            if (!typeof(TPluginContract).IsInterface)
            {
                throw new NotSupportedException("TPluginContract has to be only interface.");
            }
            if (lazyIndirectReference == null)
            {
                throw new ArgumentNullException("lazyIndirectReference");
            }
            if (lazyIndirectReference.Metadata == null)
            {
                throw new ArgumentException("lazyIndirectReference.Metadata");
            }
            if (string.IsNullOrEmpty(lazyIndirectReference.Metadata.Name))
            {
                throw new ArgumentException("lazyIndirectReference.Metadata.Name");
            }
            if (string.IsNullOrEmpty(lazyIndirectReference.Metadata.Description))
            {
                throw new ArgumentException("lazyIndirectReference.Metadata.Description");
            }
            if (lazyIndirectReference.Value == null)
            {
                throw new ArgumentException("lazyIndirectReference.Value");
            }

            Name = lazyIndirectReference.Metadata.Name;
            Description = lazyIndirectReference.Metadata.Description;
            Plugin = lazyIndirectReference.Value;
        }
        #endregion

       
    }
}
