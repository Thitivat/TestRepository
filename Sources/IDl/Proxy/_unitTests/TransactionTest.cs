using BND.Services.Payments.iDeal.Models;
using BND.Services.Payments.iDeal.Proxy.Interfaces;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Security;

namespace BND.Services.Payments.iDeal.Proxy.Tests
{
    [TestFixture]
    public class TransactionTest
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

            using (ITransaction transaction = new Transaction(baseAddress, token))
            {
                // just call the constructor that didn't call in another test case.
            }
        }

        [TestCase("http://localhost/")]
        [TestCase("http://localhost")]
        public void CreateTransaction_Should_ReturnTransactionResponse_When_EverythingIsFine(string baseUrl)
        {
            string baseAddress = baseUrl;
            string token = "token";

            TransactionResponseModel expectedResponse = new TransactionResponseModel
            {
                EntranceCode = "fhkdlgkj4g5a",
                IssuerAuthenticationURL = new System.Uri("http://localhost/api/Transactions"),
                PurchaseID = "1234",
                TransactionID = "abcd1234"
            };

            // Set expected data that will return from mock http client.
            _httpMessageHandler.AddMockResponse("Transactions", HttpStatusCode.OK, expectedResponse);

            using (ITransaction transaction = new Transaction(baseAddress, token, _mockHttpClient))
            {
                TransactionRequestModel request = new TransactionRequestModel
                {
                    Amount = 60m,
                    PurchaseID = "1234",
                    MerchantReturnURL = "http://localhost/api/Transactions"
                };

                var actual = transaction.CreateTransaction(request);

                Assert.True(Helpers.ObjectIsEqual(actual, expectedResponse));
            }
        }

        [Test]
        public void CreateTransaction_Should_ThrowsException_When_TransactionRequestIsNull()
        {
            string baseAddress = "http://localhost";
            string token = "token";

            using (ITransaction transaction = new Transaction(baseAddress, token, _mockHttpClient))
            {
                TransactionRequestModel request = null;

                Assert.Throws(typeof(ArgumentNullException), () => transaction.CreateTransaction(request));
            }
        }

        [TestCase(HttpStatusCode.BadRequest, typeof(ArgumentException))]
        [TestCase(HttpStatusCode.Forbidden, typeof(SecurityException))]
        [TestCase(HttpStatusCode.Unauthorized, typeof(UnauthorizedAccessException))]
        [TestCase(HttpStatusCode.InternalServerError, typeof(iDealOperationException))]
        public void CreateTransaction_Should_ThrowsException_When_Error(HttpStatusCode statusCode, Type exception)
        {
            string baseAddress = "http://localhost";
            string token = "token";

            ApiErrorModel expectedResponse = new ApiErrorModel
            {
                ErrorCode = "123",
                Message = "Error",
                StatusCode = statusCode
            };

            // Set expected data that will return from mock http client.
            _httpMessageHandler.AddMockResponse("Transactions", statusCode, expectedResponse);

            using (ITransaction transaction = new Transaction(baseAddress, token, _mockHttpClient))
            {
                TransactionRequestModel request = new TransactionRequestModel
                {
                    Amount = 60m,
                    PurchaseID = "1234",
                    MerchantReturnURL = "http://localhost/api/Transactions"
                };

                Assert.Throws(exception, () => transaction.CreateTransaction(request));
            }
        }

        [Test]
        public void CreateTransaction_Should_ThrowsiDealOperationException_When_Error()
        {
            string baseAddress = "http://localhost";
            string token = "token";

            string expectedResponse = "not return in ApiErrorModel class";

            // Set expected data that will return from mock http client.
            _httpMessageHandler.AddMockResponse("Transactions", HttpStatusCode.InternalServerError, expectedResponse);

            using (ITransaction transaction = new Transaction(baseAddress, token, _mockHttpClient))
            {
                TransactionRequestModel request = new TransactionRequestModel
                {
                    Amount = 60m,
                    PurchaseID = "1234",
                    MerchantReturnURL = "http://localhost/api/Transactions"
                };

                Assert.Throws(typeof(iDealOperationException), () => transaction.CreateTransaction(request));
            }
        }

        [Test]
        public void GetStatus_Should_ReturnResult_When_EverythingIsFine()
        {
            string baseAddress = "http://localhost/";
            string token = "token";

            string transactionid = "abcd1234";
            string entranceCode = "fhkdlgkj4g5a";

            EnumQueryStatus expectedStatus = EnumQueryStatus.Success;

            string getStatusUrl = String.Format("Transactions/{0}/Status?entranceCode={1}", transactionid, entranceCode);

            // Set expected data that will return from mock http client.
            _httpMessageHandler.AddMockResponse(getStatusUrl, HttpStatusCode.OK, expectedStatus);


            using (ITransaction transaction = new Transaction(baseAddress, token, _mockHttpClient))
            {
                TransactionRequestModel request = new TransactionRequestModel
                {
                    Amount = 60m,
                    PurchaseID = "1234",
                    MerchantReturnURL = String.Format("http://localhost/api/{0}", getStatusUrl)
                };

                var actual = transaction.GetStatus(entranceCode, transactionid);
                Assert.AreEqual(expectedStatus, actual);
            }
        }

        [TestCase("", "abcd1234", typeof(ArgumentNullException))]
        [TestCase("fhkdlgkj4g5a", "", typeof(ArgumentNullException))]
        [TestCase("....", "abcd1234", typeof(ArgumentException))]
        [TestCase("fhkdlgkj4g5a", "....", typeof(ArgumentException))]
        public void GetStatus_Should_ThrowArgumentExcetion_When_ParametersAreEmpty(string entranceCode, string tranacetionID, Type exceptoin)
        {
            string baseAddress = "http://localhost/";
            string token = "token";

            EnumQueryStatus expectedStatus = EnumQueryStatus.Success;

            string getStatusUrl = String.Format("Transactions/{0}/Status?entranceCode={1}", tranacetionID, entranceCode);

            // Set expected data that will return from mock http client.
            _httpMessageHandler.AddMockResponse(getStatusUrl, HttpStatusCode.OK, expectedStatus);


            using (ITransaction transaction = new Transaction(baseAddress, token, _mockHttpClient))
            {
                TransactionRequestModel request = new TransactionRequestModel
                {
                    Amount = 60m,
                    PurchaseID = "1234",
                    MerchantReturnURL = String.Format("http://localhost/api/{0}", getStatusUrl)
                };

                Assert.Throws(exceptoin, () => transaction.GetStatus(entranceCode, tranacetionID));
            }
        }

        [Test]
        public void GetStatus_Should_ThrowiDealOperationException_When_StatusResponseIsWrong()
        {
            string baseAddress = "http://localhost/";
            string token = "token";

            string transactionid = "abcd1234";
            string entranceCode = "fhkdlgkj4g5a";

            string getStatusUrl = String.Format("Transactions/{0}/Status?entranceCode={1}", transactionid, entranceCode);

            // Set expected data that will return from mock http client.
            _httpMessageHandler.AddMockResponse(getStatusUrl, HttpStatusCode.OK, "WrongStatus");


            using (ITransaction transaction = new Transaction(baseAddress, token, _mockHttpClient))
            {
                TransactionRequestModel request = new TransactionRequestModel
                {
                    Amount = 60m,
                    PurchaseID = "1234",
                    MerchantReturnURL = String.Format("http://localhost/api/{0}", getStatusUrl)
                };

                Assert.Throws(typeof(iDealOperationException), () => transaction.GetStatus(entranceCode, transactionid));

            }
        }

    }
}
