using NUnit.Framework;

namespace BND.Services.Security.OTP.Plugins.LibsTest
{
    [TestFixture]
    public class PluginChannelBaseTest
    {
        [Test]
        public void Test_PluginChannelBase_Success()
        {
            TestPluginChannel plugin = new TestPluginChannel();

            ChannelParams channelParam = new ChannelParams()
            {
                Type = "sms",
                Sender = "Brand New Day",
                Message = "OTP: {Otp}\r\nRef: {RefCode}",
                Address = "6681234567",
                RefCode = "ABCDEF",
                OtpCode = "123456"
            };
            Assert.IsNull(plugin.Send(channelParam));
        }
    }
}
