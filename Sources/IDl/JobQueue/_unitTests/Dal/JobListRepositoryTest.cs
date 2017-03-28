using BND.Services.Payments.iDeal.JobQueue.Dal.Interfaces;
using BND.Services.Payments.iDeal.JobQueue.Dal.Pocos;
using BND.Services.Payments.iDeal.JobQueue.Dal.Repositories;
using NUnit.Framework;
using System;

namespace BND.Services.Payments.iDeal.JobQueue.Tests.Dal
{
    [TestFixture]
    public class JobListRepositoryTest
    {
        private IUnitOfWork _unitOfWork;

        [SetUp]
        public void SetUp()
        {
            _unitOfWork = new MockUnitOfWork(new MockDbContext(), false);
        }

        [Test]
        public void Insert_Should_ReturnOneAffectedRow()
        {
            IJobListRepository repository = new JobListRepository(_unitOfWork);

            p_JobList jobList = new p_JobList()
            {
                JobClassName = "GetIdealStatus",
                Label = "GetIdealStatus: Entrancecode 0001, TrxID 0001",
                JobParameters = "Entrancecode:0001, TrxID:0001",
                StartedBy = "iDealService",
                Inserted = DateTime.Now.AddMinutes(15),
                JobPriorityID = 99,
                Parallel = false,
                SyncChainID = 3,
                JobStatusID = 0
            };

            var affectedRowCount = repository.Insert(jobList);

            Assert.AreEqual(1, affectedRowCount);
        }
    }
}
