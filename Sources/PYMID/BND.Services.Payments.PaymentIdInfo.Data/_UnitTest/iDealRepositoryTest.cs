using AutoMapper;
using BND.Services.Payments.PaymentIdInfo.Libs;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace BND.Services.Payments.PaymentIdInfo.Dal.Test
{
    public class iDealRepositoryTest
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        [SetUp]
        public void SetUp()
        {
            _unitOfWork = new MockUnitOfWork(new MockDbContext(), false);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new PaymentIdInfoDalProfile());
            });
            _mapper = config.CreateMapper();
        }

        [Test]
        public void Get_Should_ReturnData()
        {
            // prepare items
            string transactionId = "0000000000000001";
            string entranceCode = "5dbc50f77d6344db944b0b481af9d8a2";
            decimal amount = 1000;
            string bndIban = "BND999999";
            string sourceIban = "BND123456";
            DateTime transactionCreateDateTimestamp = DateTime.Now.AddMinutes(10);
            int expirationPeriod = 30;
            bool isSystemFail = false;
            int todayAttempts = 3;
            DateTime? latestAttemptsDateTimestamp = DateTime.Now.AddMinutes(9);
            
            p_iDealTransaction defaultTransaction = new p_iDealTransaction
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
                ConsumerIBAN = sourceIban,
                ConsumerBIC = "BIC123456",
                TransactionRequestDateTimestamp = DateTime.Now,
                TransactionResponseDateTimestamp = DateTime.Now,
                TransactionCreateDateTimestamp = transactionCreateDateTimestamp,
                TodayAttempts = todayAttempts,
                LatestAttemptsDateTimestamp = latestAttemptsDateTimestamp,
                BNDIBAN = bndIban,
                PaymentType = "iDeal",
                ExpirationSecondPeriod = expirationPeriod,
                ExpectedCustomerIBAN = "BND000000",
                IsSystemFail = false,
            };
            _unitOfWork.GetRepository<p_iDealTransaction>().Insert(defaultTransaction);

            // insert the 3 histories for yesterday
            for (int i = 0; i < 3; i++)
            {
                p_iDealTransactionStatusHistory transactionStatusHistory = new p_iDealTransactionStatusHistory
                {
                    Status = "Status Yesterday" + i.ToString(),
                    TransactionID = defaultTransaction.TransactionID,
                    StatusDateTimeStamp = DateTime.Now.Date.AddDays(-1).AddMinutes(i),
                    StatusRequestDateTimeStamp = DateTime.Now.Date.AddDays(-1).AddMinutes(i),
                    StatusResponseDateTimeStamp = DateTime.Now.Date.AddDays(-1).AddMinutes(i),
                };
                _unitOfWork.GetRepository<p_iDealTransactionStatusHistory>().Insert(transactionStatusHistory);
            }

            // insert the 2 histories for today
            for (int i = 0; i < 2; i++)
            {
                p_iDealTransactionStatusHistory transactionStatusHistory = new p_iDealTransactionStatusHistory
                {
                    Status = "Status Yesterday" + i.ToString(),
                    TransactionID = defaultTransaction.TransactionID,
                    StatusDateTimeStamp = DateTime.Now.Date.AddMinutes(1),
                    StatusRequestDateTimeStamp = DateTime.Now.Date.AddMinutes(1),
                    StatusResponseDateTimeStamp = DateTime.Now.Date.AddMinutes(1),
                };
                _unitOfWork.GetRepository<p_iDealTransactionStatusHistory>().Insert(transactionStatusHistory);
            }

            DateTime lastestDateTimeStamp = DateTime.Now.Date.AddMinutes(10);
            // insert the latest history for today, so today have total 3 status histories
            p_iDealTransactionStatusHistory latestTransactionStatusHistory = new p_iDealTransactionStatusHistory
            {
                Status = "Latest Status For Today",
                TransactionID = defaultTransaction.TransactionID,
                StatusDateTimeStamp = lastestDateTimeStamp,
                StatusRequestDateTimeStamp = lastestDateTimeStamp,
                StatusResponseDateTimeStamp = lastestDateTimeStamp,
            };
            _unitOfWork.GetRepository<p_iDealTransactionStatusHistory>().Insert(latestTransactionStatusHistory);
            _unitOfWork.CommitChanges();

            

            // do test
            iDealRepository repository = new iDealRepository(_unitOfWork,_mapper);
            IEnumerable<iDealTransaction> iDealTransaction = repository.GetByBndIban(bndIban);

            Assert.IsNotNull(iDealTransaction);
            iDealTransaction transactionResult = iDealTransaction.FirstOrDefault();
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

            Assert.AreEqual(defaultTransaction.AcquirerID, iDealTransaction.FirstOrDefault().AcquirerID);
            Assert.AreEqual(1, iDealTransaction.Count());

            // test GetBySourceIban method
            iDealTransaction = repository.GetBySourceIban(sourceIban);
            transactionResult = iDealTransaction.FirstOrDefault();
            Assert.AreEqual(sourceIban, transactionResult.ConsumerIBAN);

            // test GetByTransactionId method
            iDealTransaction = repository.GetByTransactionId(transactionId);
            transactionResult = iDealTransaction.FirstOrDefault();
            Assert.AreEqual(transactionId, transactionResult.TransactionID);
        }
    }
}
