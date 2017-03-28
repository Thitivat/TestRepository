using BND.Services.Payments.PaymentIdInfo.Api.Helpers;
using NUnit.Framework;
using System;

namespace BND.Services.Payments.PaymentIdInfo.Api.Test
{
    [TestFixture]
    public class SecurityTest
    {
        private string _token = "BND123456";
        Security security = new Security();

        [Test]
        public void ValidateToken_Should_NotThrowAnyException_When_Success()
        {
            Assert.DoesNotThrow(() => security.ValidateToken(_token));
        }

        [Test]
        public void ValidateToken_Should_ThrowUnauthorizedAccessException_When_TokenEmpty()
        {
            Assert.Throws<UnauthorizedAccessException>(() => security.ValidateToken(String.Empty));
        }
    }
}
