using BND.Services.Security.OTP.Plugins;
using System;
using System.Collections.Generic;

namespace BND.Services.Security.OTP.Interfaces
{
    /// <summary>
    /// The IChannelFactory interface provides channel as <see cref="BND.Services.Security.OTP.Plugins.IPluginChannel"/> interface for sending
    /// <a href="https://en.wikipedia.org/wiki/One-time_password" target="_blank">OTP</a> code such as sms channel and email channel.
    /// You can get any channel object what you want by passing channel type name. It has been designed by using
    /// <a href="https://msdn.microsoft.com/en-us/library/dd460648(v=vs.110).aspx">MEF</a> framework to make more flexible.
    /// </summary>
    public interface IChannelFactory : IDisposable
    {
        /// <summary>
        /// Gets the channel object by using channel name.
        /// </summary>
        /// <param name="channel">A channel name to get channel.</param>
        /// <returns>A channel object as IPluginChannel.</returns>
        IPluginChannel GetChannel(string channel);
        /// <summary>
        /// Gets all channel type names from plugin folder.
        /// </summary>
        /// <returns>The collection of all channel type names.</returns>
        IEnumerable<string> GetAllChannelTypeNames();
    }
}
