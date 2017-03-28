using BND.Services.Payments.iDeal.JobQueue.Dal;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BND.Services.Payments.iDeal.JobQueue.Tests.Dal
{
    [TestFixture]
    public class JobQueueUnitOfWorkTest
    {
        [Test]
        public void Ctor_Should_DoesNotThrow_When_HasBeenCreated()
        {
            Assert.DoesNotThrow(() =>
            {
                var unitOfWork = new JobQueueUnitOfWork("Data Source=.;Integrated Security=True;Pooling=False;Connect Timeout=30", false);
            });
        }
    }
}
