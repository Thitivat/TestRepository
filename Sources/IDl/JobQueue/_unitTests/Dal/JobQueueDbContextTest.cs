using BND.Services.Payments.iDeal.JobQueue.Dal;
using BND.Services.Payments.iDeal.JobQueue.Dal.Pocos;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BND.Services.Payments.iDeal.JobQueue.Tests.Dal
{
    [TestFixture]
    public class JobQueueDbContextTest
    {
        [Test]
        public void JobList_Should_BeAffectedInDb_When_Insert()
        {
            Assert.DoesNotThrow(() =>
            {
                int expectedRowsAffected = 1;
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

                var dbContext = new JobQueueDbContext(Effort.DbConnectionFactory.CreateTransient());

                dbContext.JobList.Add(jobList);

                Assert.AreEqual(expectedRowsAffected, dbContext.SaveChanges());
            });
        }
    }
}
