using NUnit.Framework;
using System;

namespace BND.Services.Security.OTP.Plugins.EmailPluginChannelTest
{
    [TestFixture]
    public class EmailPluginChannelTest
    {
        [Test]
        public void Test_Send_Failed()
        {
            ChannelParams channelParam = new ChannelParams()
            {
                Type = "email",
                Sender = "Brand New Day",
                Message = "This is a fail message.",
                Address = "THIS_IS_INVALID_EMAIL_ADDRESS",
                RefCode = "RX0001",
                OtpCode = "123456"
            };

            EmailPluginChannel emailPlugin = new EmailPluginChannel();
            Assert.Throws<ArgumentException>(() => emailPlugin.Send(channelParam));
        }

        [Test]
        public void Test_Send_Success()
        {
            ChannelParams channelParam = new ChannelParams()
            {
                Type = "email",
                Sender = "Brand New Day",
                Message = "OTP: {Otp}\r\nRef: {RefCode}",
                Address = "itim@kobkiat-it.com",
                RefCode = "RX0001",
                OtpCode = "123456"
            };

            EmailPluginChannel emailPlugin = new EmailPluginChannel();
            PluginChannelResult result = emailPlugin.Send(channelParam);

            Assert.IsNotNull(result);
            Assert.AreEqual("EMAIL", result.PluginName);
            Assert.AreEqual(true, result.Success);
            Assert.IsNotNull(result.Messages);
        }

        [Test]
        public void Test_Send_Failed_Sender_Miss()
        {
            ChannelParams channelParam = new ChannelParams()
            {
                Type = "email",
                Message = "This is a miss sender.",
                Address = "itim@kobkiat-it.com",
                RefCode = "RX0001",
                OtpCode = "123456"
            };

            EmailPluginChannel emailPlugin = new EmailPluginChannel();
            Assert.Throws<ArgumentException>(() => emailPlugin.Send(channelParam));
        }

        [Test]
        public void Test_Send_Exception_ChannelNull()
        {
            EmailPluginChannel emailPlugin = new EmailPluginChannel();
            Assert.Throws<ArgumentNullException>(() => emailPlugin.Send(null));
        }

        [Test]
        public void Test_Send_Failed_Message_Miss()
        {
            ChannelParams channelParam = new ChannelParams()
            {
                Type = "email",
                Sender = "Brand New Day",
                Address = "itim@kobkiat-it.com",
                RefCode = "RX0001",
                OtpCode = "123456"
            };

            EmailPluginChannel emailPlugin = new EmailPluginChannel();
            Assert.Throws<ArgumentException>(() => emailPlugin.Send(channelParam));
        }

        [Test]
        public void Test_Send_Failed_Address_Miss()
        {
            ChannelParams channelParam = new ChannelParams()
            {
                Type = "email",
                Sender = "Brand New Day",
                Message = "This is a miss address.",
                RefCode = "RX0001",
                OtpCode = "123456"
            };

            EmailPluginChannel emailPlugin = new EmailPluginChannel();
            Assert.Throws<ArgumentException>(() => emailPlugin.Send(channelParam));
        }

        [Test]
        public void Test_Send_Failed_OtpCode_Miss()
        {
            ChannelParams channelParam = new ChannelParams()
            {
                Type = "email",
                Sender = "Brand New Day",
                Message = "This is a miss address.",
                Address = "itim@kobkiat-it.com",
                RefCode = "RX0001"
            };

            EmailPluginChannel emailPlugin = new EmailPluginChannel();
            Assert.Throws<ArgumentException>(() => emailPlugin.Send(channelParam));
        }

        [Test]
        public void Test_Send_Failed_RefCode_Miss()
        {
            ChannelParams channelParam = new ChannelParams()
            {
                Type = "email",
                Sender = "Brand New Day",
                Message = "This is a miss address.",
                Address = "itim@kobkiat-it.com",
                OtpCode = "123456"
            };

            EmailPluginChannel emailPlugin = new EmailPluginChannel();
            Assert.Throws<ArgumentException>(() => emailPlugin.Send(channelParam));
        }
    }
}
