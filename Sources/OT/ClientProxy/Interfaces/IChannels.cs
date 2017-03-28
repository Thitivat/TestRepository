using System.Collections.Generic;

namespace BND.Services.Security.OTP.ClientProxy.Interfaces
{
    /// <summary>
    /// Interface IChannels is an interface providing channel resource.
    /// </summary>
    public interface IChannels
    {
        /// <summary>
        /// Gets all channel type names.
        /// </summary>
        /// <returns>The collection of channel type names, it will has value when has retrieved all channel type names.</returns>
        IList<string> GetAllChannelTypeNames();
    }
}
