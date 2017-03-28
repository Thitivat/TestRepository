using System.Collections.Generic;

namespace BND.Services.Security.OTP.Plugins.LibsTest
{
    [ChannelExport("Mock", "This is mock class")]
    internal class MockPluginChannel : IPluginChannel
    {
        bool _success;
        List<PluginChannelMessage> _errorMessage;


        public MockPluginChannel(bool success, List<PluginChannelMessage> error)
        {
            _success = success;
            _errorMessage = error;
        }

        public PluginChannelResult Send(ChannelParams parameters)
        {
            return new Plugins.PluginChannelResult(_success, _errorMessage);
        }
    }

    internal class MockPluginChannelInvalid : IPluginChannel
    {
        public PluginChannelResult Send(ChannelParams parameters)
        {
            return new Plugins.PluginChannelResult(true, null);
        }
    }

}
