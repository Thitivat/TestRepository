using BND.Services.Payments.iDeal.JobQueue.Bll;
using BND.Services.Payments.iDeal.JobQueue.Bll.Interfaces;
using BND.Services.Payments.iDeal.JobQueue.Dal.Interfaces;
using BND.Services.Payments.iDeal.JobQueue.Dal.Pocos;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BND.Services.Payments.iDeal.JobQueue.Tests.Bll
{
    [TestFixture]
    public class JobQueueManagerTest
    {
        private Mock<IJobListRepository> _jobListRepository;
        private IJobQueueManager _jobQueueManager;

        [SetUp]
        public void SetUp()
        {
            _jobListRepository = new Mock<IJobListRepository>();
            _jobListRepository.Setup(x => x.Insert(It.IsAny<p_JobList>())).Returns(1);

            _jobQueueManager = new JobQueueManager(_jobListRepository.Object);
        }

        [Test]
        public void CreateJobQueue_Should_ThrowArgumentNullException_When_EmptyTransactionId()
        {
            Assert.Throws<ArgumentNullException>(() => _jobQueueManager.CreateJobQueue(String.Empty, "0001", 15));
        }

        [Test]
        public void CreateJobQueue_Should_ThrowArgumentNullException_When_EmptyEntranceCode()
        {
            Assert.Throws<ArgumentNullException>(() => _jobQueueManager.CreateJobQueue("0001", String.Empty, 15));
        }

        [Test]
        public void CreateJobQueue_Should_ReturnOneAffectedRow_When_InputData()
        {
            Assert.AreEqual(1, _jobQueueManager.CreateJobQueue("0001", "0001", 15));
        }
    }
}
