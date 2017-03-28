using BND.Services.Payments.iDeal.ClientData.Dal.Interfaces;
using BND.Services.Payments.iDeal.Interfaces;
using Moq;
using NUnit.Framework;
using System;

namespace BND.Services.Payments.iDeal.ClientData.Tests
{
    [TestFixture]
    public class ClientDataProviderTest
    {
        private const string IBAN = "NL77BNDA6370233804";

        [Test]
        public void GetClientNameByIban_Should_ReturnClientNameFromRepository_When_InputIban()
        {
            Mock<IClientUserRepository> mockRepo = new Mock<IClientUserRepository>();
            mockRepo.Setup(x => x.GetClientName(It.IsAny<string>()))
                .Returns("Pao Bun Gin");


            IClientDataProvider provider = new ClientDataProvider(mockRepo.Object);
            string result = provider.GetClientNameByIban(IBAN);

            Assert.AreEqual("Pao Bun Gin", result);
        }

        [Test]
        public void GetClientNameByIban_Should_ThrowArgumentNullException_When_IbanIsMissing()
        {
            IClientDataProvider provider = new ClientDataProvider(null);

            Assert.Throws<ArgumentNullException>(() =>
            {
                provider.GetClientNameByIban(null);
            });
        }
    }
}
