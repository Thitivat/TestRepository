using System;
using BND.Services.Matrix.Entities;
using BND.Services.Matrix.Proxy.NET4.Interfaces;
using NUnit.Framework;
using BND.Services.Payments.iDeal.Booking;
using BND.Services.Payments.iDeal.Dal;
using BND.Services.Payments.iDeal.Dal.Enums;
using BND.Services.Payments.iDeal.Dal.Pocos;
using BND.Services.Payments.iDeal.Interfaces;
using BND.Services.Payments.iDeal.Models;
using Moq;
using BND.Services.Payments.iDeal.Dal.Pocos;

namespace BND.Service.Payments.iDeal.Booking.Tests
{
    [TestFixture]
    public class BookingManagerTest
    {
        //[Test]
        //public void TestMethod1()
        //{
        //    IBookingManager manager = new BookingManager("http://brandnewday.mooo.com:8032/");
        //    int result = manager.BookToMatrix("NL37BNDA4818136652", "123", "123456", 50);

        //}

        // Validation

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("     ")]
        public void BookToMatrix_should_throw_validation_exception_access_token(string accessToken)
        {
            IBookingManager manager = new BookingManager(GetTransactionRepositoryMock(), GetAccountsApiMock(), GetLoggerMock());
            BookingModel bm = new BookingModel();
            Assert.Throws<ArgumentException>(() => manager.BookToMatrix(bm, accessToken, "transId", "entranceCode"));
        }

        private ITransactionRepository GetTransactionRepositoryMock(p_Transaction expectedTransaction = null)
        {
            Mock<ITransactionRepository> trMock = new Mock<ITransactionRepository>();

            trMock.Setup(s => s.GetTransactionWithLatestStatus(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(expectedTransaction);

            return trMock.Object;
        }

        private IAccountsApi GetAccountsApiMock()
        {
            Mock<IAccountsApi> apiMock = new Mock<IAccountsApi>();

            return apiMock.Object;
        }

        private ILogger GetLoggerMock()
        {
            Mock<ILogger> logger = new Mock<ILogger>();

            return logger.Object;
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void BookToMatrix_should_throw_validation_exception_bookingModel_BookFromIban(string bookFromIban)
        {
            IBookingManager manager = new BookingManager(GetTransactionRepositoryMock(), GetAccountsApiMock(), GetLoggerMock());
            BookingModel bm = GetBookingModelCorrect();

            bm.BookFromIban = bookFromIban;

            Assert.Throws<ArgumentException>(() => manager.BookToMatrix(bm, "someaccesstoken", "transId", "entranceCode"));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void BookToMatrix_should_throw_validation_exception_bookingModel_BookToIban(string bookToIban)
        {
            IBookingManager manager = new BookingManager(GetTransactionRepositoryMock(), GetAccountsApiMock(), GetLoggerMock());
            BookingModel bm = GetBookingModelCorrect();

            bm.BookToIban = bookToIban;

            Assert.Throws<ArgumentException>(() => manager.BookToMatrix(bm, "someaccesstoken", "transId", "entranceCode"));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void BookToMatrix_should_throw_validation_exception_bookingModel_bookToBicn(string bookToBic)
        {
            IBookingManager manager = new BookingManager(GetTransactionRepositoryMock(), GetAccountsApiMock(), GetLoggerMock());
            BookingModel bm = GetBookingModelCorrect();

            bm.BookToBic = bookToBic;

            Assert.Throws<ArgumentException>(() => manager.BookToMatrix(bm, "someaccesstoken", "transId", "entranceCode"));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void BookToMatrix_should_throw_validation_exception_bookingModel_transactionId(string transactionId)
        {
            IBookingManager manager = new BookingManager(GetTransactionRepositoryMock(), GetAccountsApiMock(), GetLoggerMock());
            BookingModel bm = GetBookingModelCorrect();


            Assert.Throws<ArgumentException>(() => manager.BookToMatrix(bm, "someaccesstoken", transactionId, "entranceCode"));
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void BookToMatrix_should_throw_validation_exception_amount(decimal amount)
        {
            IBookingManager manager = new BookingManager(GetTransactionRepositoryMock(), GetAccountsApiMock(), GetLoggerMock());
            BookingModel bm = GetBookingModelCorrect();

            bm.Amount = amount;

            Assert.Throws<ArgumentException>(() => manager.BookToMatrix(bm, "someaccesstoken", "sth", "entranceCode"));
        }

        [TestCase]
        public void BookToMatrix_should_throw_validation_exception_bookingModel_bookDate()
        {
            IBookingManager manager = new BookingManager(GetTransactionRepositoryMock(), GetAccountsApiMock(), GetLoggerMock());
            BookingModel bm = GetBookingModelCorrect();

            bm.BookDate = default(DateTime);

            Assert.Throws<ArgumentException>(() => manager.BookToMatrix(bm, "someaccesstoken", "transId", "entranceCode"));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void BookToMatrix_should_throw_validation_exception_transactionId(string transactionId)
        {
            IBookingManager manager = new BookingManager(GetTransactionRepositoryMock(), GetAccountsApiMock(), GetLoggerMock());
            BookingModel bm = GetBookingModelCorrect();

            bm.BookDate = default(DateTime);

            Assert.Throws<ArgumentException>(() => manager.BookToMatrix(bm, "someaccesstoken", transactionId, "sth"));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void BookToMatrix_should_throw_validation_exception_entranceCode(string entranceCode)
        {
            IBookingManager manager = new BookingManager(GetTransactionRepositoryMock(), GetAccountsApiMock(), GetLoggerMock());
            BookingModel bm = GetBookingModelCorrect();

            bm.BookDate = default(DateTime);

            Assert.Throws<ArgumentException>(() => manager.BookToMatrix(bm, "someaccesstoken", "sth", entranceCode));
        }

        [Test]
        public void BookToMatrix_should_throw_validation_exception_creditor()
        {
            IBookingManager manager = new BookingManager(GetTransactionRepositoryMock(), GetAccountsApiMock(), GetLoggerMock());
            BookingModel bm = GetBookingModelCorrect();

            bm.Creditor = null;

            Assert.Throws<ArgumentException>(() => manager.BookToMatrix(bm, "someaccesstoken", "sth", "entranceCode"));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void BookToMatrix_should_throw_validation_exception_creditor_customerName(string customerName)
        {
            IBookingManager manager = new BookingManager(GetTransactionRepositoryMock(), GetAccountsApiMock(), GetLoggerMock());
            BookingModel bm = GetBookingModelCorrect();

            bm.Creditor.CustomerName = customerName;

            Assert.Throws<ArgumentException>(() => manager.BookToMatrix(bm, "someaccesstoken", "sth", "entranceCode"));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void BookToMatrix_should_throw_validation_exception_creditor_countrycode(string countryCode)
        {
            IBookingManager manager = new BookingManager(GetTransactionRepositoryMock(), GetAccountsApiMock(), GetLoggerMock());
            BookingModel bm = GetBookingModelCorrect();

            bm.Creditor.CountryCode = countryCode;

            Assert.Throws<ArgumentException>(() => manager.BookToMatrix(bm, "someaccesstoken", "sth", "entranceCode"));
        }

        [Test]
        public void BookToMatrix_should_throw_validation_exception_debtor()
        {
            IBookingManager manager = new BookingManager(GetTransactionRepositoryMock(), GetAccountsApiMock(), GetLoggerMock());
            BookingModel bm = GetBookingModelCorrect();

            bm.Debtor = null;

            Assert.Throws<ArgumentException>(() => manager.BookToMatrix(bm, "someaccesstoken", "sth", "entranceCode"));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void BookToMatrix_should_throw_validation_exception_debtor_customerName(string customerName)
        {
            IBookingManager manager = new BookingManager(GetTransactionRepositoryMock(), GetAccountsApiMock(), GetLoggerMock());
            BookingModel bm = GetBookingModelCorrect();

            bm.Debtor.CustomerName = customerName;

            Assert.Throws<ArgumentException>(() => manager.BookToMatrix(bm, "someaccesstoken", "sth", "entranceCode"));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void BookToMatrix_should_throw_validation_exception_debtor_countrycode(string countryCode)
        {
            IBookingManager manager = new BookingManager(GetTransactionRepositoryMock(), GetAccountsApiMock(), GetLoggerMock());
            BookingModel bm = GetBookingModelCorrect();

            bm.Debtor.CountryCode = countryCode;

            Assert.Throws<ArgumentException>(() => manager.BookToMatrix(bm, "someaccesstoken", "sth", "entranceCode"));
        }

        // Transaction - cannot find
        [TestCase]
        public void BookToMatrix_should_throw_exception_transaction_notfound()
        {
            Mock<ITransactionRepository> trMock = new Mock<ITransactionRepository>();
            p_Transaction transaction = null;
            trMock.Setup(a => a.GetTransactionWithLatestStatus(It.IsAny<string>(), It.IsAny<string>())).Returns(transaction);
            IBookingManager manager = new BookingManager(trMock.Object, GetAccountsApiMock(), GetLoggerMock());

            BookingModel bm = GetBookingModelCorrect();

            Assert.Throws<NullReferenceException>(() => manager.BookToMatrix(bm, "someaccesstoken", "sth", "someCode"));
        }

        [TestCase]
        public void BookToMatrix_return_movementId_when_booked()
        {
            Mock<ITransactionRepository> trMock = new Mock<ITransactionRepository>();
            p_Transaction transaction = new p_Transaction()
            {
                BookingStatus = EnumBookingStatus.Booked.ToString(),
                MovementId = 999
            };
            trMock.Setup(a => a.GetTransactionWithLatestStatus(It.IsAny<string>(), It.IsAny<string>())).Returns(transaction);
            IBookingManager manager = new BookingManager(trMock.Object, GetAccountsApiMock(), GetLoggerMock());

            BookingModel bm = GetBookingModelCorrect();

            int result = manager.BookToMatrix(bm, "someaccesstoken", "sth", "someCode");
            Assert.AreEqual(transaction.MovementId, result);
        }

        // transaction not booked - AccountsApi.CreateIncomingPayment called (api throw exception)
        [Test]
        public void BookToMatrix_API_throw_exception_and_stats_should_change_back_to()
        {
            Mock<ITransactionRepository> trMock = new Mock<ITransactionRepository>();
            p_Transaction transaction = new p_Transaction()
            {
                BookingStatus = EnumBookingStatus.NotBooked.ToString(),
                MovementId = 999
            };
            trMock.Setup(a => a.GetTransactionWithLatestStatus(It.IsAny<string>(), It.IsAny<string>())).Returns(transaction);

            Mock<IAccountsApi> accountsApiMock = new Mock<IAccountsApi>();
            accountsApiMock.Setup(
                x => x.CreateIncomingPayment(It.IsAny<string>(), It.IsAny<Payment>(), It.IsAny<string>()))
                .Throws(new Exception("ass"));

            IBookingManager manager = new BookingManager(trMock.Object, accountsApiMock.Object, GetLoggerMock());

            BookingModel bm = GetBookingModelCorrect();

            // Make sure it will throw exception to outside
            Assert.Throws<Exception>(() => manager.BookToMatrix(bm, "someaccesstoken", "sth", "someCode"));
            // Make sure the statys wil lbe changed back to NotBooked when booking in matrix failed
            trMock.Verify(a => a.UpdateBookingStatus(It.IsAny<string>(), It.IsAny<string>(), EnumBookingStatus.NotBooked),Times.Once);
        }

        // transaction not booked - AccountsApi.CreateIncomingPayment called and TransactionRepository.UpdateBookingData called (db throws exception)
        [Test]
        public void BookToMatrix_API_throw_exception_when_db_update_failed()
        {
            Mock<ITransactionRepository> trMock = new Mock<ITransactionRepository>();
            p_Transaction transaction = new p_Transaction()
            {
                BookingStatus = EnumBookingStatus.NotBooked.ToString(),
                MovementId = 999
            };
            trMock.Setup(a => a.GetTransactionWithLatestStatus(It.IsAny<string>(), It.IsAny<string>())).Returns(transaction);
            trMock.Setup(a => a.UpdateBookingData(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<EnumBookingStatus>(), It.IsAny<DateTime>()))
                .Throws(new Exception("ass"));

            IBookingManager manager = new BookingManager(trMock.Object, GetAccountsApiMock(), GetLoggerMock());

            BookingModel bm = GetBookingModelCorrect();

            Assert.Throws<Exception>(() => manager.BookToMatrix(bm, "someaccesstoken", "sth", "someCode"));
        }

        private BookingModel GetBookingModelCorrect()
        {
            BookingModel bm = new BookingModel()
            {
                BookFromIban = "NL1234567890",
                BookToIban = "NL1234567890",
                BookToBic = "ASDFGH",
                Amount = 1,
                BookDate = DateTime.Now,
                Debtor = new Contract()
                {
                    CustomerName = "lol",
                    CountryCode = "NL"
                },
                Creditor = new Contract()
                {
                    CustomerName = "lol2",
                    CountryCode = "NL"
                }
            };

            return bm;
        }



    }
}
