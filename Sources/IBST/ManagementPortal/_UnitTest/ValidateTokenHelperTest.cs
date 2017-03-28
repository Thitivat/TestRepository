using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using BND.Services.IbanStore.ManagementPortal.Helper;

namespace BND.Services.IbanStore.ManagementPortalTest
{
    [TestFixture]
    public class ValidateTokenHelperTest
    {
        [Test]
        public void Test_IsValid_Success()
        {
            var actualResult = ValidateTokenHelper.IsValid();
            var expectedResult = true;
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
