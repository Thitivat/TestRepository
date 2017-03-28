using NUnit.Framework;
using System;

namespace BND.Services.Payments.iDeal.Libs.Tests
{
    [TestFixture]
    public class iDealOperationExceptionTest
    {
        [Test]
        public void Constructor_Should_ReturnSameErrorCode_When_CreateWithErrorCode()
        {
            string expectedCode = "EXCODE ";
            iDealOperationException exception = new iDealOperationException(expectedCode);

            Assert.AreEqual(expectedCode, exception.ErrorCode);
        }

        [Test]
        public void Constructor_Should_ReturnSameErrorCodeAndMessage_When_CreateWithErrorCodeAndMessage()
        {
            string expectedCode = "EXCODE ";
            string expectedMessage = "This is message log";
            iDealOperationException exception = new iDealOperationException(expectedCode, expectedMessage);

            Assert.AreEqual(expectedCode, exception.ErrorCode);
            Assert.AreEqual(expectedMessage, exception.Message);
        }

        [Test]
        public void Constructor_Should_ReturnSameAll_When_CreateWithErrorCodeMessageAndException()
        {
            string expectedCode = "EXCODE ";
            string expectedMessage = "This is message log";
            ArgumentException expectedException = new ArgumentException();

            iDealOperationException exception = new iDealOperationException(expectedCode, expectedMessage, expectedException);

            Assert.AreEqual(expectedCode, exception.ErrorCode);
            Assert.AreEqual(expectedMessage, exception.Message);
            Assert.IsTrue(expectedException.GetType().Equals(exception.InnerException.GetType()));
        }



    }
}
