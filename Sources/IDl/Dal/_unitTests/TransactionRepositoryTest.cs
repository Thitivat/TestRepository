using BND.Services.Payments.iDeal.Dal.Pocos;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Linq;

namespace BND.Services.Payments.iDeal.Dal.Tests
{
    [TestFixture]
    public class TransactionRepositoryTest
    {
        private IUnitOfWork _unitOfWork;

        [SetUp]
        public void SetUp()
        {
            _unitOfWork = new MockUnitOfWork(new MockDbContext(), false);
        }

        [Test]
        public void Insert_Should_HasDataEqualInput_When_InputData()
        {
            ITransactionRepository repository = new TransactionRepository(_unitOfWork);

            p_Transaction expectedTransaction = new p_Transaction
            {
                TransactionID = "0000000000000001",
                AcquirerID = "0001",
                MerchantID = "000000001",
                SubID = 000001,
                IssuerID = "00000000001",
                IssuerAuthenticationURL = "http://www.google.com",
                MerchantReturnURL = "http://www.bing.com",
                EntranceCode = "ENTRANCE_CODE",
                PurchaseID = "PURCHASE_ID",
                Amount = 1000,
                Currency = "THB",
                Language = "TH",
                Description = "DESC",
                ConsumerName = "CONSUMER_NAME",
                ConsumerIBAN = "BND123456",
                ConsumerBIC = "BIC123456",
                TransactionRequestDateTimestamp = DateTime.Now,
                TransactionResponseDateTimestamp = DateTime.Now,
                TransactionCreateDateTimestamp = DateTime.Now,
                BNDIBAN = "BND999999",
                PaymentType = "iDeal",
                ExpirationSecondPeriod = 30,
                ExpectedCustomerIBAN = "BND000000",
            };

            int affectedRowCount = repository.Insert(expectedTransaction);

            p_Transaction insertedTransaction = _unitOfWork.GetRepository<p_Transaction>().GetById(expectedTransaction.TransactionID);

            Assert.AreEqual(1, affectedRowCount);
            var expected = JsonConvert.SerializeObject(expectedTransaction);
            var actual = JsonConvert.SerializeObject(insertedTransaction);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetTransactionWithLatestStatus_Should_ReturnWithTheHistoriesWithMaxResponseDateTimeStampAndAttempsEqual3_When_TransactionHas3TodayStatusHistories()
        {
            ITransactionRepository repository = new TransactionRepository(_unitOfWork);

            string transactionId = "0000000000000001";
            string entranceCode = "5dbc50f77d6344db944b0b481af9d8a2";
            decimal amount = 1000;
            DateTime transactionCreateDateTimestamp = DateTime.Now.AddMinutes(10);
            int expirationPeriod = 30;
            bool isSystemFail = false;
            int todayAttempts = 3;
            DateTime? latestAttemptsDateTimestamp = DateTime.Now.AddMinutes(9);

            // insert the transaction
            p_Transaction transaction = new p_Transaction
            {
                TransactionID = transactionId,
                AcquirerID = "0001",
                MerchantID = "000000001",
                SubID = 1000001,
                IssuerID = "00000000001",
                IssuerAuthenticationURL = "http://www.google.com",
                MerchantReturnURL = "http://www.bing.com",
                EntranceCode = entranceCode,
                PurchaseID = "PURCHASE_ID",
                Amount = amount,
                Currency = "THB",
                Language = "TH",
                Description = "DESC",
                ConsumerName = "CONSUMER_NAME",
                ConsumerIBAN = "BND123456",
                ConsumerBIC = "BIC123456",
                TransactionRequestDateTimestamp = DateTime.Now,
                TransactionResponseDateTimestamp = DateTime.Now,
                TransactionCreateDateTimestamp = transactionCreateDateTimestamp,
                TodayAttempts = todayAttempts,
                LatestAttemptsDateTimestamp = latestAttemptsDateTimestamp,
                BNDIBAN = "BND999999",
                PaymentType = "iDeal",
                ExpirationSecondPeriod = expirationPeriod,
                ExpectedCustomerIBAN = "BND000000",
                IsSystemFail = false,
            };
            _unitOfWork.GetRepository<p_Transaction>().Insert(transaction);

            // insert the 3 histories for yesterday
            for (int i = 0; i < 3; i++)
            {
                p_TransactionStatusHistory transactionStatusHistory = new p_TransactionStatusHistory
                {
                    Status = "Status Yesterday" + i.ToString(),
                    TransactionID = transaction.TransactionID,
                    StatusDateTimeStamp = DateTime.Now.Date.AddDays(-1).AddMinutes(i),
                    StatusRequestDateTimeStamp = DateTime.Now.Date.AddDays(-1).AddMinutes(i),
                    StatusResponseDateTimeStamp = DateTime.Now.Date.AddDays(-1).AddMinutes(i),
                };
                _unitOfWork.GetRepository<p_TransactionStatusHistory>().Insert(transactionStatusHistory);
            }
            // insert the 2 histories for today
            for (int i = 0; i < 2; i++)
            {
                p_TransactionStatusHistory transactionStatusHistory = new p_TransactionStatusHistory
                {
                    Status = "Status Yesterday" + i.ToString(),
                    TransactionID = transaction.TransactionID,
                    StatusDateTimeStamp = DateTime.Now.Date.AddMinutes(1),
                    StatusRequestDateTimeStamp = DateTime.Now.Date.AddMinutes(1),
                    StatusResponseDateTimeStamp = DateTime.Now.Date.AddMinutes(1),
                };
                _unitOfWork.GetRepository<p_TransactionStatusHistory>().Insert(transactionStatusHistory);
            }

            DateTime latsetDateTimeStamp = DateTime.Now.Date.AddMinutes(10);
            // insert the latest history for today, so today have total 3 status histories
            p_TransactionStatusHistory latestTransactionStatusHistory = new p_TransactionStatusHistory
            {
                Status = "Latest Status For Today",
                TransactionID = transaction.TransactionID,
                StatusDateTimeStamp = latsetDateTimeStamp,
                StatusRequestDateTimeStamp = latsetDateTimeStamp,
                StatusResponseDateTimeStamp = latsetDateTimeStamp,
            };
            _unitOfWork.GetRepository<p_TransactionStatusHistory>().Insert(latestTransactionStatusHistory);
            _unitOfWork.CommitChanges();

            p_Transaction transactionResult = repository.GetTransactionWithLatestStatus(transactionId, entranceCode);

            Assert.IsNotNull(transactionResult);
            Assert.AreEqual(transactionId, transactionResult.TransactionID);
            Assert.AreEqual(amount, transactionResult.Amount);
            Assert.AreEqual(transactionCreateDateTimestamp, transactionResult.TransactionCreateDateTimestamp);
            Assert.AreEqual(expirationPeriod, transactionResult.ExpirationSecondPeriod);
            Assert.AreEqual(isSystemFail, transactionResult.IsSystemFail);
            Assert.AreEqual(todayAttempts, transactionResult.TodayAttempts);
            Assert.AreEqual(latestAttemptsDateTimestamp, transactionResult.LatestAttemptsDateTimestamp);

            Assert.AreEqual(transactionId, transactionResult.TransactionID);
            Assert.AreEqual(3, transactionResult.TodayAttempts);
            Assert.NotNull(transactionResult.TransactionStatusHistories);
            Assert.AreEqual(1, transactionResult.TransactionStatusHistories.Count);
            Assert.AreEqual(latsetDateTimeStamp, transactionResult.TransactionStatusHistories.First().StatusResponseDateTimeStamp);
        }

        [Test]
        public void GetTransactionWithLatestStatus_Should_ReturnNull_When_InputTransacionDoseNotExists()
        {
            string transactionId = "0000000000000001";
            string entranceCode = "5dbc50f77d6344db944b0b481af9d8a2";

            ITransactionRepository repository = new TransactionRepository(_unitOfWork);
            p_Transaction transactionResult = repository.GetTransactionWithLatestStatus(transactionId, entranceCode);

            Assert.IsNull(transactionResult);
        }

        [Test]
        public void GetTransactionWithLatestStatus_Should_ReturnTransactionWithEmptyStatusHistories_When_TransactionHasNoStatusHistory()
        {
            string transactionId = "0000000000000001";
            string entranceCode = "5dbc50f77d6344db944b0b481af9d8a2";
            decimal amount = 1000;
            DateTime transactionCreateDateTimestamp = DateTime.Now.AddMinutes(10);
            int expirationPeriod = 30;
            bool isSystemFail = false;
            int todayAttempts = 0;
            DateTime? latestAttemptsDateTimestamp = null;

            ITransactionRepository repository = new TransactionRepository(_unitOfWork);
            p_Transaction expectedTransaction = new p_Transaction
            {
                TransactionID = transactionId,
                AcquirerID = "0001",
                MerchantID = "000000001",
                SubID = 1000001,
                IssuerID = "00000000001",
                IssuerAuthenticationURL = "http://www.google.com",
                MerchantReturnURL = "http://www.bing.com",
                EntranceCode = entranceCode,
                PurchaseID = "PURCHASE_ID",
                Amount = amount,
                Currency = "THB",
                Language = "TH",
                Description = "DESC",
                ConsumerName = "CONSUMER_NAME",
                ConsumerIBAN = "BND123456",
                ConsumerBIC = "BIC123456",
                TransactionRequestDateTimestamp = DateTime.Now,
                TransactionResponseDateTimestamp = DateTime.Now,
                TransactionCreateDateTimestamp = transactionCreateDateTimestamp,
                TodayAttempts = todayAttempts,
                LatestAttemptsDateTimestamp = latestAttemptsDateTimestamp,
                BNDIBAN = "BND999999",
                PaymentType = "iDeal",
                ExpirationSecondPeriod = expirationPeriod,
                ExpectedCustomerIBAN = "BND000000",
                IsSystemFail = false,
                BookingDate = DateTime.Now,
                BookingGuid = Guid.NewGuid(),
                BookingStatus = "Booked",
                MovementId = 1
            };

            repository.Insert(expectedTransaction);

            p_Transaction transactionResult = repository.GetTransactionWithLatestStatus(transactionId, entranceCode);

            Assert.IsNotNull(transactionResult);
            Assert.AreEqual(transactionId, transactionResult.TransactionID);
            Assert.AreEqual(amount, transactionResult.Amount);
            Assert.AreEqual(transactionCreateDateTimestamp, transactionResult.TransactionCreateDateTimestamp);
            Assert.AreEqual(expirationPeriod, transactionResult.ExpirationSecondPeriod);
            Assert.AreEqual(isSystemFail, transactionResult.IsSystemFail);
            Assert.AreEqual(todayAttempts, transactionResult.TodayAttempts);
            Assert.AreEqual(latestAttemptsDateTimestamp, transactionResult.LatestAttemptsDateTimestamp);

            Assert.IsNotNull(transactionResult.TransactionStatusHistories);
            Assert.AreEqual(0, transactionResult.TransactionStatusHistories.Count);
        }

        [Test]
        public void UpdateStatus_Should_UpdateTransactionAndInsertTransactionStatusHistory_When_InputData()
        {
            string transactionId = "0030000041750523";
            string entranceCode = "5dbc50f77d6344db944b0b481af9d8a2";
            string newConsumerName = null;
            string newConsumerIban = null;
            string newConsumerBic = null;
            bool newIsSystemFail = true;

            ITransactionRepository repository = new TransactionRepository(_unitOfWork);

            // insert the transaction
            p_Transaction transaction = new p_Transaction
            {
                TransactionID = transactionId,
                AcquirerID = "0001",
                MerchantID = "000000001",
                SubID = 1000001,
                IssuerID = "00000000001",
                IssuerAuthenticationURL = "http://www.google.com",
                MerchantReturnURL = "http://www.bing.com",
                EntranceCode = entranceCode,
                PurchaseID = "PURCHASE_ID",
                Amount = 1000,
                Currency = "THB",
                Language = "TH",
                Description = "DESC",
                ConsumerName = "CONSUMER_NAME",
                ConsumerIBAN = "BND123456",
                ConsumerBIC = "BIC123456",
                TransactionRequestDateTimestamp = DateTime.Now,
                TransactionResponseDateTimestamp = DateTime.Now,
                TransactionCreateDateTimestamp = DateTime.Now,
                BNDIBAN = "BND999999",
                PaymentType = "iDeal",
                ExpirationSecondPeriod = 30,
                ExpectedCustomerIBAN = "BND000000",
                IsSystemFail = false,
            };
            _unitOfWork.GetRepository<p_Transaction>().Insert(transaction);
            _unitOfWork.CommitChanges();

            p_TransactionStatusHistory transactionStatusHistory = new p_TransactionStatusHistory
            {
                Status = "Mock Status",
                TransactionID = transaction.TransactionID,
                StatusDateTimeStamp = null,
                StatusRequestDateTimeStamp = DateTime.Now,
                StatusResponseDateTimeStamp = DateTime.Now,
            };
            int affectedRowCount = repository.UpdateStatus(newConsumerName, newConsumerIban, newConsumerBic, newIsSystemFail, transactionStatusHistory);

            p_TransactionStatusHistory insertedStatusHistory =
                _unitOfWork.GetRepository<p_TransactionStatusHistory>().Get(x => x.TransactionID == transactionId).FirstOrDefault();
            p_Transaction updatedStatusHistory = insertedStatusHistory.Transaction;

            // affectedRow should be 2 : (updated)Transaction and (inserted)TransactionStatusHistory
            Assert.AreEqual(2, affectedRowCount);
            Assert.AreEqual(transactionStatusHistory.Status, insertedStatusHistory.Status);
            Assert.AreEqual(transactionStatusHistory.TransactionID, insertedStatusHistory.TransactionID);
            Assert.AreEqual(transactionStatusHistory.Status, insertedStatusHistory.Status);
            Assert.AreEqual(transactionStatusHistory.Status, insertedStatusHistory.Status);
            Assert.IsNull(transactionStatusHistory.StatusDateTimeStamp);

            Assert.NotNull(updatedStatusHistory);
            Assert.AreEqual(transactionId, updatedStatusHistory.TransactionID);
            Assert.IsNull(updatedStatusHistory.ConsumerName);
            Assert.IsNull(updatedStatusHistory.ConsumerIBAN);
            Assert.IsNull(updatedStatusHistory.ConsumerBIC);
            Assert.AreEqual(newIsSystemFail, updatedStatusHistory.IsSystemFail);
        }

        [Test]
        public void UpdateTransactionAttempts_Should_UpdateAttemptsEqualInputAndUpdateLatestAttemptsDateTimeStampToNo_When_InputData()
        {
            string transactionId = "0030000041750523";
            string entranceCode = "5dbc50f77d6344db944b0b481af9d8a2";

            p_Transaction transaction = new p_Transaction
            {
                TransactionID = transactionId,
                AcquirerID = "0001",
                MerchantID = "000000001",
                SubID = 1000001,
                IssuerID = "00000000001",
                IssuerAuthenticationURL = "http://www.google.com",
                MerchantReturnURL = "http://www.bing.com",
                EntranceCode = entranceCode,
                PurchaseID = "PURCHASE_ID",
                Amount = 1000,
                Currency = "THB",
                Language = "TH",
                Description = "DESC",
                ConsumerName = "CONSUMER_NAME",
                ConsumerIBAN = "BND123456",
                ConsumerBIC = "BIC123456",
                TransactionRequestDateTimestamp = DateTime.Now,
                TransactionResponseDateTimestamp = DateTime.Now,
                TransactionCreateDateTimestamp = DateTime.Now,
                BNDIBAN = "BND999999",
                PaymentType = "iDeal",
                ExpirationSecondPeriod = 30,
                ExpectedCustomerIBAN = "BND000000",
                IsSystemFail = false,
            };
            _unitOfWork.GetRepository<p_Transaction>().Insert(transaction);
            _unitOfWork.CommitChanges();

            ITransactionRepository repository = new TransactionRepository(_unitOfWork);
            int newAttempts = 2;
            DateTime latestAttemptsDateTimeStamp = DateTime.Now;
            int affectedRowCount = repository.UpdateTransactionAttempts(transactionId, entranceCode, newAttempts);

            p_Transaction updatedTransaction = _unitOfWork.GetRepository<p_Transaction>().GetById(transactionId);

            Assert.AreEqual(1, affectedRowCount);
            Assert.AreEqual(newAttempts, updatedTransaction.TodayAttempts);
            Assert.GreaterOrEqual(updatedTransaction.LatestAttemptsDateTimestamp, latestAttemptsDateTimeStamp);

        }

        [Test]
        public void UpdateTransactionAttempts_Should_ReturnRowEffect0_When_TransactionToUpdateDoseNotExists()
        {
            string transactionId = "0030000041750523";
            string entranceCode = "5dbc50f77d6344db944b0b481af9d8a2";
            ITransactionRepository repository = new TransactionRepository(_unitOfWork);
            int newAttempts = 2;

            int affectedRowCount = repository.UpdateTransactionAttempts(transactionId, entranceCode, newAttempts);

            p_Transaction updatedTransaction = _unitOfWork.GetRepository<p_Transaction>().GetById(transactionId);

            Assert.AreEqual(0, affectedRowCount);
        }

        [TestCase(Enums.EnumBookingStatus.Booking)]
        [TestCase(Enums.EnumBookingStatus.NotBooked)]
        public void UpdateBookingStatus_Should_Return_Transaction(Enums.EnumBookingStatus status)
        {
            string transactionId = "0030000041750523";
            string entranceCode = "5dbc50f77d6344db944b0b481af9d8a2";

            ITransactionRepository repository = new TransactionRepository(_unitOfWork);

            // have to insert before updating the status
            repository.Insert(CrateTransactionObject(transactionId, entranceCode));
            _unitOfWork.CommitChanges();
            
            // call update booking status
            p_Transaction transaction = repository.UpdateBookingStatus(transactionId, entranceCode, status);

            Assert.IsNotNull(transaction);
            Assert.IsNull(transaction.BookingDate);
            Assert.IsNull(transaction.MovementId);
            Assert.AreEqual(status.ToString(), transaction.BookingStatus);
        }

        [Test]
        public void UpdateBookingStatus_Should_Return_Null_When_NotFound_Transaction()
        {
            string transactionId = "0030000041750523";
            string entranceCode = "5dbc50f77d6344db944b0b481af9d8a2";

            ITransactionRepository repository = new TransactionRepository(_unitOfWork);
            
            // call update booking status
            p_Transaction transaction = repository.UpdateBookingStatus(transactionId, entranceCode, Enums.EnumBookingStatus.Booking);

            Assert.IsNull(transaction);
        }


        [Test]
        public void UpdateBookingData_Should_Return_Transaction()
        {
            string transactionId = "0030000041750523";
            string entranceCode = "5dbc50f77d6344db944b0b481af9d8a2";

            ITransactionRepository repository = new TransactionRepository(_unitOfWork);

            // have to insert before updating the status
            repository.Insert(CrateTransactionObject(transactionId, entranceCode));
            _unitOfWork.CommitChanges();

            int expectedMovementId = 1;
            Enums.EnumBookingStatus expectedStatus = Enums.EnumBookingStatus.Booked;
            DateTime expectedDate = DateTime.Now;

            // call update booking status
            p_Transaction transaction = repository.UpdateBookingData(transactionId, entranceCode, expectedMovementId, expectedStatus, expectedDate);

            Assert.IsNotNull(transaction);
            Assert.AreEqual(expectedDate, transaction.BookingDate);
            Assert.AreEqual(expectedMovementId, transaction.MovementId);
            Assert.AreEqual(expectedStatus.ToString(), transaction.BookingStatus);
        }

        [Test]
        public void UpdateBookingData_Should_Return_Null_When_NotFound_Transaction()
        {
            string transactionId = "0030000041750523";
            string entranceCode = "5dbc50f77d6344db944b0b481af9d8a2";            

            ITransactionRepository repository = new TransactionRepository(_unitOfWork);

            int expectedMovementId = 1;
            Enums.EnumBookingStatus expectedStatus = Enums.EnumBookingStatus.Booked;
            DateTime expectedDate = DateTime.Now;

            // call update booking status
            p_Transaction transaction = repository.UpdateBookingData(transactionId, entranceCode, expectedMovementId, expectedStatus, expectedDate);

            Assert.IsNull(transaction);
           
        }
        private p_Transaction CrateTransactionObject(string transactionId, string entranceCode)
        {
            return new p_Transaction
            {
                TransactionID = transactionId,
                EntranceCode = entranceCode,
                AcquirerID = "0001",
                MerchantID = "000000001",
                SubID = 1000001,
                IssuerID = "00000000001",
                IssuerAuthenticationURL = "http://www.google.com",
                MerchantReturnURL = "http://www.bing.com",                
                PurchaseID = "PURCHASE_ID",
                Amount = 1000,
                Currency = "THB",
                Language = "TH",
                Description = "DESC",
                ConsumerName = "CONSUMER_NAME",
                ConsumerIBAN = "BND123456",
                ConsumerBIC = "BIC123456",
                TransactionRequestDateTimestamp = DateTime.Now,
                TransactionResponseDateTimestamp = DateTime.Now,
                TransactionCreateDateTimestamp = DateTime.Now,
                BNDIBAN = "BND999999",
                PaymentType = "iDeal",
                ExpirationSecondPeriod = 30,
                ExpectedCustomerIBAN = "BND000000",
                IsSystemFail = false,

                // defaul valud not effected in effort library have to manual set
                BookingStatus = Enums.EnumBookingStatus.NotBooked.ToString()
                
            };
        }
    }
}
