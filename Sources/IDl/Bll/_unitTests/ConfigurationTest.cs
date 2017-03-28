using BND.Services.Payments.iDeal.iDealClients.Interfaces;
using NUnit.Framework;
using SYSConfig = System.Configuration;

namespace BND.Services.Payments.iDeal.Bll.Tests
{
    [TestFixture]
    public class ConfigurationTest
    {
        public IConfigurationSectionHandler _configurationSectionHandler;

        [TearDown]
        public void CleanUp()
        {
            _configurationSectionHandler = null;
        }

        [Test]
        public void DefaultConfiguration_Should_ThrowsConfigurationErrorsException_When_ConfigurationIsWrong()
        {
            _configurationSectionHandler = (IConfigurationSectionHandler)SYSConfig.ConfigurationManager.GetSection("iDealNoAcceptant");
            Assert.Throws<SYSConfig.ConfigurationErrorsException>(() => new DefaultConfiguration(_configurationSectionHandler, null,
                                                                                                 _configurationSectionHandler.MerchantSubId));

            _configurationSectionHandler = (IConfigurationSectionHandler)SYSConfig.ConfigurationManager.GetSection("iDealNoPass");
            Assert.Throws<SYSConfig.ConfigurationErrorsException>(() => new DefaultConfiguration(_configurationSectionHandler, null,
                                                                                                 _configurationSectionHandler.MerchantSubId));

            _configurationSectionHandler = (IConfigurationSectionHandler)SYSConfig.ConfigurationManager.GetSection("iDealAcceptantLocation");
            Assert.Throws<SYSConfig.ConfigurationErrorsException>(() => new DefaultConfiguration(_configurationSectionHandler, null,
                                                                                                 _configurationSectionHandler.MerchantSubId));

            _configurationSectionHandler = (IConfigurationSectionHandler)SYSConfig.ConfigurationManager.GetSection("iDealAcceptantThumb");
            Assert.Throws<SYSConfig.ConfigurationErrorsException>(() => new DefaultConfiguration(_configurationSectionHandler, null,
                                                                                                 _configurationSectionHandler.MerchantSubId));

            _configurationSectionHandler = (IConfigurationSectionHandler)SYSConfig.ConfigurationManager.GetSection("iDealAcceptantStoreWrong");
            Assert.Throws<SYSConfig.ConfigurationErrorsException>(() => new DefaultConfiguration(_configurationSectionHandler, null,
                                                                                                 _configurationSectionHandler.MerchantSubId));

            _configurationSectionHandler = (IConfigurationSectionHandler)SYSConfig.ConfigurationManager.GetSection("iDealAcceptantStoreSuccess");
            Assert.Throws<SYSConfig.ConfigurationErrorsException>(() => new DefaultConfiguration(_configurationSectionHandler, null,
                                                                                                 _configurationSectionHandler.MerchantSubId));
        }

        [Test]
        public void GetCertificateFromFile_Should_ThrowsConfigurationErrorsException_When_ConfigurationIsWrong()
        {
            _configurationSectionHandler = (IConfigurationSectionHandler)SYSConfig.ConfigurationManager.GetSection("iDealAcceptantPath");
            Assert.Throws<SYSConfig.ConfigurationErrorsException>(() => new DefaultConfiguration(_configurationSectionHandler,
                                                                                                 _configurationSectionHandler.MerchantId,
                                                                                                 _configurationSectionHandler.MerchantSubId));
        }
    }
}
