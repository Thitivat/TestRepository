using System.Threading.Tasks;

using BND.Services.Matrix.Entities;

namespace BND.Services.Matrix.Proxy.Interfaces
{
    /// <summary>
    /// The CenterApi interface.
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
        /// The <see cref="Task"/>.
        /// </returns>
        Task<string> GetCurrentCenter(string accessToken);
    }
}
