using BND.Services.IbanStore.Service;
using System;
using NUnit.Framework;

namespace BND.Services.IbanStore.ServiceTest
{
    [TestFixture]
    public class IBanOperationExceptionTest
    {
        [Test]
        public void Test_Constructor_1()
        {
            int expectedCode = 1000;
            IbanOperationException exception = new IbanOperationException(1000);

            Assert.AreEqual(expectedCode, exception.ErrorCode);
        }

        [Test]
        public void Test_Constructor_2()
        {
            int expectedCode = 1000;
            string expectedMessage = "Error Message";
            IbanOperationException exception = new IbanOperationException(1000, "Error Message");
            
            Assert.AreEqual(expectedCode, exception.ErrorCode);
            Assert.AreEqual(expectedMessage, exception.Message);
            
        }

        [Test]
        public void Test_Constructor_3()
        {
            int expectedCode = 1000;
            string expectedMessage = "Error Message";

            IbanOperationException exception = new IbanOperationException(1000, "Error Message", new ArgumentException());

            Assert.AreEqual(expectedCode, exception.ErrorCode);
            Assert.AreEqual(expectedMessage, exception.Message);
            Assert.IsTrue(exception.InnerException is ArgumentException);

        }
    }
}
