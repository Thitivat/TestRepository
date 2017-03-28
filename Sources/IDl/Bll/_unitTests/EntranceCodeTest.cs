using BND.Services.Payments.iDeal.Utilities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
namespace BND.Services.Payments.iDeal.Bll.Tests
{
    [TestFixture]
    public class EntranceCodeTest
    {
        [Test]
        public void GetEntranceCode_Should_NotBeNullAndAlwaysUnigue_When_HasBeenCalled()
        {
            // Checks null.
            string entranceCode = EntranceCode.GenerateCode();
            Assert.IsNotNull(entranceCode);
            Assert.IsNotEmpty(entranceCode);

            // Checks duplicate.
            int expectedNumberOfCalls = 100;
            List<string> results = new List<string>();
            string actualResult;
            for (int i = 0; i < expectedNumberOfCalls; i++)
            {
                actualResult = EntranceCode.GenerateCode();
                Assert.IsFalse(results.Any(ec => ec == actualResult));
                results.Add(EntranceCode.GenerateCode());
            }
        }
    }
}
