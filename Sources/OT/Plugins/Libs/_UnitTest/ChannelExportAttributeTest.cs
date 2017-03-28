using NUnit.Framework;
using System;

namespace BND.Services.Security.OTP.Plugins.LibsTest
{
    [TestFixture]
    public class ChannelExportAttributeTest
    {
        [Test]
        public void Test_Constructor_Success()
        {
            string pluginName = "PluginName";
            string description = "Description";
            ChannelExportAttribute attribute = new ChannelExportAttribute(pluginName, description);

            Assert.IsNotNull(attribute);
            Assert.AreEqual(pluginName, attribute.Name);
            Assert.AreEqual(description, attribute.Description);
        }

        [Test]
        public void Test_Constructor_Fail_Name_IsNull()
        {
            string pluginName = "";
            string description = "Description";
            Assert.Throws<ArgumentNullException>(() => new ChannelExportAttribute(pluginName, description));
        }

        [Test]
        public void Test_Constructor_Fail_Description_IsNull()
        {
            string pluginName = "PluginName";
            string description = "";
            Assert.Throws<ArgumentNullException>(() => new ChannelExportAttribute(pluginName, description));
        }
    }
}
