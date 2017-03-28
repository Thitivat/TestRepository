using BND.Services.Payments.iDeal.Dal.Pocos;
using NUnit.Framework;
using System;
using System.Linq;

namespace BND.Services.Payments.iDeal.Dal.Tests
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

            p_Log expectedLog = new p_Log()
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

            p_Log actualLog = _unitOfWork.GetRepository<p_Log>().GetQueryable().FirstOrDefault();

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
