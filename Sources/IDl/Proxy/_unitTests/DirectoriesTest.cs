using BND.Services.Payments.iDeal.Models;
using BND.Services.Payments.iDeal.Proxy.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security;

namespace BND.Services.Payments.iDeal.Proxy.Tests
{
    [TestFixture]
    public class DirectoriesTest
    {
        private MockHttpMessageHandler _httpMessageHandler;
        private HttpClient _mockHttpClient;
        private Uri _baseUri;

        [SetUp]
        public void Initialize()
        {
            _baseUri = new Uri("http://localhost/api/");
            _httpMessageHandler = new MockHttpMessageHandler(_baseUri);
            _mockHttpClient = new HttpClient(_httpMessageHandler);
        }

        [Test]
        public void Constructor_Should_WorkFine_When_EverythingIsFine()
        {
            string baseAddress = "http://localhost/";
            string token = "token";

            using (IDirectories directories = new Directories(baseAddress, token))
            {
                // just call the constructor that didn't call in another test case.
            }
        }

        [TestCase("http://localhost/")]
        [TestCase("http://localhost")]
        public void GetDirectory_Should_ReturnDirectoryList_When_EverythingIsFine(string baseUrl)
        {
            string baseAddress = baseUrl;
            string token = "token";

            List<DirectoryModel> expectedDirectory = new List<DirectoryModel> 
                {
                    new DirectoryModel
                    {
                         CountryNames  ="Country name",
                         Issuers = new List<IssuerModel>
                         {
                             new IssuerModel
                             {
                                 IssuerID="001",
                                 IssuerName = "name 01"
                             },
                             new IssuerModel
                             {
                                 IssuerID="002",
                                 IssuerName = "name 02"
                             }
                         }
                    }
                };

            // Set expected data that will return from mock http client.
            _httpMessageHandler.AddMockResponse("Directories", HttpStatusCode.OK, expectedDirectory);

            using (IDirectories directory = new Directories(baseAddress, token, _mockHttpClient))
            {
                var actual = directory.GetDirectories();
                Assert.AreEqual(expectedDirectory.Count(), actual.Count());
                Assert.AreEqual(expectedDirectory.First().CountryNames, actual.First().CountryNames);
                Assert.AreEqual(expectedDirectory.First().Issuers.First().IssuerID, actual.First().Issuers.First().IssuerID);
                Assert.AreEqual(expectedDirectory.First().Issuers.First().IssuerName, actual.First().Issuers.First().IssuerName);
            }
        }

        [TestCase(HttpStatusCode.BadRequest, typeof(ArgumentException))]
        [TestCase(HttpStatusCode.Forbidden, typeof(SecurityException))]
        [TestCase(HttpStatusCode.Unauthorized, typeof(UnauthorizedAccessException))]
        [TestCase(HttpStatusCode.InternalServerError, typeof(iDealOperationException))]
        public void GetDirectory_Should_ThrowAnError_When_TheHttpStatusCodeIsNot200(HttpStatusCode statusCode, Type exception)
        {
            string baseAddress = "http://localhost/";
            string token = "token";

            // set expected response.
            ApiErrorModel expectedResponse = new ApiErrorModel
            {
                ErrorCode = "123",
                Message = "Error",
                StatusCode = statusCode
            };

            // Set expected data that will return from mock http client.
            _httpMessageHandler.AddMockResponse("Directories", statusCode, expectedResponse);

            using (IDirectories directory = new Directories(baseAddress, token, _mockHttpClient))
            {
                Assert.Throws(exception, () => directory.GetDirectories());
            }
        }
    }
}