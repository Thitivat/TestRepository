using BND.Services.Security.OTP.Api;
using BND.Services.Security.OTP.Api.Models;
using BND.Services.Security.OTP.Api.Utils;
using BND.Services.Security.OTP.Dal.Pocos;
using BND.Services.Security.OTP.Interfaces;
using BND.Services.Security.OTP.Models;
using BND.Services.Security.OTP.Plugins;
using BND.Services.Security.OTP.Repositories.Interfaces;
//using MsTest = Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Moq;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BND.Services.Security.OTP.ApiUnitTest
{
    [TestFixture]
    public class ApiManagerTest
    {
        private ApiSettings _apiSettings = new ApiSettings
        {
            AccountId = "5861F73F-CAD5-419D-96D4-56BD07211297",
            ApiKey = "1phM4nLk14tefH8cntFJfuINtH0w_POg1zdKO9EPiu28TYjwLH0mOWzvcFiD0h3pPvf9wlhxhYk5hA6Ur0BHg8InK91GwhfCbW4kQU_6KkbKTb1H9gkOqTnFZxY4lPyl",
            ChannelPluginsPath = @"D:\",
            ClientIp = "127.0.0.1",
            ConnectionString = "Server=.;Trusted_Connection=False;MultipleActiveResultSets=true;",
            OtpCodeLength = 6,
            OtpIdLength = 128,
            RefCodeLength = 6,
            UserAgent = "user agent"
        };
        private OtpRequestModel _otpRequestModelMock = new OtpRequestModel
        {
            Channel = new ChannelModel
            {
                Type = "SMS",
                Address = "Test@Email.com",
                Message = "Test message",
                Sender = "Sender",
            },
            Payload = "payload",
            Suid = "suid"
        };
        private Setting _settingExpireMock = new Setting
        {
            Key = "Expiration",
            Value = "800"
        };
        private Setting _settingRetryCountMock = new Setting
        {
            Key = "RetryCount",
            Value = "3"
        };

        [Test]
        public void Test_Constructor_Success()
        {
            //var actual = new ApiManager(_apiSettings);
        }

        [Test]
        public void Test_Constructor_Exception_SettingsNull()
        {
            Assert.Throws(Is.TypeOf<ArgumentNullException>(), delegate { new ApiManager(null); });
        }

        [Test]
        public void Test_Constructor_Exception_SettingsApiKeyNull()
        {
            Assert.Throws(Is.TypeOf<ArgumentException>(), delegate { new ApiManager(new ApiSettings()); });
        }

        [Test]
        public void Test_Constructor_Exception_SettingsAccountIdNull()
        {
            Assert.Throws(Is.TypeOf<ArgumentException>(), delegate { new ApiManager(new ApiSettings { ApiKey = "api-key" }); });
        }

        [Test]
        public void Test_Constructor_Exception_SettingsConnectionStringNull()
        {
            Assert.Throws(Is.TypeOf<ArgumentException>(), delegate { new ApiManager(new ApiSettings { ApiKey = "api-key", AccountId = "account-id" }); });
        }

        [Test]
        public void Test_Constructor_Exception_SettingsChannelPluginsPathNull()
        {
            Assert.Throws(Is.TypeOf<ArgumentException>(), delegate { new ApiManager(new ApiSettings { ApiKey = "api-key", AccountId = "account-id", ConnectionString = "connection string" }); });
        }

        [Test]
        public void Test_Constructor_Exception_SettingsOtpIdLengthNull()
        {
            Assert.Throws(Is.TypeOf<ArgumentException>(), delegate
            {
                new ApiManager(new ApiSettings
                {
                    ApiKey = "api-key",
                    AccountId = "account-id",
                    ConnectionString = "connection string",
                    ChannelPluginsPath = "path"
                });
            });
        }

        [Test]
        public void Test_Constructor_Exception_SettingsOtpCodeLengthNull()
        {
            Assert.Throws(Is.TypeOf<ArgumentException>(), delegate
            {
                new ApiManager(new ApiSettings
                {
                    ApiKey = "api-key",
                    AccountId = "account-id",
                    ConnectionString = "connection string",
                    ChannelPluginsPath = "path",
                    OtpIdLength = 128
                });
            });
        }

        [Test]
        public void Test_Constructor_Exception_SettingsRefCodeLengthNull()
        {
            Assert.Throws(Is.TypeOf<ArgumentException>(), delegate
            {
                new ApiManager(new ApiSettings
                {
                    ApiKey = "api-key",
                    AccountId = "account-id",
                    ConnectionString = "connection string",
                    ChannelPluginsPath = "path",
                    OtpIdLength = 128,
                    OtpCodeLength = 6
                });
            });
        }

        [Test]
        public void Test_VerifyAccount_Success()
        {
            Account accountMock = new Account
            {
                AccountId = new Guid(_apiSettings.AccountId),
                Salt = "ALzIYz8BIgvLsk1q",
                ApiKey = "lLTHykjZ3oZ5ORx1ePayit2+orgbNxI8d5N3296EhT7lSawnoygnfjKbyQdTrxVFLlZ8AfDjn1OnW5Hy2oAgWg==",
                IsActive = true
            };

            Mock<IUnitOfWork> uowMock = new Mock<IUnitOfWork>();
            uowMock.Setup(u => u.GetRepository<Account>()).Returns(MockRepository.MockRepo<Account>(new List<Account>(), accountMock, 1));

            //MsTest.PrivateObject apiManager = new MsTest.PrivateObject(new ApiManager(_apiSettings));
            //apiManager.SetField("_otpUow", uowMock.Object);

            //((ApiManager)apiManager.Target).VerifyAccount();
        }

        [Test]
        public void Test_VerifyAccount_Exception_AccountIdNotFound()
        {
            Mock<IUnitOfWork> uowMock = new Mock<IUnitOfWork>();
            uowMock.Setup(u => u.GetRepository<Account>()).Returns(MockRepository.MockRepo<Account>(new List<Account>(), null, 1));

            //MsTest.PrivateObject apiManager = new MsTest.PrivateObject(new ApiManager(_apiSettings));
            //apiManager.SetField("_otpUow", uowMock.Object);

            //Assert.Throws(Is.TypeOf<UnauthorizedAccessException>(), delegate { ((ApiManager)apiManager.Target).VerifyAccount(); });
        }

        [Test]
        public void Test_VerifyAccount_Exception_ApiKeyInvalid()
        {
            Account accountMock = new Account
            {
                AccountId = new Guid(_apiSettings.AccountId),
                Salt = "wrong salt",
                ApiKey = "lLTHykjZ3oZ5ORx1ePayit2+orgbNxI8d5N3296EhT7lSawnoygnfjKbyQdTrxVFLlZ8AfDjn1OnW5Hy2oAgWg==",
                IsActive = true
            };

            Mock<IUnitOfWork> uowMock = new Mock<IUnitOfWork>();
            uowMock.Setup(u => u.GetRepository<Account>()).Returns(MockRepository.MockRepo<Account>(new List<Account>(), accountMock, 1));

            //MsTest.PrivateObject apiManager = new MsTest.PrivateObject(new ApiManager(_apiSettings));
            //apiManager.SetField("_otpUow", uowMock.Object);

            //Assert.Throws(Is.TypeOf<UnauthorizedAccessException>(), delegate { ((ApiManager)apiManager.Target).VerifyAccount(); });
        }

        [Test]
        public void Test_VerifyAccount_Exception_AccountLocked()
        {
            Account accountMock = new Account
            {
                AccountId = new Guid(_apiSettings.AccountId),
                Salt = "ALzIYz8BIgvLsk1q",
                ApiKey = "lLTHykjZ3oZ5ORx1ePayit2+orgbNxI8d5N3296EhT7lSawnoygnfjKbyQdTrxVFLlZ8AfDjn1OnW5Hy2oAgWg==",
                IsActive = false
            };

            Mock<IUnitOfWork> uowMock = new Mock<IUnitOfWork>();
            uowMock.Setup(u => u.GetRepository<Account>()).Returns(MockRepository.MockRepo<Account>(new List<Account>(), accountMock, 1));

            //MsTest.PrivateObject apiManager = new MsTest.PrivateObject(new ApiManager(_apiSettings));
            //apiManager.SetField("_otpUow", uowMock.Object);

            //Assert.Throws(Is.TypeOf<UnauthorizedAccessException>(), delegate { ((ApiManager)apiManager.Target).VerifyAccount(); });
        }

        [Test]
        public void Test_AddOtp_Success()
        {
            Mock<IUnitOfWork> uowMock = new Mock<IUnitOfWork>();
            uowMock.Setup(u => u.GetRepository<OneTimePassword>())
                   .Returns(MockRepository.MockRepo<OneTimePassword>(new List<OneTimePassword> { }, null, 1));
            uowMock.Setup(u => u.GetRepository<Setting>())
                   .Returns(MockRepository.MockRepo<Setting>(new List<Setting> { _settingExpireMock }, _settingExpireMock, 1));

            Mock<IChannelFactory> channelFactoryMock = new Mock<IChannelFactory>();
            channelFactoryMock.Setup(c => c.GetChannel(It.IsAny<string>()))
                              .Returns(new MockPluginChannel(true, new List<PluginChannelMessage>()));

            //MsTest.PrivateObject apiManager = new MsTest.PrivateObject(new ApiManager(_apiSettings));
            //apiManager.SetField("_otpUow", uowMock.Object);
            //apiManager.SetField("_channelFactory", channelFactoryMock.Object);

            //MapperConfig.Register();
            //OtpResultModel actual = ((ApiManager)apiManager.Target).AddOtp(_otpRequestModelMock);

            //Assert.IsNotNull(actual);
            //Assert.AreEqual(_apiSettings.OtpIdLength, actual.OtpId.Length);
            //Assert.AreEqual(_apiSettings.RefCodeLength, actual.RefCode.Length);
        }

        [Test]
        public void Test_AddOtp_Exception_OtpRequestNull()
        {
            //Assert.Throws(Is.TypeOf<ArgumentNullException>(), delegate { new ApiManager(_apiSettings).AddOtp(null); });
        }

        [Test]
        public void Test_AddOtp_Exception_OtpRequestSuidNull()
        {
            //Assert.Throws(Is.TypeOf<ArgumentNullException>(), delegate { new ApiManager(_apiSettings).AddOtp(new OtpRequestModel()); });
        }

        [Test]
        public void Test_AddOtp_Exception_OtpRequestChannelNull()
        {
            //Assert.Throws(Is.TypeOf<ArgumentNullException>(), delegate { new ApiManager(_apiSettings).AddOtp(new OtpRequestModel { Suid = "suid" }); });
        }

        [Test]
        public void Test_AddOtp_Exception_OtpRequestChannelTypeNull()
        {
            //Assert.Throws(Is.TypeOf<ArgumentNullException>(), delegate { new ApiManager(_apiSettings).AddOtp(new OtpRequestModel { Suid = "suid", Channel = new ChannelModel() }); });
        }

        [Test]
        public void Test_AddOtp_Exception_OtpRequestChannelSenderNull()
        {
            //Assert.Throws(Is.TypeOf<ArgumentNullException>(), delegate { new ApiManager(_apiSettings).AddOtp(new OtpRequestModel { Suid = "suid", Channel = new ChannelModel { Type = "Mock" } }); });
        }

        [Test]
        public void Test_AddOtp_Exception_OtpRequestChannelAddressNull()
        {
            //Assert.Throws(Is.TypeOf<ArgumentNullException>(), delegate
            //{
            //    new ApiManager(_apiSettings).AddOtp(
            //        new OtpRequestModel { Suid = "suid", Channel = new ChannelModel { Type = "Mock", Sender = "Mock" } });
            //});
        }

        [Test]
        public void Test_AddOtp_Exception_OtpDuplicate()
        {
            Mock<IUnitOfWork> uowMock = new Mock<IUnitOfWork>();
            uowMock.Setup(u => u.GetRepository<OneTimePassword>())
                   .Returns(MockRepository.MockRepo<OneTimePassword>(new List<OneTimePassword> { new OneTimePassword() }, null, 1));

            //MsTest.PrivateObject apiManager = new MsTest.PrivateObject(new ApiManager(_apiSettings));
            //apiManager.SetField("_otpUow", uowMock.Object);

            //MapperConfig.Register();
            //Assert.Throws(Is.TypeOf<ArgumentException>(), delegate { ((ApiManager)apiManager.Target).AddOtp(_otpRequestModelMock); });
        }

        [Test]
        public void Test_AddOtp_Exception_SendFailed()
        {
            Mock<IUnitOfWork> uowMock = new Mock<IUnitOfWork>();
            uowMock.Setup(u => u.GetRepository<OneTimePassword>())
                   .Returns(MockRepository.MockRepo<OneTimePassword>(new List<OneTimePassword> { }, null, 1));
            uowMock.Setup(u => u.GetRepository<Setting>())
                   .Returns(MockRepository.MockRepo<Setting>(new List<Setting> { _settingExpireMock }, _settingExpireMock, 1));

            Mock<IChannelFactory> channelFactoryMock = new Mock<IChannelFactory>();
            channelFactoryMock.Setup(c => c.GetChannel(It.IsAny<string>()))
                              .Returns(new MockPluginChannel(false, new List<PluginChannelMessage>()));

            //MsTest.PrivateObject apiManager = new MsTest.PrivateObject(new ApiManager(_apiSettings));
            //apiManager.SetField("_otpUow", uowMock.Object);
            //apiManager.SetField("_channelFactory", channelFactoryMock.Object);

            //MapperConfig.Register();
            //Assert.Throws(Is.TypeOf<ChannelOperationException>(), delegate { ((ApiManager)apiManager.Target).AddOtp(_otpRequestModelMock); });
        }

        [Test]
        public void Test_AddOtp_Exception_OtpRequestChannelMessageNull()
        {
            //Assert.Throws(Is.TypeOf<ArgumentNullException>(), delegate
            //{
            //    new ApiManager(_apiSettings).AddOtp(
            //        new OtpRequestModel { Suid = "suid", Channel = new ChannelModel { Type = "Mock", Sender = "Mock", Address = "Mock" } });
            //});
        }

        [Test]
        public void Test_VerifyOtp_Success()
        {
            OneTimePassword otpPocoMock = new OneTimePassword
            {
                OtpId = "otp-id",
                Suid = "suid",
                ChannelType = "MOCK",
                ChannelSender = "sender",
                ChannelAddress = "address",
                ChannelMessage = "message",
                ExpiryDate = DateTime.Now.AddHours(1),
                Payload = "payload",
                RefCode = "REFCODE",
                Status = "Pending",
                Code = "012345"
            };

            Mock<IUnitOfWork> uowMock = new Mock<IUnitOfWork>();
            uowMock.Setup(u => u.GetRepository<OneTimePassword>())
                   .Returns(MockRepository.MockRepo<OneTimePassword>(new List<OneTimePassword>(), otpPocoMock, 1));
            uowMock.Setup(u => u.GetRepository<Setting>())
                   .Returns(MockRepository.MockRepo<Setting>(new List<Setting> { _settingExpireMock }, _settingExpireMock, 1));

            //MsTest.PrivateObject apiManager = new MsTest.PrivateObject(new ApiManager(_apiSettings));
            //apiManager.SetField("_otpUow", uowMock.Object);

            //MapperConfig.Register();
            //OtpModel actual = ((ApiManager)apiManager.Target).VerifyOtp("id", JObject.FromObject(new { otpCode = otpPocoMock.Code }));

            //Assert.IsNotNull(actual);
            //Assert.AreEqual(otpPocoMock.OtpId, actual.Id);
            //Assert.AreEqual(otpPocoMock.Suid, actual.Suid);
            //Assert.IsNotNull(actual.Channel);
            //Assert.AreEqual(otpPocoMock.ChannelType, actual.Channel.Type);
            //Assert.AreEqual(otpPocoMock.ChannelSender, actual.Channel.Sender);
            //Assert.AreEqual(otpPocoMock.ChannelAddress, actual.Channel.Address);
            //Assert.AreEqual(otpPocoMock.ChannelMessage, actual.Channel.Message);
            //Assert.AreEqual(otpPocoMock.ExpiryDate, actual.ExpiryDate);
            //Assert.AreEqual(otpPocoMock.Payload, actual.Payload);
            //Assert.AreEqual(otpPocoMock.RefCode, actual.RefCode);
            //Assert.AreEqual(otpPocoMock.Status, actual.Status);
        }

        [Test]
        public void Test_VerifyOtp_Exception_OtpIdNull()
        {
            //Assert.Throws(Is.TypeOf<ArgumentNullException>(), delegate { new ApiManager(_apiSettings).VerifyOtp(null, null); });
        }

        [Test]
        public void Test_VerifyOtp_Exception_EnteredOtpCodeNull()
        {
            //Assert.Throws(Is.TypeOf<ArgumentNullException>(), delegate { new ApiManager(_apiSettings).VerifyOtp("otp-id", null); });
        }

        [Test]
        public void Test_VerifyOtp_Exception_EnteredOtpCodeNotFound()
        {
            //Assert.Throws(Is.TypeOf<ArgumentNullException>(), delegate { new ApiManager(_apiSettings).VerifyOtp("otp-id", new JObject()); });
        }

        [Test]
        public void Test_VerifyOtp_Exception_OtpIdNotFound()
        {
            Mock<IUnitOfWork> uowMock = new Mock<IUnitOfWork>();
            uowMock.Setup(u => u.GetRepository<OneTimePassword>())
                   .Returns(MockRepository.MockRepo<OneTimePassword>(new List<OneTimePassword>(), null, 1));

            //MsTest.PrivateObject apiManager = new MsTest.PrivateObject(new ApiManager(_apiSettings));
            //apiManager.SetField("_otpUow", uowMock.Object);

            //Assert.Throws(Is.TypeOf<ArgumentNullException>(), delegate { ((ApiManager)apiManager.Target).VerifyOtp("id", JObject.FromObject(new { otpCode = "012345" })); });
        }

        [Test]
        public void Test_VerifyOtp_Exception_OtpCanceled()
        {
            Mock<IUnitOfWork> uowMock = new Mock<IUnitOfWork>();
            uowMock.Setup(u => u.GetRepository<OneTimePassword>())
                   .Returns(MockRepository.MockRepo<OneTimePassword>(new List<OneTimePassword>(),
                                                                     new OneTimePassword { Status = "Canceled" }, 1));

            //MsTest.PrivateObject apiManager = new MsTest.PrivateObject(new ApiManager(_apiSettings));
            //apiManager.SetField("_otpUow", uowMock.Object);

            //Assert.Throws(Is.TypeOf<UnauthorizedAccessException>(), delegate { ((ApiManager)apiManager.Target).VerifyOtp("id", JObject.FromObject(new { otpCode = "012345" })); });
        }

        [Test]
        public void Test_VerifyOtp_Exception_OtpDeleted()
        {
            Mock<IUnitOfWork> uowMock = new Mock<IUnitOfWork>();
            uowMock.Setup(u => u.GetRepository<OneTimePassword>())
                   .Returns(MockRepository.MockRepo<OneTimePassword>(new List<OneTimePassword>(),
                                                                     new OneTimePassword { Status = "Deleted" }, 1));

            //MsTest.PrivateObject apiManager = new MsTest.PrivateObject(new ApiManager(_apiSettings));
            //apiManager.SetField("_otpUow", uowMock.Object);

            //Assert.Throws(Is.TypeOf<UnauthorizedAccessException>(), delegate { ((ApiManager)apiManager.Target).VerifyOtp("id", JObject.FromObject(new { otpCode = "012345" })); });
        }

        [Test]
        public void Test_VerifyOtp_Exception_OtpLocked()
        {
            Mock<IUnitOfWork> uowMock = new Mock<IUnitOfWork>();
            uowMock.Setup(u => u.GetRepository<OneTimePassword>())
                   .Returns(MockRepository.MockRepo<OneTimePassword>(new List<OneTimePassword>(),
                                                                     new OneTimePassword { Status = "Locked" }, 1));

            //MsTest.PrivateObject apiManager = new MsTest.PrivateObject(new ApiManager(_apiSettings));
            //apiManager.SetField("_otpUow", uowMock.Object);

            //Assert.Throws(Is.TypeOf<UnauthorizedAccessException>(), delegate { ((ApiManager)apiManager.Target).VerifyOtp("id", JObject.FromObject(new { otpCode = "012345" })); });
        }

        [Test]
        public void Test_VerifyOtp_Exception_OtpVerified()
        {
            Mock<IUnitOfWork> uowMock = new Mock<IUnitOfWork>();
            uowMock.Setup(u => u.GetRepository<OneTimePassword>())
                   .Returns(MockRepository.MockRepo<OneTimePassword>(new List<OneTimePassword>(),
                                                                     new OneTimePassword { Status = "Verified" }, 1));

            //MsTest.PrivateObject apiManager = new MsTest.PrivateObject(new ApiManager(_apiSettings));
            //apiManager.SetField("_otpUow", uowMock.Object);

            //Assert.Throws(Is.TypeOf<UnauthorizedAccessException>(), delegate { ((ApiManager)apiManager.Target).VerifyOtp("id", JObject.FromObject(new { otpCode = "012345" })); });
        }

        [Test]
        public void Test_VerifyOtp_Exception_OtpExpired()
        {
            Mock<IUnitOfWork> uowMock = new Mock<IUnitOfWork>();
            uowMock.Setup(u => u.GetRepository<OneTimePassword>())
                   .Returns(MockRepository.MockRepo<OneTimePassword>(new List<OneTimePassword>(),
                                                                     new OneTimePassword { Status = "Expired" }, 1));

            //MsTest.PrivateObject apiManager = new MsTest.PrivateObject(new ApiManager(_apiSettings));
            //apiManager.SetField("_otpUow", uowMock.Object);

            //Assert.Throws(Is.TypeOf<UnauthorizedAccessException>(), delegate { ((ApiManager)apiManager.Target).VerifyOtp("id", JObject.FromObject(new { otpCode = "012345" })); });
        }

        [Test]
        public void Test_VerifyOtp_Exception_OtpStatusNotImplement()
        {
            Mock<IUnitOfWork> uowMock = new Mock<IUnitOfWork>();
            uowMock.Setup(u => u.GetRepository<OneTimePassword>())
                   .Returns(MockRepository.MockRepo<OneTimePassword>(new List<OneTimePassword>(),
                                                                     new OneTimePassword { Status = "Reserved" }, 1));

            //MsTest.PrivateObject apiManager = new MsTest.PrivateObject(new ApiManager(_apiSettings));
            //apiManager.SetField("_otpUow", uowMock.Object);

            //Assert.Throws(Is.TypeOf<NotImplementedException>(), delegate { ((ApiManager)apiManager.Target).VerifyOtp("id", JObject.FromObject(new { otpCode = "012345" })); });
        }

        [Test]
        public void Test_VerifyOtp_Exception_OtpStatusNotSupport()
        {
            Mock<IUnitOfWork> uowMock = new Mock<IUnitOfWork>();
            uowMock.Setup(u => u.GetRepository<OneTimePassword>())
                   .Returns(MockRepository.MockRepo<OneTimePassword>(new List<OneTimePassword>(),
                                                                     new OneTimePassword { Status = "NewStatus" }, 1));

            //MsTest.PrivateObject apiManager = new MsTest.PrivateObject(new ApiManager(_apiSettings));
            //apiManager.SetField("_otpUow", uowMock.Object);

            //Assert.Throws(Is.TypeOf<NotSupportedException>(), delegate { ((ApiManager)apiManager.Target).VerifyOtp("id", JObject.FromObject(new { otpCode = "012345" })); });
        }

        [Test]
        public void Test_VerifyOtp_Exception_OtpCodeInvalid()
        {
            OneTimePassword otpPocoMock = new OneTimePassword { Status = "Pending", Code = "123456" };

            Mock<IUnitOfWork> uowMock = new Mock<IUnitOfWork>();
            uowMock.Setup(u => u.GetRepository<OneTimePassword>())
                   .Returns(MockRepository.MockRepo<OneTimePassword>(new List<OneTimePassword>(), otpPocoMock, 1));
            uowMock.Setup(u => u.GetRepository<Setting>())
                   .Returns(MockRepository.MockRepo<Setting>(new List<Setting>(), _settingRetryCountMock, 1));
            uowMock.Setup(u => u.GetRepository<Attempt>())
                   .Returns(MockRepository.MockRepo<Attempt>(new List<Attempt>(), new Attempt(), 0));

            //MsTest.PrivateObject apiManager = new MsTest.PrivateObject(new ApiManager(_apiSettings));
            //apiManager.SetField("_otpUow", uowMock.Object);


            //Assert.Throws(Is.TypeOf<UnauthorizedAccessException>(), delegate { ((ApiManager)apiManager.Target).VerifyOtp("id", JObject.FromObject(new { otpCode = "012345" })); });
        }

        [Test]
        public void Test_VerifyOtp_Fail_OtpCodeLocked()
        {
            OneTimePassword otpPocoMock = new OneTimePassword { Status = "Pending", Code = "123456" };

            Mock<IUnitOfWork> uowMock = new Mock<IUnitOfWork>();
            uowMock.Setup(u => u.GetRepository<OneTimePassword>())
                   .Returns(MockRepository.MockRepo<OneTimePassword>(new List<OneTimePassword>(), otpPocoMock, 1));
            uowMock.Setup(u => u.GetRepository<Setting>())
                   .Returns(MockRepository.MockRepo<Setting>(new List<Setting>(), _settingRetryCountMock, 1));
            uowMock.Setup(u => u.GetRepository<Attempt>())
                   .Returns(MockRepository.MockRepo<Attempt>(new List<Attempt>(), new Attempt(), Convert.ToInt32(_settingRetryCountMock.Value)));

            //MsTest.PrivateObject apiManager = new MsTest.PrivateObject(new ApiManager(_apiSettings));
            //apiManager.SetField("_otpUow", uowMock.Object);

            //try
            //{
            //    ((ApiManager)apiManager.Target).VerifyOtp("id", JObject.FromObject(new { otpCode = "012345" }));

            //    Assert.Fail("This test has to throws UnauthorizedAccessException.");
            //}
            //catch (UnauthorizedAccessException)
            //{
            //    Assert.AreEqual("Locked", otpPocoMock.Status);
            //}
        }

        [Test]
        public void Test_VerifyOtp_Fail_OtpCodeExpired()
        {
            OneTimePassword otpPocoMock = new OneTimePassword { Status = "Pending", Code = "123456", ExpiryDate = DateTime.Now.AddHours(-1) };

            Mock<IUnitOfWork> uowMock = new Mock<IUnitOfWork>();
            uowMock.Setup(u => u.GetRepository<OneTimePassword>())
                   .Returns(MockRepository.MockRepo<OneTimePassword>(new List<OneTimePassword>(), otpPocoMock, 1));
            uowMock.Setup(u => u.GetRepository<Setting>())
                   .Returns(MockRepository.MockRepo<Setting>(new List<Setting>(), _settingRetryCountMock, 1));
            uowMock.Setup(u => u.GetRepository<Attempt>())
                   .Returns(MockRepository.MockRepo<Attempt>(new List<Attempt>(), new Attempt(), 0));

            //MsTest.PrivateObject apiManager = new MsTest.PrivateObject(new ApiManager(_apiSettings));
            //apiManager.SetField("_otpUow", uowMock.Object);

            //try
            //{
            //    ((ApiManager)apiManager.Target).VerifyOtp("id", JObject.FromObject(new { otpCode = otpPocoMock.Code }));

            //    Assert.Fail("This test has to throws UnauthorizedAccessException.");
            //}
            //catch (UnauthorizedAccessException)
            //{
            //    Assert.AreEqual("Expired", otpPocoMock.Status);
            //}
        }

        [Test]
        public void Test_GetChannelNames_Success()
        {
            List<string> expectedAllChannelNames = new List<string> { "EMAIL", "SMS", "MOCK" };

            Mock<IChannelFactory> channelFactoryMock = new Mock<IChannelFactory>();
            channelFactoryMock.Setup(c => c.GetAllChannelTypeNames()).Returns(expectedAllChannelNames);

            //MsTest.PrivateObject apiManager = new MsTest.PrivateObject(new ApiManager(_apiSettings));
            //apiManager.SetField("_channelFactory", channelFactoryMock.Object);

            //IEnumerable<string> actual = ((ApiManager)apiManager.Target).GetChannelNames();

            //Assert.IsNotNull(actual);
            //Assert.IsTrue(actual.SequenceEqual(expectedAllChannelNames));
        }

        [Test]
        public void Test_Dispose_Success()
        {
            //MsTest.PrivateObject apiManager = new MsTest.PrivateObject(new ApiManager(_apiSettings));
            //((ApiManager)apiManager.Target).Dispose();
            //((ApiManager)apiManager.Target).Dispose();

            //Assert.IsNull(apiManager.GetField("_otpUow"));
            //Assert.IsNull(apiManager.GetField("_channelFactory"));
            //Assert.IsTrue((bool)apiManager.GetField("Disposed"));
        }
    }
}
