using System.Threading.Tasks;

using BND.Services.Matrix.Entities;

namespace BND.Services.Matrix.Proxy.Interfaces
{
    /// <summary>
    /// The CoreApi interface.
    /// </summary>
    public interface ICoreApi
    {
        /// <summary>
        /// Ping service
        /// </summary>
        /// <param name="accessToken">The access token</param>
        /// <returns>The specified <see cref="ServiceInfo"/> entity</returns>
        Task<ServiceInfo> Ping(string accessToken);
    }
}
