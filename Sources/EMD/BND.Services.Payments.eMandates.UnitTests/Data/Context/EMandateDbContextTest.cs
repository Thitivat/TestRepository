using BND.Services.Payments.eMandates.Data.Context;
using BND.Services.Payments.eMandates.Models;
using NUnit.Framework;
using System;
using System.Linq;

namespace BND.Services.Payments.eMandates.UnitTests.Data.Context
{
    [TestFixture]
    public class EMandateDbContextTest
    {
        [Test]
        public void Logs_Should_BeAffectedInDb_When_PerformCRUD()
        {
            Assert.DoesNotThrow(() =>
            {
                int expectedRowsAffected = 1;
                var expectedLog = new Log
                {
                    Prival = 121,
                    Version = 1,
                    Timestamp = DateTime.Now,
                    Hostname = "eMandate",
                    AppName = "Unit test",
                    ProcId = "Test constructor",
                    MsgId = "MSG001",
                    Msg = "log message"
                };
                var dbContext = new EMandateDbContext(Effort.DbConnectionFactory.CreateTransient());

                // Test insert.
                dbContext.Logs.Add(expectedLog);
                Assert.AreEqual(expectedRowsAffected, dbContext.SaveChanges());

                // Test update.
                var log = dbContext.Logs.Find(expectedLog.LogID);
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
