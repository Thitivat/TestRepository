using NUnit.Framework;

namespace BND.Services.Payments.iDeal.Dal.Tests
{
    [TestFixture]
    public class iDealUnitOfWorkTest
    {
        [Test]
        public void Ctor_Should_DoesNotThrow_When_HasBeenCreated()
        {
            Assert.DoesNotThrow(() =>
            {
                var unitOfWork = new iDealUnitOfWork("Data Source=.;Integrated Security=True;Pooling=False;Connect Timeout=30", false);
            });
        }
    }
}
