using BND.Services.Payments.PaymentIdInfo.Api.Controllers;
using BND.Services.Payments.PaymentIdInfo.Bll;
using BND.Services.Payments.PaymentIdInfo.Libs.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Web.Http.Results;

namespace BND.Services.Payments.PaymentIdInfo.Api.Test
{
    [TestFixture]
    public class PaymentIdInfosControllerTest
    {
        private Mock<IPaymentIdInfoManager> _paymentIdInfoManager;
        private List<EnumFilterType> _enumFilterTypeList;
        private List<PaymentIdInfoModel> _expectedResponse;
        private string _filterType;
        private string _bndIban;
        private string _sourceIban;
        private string _transactionId;

        [SetUp]
        public void Initialize()
        {
            _paymentIdInfoManager = new Mock<IPaymentIdInfoManager>();
            _enumFilterTypeList = new List<EnumFilterType>() { EnumFilterType.ideal };

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
        public void GetFilterTypes_Should_Return_ListFilterTypes()
        {
            _paymentIdInfoManager.Setup(service => service.GetFilterTypes()).Returns(_enumFilterTypeList);
            var controller = new PaymentIdInfosController(_paymentIdInfoManager.Object);
            var result = controller.GetFilterTypes();
            var actual = result as OkNegotiatedContentResult<IEnumerable<EnumFilterType>>;

            Assert.IsNotNull(actual);
            Assert.IsTrue(_enumFilterTypeList.Equals(actual.Content));
        }

        [Test]
        public void GetFilterTypes_Should_Return_Empty_When_ListEmpty()
        {
            _paymentIdInfoManager.Setup(service => service.GetFilterTypes()).Returns(new List<EnumFilterType>());
            var controller = new PaymentIdInfosController(_paymentIdInfoManager.Object);
            var result = controller.GetFilterTypes();
            var actual = result as OkNegotiatedContentResult<IEnumerable<EnumFilterType>>;

            Assert.IsEmpty(actual.Content);
        }

        [Test]
        public void GetPaymentInfoByBndIban_Should_Return_ListOfPaymentIdInfoModel()
        {
            _paymentIdInfoManager.Setup(service => service.GetPaymentIdByBndIban(_bndIban, String.Empty)).Returns(_expectedResponse);
            var controller = new PaymentIdInfosController(_paymentIdInfoManager.Object);
            var result = controller.GetPaymentInfoByBndIban(_bndIban, String.Empty);
            var actual = result as OkNegotiatedContentResult<IEnumerable<PaymentIdInfoModel>>;

            Assert.IsNotNull(actual);
            Assert.IsTrue(_expectedResponse.Equals(actual.Content));
        }

        [Test]
        public void GetPaymentInfoBySourceIban_Should_Return_ListOfPaymentIdInfoModel()
        {
            _paymentIdInfoManager.Setup(service => service.GetPaymentIdBySourceIban(_sourceIban, String.Empty)).Returns(_expectedResponse);
            var controller = new PaymentIdInfosController(_paymentIdInfoManager.Object);
            var result = controller.GetPaymentInfoBySourceIban(_sourceIban, String.Empty);
            var actual = result as OkNegotiatedContentResult<IEnumerable<PaymentIdInfoModel>>;

            Assert.IsNotNull(actual);
            Assert.IsTrue(_expectedResponse.Equals(actual.Content));
        }

        [Test]
        public void GetPaymentInfoByTransaction_Should_Return_ListOfPaymentIdInfoModel()
        {
            _paymentIdInfoManager.Setup(service => service.GetPaymentIdByTransaction(_transactionId, String.Empty)).Returns(_expectedResponse);
            var controller = new PaymentIdInfosController(_paymentIdInfoManager.Object);
            var result = controller.GetPaymentInfoByTransaction(_transactionId, String.Empty);
            var actual = result as OkNegotiatedContentResult<IEnumerable<PaymentIdInfoModel>>;

            Assert.IsNotNull(actual);
            Assert.IsTrue(_expectedResponse.Equals(actual.Content));
        }
    }
}
