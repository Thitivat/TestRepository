
namespace BND.Services.Security.OTP.Mef
{
    /// <summary>
    /// Interface IPluginMetaData is the contract that provides the properties for plugin metadata.
    /// 
    /// </summary>
    public interface IPluginMetaData
    {
        #region [Properties]

        /// <summary>
        /// Gets the name of extension.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>The description.</value>
        string Description { get; }

        #endregion

    }
}
