using System;
using NUnit.Framework;
//using Microsoft.QualityTools.Testing.Fakes;

namespace BND.Services.Security.OTP.Plugins.SmsPluginChannelTest
{
    [TestFixture]
    public class SmsPluginChannelTest
    {
        [Test]
        public void Test_SendSms_Success()
        {
            ChannelParams channelParam = new ChannelParams()
            {
                Type = "sms",
                Sender = "BrandNewDay",
                Message = "OTP: {Otp}\r\nRef: {RefCode}",
                Address = "66946502546",
                RefCode = "ABCDEF",
                OtpCode = "123456"
            };

            SmsPluginChannel smsPlugin = new SmsPluginChannel();
            PluginChannelResult result = smsPlugin.Send(channelParam);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result);
            Assert.AreEqual("SMS", result.PluginName);
            Assert.AreEqual(true, result.Success);
        }

        [Test]
        public void Test_SendSms_Fail_WrongNumber1()
        {
            ChannelParams channelParam = new ChannelParams()
            {
                Type = "sms",
                Sender = "BrandNewDay",
                Message = "OTP: {Otp}\r\nRef: {RefCode}",
                Address = "66946502546A",
                RefCode = "ABCDEF",
                OtpCode = "123456"
            };

            SmsPluginChannel smsPlugin = new SmsPluginChannel();
            PluginChannelResult result = smsPlugin.Send(channelParam);

            Assert.IsNotNull(result);
            Assert.AreEqual("SMS", result.PluginName);
            Assert.AreEqual(false, result.Success);
            Assert.IsNotNull(result.Messages);
        }

        [Test]
        public void Test_SendSms_Fail_WrongNumber2()
        {
            ChannelParams channelParam = new ChannelParams()
            {
                Type = "sms",
                Sender = "BrandNewDay",
                Message = "OTP: {Otp}\r\nRef: {RefCode}",
                Address = "88545",
                RefCode = "ABCDEF",
                OtpCode = "123456"
            };

            SmsPluginChannel smsPlugin = new SmsPluginChannel();
            PluginChannelResult result = smsPlugin.Send(channelParam);

            Assert.IsNotNull(result);
            Assert.AreEqual("SMS", result.PluginName);
            Assert.AreEqual(false, result.Success);
            Assert.IsNotNull(result.Messages);
        }

        [Test]
        public void Test_SendSms_Fail_SenderMaximum()
        {
            ChannelParams channelParam = new ChannelParams()
            {
                Type = "sms",
                Sender = "Brand New Day", //the maximum length is 11 characters.
                Message = "OTP: {Otp}\r\nRef: {RefCode}",
                Address = "66946502546",
                RefCode = "ABCDEF",
                OtpCode = "123456"
            };

            SmsPluginChannel smsPlugin = new SmsPluginChannel();
            PluginChannelResult result = smsPlugin.Send(channelParam);

            Assert.IsNotNull(result);
            Assert.AreEqual("SMS", result.PluginName);
            Assert.AreEqual(false, result.Success);
            Assert.IsNotNull(result.Messages);
        }

        [Test]
        public void Test_SendSms_Fail_EmptySender()
        {
            ChannelParams channelParam = new ChannelParams()
            {
                Type = "sms",
                Message = "OTP: {Otp}\r\nRef: {RefCode}",
                Address = "66946502546",
                RefCode = "ABCDEF",
                OtpCode = "123456"
            };

            SmsPluginChannel smsPlugin = new SmsPluginChannel();
            Assert.Throws<ArgumentException>(() => smsPlugin.Send(channelParam));
        }

        [Test]
        public void Test_SendSms_Fail_EmptyAddress()
        {
            ChannelParams channelParam = new ChannelParams()
            {
                Type = "sms",
                Sender = "BrandNewDay",
                Message = "OTP: {Otp}\r\nRef: {RefCode}",
                RefCode = "ABCDEF",
                OtpCode = "123456"
            };

            SmsPluginChannel smsPlugin = new SmsPluginChannel();
            Assert.Throws<ArgumentException>(() => smsPlugin.Send(channelParam));
        }

        [Test]
        public void Test_SendSms_Fail_EmptyMessage()
        {
            ChannelParams channelParam = new ChannelParams()
            {
                Type = "sms",
                Sender = "BrandNewDay",
                Address = "66812345678",
                RefCode = "ABCDEF",
                OtpCode = "123456"
            };

            SmsPluginChannel smsPlugin = new SmsPluginChannel();
            Assert.Throws<ArgumentException>(() => smsPlugin.Send(channelParam));
        }

        [Test]
        public void Test_SendSms_Fail_EmptyOtpCode()
        {
            ChannelParams channelParam = new ChannelParams()
            {
                Type = "sms",
                Sender = "BrandNewDay",
                Message = "OTP: {Otp}\r\nRef: {RefCode}",
                Address = "66812345678",
                RefCode = "ABCDEF"
            };

            SmsPluginChannel smsPlugin = new SmsPluginChannel();
            Assert.Throws<ArgumentException>(() => smsPlugin.Send(channelParam));
        }

        [Test]
        public void Test_SendSms_Fail_EmptyRefCode()
        {
            ChannelParams channelParam = new ChannelParams()
            {
                Type = "sms",
                Sender = "BrandNewDay",
                Message = "OTP: {Otp}\r\nRef: {RefCode}",
                Address = "66812345678",
                OtpCode = "123456"
            };

            SmsPluginChannel smsPlugin = new SmsPluginChannel();
            Assert.Throws<ArgumentException>(() => smsPlugin.Send(channelParam));
        }

        [Test]
        public void Test_SendSms_StatusFailed()
        {
            //using (ShimsContext.Create())
            //{
            //    MessageBird.Objects.Fakes.ShimRecipient.AllInstances.StatusGet = (r) => MessageBird.Objects.Recipient.RecipientStatus.Failed;

            //    ChannelParams channelParam = new ChannelParams()
            //    {
            //        Type = "sms",
            //        Sender = "BrandNewDay",
            //        Message = "OTP: {Otp}\r\nRef: {RefCode}",
            //        Address = "66946502546",
            //        RefCode = "ABCDEF",
            //        OtpCode = "123456"
            //    };

            //    SmsPluginChannel smsPlugin = new SmsPluginChannel();
            //    PluginChannelResult actual = smsPlugin.Send(channelParam);
            //}
        }
    }
}
