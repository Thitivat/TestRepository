using NUnit.Framework;
using System;
using System.Linq;

namespace BND.Services.Payments.iDeal.Dal.Tests
{
    [TestFixture]
    public class iDealDbContextTest
    {
        [Test]
        public void Logs_Should_BeAffectedInDb_When_PerformCRUD()
        {
            Assert.DoesNotThrow(() =>
            {
                int expectedRowsAffected = 1;
                var expectedLog = new Pocos.p_Log
                {
                    Prival = 121,
                    Version = 1,
                    Timestamp = DateTime.Now,
                    Hostname = "iDeal",
                    AppName = "Dal unit test",
                    ProcId = "Test constructor",
                    MsgId = "MSG001",
                    Msg = "log message"
                };
                var dbContext = new iDealDbContext(Effort.DbConnectionFactory.CreateTransient());

                // Test insert.
                dbContext.Logs.Add(expectedLog);
                Assert.AreEqual(expectedRowsAffected, dbContext.SaveChanges());

                // Test update.
                var log = dbContext.Logs.Find(expectedLog.LogId);
                log.Msg = "test";
                Assert.AreEqual(expectedRowsAffected, dbContext.SaveChanges());

                // Test select.
                var logs = dbContext.Logs.AsEnumerable();
                Assert.NotNull(logs);
                Assert.AreEqual(expectedRowsAffected, logs.Count());

                // Test delete.
                dbContext.Logs.Remove(log);
                Assert.AreEqual(expectedRowsAffected, dbContext.SaveChanges());
            });
        }
    }
}
