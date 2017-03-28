using System;
//using MsTest = Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace BND.Services.Security.OTP.ChannelFactoryTest
{
    [TestFixture]
    public class ChannelLoadExceptionTest
    {
        [Test]
        public void Test_Ctor1_Success()
        {
            int expectedCode = 99;
            ChannelOperationException exception = new ChannelOperationException(expectedCode);

            Assert.AreEqual(expectedCode, exception.ErrorCode);
        }

        [Test]
        public void Test_Ctor2_Success()
        {
            int expectedCode = 99;
            string expectedMessage = "Exception!";
            ChannelOperationException exception = new ChannelOperationException(expectedCode, expectedMessage);

            Assert.AreEqual(expectedCode, exception.ErrorCode);
            Assert.AreEqual(expectedMessage, exception.Message);
        }

        [Test]
        public void Test_Ctor3_Success()
        {
            int expectedCode = 99;
            string expectedMessage = "Exception!";
            Exception expectedInnerException = new ArgumentException("Inner!");
            ChannelOperationException exception = new ChannelOperationException(expectedCode, expectedMessage, expectedInnerException);

            Assert.AreEqual(expectedCode, exception.ErrorCode);
            Assert.AreEqual(expectedMessage, exception.Message);
            Assert.AreEqual(expectedInnerException.GetType(), exception.InnerException.GetType());
            Assert.AreEqual(expectedInnerException.Message, exception.InnerException.Message);
        }
    }
}
