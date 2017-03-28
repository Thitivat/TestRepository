using BND.Services.IbanStore.Models;
using BND.Services.IbanStore.Service.Bll;
using System;
using NUnit.Framework;

namespace BND.Services.IbanStore.ServiceTest
{
    [TestFixture]
    public class IbanAlreadyAssignedExceptionTest
    {
        [Test]
        public void Test_Ctor1_Success()
        {
            Iban expectedIban = new Iban("NL50BNDA0132470519");
            IbanAlreadyAssignedException actual = new IbanAlreadyAssignedException(expectedIban);

            Assert.AreEqual(expectedIban, actual.AssignedIban);
        }

        [Test]
        public void Test_Ctor2_Success()
        {
            Iban expectedIban = new Iban("NL50BNDA0132470519");
            string expectedMessage = "error";
            IbanAlreadyAssignedException actual = new IbanAlreadyAssignedException(expectedIban, expectedMessage);

            Assert.AreEqual(expectedIban, actual.AssignedIban);
            Assert.AreEqual(expectedMessage, actual.Message);
        }

        [Test]
        public void Test_Ctor3_Success()
        {
            Iban expectedIban = new Iban("NL50BNDA0132470519");
            string expectedMessage = "error";
            Exception expectedException = new Exception(expectedMessage);
            IbanAlreadyAssignedException actual = new IbanAlreadyAssignedException(expectedIban, expectedMessage, expectedException);

            Assert.AreEqual(expectedIban, actual.AssignedIban);
            Assert.AreEqual(expectedMessage, actual.Message);
            Assert.AreEqual(expectedException, actual.InnerException);
        }
    }
}
