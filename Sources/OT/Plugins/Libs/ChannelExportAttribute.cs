using System;
using System.ComponentModel.Composition;

namespace BND.Services.Security.OTP.Plugins
{
    /// <summary>
    /// Class ChannelExportAttribute.
    /// </summary>
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ChannelExportAttribute : ExportAttribute
    {
        #region [Properties]
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelExportAttribute"/> class.
        /// </summary>
        /// <param name="pluginname">The pluginname.</param>
        /// <param name="description">The description.</param>
        /// <exception cref="System.ArgumentNullException">
        /// pluginName
        /// or
        /// description
        /// </exception>
        public ChannelExportAttribute(string pluginname, string description)
            : base(typeof(IPluginChannel))
        {
            if (String.IsNullOrWhiteSpace(pluginname))
            {
                throw new ArgumentNullException("pluginname");
            }
            if (String.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentNullException("description");
            }
            Name = pluginname;
            Description = description;
        }
        #endregion
    }
}
