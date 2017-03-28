using BND.Services.Payments.iDeal.iDealClients.Interfaces;
using BND.Services.Payments.iDeal.iDealClients.Models;
using NUnit.Framework;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace BND.Services.Payments.iDeal.Bll.Tests
{
    [TestFixture]
    public class SignatureProviderTest
    {
        public ISignatureProvider _signatureProvider;

        [SetUp]
        public void Init()
        {
            string localPrivate = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"App_Data\bnd-ideal-test.pfx");
            X509Certificate2 privateCertificate = new X509Certificate2(localPrivate, "123456");
            _signatureProvider = new SignatureProvider(privateCertificate);

        }

        [Test]
        public void SignXmlFile_Should_ThrowArgumentException_When_ParameterIsWrong()
        {
            Assert.Throws<ArgumentException>(() => _signatureProvider.SignXmlFile(null, true));
        }
    }
}
