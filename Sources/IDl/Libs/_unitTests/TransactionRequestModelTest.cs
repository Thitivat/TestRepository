using BND.Services.Payments.iDeal.Models;
using NUnit.Framework;

namespace BND.Services.Payments.iDeal.Libs.Tests
{
    [TestFixture]
    public class TransactionRequestModelTest
    {
        [Test]
        public void Ctor_Should_BeNLAndEUR_When_HasBeenCreatedWithoutSetiingOnLanguageAndCurrencyProperties()
        {
            string expectedLanguage = "NL";
            string expectedCurrency = "EUR";

            TransactionRequestModel transactionRequest = new TransactionRequestModel();

            Assert.AreEqual(expectedLanguage, transactionRequest.Language);
            Assert.AreEqual(expectedCurrency, transactionRequest.Currency);
        }

        [Test]
        public void Ctor_Should_BeWhateverYouSet_When_HasBeenCreatedWithSetiingOnLanguageAndCurrencyProperties()
        {
            string expectedLanguage = "TH";
            string expectedCurrency = "THB";

            TransactionRequestModel transactionRequest = new TransactionRequestModel
            {
                Language = expectedLanguage,
                Currency = expectedCurrency
            };

            Assert.AreEqual(expectedLanguage, transactionRequest.Language);
            Assert.AreEqual(expectedCurrency, transactionRequest.Currency);
        }
    }
}
