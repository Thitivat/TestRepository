using BND.Services.Payments.eMandates.Entities;
using BND.Services.Payments.eMandates.Enums;
using BND.Services.Payments.eMandates.Proxy;
using BND.Services.Payments.eMandates.Proxy.Interfaces;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Security;

namespace BND.Services.Payments.eMandates.UnitTests.Proxy
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
            _baseUri = new Uri("http://localhost/v1/");
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
                CreatedDate = DateTime.UtcNow,
                IssuerAuthenticationUrl = "http://localhost/api/transactions",
                TransactionId = "",
                TransactionRequestDateTimeStamp = DateTime.UtcNow
            };

            // Set expected data that will return from mock http client.
            _httpMessageHandler.AddMockResponse("transactions", HttpStatusCode.OK, expectedResponse);

            using (ITransaction transaction = new Transaction(baseAddress, token, _mockHttpClient))
            {
                NewTransactionModel request = new NewTransactionModel
                {
                    DebtorBankId = "INGBNL2A",
                    MaxAmount = 55,
                    DebtorReference = "ref",
                    EMandateId = Guid.NewGuid().ToString(),
                    EMandateReason = "test",
                    ExpirationPeriod = new TimeSpan(DateTime.UtcNow.Ticks),
                    Language = "NL",
                    PurchaseId = "0000000001",
                    SequenceType = "oOff"
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
                NewTransactionModel request = null;

                Assert.Throws(typeof(ArgumentNullException), () => transaction.CreateTransaction(request));
            }
        }

        [TestCase(HttpStatusCode.BadRequest, typeof(ArgumentException))]
        [TestCase(HttpStatusCode.Forbidden, typeof(SecurityException))]
        [TestCase(HttpStatusCode.Unauthorized, typeof(UnauthorizedAccessException))]
        [TestCase(HttpStatusCode.InternalServerError, typeof(eMandateOperationException))]
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
            _httpMessageHandler.AddMockResponse("transactions", statusCode, expectedResponse);

            using (ITransaction transaction = new Transaction(baseAddress, token, _mockHttpClient))
            {
                NewTransactionModel request = new NewTransactionModel
                {
                    DebtorBankId = "INGBNL2A",
                    MaxAmount = 55,
                    DebtorReference = "ref",
                    EMandateId = Guid.NewGuid().ToString(),
                    EMandateReason = "test",
                    ExpirationPeriod = new TimeSpan(DateTime.UtcNow.Ticks),
                    Language = "NL",
                    PurchaseId = "0000000001",
                    SequenceType = "oOff"
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
            _httpMessageHandler.AddMockResponse("transactions", HttpStatusCode.InternalServerError, expectedResponse);

            using (ITransaction transaction = new Transaction(baseAddress, token, _mockHttpClient))
            {
                NewTransactionModel request = new NewTransactionModel
                {
                    DebtorBankId = "INGBNL2A",
                    MaxAmount = 55,
                    DebtorReference = "ref",
                    EMandateId = Guid.NewGuid().ToString(),
                    EMandateReason = "test",
                    ExpirationPeriod = new TimeSpan(DateTime.UtcNow.Ticks),
                    Language = "NL",
                    PurchaseId = "0000000001",
                    SequenceType = "oOff"
                };

                Assert.Throws(typeof(eMandateOperationException), () => transaction.CreateTransaction(request));
            }
        }

        [Test]
        public void GetStatus_Should_ReturnResult_When_EverythingIsFine()
        {
            string baseAddress = "http://localhost/";
            string token = "token";

            string transactionid = "1234";

            EnumQueryStatus expectedStatus = EnumQueryStatus.Success;

            string getStatusUrl = String.Format("transactions/{0}/status", transactionid);

            // Set expected data that will return from mock http client.
            _httpMessageHandler.AddMockResponse(getStatusUrl, HttpStatusCode.OK, expectedStatus);


            using (ITransaction transaction = new Transaction(baseAddress, token, _mockHttpClient))
            {
                NewTransactionModel request = new NewTransactionModel
                {
                    DebtorBankId = "INGBNL2A",
                    MaxAmount = 55,
                    DebtorReference = "ref",
                    EMandateId = Guid.NewGuid().ToString(),
                    EMandateReason = "test",
                    ExpirationPeriod = new TimeSpan(DateTime.UtcNow.Ticks),
                    Language = "NL",
                    PurchaseId = "0000000001",
                    SequenceType = "oOff"
                };

                var actual = transaction.GetTransactionStatus(transactionid);
                Assert.AreEqual(expectedStatus, actual);
            }
        }

        [TestCase("", typeof(ArgumentNullException))]
        [TestCase("132456hdfgh", typeof(ArgumentException))]
        public void GetStatus_Should_ThrowArgumentExcetion_When_ParametersAreEmpty(string transactionId, Type exception)
        {
            string baseAddress = "http://localhost/";
            string token = "token";

            EnumQueryStatus expectedStatus = EnumQueryStatus.Success;

            string getStatusUrl = String.Format("transactions/{0}/status", transactionId);

            // Set expected data that will return from mock http client.
            _httpMessageHandler.AddMockResponse(getStatusUrl, HttpStatusCode.OK, expectedStatus);


            using (ITransaction transaction = new Transaction(baseAddress, token, _mockHttpClient))
            {
                NewTransactionModel request = new NewTransactionModel
                {
                    DebtorBankId = "INGBNL2A",
                    MaxAmount = 55,
                    DebtorReference = "ref",
                    EMandateId = Guid.NewGuid().ToString(),
                    EMandateReason = "test",
                    ExpirationPeriod = new TimeSpan(DateTime.UtcNow.Ticks),
                    Language = "NL",
                    PurchaseId = "0000000001",
                    SequenceType = "oOff"
                };

                Assert.Throws(exception, () => transaction.GetTransactionStatus( transactionId));
            }
        }

        [Test]
        public void GetStatus_Should_ThroweMandateOperationException_When_StatusResponseIsWrong()
        {
            string baseAddress = "http://localhost/";
            string token = "token";

            string transactionid = "123456";

            string getStatusUrl = String.Format("transactions/{0}/status", transactionid);

            // Set expected data that will return from mock http client.
            _httpMessageHandler.AddMockResponse(getStatusUrl, HttpStatusCode.OK, "WrongStatus");


            using (ITransaction transaction = new Transaction(baseAddress, token, _mockHttpClient))
            {
                NewTransactionModel request = new NewTransactionModel
                {
                    DebtorBankId = "INGBNL2A",
                    MaxAmount = 55,
                    DebtorReference = "ref",
                    EMandateId = Guid.NewGuid().ToString(),
                    EMandateReason = "test",
                    ExpirationPeriod = new TimeSpan(DateTime.UtcNow.Ticks),
                    Language = "NL",
                    PurchaseId = "0000000001",
                    SequenceType = "oOff"
                };

                Assert.Throws(typeof(eMandateOperationException), () => transaction.GetTransactionStatus(transactionid));

            }
        }

    }
}
