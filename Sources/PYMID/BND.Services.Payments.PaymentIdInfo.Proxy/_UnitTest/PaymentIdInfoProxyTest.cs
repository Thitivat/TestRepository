using BND.Services.Infrastructure.ErrorHandling;
using BND.Services.Payments.PaymentIdInfo.Libs.Models;
using BND.Services.Payments.PaymentIdInfo.Proxy.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace BND.Services.Payments.PaymentIdInfo.Proxy.Test
{
    [TestFixture]
    public class PaymentIdInfoProxyTest
    {
        private MockHttpMessageHandler _httpMessageHandler;
        private HttpClient _mockHttpClient;
        private Uri _baseApiUri;
        private string _baseAddress;
        private string _token;
        private string _filterType;
        private string _bndIban;
        private string _sourceIban;
        private string _transactionId;
        private List<PaymentIdInfoModel> _expectedResponse;

        [SetUp]
        public void Initialize()
        {
            _baseApiUri = new Uri("http://localhost/api/");
            _httpMessageHandler = new MockHttpMessageHandler(_baseApiUri);
            _mockHttpClient = new HttpClient(_httpMessageHandler);
            _baseAddress = "http://localhost/";
            _token = "Token";
            _filterType = "ideal";
            _bndIban = "NL37BNDA4818136651";
            _sourceIban = "NL37BNDA4818136652";
            _transactionId = "123";

            _expectedResponse = new List<PaymentIdInfoModel>()
            { 
                new PaymentIdInfoModel() {
                    BndIban = _bndIban,
                    SourceIban = _sourceIban,
                    SourceAccountHolderName = "Test",
                    TransactionDate = DateTime.Now,
                    TransactionId = _transactionId,
                    TransactionType = _filterType
                }
            };
        }

        [Test]
        public void Constructor_Should_WorkFine_When_EverythingIsFine()
        {
            using (IPaymentIdInfoProxy paymentIdInfo = new PaymentIdInfoProxy(_baseAddress, _token))
            {
                // just call the constructor that didn't call in another test case.
            }
        }

        [Test]
        public void GetFilterTypes_Should_Return_FilterTypes()
        {
            List<EnumFilterType> expected = new List<EnumFilterType>();
            expected.Add(EnumFilterType.ideal);

            // Set expected data that will return from mock http client.
            _httpMessageHandler.AddMockResponse("PaymentIdInfos/GetFilterTypes", HttpStatusCode.OK, expected);

            using (IPaymentIdInfoProxy paymentIdInfo = new PaymentIdInfoProxy(_baseAddress, _token, _mockHttpClient))
            {
                var actual = paymentIdInfo.GetFilterTypes();
                Assert.AreEqual(expected.Count(), actual.Count());
                Assert.AreEqual(expected.First().ToString(), actual.First().ToString());
            }
        }

        [Test]
        public void GetFilterTypes_Should_ThrowAnError_When_TheHttpStatusCodeIsNot200()
        {
            // set expected response.
            ApiErrorModel expectedResponse = new ApiErrorModel
            {
                ErrorCode = "123",
                Message = "Error",
                StatusCode = HttpStatusCode.InternalServerError
            };

            // Set expected data that will return from mock http client.
            _httpMessageHandler.AddMockResponse("PaymentIdInfos/GetFilterTypes", expectedResponse.StatusCode, expectedResponse);

            using (IPaymentIdInfoProxy paymentIdInfo = new PaymentIdInfoProxy(_baseAddress, _token, _mockHttpClient))
            {
                Assert.Throws(typeof(ProxyException), () => paymentIdInfo.GetFilterTypes());
            }
        }

        [Test]
        public void GetByBndIban_Should_Return_PaymentIdInfoModel()
        {
            // Set expected data that will return from mock http client.
            _httpMessageHandler.AddMockResponse("PaymentIdInfos/BndIban/" + _bndIban, HttpStatusCode.OK, _expectedResponse);

            using(IPaymentIdInfoProxy paymentIdInfo = new PaymentIdInfoProxy(_baseAddress, _token, _mockHttpClient))
            {
                var actual = paymentIdInfo.GetByBndIban(_bndIban, String.Empty);

                Assert.AreEqual(_expectedResponse.First().BndIban, actual.First().BndIban);
                Assert.True(actual.All(c => c.BndIban == _expectedResponse.First().BndIban));
            }
        }

        [Test]
        public void GetByBndIban_WithFilterType_Should_Return_PaymentIdInfoModel()
        {
            // Set expected data that will return from mock http client.
            _httpMessageHandler.AddMockResponse("PaymentIdInfos/BndIban/" + _bndIban + "?filtertype=ideal", HttpStatusCode.OK, _expectedResponse);

            using (IPaymentIdInfoProxy paymentIdInfo = new PaymentIdInfoProxy(_baseAddress, _token, _mockHttpClient))
            {
                var actual = paymentIdInfo.GetByBndIban(_bndIban, _filterType);

                Assert.AreEqual(_expectedResponse.First().BndIban, actual.First().BndIban);
                Assert.True(actual.All(c => c.BndIban == _expectedResponse.First().BndIban));
                Assert.True(actual.All(c => c.TransactionType == _filterType));
            }
        }

        [Test]
        public void GetByBndIban_Should_ThrowError_When_NotSendBndIban()
        {
            // Set expected data that will return from mock http client.
            _httpMessageHandler.AddMockResponse("PaymentIdInfos/BndIban/" + _bndIban, HttpStatusCode.OK, _expectedResponse);

            using (IPaymentIdInfoProxy paymentIdInfo = new PaymentIdInfoProxy(_baseAddress, _token, _mockHttpClient))
            {
                Assert.Throws(typeof(ProxyException), () => paymentIdInfo.GetByBndIban(String.Empty, String.Empty));
            }
        }

        [Test]
        public void GetBySourceIban_Should_Return_PaymentIdInfoModel()
        {
            // Set expected data that will return from mock http client.
            _httpMessageHandler.AddMockResponse("PaymentIdInfos/SourceIban/" + _sourceIban, HttpStatusCode.OK, _expectedResponse);

            using (IPaymentIdInfoProxy paymentIdInfo = new PaymentIdInfoProxy(_baseAddress, _token, _mockHttpClient))
            {
                var actual = paymentIdInfo.GetBySourceIban(_sourceIban, String.Empty);

                Assert.AreEqual(_expectedResponse.First().SourceIban, actual.First().SourceIban);
                Assert.True(actual.All(c => c.SourceIban == _expectedResponse.First().SourceIban));
            }
        }

        [Test]
        public void GetBySourceIban_WithFilterType_Should_Return_PaymentIdInfoModel()
        {
            // Set expected data that will return from mock http client.
            _httpMessageHandler.AddMockResponse("PaymentIdInfos/SourceIban/" + _sourceIban + "?filtertype=ideal", HttpStatusCode.OK, _expectedResponse);

            using (IPaymentIdInfoProxy paymentIdInfo = new PaymentIdInfoProxy(_baseAddress, _token, _mockHttpClient))
            {
                var actual = paymentIdInfo.GetBySourceIban(_sourceIban, _filterType);

                Assert.AreEqual(_expectedResponse.First().SourceIban, actual.First().SourceIban);
                Assert.True(actual.All(c => c.SourceIban == _expectedResponse.First().SourceIban));
                Assert.True(actual.All(c => c.TransactionType == _filterType));
            }
        }

        [Test]
        public void GetBySourceIban_Should_ThrowError_When_NotSendSourceIban()
        {
            // Set expected data that will return from mock http client.
            _httpMessageHandler.AddMockResponse("PaymentIdInfos/SourceIban/" + _sourceIban, HttpStatusCode.OK, _expectedResponse);

            using (IPaymentIdInfoProxy paymentIdInfo = new PaymentIdInfoProxy(_baseAddress, _token, _mockHttpClient))
            {
                Assert.Throws(typeof(ProxyException), () => paymentIdInfo.GetBySourceIban(String.Empty, String.Empty));
            }
        }

        [Test]
        public void GetByTransactionId_Should_Return_PaymentIdInfoModel()
        {
            // Set expected data that will return from mock http client.
            _httpMessageHandler.AddMockResponse("PaymentIdInfos/Transaction/" + _transactionId, HttpStatusCode.OK, _expectedResponse);

            using (IPaymentIdInfoProxy paymentIdInfo = new PaymentIdInfoProxy(_baseAddress, _token, _mockHttpClient))
            {
                var actual = paymentIdInfo.GetByTransactionId(_transactionId, String.Empty);

                Assert.AreEqual(_expectedResponse.First().TransactionId, actual.First().TransactionId);
                Assert.True(actual.All(c => c.TransactionId == _expectedResponse.First().TransactionId));
            }
        }

        [Test]
        public void GetByTransactionId_WithFilterType_Should_Return_PaymentIdInfoModel()
        {
            // Set expected data that will return from mock http client.
            _httpMessageHandler.AddMockResponse("PaymentIdInfos/Transaction/" + _transactionId + "?filtertype=ideal", HttpStatusCode.OK, _expectedResponse);

            using (IPaymentIdInfoProxy paymentIdInfo = new PaymentIdInfoProxy(_baseAddress, _token, _mockHttpClient))
            {
                var actual = paymentIdInfo.GetByTransactionId(_transactionId, _filterType);

                Assert.AreEqual(_expectedResponse.First().TransactionId, actual.First().TransactionId);
                Assert.True(actual.All(c => c.TransactionId == _expectedResponse.First().TransactionId));
                Assert.True(actual.All(c => c.TransactionType == _filterType));
            }
        }

        [Test]
        public void GetByTransactionId_Should_ThrowError_When_NotSendTransactionId()
        {
            // Set expected data that will return from mock http client.
            _httpMessageHandler.AddMockResponse("PaymentIdInfos/Transaction/" + _transactionId, HttpStatusCode.OK, _expectedResponse);

            using (IPaymentIdInfoProxy paymentIdInfo = new PaymentIdInfoProxy(_baseAddress, _token, _mockHttpClient))
            {
                Assert.Throws(typeof(ProxyException), () => paymentIdInfo.GetByTransactionId(String.Empty, String.Empty));
            }
        }
    }
}
