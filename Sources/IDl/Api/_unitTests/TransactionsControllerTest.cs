using BND.Services.Payments.iDeal.Api.Controllers;
using BND.Services.Payments.iDeal.Interfaces;
using BND.Services.Payments.iDeal.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Web.Http.Results;
using ILogger = BND.Services.Payments.iDeal.Interfaces.ILogger;

namespace BND.Services.Payments.iDeal.Api.Tests
{
    [TestFixture]
    public class TransactionsControllerTest
    {
        public Mock<IiDealService> idealMock = new Mock<IiDealService>();
        public Mock<ILogger> loggerMock = new Mock<ILogger>();

        TransactionRequestModel transactionRequestModel = new TransactionRequestModel()
        {
            PurchaseID = "purchaseID",
            Amount = 10,
            BNDIBAN = "bndIBAN",
            Currency = "xxx",
            CustomerIBAN = "customerIBAN",
            ExpirationPeriod = 3600,
            IssuerID = "issuerID",
            Language = "xx",
            MerchantReturnURL = "https://testURLPath",
            PaymentType = "iDeal",
            Description = "description"
        };

        TransactionResponseModel transactionalResponseModel = new TransactionResponseModel()
        {
            EntranceCode = "entranceCode",
            IssuerAuthenticationURL = new Uri("https://issuerAuthen"),
            PurchaseID = "purchaseID",
            TransactionID = "transactionID"
        };

        [Test]
        public void CreateTransaction_Should_ReturnTrue_When_TransactionalResponseModelIsNotNull()
        {
            idealMock.Setup(service => service.CreateTransaction(transactionRequestModel)).Returns(transactionalResponseModel);

            var controller = new TransactionsController(idealMock.Object, loggerMock.Object);

            var result = controller.Post(transactionRequestModel);

            var actualModel = result as OkNegotiatedContentResult<TransactionResponseModel>;

            Assert.IsNotNull(actualModel);
            Assert.IsTrue(transactionalResponseModel.Equals(actualModel.Content));
        }

        [Test]
        public void CreateTransaction_Should_ReturnTransactionResponseModel_When_SuccessToGetResponse()
        {
            idealMock.Setup(service => service.CreateTransaction(transactionRequestModel)).Returns(transactionalResponseModel);

            var controller = new TransactionsController(idealMock.Object, loggerMock.Object);

            var result = controller.Post(transactionRequestModel);

            var actualModel = result as OkNegotiatedContentResult<TransactionResponseModel>;

            Assert.IsNotNull(actualModel);
            Assert.AreEqual(transactionRequestModel.PurchaseID, actualModel.Content.PurchaseID);
            Assert.AreEqual(transactionalResponseModel.EntranceCode, actualModel.Content.EntranceCode);
            Assert.AreEqual(transactionalResponseModel.IssuerAuthenticationURL, actualModel.Content.IssuerAuthenticationURL);
            Assert.AreEqual(transactionalResponseModel.TransactionID, actualModel.Content.TransactionID);
        }

        [Test]
        public void CreateTransaction_Should_ReturnNull_When_TransactionaResponseModelIsNull()
        {
            idealMock.Setup(service => service.CreateTransaction(transactionRequestModel)).Returns(() => null);

            var controller = new TransactionsController(idealMock.Object, loggerMock.Object);

            var result = controller.Post(transactionRequestModel);

            var actualModel = result as OkNegotiatedContentResult<TransactionResponseModel>;

            Assert.IsNull(actualModel.Content);
        }

        [Test]
        public void GetStatus_Should_ReturnSuccess_When_TransactionStatusIsSuccess()
        {
            idealMock.Setup(
                service =>
                    service.GetStatus(transactionalResponseModel.EntranceCode, transactionalResponseModel.TransactionID))
                .Returns(EnumQueryStatus.Success);

            var controller = new TransactionsController(idealMock.Object, loggerMock.Object);

            var result = controller.GetStatus(transactionalResponseModel.EntranceCode,
                transactionalResponseModel.TransactionID);

            var actualModel = result as OkNegotiatedContentResult<string>;

            Assert.AreEqual("Success", actualModel.Content);
            Assert.AreSame(EnumQueryStatus.Success.ToString(), actualModel.Content);
        }

        [Test]
        public void GetStatus_Should_ReturnOpen_When_TransactionStatusIsOpen()
        {
            idealMock.Setup(
                service =>
                    service.GetStatus(transactionalResponseModel.EntranceCode, transactionalResponseModel.TransactionID))
                .Returns(EnumQueryStatus.Open);

            var controller = new TransactionsController(idealMock.Object, loggerMock.Object);

            var result = controller.GetStatus(transactionalResponseModel.EntranceCode,
                transactionalResponseModel.TransactionID);

            var actualModel = result as OkNegotiatedContentResult<string>;

            Assert.AreEqual("Open", actualModel.Content);
            Assert.AreSame(EnumQueryStatus.Open.ToString(), actualModel.Content);
        }

        [Test]
        public void GetStatus_Should_ReturnFailed_When_TransactionStatusIsFailure()
        {
            idealMock.Setup(
                service =>
                    service.GetStatus(transactionalResponseModel.EntranceCode, transactionalResponseModel.TransactionID))
                .Returns(EnumQueryStatus.Failure);

            var controller = new TransactionsController(idealMock.Object, loggerMock.Object);

            var result = controller.GetStatus(transactionalResponseModel.EntranceCode,
                transactionalResponseModel.TransactionID);

            var actualModel = result as OkNegotiatedContentResult<string>;

            Assert.AreEqual("Failure", actualModel.Content);
            Assert.AreSame(EnumQueryStatus.Failure.ToString(), actualModel.Content);
        }

        [Test]
        public void GetStatus_Should_ReturnCancelled_When_TransactionStatusIsCancelled()
        {
            idealMock.Setup(
                service =>
                    service.GetStatus(transactionalResponseModel.EntranceCode, transactionalResponseModel.TransactionID))
                .Returns(EnumQueryStatus.Cancelled);

            var controller = new TransactionsController(idealMock.Object, loggerMock.Object);

            var result = controller.GetStatus(transactionalResponseModel.EntranceCode,
                transactionalResponseModel.TransactionID);

            var actualModel = result as OkNegotiatedContentResult<string>;

            Assert.AreEqual("Cancelled", actualModel.Content);
            Assert.AreSame(EnumQueryStatus.Cancelled.ToString(), actualModel.Content);
        }

        [Test]
        public void GetStatus_Should_ReturnExpired_When_TransactionStatusIsExpired()
        {
            idealMock.Setup(
                service =>
                    service.GetStatus(transactionalResponseModel.EntranceCode, transactionalResponseModel.TransactionID))
                .Returns(EnumQueryStatus.Expired);

            var controller = new TransactionsController(idealMock.Object, loggerMock.Object);

            var result = controller.GetStatus(transactionalResponseModel.EntranceCode,
                transactionalResponseModel.TransactionID);

            var actualModel = result as OkNegotiatedContentResult<string>;

            Assert.AreEqual("Expired", actualModel.Content);
            Assert.AreSame(EnumQueryStatus.Expired.ToString(), actualModel.Content);
        }
    }
}