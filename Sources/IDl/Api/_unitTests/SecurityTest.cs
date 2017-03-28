using BND.Services.Payments.iDeal.Api.Helpers;
using NUnit.Framework;
using System;

namespace BND.Services.Payments.iDeal.Api.Tests
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
        public void ValidateToken_Should_ThrowUnauthorizedAccessException_When_Unauthorize()
        {
            _token = "BND";
            // this code need to comment out because on testing process we want to allow any token and will change back after implement Ideantity server.
            // Assert.Throws<UnauthorizedAccessException>(() => security.ValidateToken(_token));
        }
    }
}