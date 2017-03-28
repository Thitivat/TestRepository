using BND.Services.IbanStore.Proxy;
using BND.Services.IbanStore.Proxy.Interfaces;
using BND.Services.IbanStore.Proxy.Models;
using BND.Services.Infrastructure.ErrorHandling;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace BND.Services.IbanStore.ProxyTest
{
    [TestFixture]
    public class IbanResourceTest
    {
        private string _token = "IBAN_TOKEN";
        private string _prefixUid = "PREFIX_UID";
        private string _uid = "UID";
        private int _ibanId = 1;
        private string _baseAddress = "http://www.tempuri.org";

        private HttpClient GetHttpClient(HttpStatusCode statusCode, object expectedResponse, string apiAddress, string location = "http://www.tempura.org")
        {
            string jsonOutput = JsonConvert.SerializeObject(expectedResponse);

            // Do mocking HttpResponse
            var httpClientResponse = new HttpResponseMessage();
            httpClientResponse.StatusCode = statusCode;
            if (!String.IsNullOrEmpty(location))
            {
                httpClientResponse.Headers.TryAddWithoutValidation("Location", "http://www.tempura.org");
            }
            httpClientResponse.Content = new StringContent(jsonOutput);

            // Create fake HttpClient
            var fakeResponseHandler = new FakeResponseHandler();
            string baseApiAddress = _baseAddress + "/api/" + apiAddress;
            fakeResponseHandler.AddFakeResponse(new Uri(baseApiAddress), httpClientResponse);

            HttpClient httpClient = new HttpClient(fakeResponseHandler);
            httpClient.BaseAddress = new Uri(_baseAddress + "/api/");
            return httpClient;
        }

        #region [ReserveNextAvailable]
        [Test]
        public void Test_ReserveNextAvailable_Success_200()
        {
            // Do mocking HttpClient
            var jsonOutput = new { IbanId = 199 };

            // Inject HttpClient into target
            var ibanResource = new Mock<IbanResource>(_baseAddress, _token);
            ibanResource
                .Protected()
                .Setup<HttpClient>("GetHttpClient", _prefixUid)
                .Returns(GetHttpClient(HttpStatusCode.OK, jsonOutput, String.Format("Ibans/nextavailable/{0}", _uid)));

            // Start testing
            var latestIban = ibanResource.Object.ReserveNextAvailable(_prefixUid, _uid);

            // Expected variables
            Assert.AreEqual(199, latestIban);
        }

        [Test]
        public void Test_ReserveNextAvailable_Success_304()
        {
            // Do mocking HttpClient
            var jsonOutput = new { IbanId = 199 };

            // Inject HttpClient into target
            var ibanResource = new Mock<IbanResource>(_baseAddress, _token);
            ibanResource
                .Protected()
                .Setup<HttpClient>("GetHttpClient", _prefixUid)
                .Returns(GetHttpClient(HttpStatusCode.NotModified, jsonOutput, String.Format("Ibans/nextavailable/{0}", _uid)));

            // Start testing
            var latestIban = ibanResource.Object.ReserveNextAvailable(_prefixUid, _uid);

            // Expected variables
            Assert.AreEqual(199, latestIban);
        }

        [Test]
        public void ReserveNextAvailable_Should_ThrowProxyException_When_StatusCodeIsNot200Or304()
        {
            // Do mocking HttpClient
            var jsonOutput = new { Message = "Bad Request" };

            // Inject HttpClient into target
            var ibanResource = new Mock<IbanResource>(_baseAddress, _token);
            ibanResource
                .Protected()
                .Setup<HttpClient>("GetHttpClient", _prefixUid)
                .Returns(GetHttpClient(HttpStatusCode.BadRequest, jsonOutput, String.Format("Ibans/nextavailable/{0}", _uid)));

            // Start testing
            Assert.Throws<ProxyException>(() =>
            {
                ibanResource.Object.ReserveNextAvailable(_prefixUid, _uid);
            });
        }

        [Test]
        public void ReserveNextAvailable_Should_ThrowArgumentNullException_When_UidPrefixIsNull()
        {
            // Start testing
            var ibanResource = new IbanResource(_baseAddress, _token);
            Assert.Throws<ArgumentNullException>(() =>
            {
                ibanResource.ReserveNextAvailable(null, _uid);
            });
        }

        [Test]
        public void ReserveNextAvailable_Should_ThrowArgumentNullException_When_UidIsNull()
        {
            // Start testing
            var ibanResource = new IbanResource(_baseAddress, _token);
            Assert.Throws<ArgumentNullException>(() =>
            {
                ibanResource.ReserveNextAvailable(_prefixUid, null);
            });
        }

        [Test]
        public void ReserveNextAvailable_Should_ThrowProxyException_When_LocationIsNull()
        {
            // Do mocking HttpClient
            var jsonOutput = new { Message = "Bad Request" };

            // Inject HttpClient into target
            var ibanResource = new Mock<IbanResource>(_baseAddress, _token);
            ibanResource
                .Protected()
                .Setup<HttpClient>("GetHttpClient", _prefixUid)
                .Returns(GetHttpClient(HttpStatusCode.OK, jsonOutput, String.Format("Ibans/nextavailable/{0}", _uid), null));

            // Start testing
            Assert.Throws<ProxyException>(() =>
            {
                ibanResource.Object.ReserveNextAvailable(_prefixUid, _uid);
            });
        }

        #endregion

        #region [Assign]
        [Test]
        public void Test_Assign_Success_200()
        {
            // Inject HttpClient into target
            var ibanResource = new Mock<IbanResource>(_baseAddress, _token);
            ibanResource
                .Protected()
                .Setup<HttpClient>("GetHttpClient", _prefixUid)
                .Returns(GetHttpClient(HttpStatusCode.OK, null, String.Format("ibans/{0}/Assign/{1}", _ibanId, _uid)));

            // Start testing
            var ibanUrl = ibanResource.Object.Assign(_prefixUid, _uid, _ibanId);

            // Assert
            Assert.AreEqual("http://www.tempura.org/", ibanUrl);
        }

        [Test]
        public void Test_Assign_Success_304()
        {
            // Inject HttpClient into target
            var ibanResource = new Mock<IbanResource>(_baseAddress, _token);
            ibanResource
                .Protected()
                .Setup<HttpClient>("GetHttpClient", _prefixUid)
                .Returns(GetHttpClient(HttpStatusCode.NotModified, null, String.Format("ibans/{0}/Assign/{1}", _ibanId, _uid)));

            // Start testing
            var ibanUrl = ibanResource.Object.Assign(_prefixUid, _uid, _ibanId);

            // Assert
            Assert.AreEqual("http://www.tempura.org/", ibanUrl);
        }

        [Test]
        public void Assign_Should_ThrowProxyException_When_StatusCodeIdNot200Or304()
        {
            // Inject HttpClient into target
            var ibanResource = new Mock<IbanResource>(_baseAddress, _token);
            ibanResource
                .Protected()
                .Setup<HttpClient>("GetHttpClient", _prefixUid)
                .Returns(GetHttpClient(HttpStatusCode.BadRequest, null, String.Format("ibans/{0}/Assign/{1}", _ibanId, _uid)));

            // Start testing
            Assert.Throws<ProxyException>(() =>
            {
                var actualResponse = ibanResource.Object.Assign(_prefixUid, _uid, _ibanId);
            });
        }

        [Test]
        public void Assign_Should_ThrowArgumentNullException_When_UidPrefixIsNull()
        {
            // Start testing
            var ibanResource = new IbanResource(_baseAddress, _token);

            Assert.Throws<ArgumentNullException>(() =>
            {
                var actualResponse = ibanResource.Assign(null, _uid, _ibanId);
            });
        }
        #endregion

        #region [Get]
        [Test]
        public void Get_Should_ReturnNextAvailable_When_Success()
        {
            // Do mocking HttpClient
            var expected = new NextAvailable()
            {
                BankCode = "BANK_CODE",
                Bban = "BBAN",
                BbanFileName = "BBAN_FILE_NAME",
            };

            // Inject HttpClient into target
            var ibanResource = new Mock<IbanResource>(_baseAddress, _token);
            ibanResource
                .Protected()
                .Setup<HttpClient>("GetHttpClient", _prefixUid)
                .Returns(GetHttpClient(HttpStatusCode.OK, expected, String.Format("ibans/{0}", _uid)));

            // Start testing
            var actual = ibanResource.Object.Get(_prefixUid, _uid);

            // Assert
            Assert.AreEqual(expected.BankCode, actual.BankCode);
            Assert.AreEqual(expected.Bban, actual.Bban);
            Assert.AreEqual(expected.BbanFileName, actual.BbanFileName);
        }

        [Test]
        public void Get_Should_ThrowProxyException_When_StatusCodeIdNot200()
        {
            // Do mocking HttpClient
            var jsonOutput = new { Message = "Bad Request" };

            // Inject HttpClient into target
            var ibanResource = new Mock<IbanResource>(_baseAddress, _token);
            ibanResource
                .Protected()
                .Setup<HttpClient>("GetHttpClient", _prefixUid)
                .Returns(GetHttpClient(HttpStatusCode.BadRequest, jsonOutput, String.Format("ibans/{0}", _uid)));

            // Start testing
            Assert.Throws<ProxyException>(() =>
            {
                var actualResponse = ibanResource.Object.Get(_prefixUid, _uid);
            });
        }

        [Test]
        public void Get_Should_ThrowArgumentNullException_When_UidPrefixIsNull()
        {
            // Start testing
            var ibanResource = new IbanResource(_baseAddress, _token);
            Assert.Throws<ArgumentNullException>(() =>
            {
                var actualResponse = ibanResource.Get(null, _uid);
            });
        }
        #endregion

        #region [ReserveAndAssign]
        [Test]
        public void Test_ReserveAndAssign_Success()
        {
            // Init url for apis
            string apiNextAvailable = _baseAddress + "/api/" + string.Format("Ibans/nextavailable/{0}", _uid);
            string apiAssign = _baseAddress + "/api/" + string.Format("ibans/{0}/Assign/{1}", _ibanId, _uid);
            string apiGet = _baseAddress + "/api/" + string.Format("ibans/{0}", _uid);

            // Init Json
            var expected = new NextAvailable()
            {
                BankCode = "BANK_CODE",
                Bban = "BBAN",
                BbanFileName = "BBAN_FILE_NAME",
            };
            var jsonNextAvailable = JsonConvert.SerializeObject(new { IbanId = 1 });
            var jsonGet = JsonConvert.SerializeObject(expected);

            // Do mocking HttpResponse
            // >> Available
            var responseNextAvailable = new HttpResponseMessage();
            responseNextAvailable.StatusCode = HttpStatusCode.OK;
            responseNextAvailable.Headers.TryAddWithoutValidation("Location", "http://www.tempura.org");
            responseNextAvailable.Content = new StringContent(jsonNextAvailable);

            // >> Assign
            var responseAssign = new HttpResponseMessage();
            responseAssign.StatusCode = HttpStatusCode.OK;
            responseAssign.Headers.TryAddWithoutValidation("Location", "http://www.tempura.org");

            // >> Get
            var responseGet = new HttpResponseMessage();
            responseGet.StatusCode = HttpStatusCode.OK;
            responseGet.Content = new StringContent(jsonGet);

            // Create fake HttpClient
            var fakeResponseHandler = new FakeResponseHandler();
            fakeResponseHandler.AddFakeResponse(new Uri(apiNextAvailable), responseNextAvailable);
            fakeResponseHandler.AddFakeResponse(new Uri(apiAssign), responseAssign);
            fakeResponseHandler.AddFakeResponse(new Uri(apiGet), responseGet);

            // Inject HttpClient into target
            var ibanResource = new Mock<IbanResource>(_baseAddress, _token);
            ibanResource.Protected().Setup<HttpClient>("GetHttpClient", _prefixUid).Returns(() =>
            {
                // Create HttpClient
                HttpClient httpClient = new HttpClient(fakeResponseHandler);
                httpClient.BaseAddress = new Uri(_baseAddress + "/api/");
                return httpClient;
            });

            // Start testing
            var actual = ibanResource.Object.ReserveAndAssign(_prefixUid, _uid);

            // Assert
            Assert.AreEqual(expected.BankCode, actual.BankCode);
            Assert.AreEqual(expected.Bban, actual.Bban);
            Assert.AreEqual(expected.BbanFileName, actual.BbanFileName);
        }

        [Test]
        public void ReserveAndAssign_Should_ThrowProxyException_When_ErrorHasOccurredOnGet()
        {
            // Init url for apis
            string apiNextAvailable = _baseAddress + "/api/" + string.Format("Ibans/nextavailable/{0}", _uid);
            string apiAssign = _baseAddress + "/api/" + string.Format("ibans/{0}/Assign/{1}", _ibanId, _uid);
            string apiGet = _baseAddress + "/api/" + string.Format("ibans/{0}", _uid);

            // Init Json
            var jsonNextAvailable = JsonConvert.SerializeObject(new { IbanId = 1 });
            var jsonGet = JsonConvert.SerializeObject(new { Message = "Bad Request" });

            // Do mocking HttpResponse
            // >> Available
            var responseNextAvailable = new HttpResponseMessage();
            responseNextAvailable.StatusCode = HttpStatusCode.OK;
            responseNextAvailable.Headers.TryAddWithoutValidation("Location", "http://www.tempura.org");
            responseNextAvailable.Content = new StringContent(jsonNextAvailable);

            // >> Assign
            var responseAssign = new HttpResponseMessage();
            responseAssign.StatusCode = HttpStatusCode.OK;
            responseAssign.Headers.TryAddWithoutValidation("Location", "http://www.tempura.org");

            // >> Get
            var responseGet = new HttpResponseMessage();
            responseGet.StatusCode = HttpStatusCode.BadRequest;
            responseGet.Content = new StringContent(jsonGet);

            // Create fake HttpClient
            var fakeResponseHandler = new FakeResponseHandler();
            fakeResponseHandler.AddFakeResponse(new Uri(apiNextAvailable), responseNextAvailable);
            fakeResponseHandler.AddFakeResponse(new Uri(apiAssign), responseAssign);
            fakeResponseHandler.AddFakeResponse(new Uri(apiGet), responseGet);

            // Create HttpClient
            HttpClient httpClient = new HttpClient(fakeResponseHandler);
            httpClient.BaseAddress = new Uri(_baseAddress + "/api/");

            // Inject HttpClient into target
            var ibanResource = new Mock<IbanResource>(_baseAddress, _token);
            ibanResource.Protected().Setup<HttpClient>("GetHttpClient", _prefixUid).Returns(httpClient);

            // Start testing
            Assert.Throws<ProxyException>(() =>
            {
                var actualResponse = ibanResource.Object.ReserveAndAssign(_prefixUid, _uid);
            });

        }

        [Test]
        public void ReserveAndAssign_Should_ThrowArgumentNullException_When_AnyRequiredFieldIsNull()
        {
            // Init url for apis
            string apiNextAvailable = _baseAddress + "/api/" + string.Format("Ibans/nextavailable/{0}", _uid);
            string apiAssign = _baseAddress + "/api/" + string.Format("ibans/{0}/Assign/{1}", _ibanId, _uid);
            string apiGet = _baseAddress + "/api/" + string.Format("ibans/{0}", _uid);

            // Init Json
            var jsonNextAvailable = JsonConvert.SerializeObject(new { IbanId = 1 });
            var jsonGet = JsonConvert.SerializeObject(new { Message = "Bad Request" });

            // Do mocking HttpResponse
            // >> Available
            var responseNextAvailable = new HttpResponseMessage();
            responseNextAvailable.StatusCode = HttpStatusCode.OK;
            responseNextAvailable.Headers.TryAddWithoutValidation("Location", "http://www.tempura.org");
            responseNextAvailable.Content = new StringContent(jsonNextAvailable);

            // >> Assign
            var responseAssign = new HttpResponseMessage();
            responseAssign.StatusCode = HttpStatusCode.OK;
            responseAssign.Headers.TryAddWithoutValidation("Location", "http://www.tempura.org");

            // >> Get
            var responseGet = new HttpResponseMessage();
            responseGet.StatusCode = HttpStatusCode.BadRequest;
            responseGet.Content = new StringContent(jsonGet);

            // Create fake HttpClient
            var fakeResponseHandler = new FakeResponseHandler();
            fakeResponseHandler.AddFakeResponse(new Uri(apiNextAvailable), responseNextAvailable);
            fakeResponseHandler.AddFakeResponse(new Uri(apiAssign), responseAssign);
            fakeResponseHandler.AddFakeResponse(new Uri(apiGet), responseGet);

            // Create HttpClient
            HttpClient httpClient = new HttpClient(fakeResponseHandler);
            httpClient.BaseAddress = new Uri(_baseAddress + "/api/");

            // Inject HttpClient into target
            var ibanResource = new Mock<IbanResource>(_baseAddress, _token);
            ibanResource.Protected().Setup<HttpClient>("GetHttpClient", _prefixUid).Returns(httpClient);

            // Start testing
            Assert.Throws<ArgumentNullException>(() =>
            {
                var actualResponse = ibanResource.Object.ReserveAndAssign(null, _uid);
            });
        }

        #endregion

        #region GetHttpClient

        public class MockIbanResource : IbanResource
        {
            public MockIbanResource(string baseAddress, string token)
                : base(baseAddress, token)
            { }

            public HttpClient GetHttpClientTest(string uidPrefix)
            {
                return base.GetHttpClient(uidPrefix);
            }
        }

        [Test]
        public void GetHttpClient_Should_ReturnHttpClient_When_UseApiAddressWithoutSlash()
        {
            MockIbanResource ibanResource = new MockIbanResource("http://www.tempuri.org", _token);
            HttpClient httpClient = ibanResource.GetHttpClientTest(_prefixUid);

            Assert.AreEqual("http://www.tempuri.org/api/", httpClient.BaseAddress.AbsoluteUri);
            Assert.AreEqual(_token, httpClient.DefaultRequestHeaders.Authorization.Scheme);
            Assert.AreEqual(_prefixUid, httpClient.DefaultRequestHeaders.Where(x => x.Key == "Uid-Prefix").First().Value.First());
        }

        [Test]
        public void GetHttpClient_Should_ReturnHttpClient_When_UseApiAddressWithSlash()
        {
            MockIbanResource ibanResource = new MockIbanResource("http://www.tempuri.org/", _token);
            HttpClient httpClient = ibanResource.GetHttpClientTest(_prefixUid);

            Assert.AreEqual("http://www.tempuri.org/api/", httpClient.BaseAddress.AbsoluteUri);
            Assert.AreEqual(_token, httpClient.DefaultRequestHeaders.Authorization.Scheme);
            Assert.AreEqual(_prefixUid, httpClient.DefaultRequestHeaders.Where(x => x.Key == "Uid-Prefix").First().Value.First());
        }

        #endregion
    }
}
