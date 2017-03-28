using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BND.Services.Payments.PaymentIdInfo.Dal.Test
{
    [TestFixture]
    public class iDealPaymentIdInfoDbContextTest
    {
        [Test]
        public void Logs_Should_BeAffectedInDb_When_PerformCRUD()
        {
            Assert.DoesNotThrow(() =>
            {
                // prepare items
                string transactionId = "0000000000000001";
                string entranceCode = "5dbc50f77d6344db944b0b481af9d8a2";
                decimal amount = 1000;
                string bndIban = "BND999999";
                string sourceIban = "BND123456";
                DateTime transactionCreateDateTimestamp = DateTime.Now.AddMinutes(10);
                int expirationPeriod = 30;
                int todayAttempts = 3;
                DateTime? latestAttemptsDateTimestamp = DateTime.Now.AddMinutes(9);

                int expectedRowsAffected = 1;
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
                var dbContext = new iDealPaymentIdInfoDbContext(Effort.DbConnectionFactory.CreateTransient());

                // Test insert.
                dbContext.Transactions.Add(defaultTransaction);
                Assert.AreEqual(expectedRowsAffected, dbContext.SaveChanges());

                //// Test update.
                //var log = dbContext.(expectedLog.LogId);
                //log.Msg = "test";
                //Assert.AreEqual(expectedRowsAffected, dbContext.SaveChanges());

                // Test select.
                //var logs = dbContext.Logs.AsEnumerable();
                //Assert.NotNull(logs);
                //Assert.AreEqual(expectedRowsAffected, logs.Count());

                // Test delete.
                dbContext.Transactions.Remove(defaultTransaction);
                Assert.AreEqual(expectedRowsAffected, dbContext.SaveChanges());
            });
        }
    }
}
