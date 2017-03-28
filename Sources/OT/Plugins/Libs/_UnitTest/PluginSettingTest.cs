using System.IO;
using NUnit.Framework;
using System;

namespace BND.Services.Security.OTP.Plugins.LibsTest
{
    [TestFixture]
    public class PluginSettingTest
    {
        private string _settingFilePath;

        [SetUp]
        public void Test_Initialize()
        {
            _settingFilePath = Path.GetTempFileName();
            File.WriteAllText(_settingFilePath, Properties.Resources.PluginSettings);
        }

        [TearDown]
        public void Test_Cleanup()
        {
            File.Delete(_settingFilePath);
        }

        [Test]
        public void Test_Ctor_Success()
        {
            string expectedChannelName = "EMAIL";
            PluginSetting actual = new PluginSetting(_settingFilePath, expectedChannelName);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedChannelName, actual.Name);
            Assert.AreEqual("1.0.0.0", actual.Version);
            Assert.AreEqual(7, actual.Parameters.Count);
            Assert.AreEqual("BRANDNMAILQ_CZKwC83c5P3M", actual.Parameters["ComponentCode"].Value);
            Assert.AreEqual("BRANDNHtmlToXml_eruWqpbOZD7k", actual.Parameters["HtmlToText"].Value);
            Assert.AreEqual("smtp.gmail.com", actual.Parameters["EmailServer"].Value);
            Assert.AreEqual("465", actual.Parameters["EmailSmtpPort"].Value);
            Assert.AreEqual("kobkiat.peace@gmail.com", actual.Parameters[4].Value);
            Assert.AreEqual("qwerty@12345", actual.Parameters["EmailPassword"].Value);
            Assert.AreEqual("This is a test mail.", actual.Parameters["EmailSubject"].Value);
        }

        [Test]
        public void Test_Ctor_Success_WithoutVersion()
        {
            string expectedChannelName = "SMS";
            PluginSetting actual = new PluginSetting(_settingFilePath, expectedChannelName);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedChannelName, actual.Name);
            Assert.IsNull(actual.Version);
            Assert.AreEqual(1, actual.Parameters.Count);
            Assert.AreEqual("live_elaGiluktfJyREzEnJ43TikI9", actual.Parameters["SmsAccessKey"].Value);
        }

        [Test]
        public void Test_Ctor_Exception_PluginFilePathNull()
        {
            Assert.Throws<ArgumentNullException>(() => new PluginSetting(null, null));
        }

        [Test]
        public void Test_Ctor_Exception_ChannelNameNull()
        {
            Assert.Throws<ArgumentNullException>(() => new PluginSetting(_settingFilePath, null));
        }

        [Test]
        public void Test_Ctor_Exception_PluginFilePathNotExists()
        {
            Assert.Throws<FileNotFoundException>(() =>  new PluginSetting(@"d:\xxyyzz", "EMAIL"));
        }

        [Test]
        public void Test_Ctor_Exception_PluginFileNotXmlFormat()
        {
            string settingFilePath = Path.GetTempFileName();
            File.WriteAllText(settingFilePath, "this is a setting.");

            try
            {
                Assert.Throws<FormatException>(() => new PluginSetting(settingFilePath, "EMAIL"));
            }
            finally
            {
                File.Delete(settingFilePath);
            }
        }

        [Test]
        public void Test_Ctor_Exception_ChannelNotFound()
        {
            Assert.Throws<FormatException>(() => new PluginSetting(_settingFilePath, "xxx"));
        }

        [Test]
        public void Test_Ctor_Exception_TagParametersNotFound()
        {
            Assert.Throws<FormatException>(() => new PluginSetting(_settingFilePath, "NoParams"));
        }

        [Test]
        public void Test_Ctor_Exception_AttrKeyValueNotFound()
        {
            Assert.Throws<FormatException>(() => new PluginSetting(_settingFilePath, "NoKeyValue"));
        }

        [Test]
        public void Test_Ctor_Exception_AttrKeyDuplicate()
        {
            Assert.Throws<FormatException>(() => new PluginSetting(_settingFilePath, "KeyDuplicate"));
        }
    }
}
