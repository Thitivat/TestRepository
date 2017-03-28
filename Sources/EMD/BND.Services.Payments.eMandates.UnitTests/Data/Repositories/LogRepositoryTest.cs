using BND.Services.Payments.eMandates.Data.Repositories;
using BND.Services.Payments.eMandates.Domain.Interfaces;
using BND.Services.Payments.eMandates.Models;
using BND.Services.Payments.eMandates.UnitTests.Data.Context;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BND.Services.Payments.eMandates.UnitTests.Data.Repositories
{
    [TestFixture]
    public class LogRepositoryTest
    {
        private IUnitOfWork _unitOfWork;

        [SetUp]
        public void SetUp()
        {
            _unitOfWork = new MockUnitOfWork(new MockDbContext(), false);
        }

        [Test]
        public void InsertLog_Should_HasDataEqualInput_When_InputData()
        {
            LogRepository repository = new LogRepository(_unitOfWork);

            Log expectedLog = new Log()
            {
                Prival = 132,
                Version = 1,
                Timestamp = DateTime.Now,
                Hostname = "HOST_NAME",
                AppName = "APP_NAME",
                ProcId = "PROC_ID",
                MsgId = "MSG_ID",
                Msg = "MSG",
            };
            int affectedRowCount = repository.Insert(expectedLog);

            Log actualLog = _unitOfWork.GetRepository<Log>().GetQueryable().FirstOrDefault();

            Assert.AreEqual(1, affectedRowCount);
            Assert.AreEqual(expectedLog.Prival, actualLog.Prival);
            Assert.AreEqual(expectedLog.Version, actualLog.Version);
            Assert.AreEqual(expectedLog.Timestamp, actualLog.Timestamp);
            Assert.AreEqual(expectedLog.Hostname, actualLog.Hostname);
            Assert.AreEqual(expectedLog.AppName, actualLog.AppName);
            Assert.AreEqual(expectedLog.ProcId, actualLog.ProcId);
            Assert.AreEqual(expectedLog.MsgId, actualLog.MsgId);
            Assert.AreEqual(expectedLog.Msg, actualLog.Msg);
        }
    }
}
