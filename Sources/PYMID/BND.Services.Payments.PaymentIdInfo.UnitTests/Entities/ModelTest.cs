using BND.Services.Payments.PaymentIdInfo.Entities;
using NUnit.Framework;

namespace BND.Services.Payments.PaymentIdInfo.Entities.Test
{
    [TestFixture]
    public class ModelTest
    {
        [Test]
        public void Ctor_Should_NotThrow_When_Created()
        {
            Assert.DoesNotThrow(() => new ApiErrorModel());
            Assert.DoesNotThrow(() => new iDealTransaction());
            Assert.DoesNotThrow(() => new iDealTransactionStatusHistory());
            Assert.DoesNotThrow(() => new PaymentIdInfoModel());
        }
    }
}
