using BND.Services.Matrix.Entities;

namespace BND.Services.Matrix.Proxy.NET4.Interfaces
{
    /// <summary>
    /// The CoreApi interface.
    /// </summary>
    public interface ICenterApi
    {
        /// <summary>
        /// The get current center.
        /// </summary>
        /// <param name="accessToken">
        /// The access token.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string GetCurrentCenter(string accessToken);
    }
}
