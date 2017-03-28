using System.Threading.Tasks;

using BND.Services.MailMan.Entities;

namespace BND.Services.MailMan.Proxy.Interfaces
{
    /// <summary>
    /// The IMailManApi <see langwword="interface"/> is the proxy interface of the Mailman service
    /// </summary>
    public interface IMailManApi
    {
        /// <summary>
        /// Ping service
        /// </summary>
        /// <param name="accessToken">The access token</param>
        /// <returns>The specified <see cref="ServiceInfo"/> ServiceInfo entity</returns>
        Task<ServiceInfo> Ping(string accessToken);

        /// <summary>
        /// The send async.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="accessToken">
        /// The access token.
        /// </param>
        /// <returns>
        /// The generated message id as a task of <see langword="int"/>
        /// </returns>
        Task<int> SendAsync(Message message, string accessToken);
    }
}
