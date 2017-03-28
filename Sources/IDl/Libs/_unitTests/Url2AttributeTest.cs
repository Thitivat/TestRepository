using NUnit.Framework;
using System;

namespace BND.Services.Payments.iDeal.Libs.Tests
{
    [TestFixture]
    public class Url2AttributeTest
    {
        [Test]
        public void Ctor_Should_DoesNotThrow_When_HasBeenCreated()
        {
            Assert.DoesNotThrow(() => new Url2Attribute());
        }

        [Test]
        public void IsValid_Should_BeTrue_When_ValueIsValidUrl()
        {
            var urlValidator = new Url2Attribute();

            Assert.IsTrue(urlValidator.IsValid("http://www.tempuri.org"));
        }

        [Test]
        public void IsValid_Should_BeFalse_When_ValueIsInvalidUrl()
        {
            var urlValidator = new Url2Attribute();

            Assert.IsFalse(urlValidator.IsValid("bad url format"));
        }

        [Test]
        public void IsValid_Should_ThrowFormatException_When_ValueParameterIsNotString()
        {
            var urlValidator = new Url2Attribute();

            Assert.Throws<FormatException>(() => urlValidator.IsValid(default(int)));
        }

        [Test]
        public void GetDataTypeName_Should_ReturnUrl2_When_HasBeenCalled()
        {
            var urlValidator = new Url2Attribute();

            Assert.AreEqual("Url2", urlValidator.GetDataTypeName());
        }
    }
}
