using BND.Services.Infrastructure.ErrorHandling;
using BND.Services.Security.OTP.ClientProxy;
using BND.Services.Security.OTP.Models;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using Assert = NUnit.Framework.Assert;

namespace BND.Services.Security.OTP.ClientProxyTest
{
    [TestFixture]
    public class OneTimePasswordsTest
    {
        private string _baseAddress = "http://test.org";
        private string _apiKey = "apiKey";
        private string _accountId = "accountId";

        private HttpClient GetHttpClient(HttpStatusCode statusCode, object expectedResponse, string apiAddress)
        {
            string jsonOutput = JsonConvert.SerializeObject(expectedResponse);

            // Do mocking HttpResponse
            var httpClientResponse = new HttpResponseMessage();
            httpClientResponse.StatusCode = statusCode;
            httpClientResponse.Content = new StringContent(jsonOutput);

            // Create fake HttpClient
            var fakeResponseHandler = new FakeResponseHandler();
            string baseApiAddress = _baseAddress + apiAddress;
            fakeResponseHandler.AddFakeResponse(new Uri(baseApiAddress), httpClientResponse);

            HttpClient httpClient = new HttpClient(fakeResponseHandler);
            httpClient.BaseAddress = new Uri(_baseAddress + "/api/");
            return httpClient;
        }

        [Test]
        public void NewCode_Should_ReturnOTPResult_When_Success()
        {
            OtpResultModel expectedResult = new OtpResultModel
            {
                OtpId = "xxxx",
                RefCode = "yyyy"
            };

            OtpRequestModel actualRequest = new OtpRequestModel
            {
                Suid = Guid.NewGuid().ToString(),
                Channel = new ChannelModel
                {
                    Type = "EMAIL",
                    Sender = "BND",
                    Address = "bank@kobkiat-it.com",
                    Message = "Fuck up!\r\nRef = {RefCode}\r\nOTP = {Otp}"
                },
                Payload = "BND Payload"
            };

            var oneTimePasswords = new Mock<OneTimePasswords>(_baseAddress, _apiKey, _accountId);
            oneTimePasswords
                .Protected()
                .Setup<HttpClient>("GetHttpClient")
                .Returns(GetHttpClient(HttpStatusCode.OK, expectedResult, "/api/OneTimePasswords"));
            OtpResultModel result = oneTimePasswords.Object.NewCode(actualRequest);

            Assert.AreEqual(expectedResult.OtpId, result.OtpId);
            Assert.AreEqual(expectedResult.RefCode, result.RefCode);
        }

        [Test]
        public void NewCode_Should_ThrowProxyException_When_ApiAddressIsInvalid()
        {
            ApiErrorModel expectedError = new ApiErrorModel
            {
                ErrorCode = 99,
                StatusCode = HttpStatusCode.InternalServerError,
                Messages = new ErrorMessageModel[] { new ErrorMessageModel { Key = "Test", Message = "Test", Type = 0 } }
            };

            OtpRequestModel actualRequest = new OtpRequestModel
            {
                Suid = Guid.NewGuid().ToString(),
                Channel = new ChannelModel
                {
                    Type = "EMAIL",
                    Sender = "BND",
                    Address = "bank@kobkiat-it.com",
                    Message = "Fuck up!\r\nRef = {RefCode}\r\nOTP = {Otp}"
                },
                Payload = "BND Payload"
            };

            var oneTimePasswords = new Mock<OneTimePasswords>(_baseAddress, _apiKey, _accountId);
            oneTimePasswords
                .Protected()
                .Setup<HttpClient>("GetHttpClient")
                .Returns(GetHttpClient(HttpStatusCode.BadGateway, expectedError, "/api/OneTimePasswordsFAKE"));
            ProxyException ex = Assert.Throws<ProxyException>(() =>
            {
                oneTimePasswords.Object.NewCode(actualRequest);
            });
        }


        [Test]
        public void NewCode_Should_ThrowProxyException_When_ErrorHasOccurred()
        {
            ApiErrorModel expectedError = new ApiErrorModel
            {
                ErrorCode = 99,
                StatusCode = HttpStatusCode.InternalServerError,
                Messages = new ErrorMessageModel[] { new ErrorMessageModel { Key = "Test", Message = "Test", Type = 0 } }
            };

            OtpRequestModel actualRequest = new OtpRequestModel
            {
                Suid = Guid.NewGuid().ToString(),
                Channel = new ChannelModel
                {
                    Type = "EMAIL",
                    Sender = "BND",
                    Address = "bank@kobkiat-it.com",
                    Message = "Fuck up!\r\nRef = {RefCode}\r\nOTP = {Otp}"
                },
                Payload = "BND Payload"
            };

            var oneTimePasswords = new Mock<OneTimePasswords>(_baseAddress, _apiKey, _accountId);
            oneTimePasswords
                .Protected()
                .Setup<HttpClient>("GetHttpClient")
                .Returns(GetHttpClient(HttpStatusCode.BadGateway, expectedError, "/api/OneTimePasswords"));
            ProxyException ex = Assert.Throws<ProxyException>(() =>
            {
                oneTimePasswords.Object.NewCode(actualRequest);
            });
        }

        [Test]
        public void NewCode_Should_ThrowArgumentNullException_When_RequestIsNull()
        {
            ClientProxy.OneTimePasswords otp = new ClientProxy.OneTimePasswords(_baseAddress, _apiKey, _accountId);
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() =>
            {
                otp.NewCode(null);
            });
            Assert.IsTrue(ex.Message.Contains("request"));
        }

        [Test]
        public void NewCode_Should_ThrowArgumentNullException_When_RequestSUIDIsNull()
        {
            ClientProxy.OneTimePasswords otp = new ClientProxy.OneTimePasswords(_baseAddress, _apiKey, _accountId);
            ArgumentException ex = Assert.Throws<ArgumentException>(() =>
            {
                otp.NewCode(new OtpRequestModel());
            });
            Assert.IsTrue(ex.Message.Contains("request.Suid"));
        }

        [Test]
        public void NewCode_Should_ThrowArgumentNullException_When_RequestChannelNull()
        {
            ClientProxy.OneTimePasswords otp = new ClientProxy.OneTimePasswords(_baseAddress, _apiKey, _accountId);
            ArgumentException ex = Assert.Throws<ArgumentException>(() =>
            {
                otp.NewCode(new OtpRequestModel { Suid = "dd" });
            });
            Assert.IsTrue(ex.Message.Contains("request.Channel"));
        }

        [Test]
        public void NewCode_Should_ThrowArgumentNullException_When_RequestChannelTypeNull()
        {
            ClientProxy.OneTimePasswords otp = new ClientProxy.OneTimePasswords(_baseAddress, _apiKey, _accountId);
            ArgumentException ex = Assert.Throws<ArgumentException>(() =>
            {
                otp.NewCode(new OtpRequestModel { Suid = "dd", Channel = new ChannelModel() });
            });
            Assert.IsTrue(ex.Message.Contains("request.Channel.Type"));
        }

        [Test]
        public void NewCode_Should_ThrowArgumentNullException_When_RequestChannelSenderNull()
        {
            ClientProxy.OneTimePasswords otp = new ClientProxy.OneTimePasswords(_baseAddress, _apiKey, _accountId);
            ArgumentException ex = Assert.Throws<ArgumentException>(() =>
            {
                otp.NewCode(new OtpRequestModel { Suid = "dd", Channel = new ChannelModel { Type = "dd" } });
            });
            Assert.IsTrue(ex.Message.Contains("request.Channel.Sender"));
        }

        [Test]
        public void NewCode_Should_ThrowArgumentNullException_When_RequestChannelAddressNull()
        {
            ClientProxy.OneTimePasswords otp = new ClientProxy.OneTimePasswords(_baseAddress, _apiKey, _accountId);
            ArgumentException ex = Assert.Throws<ArgumentException>(() =>
            {
                otp.NewCode(new OtpRequestModel { Suid = "dd", Channel = new ChannelModel { Type = "dd", Sender = "dd" } });
            });

            Assert.IsTrue(ex.Message.Contains("request.Channel.Address"));
        }

        [Test]
        public void NewCode_Should_ThrowArgumentNullException_When_RequestChannelMessageNull()
        {
            ClientProxy.OneTimePasswords otp = new ClientProxy.OneTimePasswords(_baseAddress, _apiKey, _accountId);
            ArgumentException ex = Assert.Throws<ArgumentException>(() =>
            {
                otp.NewCode(new OtpRequestModel { Suid = "dd", Channel = new ChannelModel { Type = "dd", Sender = "dd", Address = "dd" } });
            });
            Assert.IsTrue(ex.Message.Contains("request.Channel.Message"));
        }

        [Test]
        public void VerifyCode_Should_ReturnOTPModel_When_Success()
        {
            OtpModel expectedData = new OtpModel
            {
                Id = "id",
                Suid = "Suid",
                Status = "Verified",
                RefCode = "REFCODE",
                Payload = "Payload",
                ExpiryDate = DateTime.Now,
                Channel = new ChannelModel
                {
                    Type = "EMAIL",
                    Sender = "Sender",
                    Address = "Address",
                    Message = "Message"
                }
            };

            var oneTimePasswords = new Mock<OneTimePasswords>(_baseAddress, _apiKey, _accountId);
            oneTimePasswords
                .Protected()
                .Setup<HttpClient>("GetHttpClient")
                .Returns(GetHttpClient(HttpStatusCode.OK, expectedData, String.Format("/api/OneTimePasswords/{0}/Verify", expectedData.Id)));
            OtpModel actualData = oneTimePasswords.Object.VerifyCode(expectedData.Id, "123456");

            Assert.AreEqual(expectedData.Id, actualData.Id);
            Assert.AreEqual(expectedData.Suid, actualData.Suid);
            Assert.AreEqual(expectedData.Status, actualData.Status);
            Assert.AreEqual(expectedData.RefCode, actualData.RefCode);
            Assert.AreEqual(expectedData.Payload, actualData.Payload);
            Assert.AreEqual(0, DateTime.Compare(expectedData.ExpiryDate, actualData.ExpiryDate));
            Assert.AreEqual(expectedData.Channel.Type, actualData.Channel.Type);
            Assert.AreEqual(expectedData.Channel.Sender, actualData.Channel.Sender);
            Assert.AreEqual(expectedData.Channel.Address, actualData.Channel.Address);
            Assert.AreEqual(expectedData.Channel.Message, actualData.Channel.Message);
        }

        [Test]
        public void VerifyCode_Should_ThrowProxyException_When_ErrorHasOccurred()
        {
            ApiErrorModel expectedError = new ApiErrorModel
            {
                ErrorCode = 99,
                StatusCode = HttpStatusCode.InternalServerError,
                Messages = new ErrorMessageModel[] { new ErrorMessageModel { Key = "Test", Message = "Test", Type = 0 } }
            };

            var oneTimePasswords = new Mock<OneTimePasswords>(_baseAddress, _apiKey, _accountId);
            oneTimePasswords
                .Protected()
                .Setup<HttpClient>("GetHttpClient")
                .Returns(GetHttpClient(HttpStatusCode.InternalServerError, expectedError, String.Format("/api/OneTimePasswords/{0}/Verify", "id")));

            ProxyException ex = Assert.Throws<ProxyException>(() =>
            {
                oneTimePasswords.Object.VerifyCode("id", "otp");
            });
        }


        [Test]
        public void VerifyCode_Should_ThrowProxyException_When_ApiAddressIsInvalid()
        {
            ApiErrorModel expectedError = new ApiErrorModel
            {
                ErrorCode = 99,
                StatusCode = HttpStatusCode.InternalServerError,
                Messages = new ErrorMessageModel[] { new ErrorMessageModel { Key = "Test", Message = "Test", Type = 0 } }
            };

            var oneTimePasswords = new Mock<OneTimePasswords>(_baseAddress, _apiKey, _accountId);
            oneTimePasswords
                .Protected()
                .Setup<HttpClient>("GetHttpClient")
                .Returns(GetHttpClient(HttpStatusCode.InternalServerError, expectedError, String.Format("/api/OneTimePasswords/{0}/VerifyFAKE", "id")));

            ProxyException ex = Assert.Throws<ProxyException>(() =>
            {
                oneTimePasswords.Object.VerifyCode("id", "otp");
            });
        }


        [Test]
        public void VerifyCode_Should_ThrowArgumentNullException_When_OtpIdNull()
        {
            ClientProxy.OneTimePasswords otp = new ClientProxy.OneTimePasswords(_baseAddress, _apiKey, _accountId);
            Assert.Throws<ArgumentNullException>(() =>
            {
                otp.VerifyCode(null, null);
            });
        }

        [Test]
        public void VerifyCode_Should_ThrowArgumentNullException_When_OtpCodeNull()
        {
            ClientProxy.OneTimePasswords otp = new ClientProxy.OneTimePasswords(_baseAddress, _apiKey, _accountId);
            Assert.Throws<ArgumentNullException>(() =>
            {
                otp.VerifyCode("id", null);
            });
        }
    }
}
