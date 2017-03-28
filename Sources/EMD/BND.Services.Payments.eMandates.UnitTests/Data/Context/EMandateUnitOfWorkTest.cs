using BND.Services.Payments.eMandates.Data.Context;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BND.Services.Payments.eMandates.UnitTests.Data.Context
{
    [TestFixture]
    public class EMandateUnitOfWorkTest
    {
        [Test]
        public void Ctor_Should_DoesNotThrow_When_HasBeenCreated()
        {
            Assert.DoesNotThrow(() =>
            {
                var unitOfWork = new EMandateUnitOfWork("Data Source=.;Integrated Security=True;Pooling=False;Connect Timeout=30", false);
            });
        }
    }
}
