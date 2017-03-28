using BND.Services.Security.OTP.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BND.Services.Security.OTP.ApiUnitTest
{
    [ChannelExport("MOCK", "The mocking channel for unit test.")]
    public class MockPluginChannel : IPluginChannel
    {
        private bool _success;
        private List<PluginChannelMessage> _messages;

        public MockPluginChannel(bool success, List<PluginChannelMessage> messages)
        {
            _success = success;
            _messages = messages;
        }

        public PluginChannelResult Send(ChannelParams parameters)
        {
            return new PluginChannelResult(_success, _messages);
        }
    }
}
