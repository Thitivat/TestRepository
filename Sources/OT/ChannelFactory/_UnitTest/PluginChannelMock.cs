using BND.Services.Security.OTP.Plugins;
using System;

namespace BND.Services.Security.OTP.ChannelFactoryTest
{
    public class PluginChannelMock : IPluginChannel
    {
        public PluginChannelResult Send(ChannelParams parameters)
        {
            throw new NotImplementedException();
        }
    }
}
