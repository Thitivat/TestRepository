using BND.Services.Infrastructure.ErrorHandling;
using BND.Services.Security.OTP.ClientProxy;
using BND.Services.Security.OTP.Models;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Assert = NUnit.Framework.Assert;

namespace BND.Services.Security.OTP.ClientProxyTest
{
    [TestFixture]
    public class ChannelTest
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
        public void GetAllChannelTypeNames_Should_ReturnData_When_Success()
        {
            IEnumerable<string> expectedResult = new List<string> { "SMS" };

            // Inject HttpClient into target
            var channel = new Mock<Channels>(_baseAddress, _apiKey, _accountId);
            channel
                .Protected()
                .Setup<HttpClient>("GetHttpClient")
                .Returns(GetHttpClient(HttpStatusCode.OK, expectedResult, "/api/Channels"));

            // Start testing
            IEnumerable<string> result = channel.Object.GetAllChannelTypeNames();

            Assert.AreEqual(expectedResult.Count(), result.Count());
            Assert.AreEqual(expectedResult.FirstOrDefault(), result.FirstOrDefault());
        }

        [Test]
        public void GetAllChannelTypeNames_Should_ThrowProxyException_When_ErrorHasOccurred()
        {
            ApiErrorModel errorModel = new ApiErrorModel
            {
                ErrorCode = 502,
                Messages = new ErrorMessageModel[] { 
                    new ErrorMessageModel 
                    {
                        Key = "KEY01",
                        Message = "ERROR",
                        Type = 1,
                    },
                },
                StatusCode = HttpStatusCode.BadGateway,
            };

            // Inject HttpClient into target
            var channel = new Mock<Channels>(_baseAddress, _apiKey, _accountId);
            channel
                .Protected()
                .Setup<HttpClient>("GetHttpClient")
                .Returns(GetHttpClient(HttpStatusCode.BadGateway, errorModel, "/api/Channels"));

            // Start testing
            ProxyException ex = Assert.Throws<ProxyException>(() =>
            {
                channel.Object.GetAllChannelTypeNames();
            });
        }

        [Test]
        public void GetAllChannelTypeNames_Should_ThrowProxyException_When_ApiAddressIsInvalid()
        {
            ApiErrorModel errorModel = new ApiErrorModel
            {
                ErrorCode = 502,
                Messages = new ErrorMessageModel[] { 
                    new ErrorMessageModel 
                    {
                        Key = "KEY01",
                        Message = "ERROR",
                        Type = 1,
                    },
                },
                StatusCode = HttpStatusCode.BadGateway,
            };

            // Inject HttpClient into target
            var channel = new Mock<Channels>(_baseAddress, _apiKey, _accountId);
            channel
                .Protected()
                .Setup<HttpClient>("GetHttpClient")
                .Returns(GetHttpClient(HttpStatusCode.BadGateway, errorModel, "/api/ChannelsFAKE"));

            // Start testing
            ProxyException ex = Assert.Throws<ProxyException>(() =>
            {
                channel.Object.GetAllChannelTypeNames();
            });
        }
    }
}
