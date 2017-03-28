using NUnit.Framework;

namespace BND.Services.Payments.PaymentIdInfo.Dal.Test
{
    [TestFixture]
    public class iDealPaymentIdInfoUnitOfWorkTest
    {
        [Test]
        public void Ctor_Should_DoesNotThrow_When_HasBeenCreated()
        {
            Assert.DoesNotThrow(() =>
            {
                var unitOfWork = new iDealPaymentIdInfoUnitOfWork("Data Source=.;Integrated Security=True;Pooling=False;Connect Timeout=30", false);
            });
        }
    }
}
