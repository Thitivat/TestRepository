using BND.Services.Payments.iDeal.iDealClients.Base;
using BND.Services.Payments.iDeal.iDealClients.Directory;
using BND.Services.Payments.iDeal.iDealClients.Interfaces;
using BND.Services.Payments.iDeal.iDealClients.Models;
using BND.Services.Payments.iDeal.iDealClients.Status;
using BND.Services.Payments.iDeal.iDealClients.Transaction;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using LibsModel = BND.Services.Payments.iDeal.Models;
using SYSConfig = System.Configuration;

namespace BND.Services.Payments.iDeal.Bll.Tests
{
    [TestFixture]
    public class iDealClientTest
    {
        public string _merchantId { get; set; }
        public int _merchantSubId { get; set; }

        public iDealRequestBase _requestBase;

        [SetUp]
        public void Init()
        {
            _merchantId = "003087587";
            _merchantSubId = 0;
        }

        [TearDown]
        public void CleanUp()
        {
            _requestBase = null;
        }

        #region [iDealClient]
        [Test]
        public void iDealClient_Should_ReturnNormalResults_When_StatusIsDifference()
        {
            // ### This method need to comment out for team city to build success,
            // ### and it need to update because it call http request and make unittest failed.

            //// Constructor iDealClient
            //iDealClient client = new iDealClient(_merchantId, _merchantSubId);
            //Assert.IsNotNull(client.Configuration);

            //// SendDirectoryRequest method
            //var resultDirectory = (DirectoryResponse)client.SendDirectoryRequest();
            //Assert.IsNotNull(resultDirectory);
            //Assert.IsNotNull(resultDirectory.AcquirerId);
            //Assert.IsNotNull(resultDirectory.Issuers);
            //Assert.Greater(((List<IssuerModel>)resultDirectory.Issuers).Count, 0);
            //Assert.IsNotNull(resultDirectory.DirectoryDateTimeStampLocalTime);

            //// Random selected issuer from resultDirectory
            //List<IssuerModel> issuerList = new List<IssuerModel>(resultDirectory.Issuers);
            //string issuerCode = issuerList[new Random().Next(0, issuerList.Count - 1)].IssuerID;


            //#region Success
            //// SendTransactionRequest method
            //var resultTransaction = (TransactionResponse)client.SendTransactionRequest(issuerCode: issuerCode,
            //    merchantReturnUrl: new Uri("http://www.google.co.th").ToString(),
            //    purchaseId: Guid.NewGuid().ToString().Substring(0, 16),
            //    amount: 1,
            //    expirationPeriod: TimeSpan.FromMinutes(1),
            //    description: "Testing",
            //    entranceCode: Guid.NewGuid().ToString("n"),
            //    language: "nl",
            //    currency: "EUR");
            //Assert.IsNotNull(resultTransaction);
            //Assert.IsFalse(String.IsNullOrWhiteSpace(resultTransaction.IssuerAuthenticationUrl));
            //Assert.IsFalse(String.IsNullOrWhiteSpace(resultTransaction.TransactionId));

            //// SendStatusRequest method
            //var resultStatus = (StatusResponse)client.SendStatusRequest(resultTransaction.TransactionId);
            //Assert.IsNotNull(resultStatus);
            //Assert.AreEqual(LibsModel.EnumQueryStatus.Success.ToString(), resultStatus.Status);
            //#endregion


            //#region Cancelled [200 euro cents]
            //// SendTransactionRequest method
            //var trxCancel = (TransactionResponse)client.SendTransactionRequest(issuerCode: issuerCode,
            //      merchantReturnUrl: new Uri("http://www.google.co.th").ToString(),
            //      purchaseId: Guid.NewGuid().ToString().Substring(0, 16),
            //      amount: 2,
            //      expirationPeriod: TimeSpan.FromMinutes(1),
            //      description: "Testing",
            //      entranceCode: Guid.NewGuid().ToString("n"),
            //      language: "nl",
            //      currency: "EUR");
            //Assert.IsNotNull(trxCancel);
            //Assert.IsFalse(String.IsNullOrWhiteSpace(trxCancel.IssuerAuthenticationUrl));
            //Assert.IsFalse(String.IsNullOrWhiteSpace(trxCancel.TransactionId));

            //// SendStatusRequest method
            //var statusCancel = (StatusResponse)client.SendStatusRequest(trxCancel.TransactionId);
            //Assert.IsNotNull(statusCancel);
            //Assert.AreEqual(LibsModel.EnumQueryStatus.Cancelled.ToString(), statusCancel.Status);
            //#endregion


            //#region Expired [300 euro cents]
            //// SendTransactionRequest method
            //var trxExpired = (TransactionResponse)client.SendTransactionRequest(issuerCode: issuerCode,
            //      merchantReturnUrl: new Uri("http://www.google.co.th").ToString(),
            //      purchaseId: Guid.NewGuid().ToString().Substring(0, 16),
            //      amount: 3,
            //      expirationPeriod: TimeSpan.FromMinutes(1),
            //      description: "Testing",
            //      entranceCode: Guid.NewGuid().ToString("n"),
            //      language: "nl",
            //      currency: "EUR");
            //Assert.IsNotNull(trxExpired);
            //Assert.IsFalse(String.IsNullOrWhiteSpace(trxExpired.IssuerAuthenticationUrl));
            //Assert.IsFalse(String.IsNullOrWhiteSpace(trxExpired.TransactionId));

            //// SendStatusRequest method
            //var statusExpired = (StatusResponse)client.SendStatusRequest(trxExpired.TransactionId);
            //Assert.IsNotNull(statusExpired);
            //Assert.AreEqual(LibsModel.EnumQueryStatus.Expired.ToString(), statusExpired.Status);
            //#endregion


            //#region Open [400 euro cents]
            //// SendTransactionRequest method
            //var trxOpen = (TransactionResponse)client.SendTransactionRequest(issuerCode: issuerCode,
            //      merchantReturnUrl: new Uri("http://www.google.co.th").ToString(),
            //      purchaseId: Guid.NewGuid().ToString().Substring(0, 16),
            //      amount: 4,
            //      expirationPeriod: TimeSpan.FromMinutes(1),
            //      description: "Testing",
            //      entranceCode: Guid.NewGuid().ToString("n"),
            //      language: "nl",
            //      currency: "EUR");
            //Assert.IsNotNull(trxOpen);
            //Assert.IsFalse(String.IsNullOrWhiteSpace(trxOpen.IssuerAuthenticationUrl));
            //Assert.IsFalse(String.IsNullOrWhiteSpace(trxOpen.TransactionId));

            //// SendStatusRequest method
            //var statusOpen = (StatusResponse)client.SendStatusRequest(trxOpen.TransactionId);
            //Assert.IsNotNull(statusOpen);
            //Assert.AreEqual(LibsModel.EnumQueryStatus.Open.ToString(), statusOpen.Status);
            //#endregion


            //#region Failure [500 euro cents]
            //// SendTransactionRequest method
            //var trxFailure = (TransactionResponse)client.SendTransactionRequest(issuerCode: issuerCode,
            //      merchantReturnUrl: new Uri("http://www.google.co.th").ToString(),
            //      purchaseId: Guid.NewGuid().ToString().Substring(0, 16),
            //      amount: 5,
            //      expirationPeriod: TimeSpan.FromMinutes(1),
            //      description: "Testing",
            //      entranceCode: Guid.NewGuid().ToString("n"),
            //      language: "nl",
            //      currency: "EUR");
            //Assert.IsNotNull(trxFailure);
            //Assert.IsFalse(String.IsNullOrWhiteSpace(trxFailure.IssuerAuthenticationUrl));
            //Assert.IsFalse(String.IsNullOrWhiteSpace(trxFailure.TransactionId));

            //// SendStatusRequest method
            //var statusFailure = (StatusResponse)client.SendStatusRequest(trxFailure.TransactionId);
            //Assert.IsNotNull(statusFailure);
            //Assert.AreEqual(LibsModel.EnumQueryStatus.Failure.ToString(), statusFailure.Status);
            //#endregion
        }

        [Test]
        public void iDealClient_Should_ThrowConfigurationErrorsException_When_ConfigurationIsWrong()
        {
            // MerchantId is empty or whitespace
            Assert.Throws<SYSConfig.ConfigurationErrorsException>(() => new iDealClient(string.Empty, _merchantSubId));
            Assert.Throws<SYSConfig.ConfigurationErrorsException>(() => new iDealClient("", _merchantSubId));

            // MerchantId's length greater than 9
            Assert.Throws<SYSConfig.ConfigurationErrorsException>(() => new iDealClient("1234567890", _merchantSubId));

            // MerchantSubId less than 0 or greater than 6
            Assert.Throws<SYSConfig.ConfigurationErrorsException>(() => new iDealClient(_merchantId, -1));
            Assert.Throws<SYSConfig.ConfigurationErrorsException>(() => new iDealClient(_merchantId, 7));

            // AcquirerUrl is empty or whitespace
            IConfigurationSectionHandler _configurationSectionHandler = (IConfigurationSectionHandler)SYSConfig.ConfigurationManager
                                                                                                                   .GetSection("iDealAcquiereUrl");
            Assert.Throws<SYSConfig.ConfigurationErrorsException>(() => new iDealClient(new DefaultConfiguration(_configurationSectionHandler,
                                                                                                null, _configurationSectionHandler.MerchantSubId)));

            // prepare parameter
            Mock<IConfiguration> moqConfiguration;
            X509Certificate2 tempCertificate = null;
            // AcceptantCertificate is null
            moqConfiguration = InitMockConfiguration(true);
            moqConfiguration.Setup(r => r.AcceptantCertificate).Returns(tempCertificate);
            Assert.Throws<SYSConfig.ConfigurationErrorsException>(() => new iDealClient(moqConfiguration.Object));

            // AcceptantCertificate has not a private key
            moqConfiguration = InitMockConfiguration(false);
            Assert.Throws<SYSConfig.ConfigurationErrorsException>(() => new iDealClient(moqConfiguration.Object));
        }

        [Test]
        public void HandleResponse_Should_ThrowInvalidDataException_When_ResponseXmlIsWrong()
        {
            string localCertificate = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"App_Data\bnd-ideal-test.pfx");
            X509Certificate2 certificate = new X509Certificate2(localCertificate, "123456");

            string responseXML = Properties.Resources.ErrorResponse;
            responseXML = responseXML.Replace("AcquirerErrorRes", "ErrorResult");

            iDealHttpResponseHandler handler = new iDealHttpResponseHandler();
            Assert.Throws<InvalidDataException>(() => handler.HandleResponse(responseXML, new SignatureProvider(certificate)));
        }

        private Mock<IConfiguration> InitMockConfiguration(bool HasPrivateKey)
        {
            string localAcceptant = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"App_Data\bnd-ideal-test.pfx");
            string localAcquirer = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"App_Data\bnd-ideal-test.cer");
            X509Certificate2 acceptantCertificate = new X509Certificate2(localAcceptant, "123456");
            X509Certificate2 acquirerCertificate = new X509Certificate2(localAcquirer);

            Mock<IConfiguration> moq = new Mock<IConfiguration>();
            moq.Setup(r => r.MerchantId).Returns(_merchantId);
            moq.Setup(r => r.MerchantSubId).Returns(_merchantSubId);
            moq.Setup(r => r.AcquirerUrl).Returns(Properties.Resources.ACQUIRER_URL);
            moq.Setup(r => r.AcceptantCertificate).Returns(HasPrivateKey ? acceptantCertificate : acquirerCertificate);

            return moq;
        }
        #endregion


        #region [Directory]
        [Test]
        public void SendDirectoryRequest_Should_ThrowiDealException_When_ParameterIsWrong()
        {
            // ### This method need to comment out for team city to build success,
            // ### and it need to update because it call http request and make unittest failed.
            //iDealClient client = new iDealClient("000000000", _merchantSubId);
            //var result = Assert.Throws<iDealException>(() => client.SendDirectoryRequest());

            //Assert.IsNotNull(result);
        }

        [Test]
        public void DirectoryRequest_Should_ThrowArgumentException_When_ParameterIsWrong()
        {
            // create DirectoryRequest
            _requestBase = new DirectoryRequest(_merchantId, null);
            Assert.IsNotNull(_requestBase);

            // case merchantId
            Assert.Throws<ArgumentNullException>(() => new DirectoryRequest(String.Empty, 0));
            Assert.Throws<ArgumentNullException>(() => new DirectoryRequest("  ", 0));
            Assert.Throws<ArgumentException>(() => new DirectoryRequest("0000000000", 0));

            // case merchantSubId
            Assert.Throws<ArgumentException>(() => new DirectoryRequest(_merchantId, -1));
            Assert.Throws<ArgumentException>(() => new DirectoryRequest(_merchantId, 7));
        }
        #endregion


        #region [Transaction]
        [Test]
        public void SendTransactionRequest_Should_ThrowiDealException_When_ParameterIsWrong()
        {
            // ### This method need to comment out for team city to build success,
            // ### and it need to update because it call http request and make unittest failed.
            //iDealClient client = new iDealClient(_merchantId, _merchantSubId);
            //Assert.Throws<iDealException>(() => client.SendTransactionRequest(issuerCode: "test",
            //    merchantReturnUrl: new Uri("http://www.google.co.th").ToString(),
            //    purchaseId: Guid.NewGuid().ToString().Substring(0, 16),
            //    amount: 1,
            //    expirationPeriod: TimeSpan.FromMinutes(1),
            //    description: "Testing",
            //    entranceCode: Guid.NewGuid().ToString("n"),
            //    language: "nl",
            //    currency: "EUR"));
        }

        [Test]
        public void TransactionRequest_Should_ThrowArgumentException_When_SetValueIsWrong()
        {
            // create TransactionRequest
            _requestBase = new TransactionRequest(merchantId: _merchantId,
                subId: null,
                issuerCode: "test",
                merchantReturnUrl: new Uri("http://www.google.co.th").ToString(),
                purchaseId: Guid.NewGuid().ToString().Substring(0, 16),
                amount: 1,
                expirationPeriod: null,
                description: "Testing",
                entranceCode: Guid.NewGuid().ToString("n"),
                language: "nl",
                currency: "EUR");
            Assert.IsNotNull(_requestBase);

            // case MerchantReturnUrl is empty
            Assert.Throws<ArgumentNullException>(() => ((TransactionRequest)_requestBase).MerchantReturnUrl = String.Empty);

            string tempurary = new string('x', 50);
            // case PurchaseId's length greater than 35 or empty
            Assert.Throws<ArgumentNullException>(() => ((TransactionRequest)_requestBase).PurchaseId = String.Empty);
            Assert.Throws<ArgumentException>(() => ((TransactionRequest)_requestBase).PurchaseId = tempurary);

            // case ExpirationPeriod less than 1 min or greater than 60 mins
            Assert.Throws<ArgumentException>(() => ((TransactionRequest)_requestBase).ExpirationPeriod = TimeSpan.FromMinutes(0));
            Assert.Throws<ArgumentException>(() => ((TransactionRequest)_requestBase).ExpirationPeriod = TimeSpan.FromMinutes(61));

            // case Description's length greater than 35
            Assert.Throws<ArgumentException>(() => ((TransactionRequest)_requestBase).Description = tempurary);

            // case EntranceCode's length greater than 40 or empty
            Assert.Throws<ArgumentNullException>(() => ((TransactionRequest)_requestBase).EntranceCode = String.Empty);
            Assert.Throws<ArgumentException>(() => ((TransactionRequest)_requestBase).EntranceCode = tempurary);
        }
        #endregion


        #region [Status]
        [Test]
        public void SendStatusRequest_Should_ThrowiDealException_When_TransasctionIsWrong()
        {
            // ### This method need to comment out for team city to build success,
            // ### and it need to update because it call http request and make unittest failed.
            //string temporary = new string('x', 16);
            //iDealClient client = new iDealClient(_merchantId, _merchantSubId);
            //Assert.IsNotNull(client.Configuration);
            //Assert.Throws<iDealException>(() => client.SendStatusRequest(temporary));


            //#region Syntax Error [700 euro cents]
            //// SendDirectoryRequest method
            //var resultDirectory = (DirectoryResponse)client.SendDirectoryRequest();
            //Assert.IsNotNull(resultDirectory);
            //Assert.IsNotNull(resultDirectory.AcquirerId);
            //Assert.IsNotNull(resultDirectory.Issuers);
            //Assert.Greater(((List<IssuerModel>)resultDirectory.Issuers).Count, 0);
            //Assert.IsNotNull(resultDirectory.DirectoryDateTimeStampLocalTime);

            //// Random selected issuer from resultDirectory
            //List<IssuerModel> issuerList = new List<IssuerModel>(resultDirectory.Issuers);
            //string issuerCode = issuerList[new Random().Next(0, issuerList.Count - 1)].IssuerID;

            //// SendTransactionRequest method
            //var trxError = (TransactionResponse)client.SendTransactionRequest(issuerCode: issuerCode,
            //      merchantReturnUrl: new Uri("http://www.google.co.th").ToString(),
            //      purchaseId: Guid.NewGuid().ToString().Substring(0, 16),
            //      amount: 7,
            //      expirationPeriod: TimeSpan.FromMinutes(1),
            //      description: "Testing",
            //      entranceCode: Guid.NewGuid().ToString("n"),
            //      language: "nl",
            //      currency: "EUR");
            //Assert.IsNotNull(trxError);
            //Assert.IsFalse(String.IsNullOrWhiteSpace(trxError.IssuerAuthenticationUrl));
            //Assert.IsFalse(String.IsNullOrWhiteSpace(trxError.TransactionId));

            //// SendStatusRequest method
            //Assert.Throws<iDealException>(() => client.SendStatusRequest(trxError.TransactionId));
            //#endregion
        }

        [Test]
        public void SendStatusRequest_Should_ReturnNull_When_ValueFromIdealIsNull()
        {
            string temporary = new string('x', 16);
            iDealClient client = new iDealClient(_merchantId, _merchantSubId);
            Mock<IiDealHttpRequest> mockClient = new Mock<IiDealHttpRequest>();
            StatusResponse response = null;
            mockClient.Setup(r => r.SendRequest(It.IsAny<StatusRequest>(), It.IsAny<ISignatureProvider>(), It.IsAny<string>(),
                                                It.IsAny<IiDealHttpResponseHandler>())).Returns(response);
            MockMethodReturn(client, "_iDealHttpRequest", mockClient.Object);
            var result = client.SendStatusRequest(temporary);
        }

        [Test]
        public void StatusRequest_Should_ThrowArgumentException_When_ParameterIsWrong()
        {
            string temporary = new string('x', 20);
            Assert.Throws<ArgumentException>(() => new StatusRequest(_merchantId, _merchantSubId, null));
            Assert.Throws<ArgumentException>(() => new StatusRequest(_merchantId, _merchantSubId, temporary));
            Assert.Throws<ArgumentException>(() => new StatusRequest(_merchantId, null, temporary));
        }

        [Test]
        public void StatusResponse_Should_ThrowNotSupportedException_When_StatusMandatoryIsNotExists()
        {
            string responseXml = Properties.Resources.StatusResponse;
            string startValue = "<status>";
            string endValue = "</status>";
            int startIndex = responseXml.LastIndexOf(startValue) + startValue.Length;
            int endIndex = responseXml.LastIndexOf(endValue);
            string response = string.Format("{0}{1}{2}", responseXml.Substring(0, startIndex), "Exception", responseXml.Substring(endIndex));

            Assert.Throws<NotSupportedException>(() => new StatusResponse(response));
        }

        private void MockMethodReturn(object target, string fieldName, object expectedValue)
        {
            Type targetType = target.GetType();
            FieldInfo myFieldInfo = targetType.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            myFieldInfo.SetValue(target, expectedValue);
        }
        #endregion
    }
}
