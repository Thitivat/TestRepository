using NUnit.Framework;

namespace BND.Services.Payments.iDeal.Libs.Tests
{
    [TestFixture]
    public class ErrorMessagesTest
    {
        [Test]
        public void Prop_Error_Should_ReturnBND001_When_HasBeenGotten()
        {
            Assert.AreEqual("BND001", ErrorMessages.Error.Code);
        }
    }
}
