using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BND.Services.Security.OTP.Plugins.LibsTest
{
    [TestFixture]
    public class PluginChannelResultTest
    {
        //[Test]
        //public void Test_Constructor_Success()
        //{
        //    List<PluginChannelMessage> mockError = new List<PluginChannelMessage>();

        //    MockPluginChannel reader = new MockPluginChannel(true, mockError);
        //    PluginChannelResult result = reader.Send(new ChannelParams());

        //    Assert.IsNotNull(result);
        //    Assert.AreEqual("Mock", result.PluginName);
        //    Assert.AreEqual(true, result.Success);
        //    Assert.AreEqual(mockError, result.Messages);
        //}

        [Test]
        public void Test_Constructor_Fail_NoAttribute()
        {
            MockPluginChannelInvalid reader = new MockPluginChannelInvalid();
            Assert.Throws<InvalidOperationException>(() => reader.Send(new ChannelParams()));
        }
    }
}
