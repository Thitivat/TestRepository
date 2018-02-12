using BND.Services.Matrix.Entities;

namespace BND.Services.Matrix.Proxy.NET4.Interfaces
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
        ServiceInfo Ping(string accessToken);
    }
}
