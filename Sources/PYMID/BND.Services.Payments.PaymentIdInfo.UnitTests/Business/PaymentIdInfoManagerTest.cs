using AutoMapper;
using BND.Services.Payments.PaymentIdInfo.Data.Interfaces;
using BND.Services.Payments.PaymentIdInfo.Entities;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BND.Services.Payments.PaymentIdInfo.Business.Test
{
    [TestFixture]
    public class PaymentIdInfoManagerTest
    {
        string VALID_IBAN = "NL37BNDA4818136651";

        IMapper _mapper;
        IiDealRepository _iDealRepository;

        List<iDealTransaction> idealTransactions = new List<iDealTransaction>
        {
            new iDealTransaction{},
            new iDealTransaction{},
            new iDealTransaction{}
        };

        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new PaymentIdInfoProfile());
            });
            _mapper = config.CreateMapper();

            Mock<IiDealRepository> idealRepository = new Mock<IiDealRepository>();
            idealRepository.Setup(x => x.GetByBndIban(It.IsAny<string>())).Returns(idealTransactions);
            idealRepository.Setup(x => x.GetBySourceIban(It.IsAny<string>())).Returns(idealTransactions);
            idealRepository.Setup(x => x.GetByTransactionId(It.IsAny<string>())).Returns(idealTransactions);

            _iDealRepository = idealRepository.Object;
        }

        #region GetFilterTypes
        [Test]
        public void GetFilterTypes_Should_ReturnAllFilterTypeFromEnumFilterTypes_When_Called()
        {
            var manager = new PaymentIdInfoManager(_mapper, _iDealRepository);
            var result = manager.GetFilterTypes();

            Assert.AreEqual(4, result.ToList().Count());
        }
        #endregion

        #region GetPaymentIdByBndIban
        [Test]
        public void GetPaymentIdByBndIban_Should_ReturnAllData_When_CalledWithUnknownFilterTypes()
        {
            var manager = new PaymentIdInfoManager(_mapper, _iDealRepository);

            var result = manager.GetPaymentIdByBndIban(VALID_IBAN, new List<EnumFilterType>() { EnumFilterType.Unknown });
            Assert.NotNull(result);
            Assert.AreEqual(
                idealTransactions.Count(),
                result.Count());
        }

        [Test]
        public void GetPaymentIdByBndIban_Should_ReturnAllData_When_CalledWithIdeal()
        {
            var manager = new PaymentIdInfoManager(_mapper, _iDealRepository);

            var result = manager.GetPaymentIdByBndIban(VALID_IBAN, new List<EnumFilterType>() { EnumFilterType.iDeal } );
            Assert.NotNull(result);
            Assert.AreEqual(
                idealTransactions.Count(),
                result.Count());
        }

        [Test]
        public void GetPaymentIdByBndIban_Should_ThrowArgumentNullException_When_bnaIbanIsMissing()
        {
            var manager = new PaymentIdInfoManager(_mapper, _iDealRepository);
            Assert.Throws<ArgumentNullException>(() =>
            {
                manager.GetPaymentIdByBndIban(String.Empty, new List<EnumFilterType>() { EnumFilterType.Unknown });
            });
        }

        [Test]
        public void GetPaymentIdByBndIban_Should_ThrowArgumentException_When_bnaIbanIsInvalid()
        {
            var manager = new PaymentIdInfoManager(_mapper, _iDealRepository);
            Assert.Throws<ArgumentException>(() =>
            {
                manager.GetPaymentIdByBndIban("NL37BNDA4818136650", new List<EnumFilterType>() { EnumFilterType.Unknown });
            });
        }
        #endregion

        #region GetPaymentIdBySourceIban
        [Test]
        public void GetPaymentIdBySourceIban_Should_ReturnAllData_When_CalledWithUnknownFilterTypes()
        {
            var manager = new PaymentIdInfoManager(_mapper, _iDealRepository);

            var result = manager.GetPaymentIdBySourceIban(VALID_IBAN, new List<EnumFilterType>() { EnumFilterType.Unknown });
            Assert.NotNull(result);
            Assert.AreEqual(
                idealTransactions.Count(),
                result.Count());
        }

        [Test]
        public void GetPaymentIdBySourceIban_Should_ReturnAllData_When_CalledWithIdeal()
        {
            var manager = new PaymentIdInfoManager(_mapper, _iDealRepository);

            var result = manager.GetPaymentIdBySourceIban(VALID_IBAN, new List<EnumFilterType>() { EnumFilterType.iDeal });
            Assert.NotNull(result);
            Assert.AreEqual(
                idealTransactions.Count(),
                result.Count());
        }

        [Test]
        public void GetPaymentIdBySourceIban_Should_ThrowArgumentNullException_When_bnaIbanIsMissing()
        {
            var manager = new PaymentIdInfoManager(_mapper, _iDealRepository);
            Assert.Throws<ArgumentNullException>(() =>
            {
                manager.GetPaymentIdBySourceIban(String.Empty, new List<EnumFilterType>() { EnumFilterType.Unknown });
            });
        }

        [Test]
        public void GetPaymentIdBySourceIban_Should_ThrowArgumentException_When_bnaIbanIsInvalid()
        {
            var manager = new PaymentIdInfoManager(_mapper, _iDealRepository);
            Assert.Throws<ArgumentException>(() =>
            {
                manager.GetPaymentIdBySourceIban("NL37BNDA4818136650", new List<EnumFilterType>() { EnumFilterType.Unknown });
            });
        }
        #endregion

        #region GetPaymentIdByTransaction
        [Test]
        public void GetPaymentIdByTransaction_Should_ReturnAllData_When_CalledWithUnknownFilterTypes()
        {
            var manager = new PaymentIdInfoManager(_mapper, _iDealRepository);

            var result = manager.GetPaymentIdByTransaction("TRX123456", new List<EnumFilterType>() { EnumFilterType.Unknown });
            Assert.NotNull(result);
            Assert.AreEqual(
                idealTransactions.Count(),
                result.Count());
        }

        [Test]
        public void GetPaymentIdByTransaction_Should_ReturnAllData_When_CalledWithIdeal()
        {
            var manager = new PaymentIdInfoManager(_mapper, _iDealRepository);

            var result = manager.GetPaymentIdByTransaction("TRX123456", new List<EnumFilterType>() { EnumFilterType.iDeal });
            Assert.NotNull(result);
            Assert.AreEqual(
                idealTransactions.Count(),
                result.Count());
        }

        [Test]
        public void GetPaymentIdByTransaction_Should_ThrowArgumentNullException_When_TransactionIdIsMissing()
        {
            var manager = new PaymentIdInfoManager(_mapper, _iDealRepository);
            Assert.Throws<ArgumentNullException>(() =>
            {
                manager.GetPaymentIdByTransaction(String.Empty, new List<EnumFilterType>() { EnumFilterType.Unknown });
            });
        }
        #endregion
    }

    public class PaymentIdInfoManagerMock : PaymentIdInfoManager
    {
        public PaymentIdInfoManagerMock(IMapper mapper, IiDealRepository idealRepository)
            : base(mapper, idealRepository)
        {

        }

    }
}
