using BND.Services.Payments.eMandates.Entities;
using BND.Services.Payments.eMandates.Proxy;
using BND.Services.Payments.eMandates.Proxy.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace BND.Services.Payments.eMandates.UnitTests.Proxy
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
            _baseUri = new Uri("http://localhost/v1/");
            _httpMessageHandler = new MockHttpMessageHandler(_baseUri);
            _mockHttpClient = new HttpClient(_httpMessageHandler);
        }

        [Test]
        public void Constructor_Should_WorkFine_When_EverythingIsFine()
        {
            string baseAddress = "http://localhost:29892";
            string token = "token";

            using (IDirectories directories = new Directories(baseAddress, token))
            {
                //IEnumerable<DirectoryModel> items = directories.GetDirectories();
                //items.FirstOrDefault().DebtorBan
                // just call the constructor that didn't call in another test case.
            }
        }

        [TestCase("http://localhost/")]
        [TestCase("http://localhost")]
        public void GetDirectory_Should_ReturnDirectoryList_When_EverythingIsFine(string baseUrl)
        {
            string baseAddress = baseUrl;
            string token = "token";

            DirectoryModel expectedDirectory = new DirectoryModel
                {
                    //CountryNames  ="Country name",
                    DebtorBanks = new List<DebtorBank>
                    {
                        new DebtorBank
                        {
                            DebtorBankCountry = "NL",
                            DebtorBankId = "INGBNL2A",
                            DebtorBankName = "ING Bank"
                        },
                        new DebtorBank
                        {
                            DebtorBankCountry = "NL",
                            DebtorBankId = "ABNDNL2A",
                            DebtorBankName = "ABN Bank"
                        }
                    }
                };

            // Set expected data that will return from mock http client.
            _httpMessageHandler.AddMockResponse("directories/current", HttpStatusCode.OK, expectedDirectory);

            using (IDirectories directory = new Directories(baseAddress, token, _mockHttpClient))
            {
                var actual = directory.GetDirectories();
                //Assert.AreEqual(expectedDirectory.Count(), actual.Count());
                Assert.AreEqual(expectedDirectory.DebtorBanks.First().DebtorBankCountry, actual.DebtorBanks.First().DebtorBankCountry);
                Assert.AreEqual(expectedDirectory.DebtorBanks.First().DebtorBankId, actual.DebtorBanks.First().DebtorBankId);
                Assert.AreEqual(expectedDirectory.DebtorBanks.First().DebtorBankName, actual.DebtorBanks.First().DebtorBankName);
            }
        }

        [TestCase(HttpStatusCode.BadRequest, typeof(ArgumentException))]
        [TestCase(HttpStatusCode.Forbidden, typeof(SecurityException))]
        [TestCase(HttpStatusCode.Unauthorized, typeof(UnauthorizedAccessException))]
        [TestCase(HttpStatusCode.InternalServerError, typeof(eMandateOperationException))]
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
            _httpMessageHandler.AddMockResponse("directories/current", statusCode, expectedResponse);

            using (IDirectories directory = new Directories(baseAddress, token, _mockHttpClient))
            {
                Assert.Throws(exception, () => directory.GetDirectories());
            }
        }
    }
}
