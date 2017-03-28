using BND.Services.Payments.eMandates.Data.Repositories;
using BND.Services.Payments.eMandates.Domain.Interfaces;
using BND.Services.Payments.eMandates.Models;
using BND.Services.Payments.eMandates.UnitTests.Data.Context;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data.SqlClient;

namespace BND.Services.Payments.eMandates.UnitTests.Data.Repositories
{
    [TestFixture]
    public class TransactionRepositoryTest
    {
        private IUnitOfWork _unitOfWork;
        
        [SetUp]
        public void SetUp()
        {
            _unitOfWork = new MockUnitOfWork(new MockDbContext(), false);
            
            _unitOfWork.GetRepository<EnumTransactionType>().Insert(new EnumTransactionType { TransactionTypeName = "Core", Description = "Description" });
            _unitOfWork.GetRepository<EnumSequenceType>().Insert(new EnumSequenceType { SequenceTypeName = "Ooff", Description = "Description" });
        }
        
        [Test]
        public void Insert_Should_HasDataEqualInput_When_InputData()
        {
            ITransactionRepository repository = new TransactionRepository(_unitOfWork);
            
            string transactionId = "0000000000000001";
            string entranceCode = "EC_IOHxxxHOI";

            Transaction transaction = CrateTransactionObject(transactionId, entranceCode);
            
            int affectedRowCount = repository.Insert(transaction);

            Transaction insertedTransaction = _unitOfWork.GetRepository<Transaction>().GetById(transaction.TransactionID);

            Assert.AreEqual(1+2, affectedRowCount);//plus 2 because we have to insert to 2 tables of Enum
            Assert.AreEqual(transaction.TransactionID, insertedTransaction.TransactionID);
            Assert.AreSame(transaction, insertedTransaction);
        }

        [Test]
        public void GetTransactionWithLatestStatus_Should_ReturnNull_When_InputNotExistingTransactionId()
        {
            string transactionId = "9999999999999999";
            string entranceCode = "EC_IOHxxxHOI";

            ITransactionRepository repository = new TransactionRepository(_unitOfWork);
            Transaction transactionResult = repository.GetTransactionWithLatestStatus(transactionId);

            Assert.IsNull(transactionResult);
        }

        [Test]
        public void GetTransactionWithLatestStatus_Should_ReturnTransactionWithEmptyStatusHistories_When_TransactionHasNoStatusHistory()
        {
            string transactionId = "0000000000000002";
            string entranceCode = "EC_IOHxxxHOI";

            Transaction transaction = CrateTransactionObject(transactionId, entranceCode);

            DateTime transactionCreateDateTimestamp = DateTime.Now.AddMinutes(10);
            int expirationPeriod = 30;
            bool isSystemFail = false;
            int todayAttempts = 0;
            DateTime? latestAttemptsDateTimestamp = null;

            ITransactionRepository repository = new TransactionRepository(_unitOfWork);

            transaction.TransactionCreateDateTimestamp = transactionCreateDateTimestamp;
            transaction.ExpirationPeriod = expirationPeriod;
            transaction.IsSystemFail = isSystemFail;
            transaction.TodayAttempts = 0;
            transaction.LatestAttemptsDateTimestamp = latestAttemptsDateTimestamp;

            repository.Insert(transaction);

            Transaction transactionResult = repository.GetTransactionWithLatestStatus(transactionId);

            Assert.IsNotNull(transactionResult);
            Assert.AreEqual(transactionId, transactionResult.TransactionID);
            Assert.AreEqual(expirationPeriod, transactionResult.ExpirationPeriod);
            Assert.AreEqual(isSystemFail, transactionResult.IsSystemFail);
            Assert.AreEqual(todayAttempts, transactionResult.TodayAttempts);
            Assert.AreEqual(latestAttemptsDateTimestamp, transactionResult.LatestAttemptsDateTimestamp);

            Assert.IsNotNull(transactionResult.TransactionStatusHistories);
            Assert.AreEqual(0, transactionResult.TransactionStatusHistories.Count);
        }

        [Test]
        public void GetTransactionWithLatestStatus_Should_ReturnWithTransactionHistories_When_TransactionHasStatusHistories()
        {
            ITransactionRepository repository = new TransactionRepository(_unitOfWork);

            string transactionId = "0000000000000003";
            string entranceCode = "EC_IOHxxxHOI";

            Transaction transaction = CrateTransactionObject(transactionId, entranceCode);

            DateTime transactionCreateDateTimestamp = DateTime.Now.AddMinutes(10);
            int expirationPeriod = 30;
            bool isSystemFail = false;
            DateTime? latestAttemptsDateTimestamp = DateTime.Now.AddMinutes(9);

            transaction.TransactionCreateDateTimestamp = transactionCreateDateTimestamp;
            transaction.ExpirationPeriod = expirationPeriod;
            transaction.IsSystemFail = isSystemFail;
            transaction.LatestAttemptsDateTimestamp = latestAttemptsDateTimestamp;
            _unitOfWork.GetRepository<Transaction>().Insert(transaction);
                        
            for (int i = 0; i < 3; i++)
            {
                TransactionStatusHistory transactionStatusHistory = new TransactionStatusHistory
                {
                    Status = "Previous" + i.ToString(),
                    TransactionID = transaction.TransactionID,
                    StatusDateTimestamp = DateTime.Now.Date.AddDays(-1).AddMinutes(i),
                };
                _unitOfWork.GetRepository<TransactionStatusHistory>().Insert(transactionStatusHistory);
            }
            
            for (int i = 0; i < 2; i++)
            {
                TransactionStatusHistory transactionStatusHistory = new TransactionStatusHistory
                {
                    Status = "Previous" + i.ToString(),
                    TransactionID = transaction.TransactionID,
                    StatusDateTimestamp = DateTime.Now.Date.AddMinutes(1),
                };
                _unitOfWork.GetRepository<TransactionStatusHistory>().Insert(transactionStatusHistory);
            }

            DateTime latestDateTimeStamp = DateTime.Now.Date.AddMinutes(10);
            
            TransactionStatusHistory latestTransactionStatusHistory = new TransactionStatusHistory
            {
                Status = "LatestStt",
                TransactionID = transaction.TransactionID,
                StatusDateTimestamp = latestDateTimeStamp,
            };

            _unitOfWork.GetRepository<TransactionStatusHistory>().Insert(latestTransactionStatusHistory);
            _unitOfWork.CommitChanges();

            Transaction transactionResult = repository.GetTransactionWithLatestStatus(transactionId);

            Assert.IsNotNull(transactionResult);
            Assert.AreEqual(transactionId, transactionResult.TransactionID);
            Assert.AreEqual(transactionCreateDateTimestamp, transactionResult.TransactionCreateDateTimestamp);
            Assert.AreEqual(expirationPeriod, transactionResult.ExpirationPeriod);
            Assert.AreEqual(isSystemFail, transactionResult.IsSystemFail);
            Assert.AreEqual(latestAttemptsDateTimestamp, transaction.LatestAttemptsDateTimestamp);

            Assert.AreEqual(transactionId, transactionResult.TransactionID);
            Assert.NotNull(transactionResult.TransactionStatusHistories);
            Assert.AreEqual(1, transactionResult.TransactionStatusHistories.Count);
            Assert.AreEqual(latestDateTimeStamp, transactionResult.TransactionStatusHistories.First().StatusDateTimestamp);
        }        

        [Test]
        public void UpdateTransactionAttempts_Should_UpdateAttemptsEqualInputAndUpdateLatestAttemptsDateTimeStampToNo_When_InputData()
        {
            string transactionId = "0000000000000004";
            string entranceCode = "EC_IOHxxxHOI";

            Transaction transaction = CrateTransactionObject(transactionId, entranceCode);
            _unitOfWork.GetRepository<Transaction>().Insert(transaction);
            _unitOfWork.CommitChanges();

            ITransactionRepository repository = new TransactionRepository(_unitOfWork);
            int newAttempts = 2;
            DateTime latestAttemptsDateTimeStamp = DateTime.Now;
            int affectedRowCount = repository.UpdateTransactionAttempts(transactionId, newAttempts);

            Transaction updatedTransaction = _unitOfWork.GetRepository<Transaction>().GetById(transactionId);

            Assert.AreEqual(1, affectedRowCount);
            Assert.AreEqual(newAttempts, updatedTransaction.TodayAttempts);
            Assert.GreaterOrEqual(updatedTransaction.LatestAttemptsDateTimestamp, latestAttemptsDateTimeStamp);
        }

        [Test]
        public void UpdateTransactionAttempts_Should_ZeroReturnRowEffect_When_NotExistingTransactionToUpdate()
        {
            string transactionId = "9999999999999999";
            string entranceCode = "EC_IOHxxxHOI";

            ITransactionRepository repository = new TransactionRepository(_unitOfWork);

            int newAttempts = 2;
            int affectedRowCount = repository.UpdateTransactionAttempts(transactionId, newAttempts);

            Transaction updatedTransaction = _unitOfWork.GetRepository<Transaction>().GetById(transactionId);

            Assert.AreEqual(0, affectedRowCount);
        }
                
        [Test]
        public void UpdateStatus_Should_UpdateTransactionAndInsertTransactionStatusHistory_When_InputData()
        {
            string transactionId = "0000000000000005";
            string entranceCode = "EC_IOHxxxHOI";
            bool newIsSystemFail = true;

            ITransactionRepository repository = new TransactionRepository(_unitOfWork);

            Transaction transaction = CrateTransactionObject(transactionId, entranceCode);
            _unitOfWork.GetRepository<Transaction>().Insert(transaction);
            _unitOfWork.CommitChanges();

            TransactionStatusHistory transactionStatusHistory = new TransactionStatusHistory
            {
                Status = "MckStatus",
                TransactionID = transaction.TransactionID,
                StatusDateTimestamp = null,
            };
            int affectedRowCount = repository.UpdateStatus(newIsSystemFail, transactionStatusHistory);

            TransactionStatusHistory insertedStatusHistory =
                _unitOfWork.GetRepository<TransactionStatusHistory>().Get(x => x.TransactionID == transactionId).FirstOrDefault();
            Transaction updatedStatusHistory = insertedStatusHistory.Transaction;

            // affectedRow should be 2 : one (updated)Transaction and another (inserted)TransactionStatusHistory
            Assert.AreEqual(2, affectedRowCount);
            Assert.AreEqual(transactionStatusHistory.Status, insertedStatusHistory.Status);
            Assert.AreEqual(transactionStatusHistory.TransactionID, insertedStatusHistory.TransactionID);
            Assert.AreEqual(transactionStatusHistory.Status, insertedStatusHistory.Status);
            Assert.AreEqual(transactionStatusHistory.Status, insertedStatusHistory.Status);
            
            Assert.NotNull(updatedStatusHistory);
            Assert.AreEqual(transactionId, updatedStatusHistory.TransactionID);
            Assert.AreEqual(newIsSystemFail, updatedStatusHistory.IsSystemFail);
        }
        
        private Transaction CrateTransactionObject(string transactionId, string entranceCode)
        {
            return new Transaction
            {
                TransactionID = transactionId,
                EntranceCode = entranceCode,
                DebtorBankID = "0001",
                DebtorReference = "Test",
                EMandateID = "0000000001",
                EMandateReason = "Test",
                ExpirationPeriod = 7,
                IssuerAuthenticationUrl = "http://www.google.com",
                IsSystemFail = false,
                Language = "TH",
                LatestAttemptsDateTimestamp = DateTime.Now,
                MaxAmount = 1000,
                MerchantReturnUrl = "http://www.bing.com",
                MessageID = "1234567",
                OriginalDebtorBankID = "0001",
                OriginalIban = "BND123456",
                PurchaseID = "0000002",
                SequenceType = "Ooff",
                TodayAttempts = 0,
                TransactionCreateDateTimestamp = DateTime.Now,
                TransactionType = "Core"
            };
        }                
    }
}
