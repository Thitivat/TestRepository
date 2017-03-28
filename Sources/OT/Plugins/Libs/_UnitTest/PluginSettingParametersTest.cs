using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace BND.Services.Security.OTP.Plugins.LibsTest
{
    [TestFixture]
    public class PluginSettingParametersTest
    {
        [Test]
        public void Test_Foreach()
        {
            string settingFilePath = Path.GetTempFileName();
            File.WriteAllText(settingFilePath, Properties.Resources.PluginSettings);

            try
            {
                PluginSetting actual = new PluginSetting(settingFilePath, "EMAIL");
                foreach (KeyValuePair<string, string> param in actual.Parameters)
                {
                    Assert.IsNotNull(param);
                    Assert.IsNotNull(param.Key);
                    Assert.IsNotNull(param.Value);
                }
            }
            finally
            {
                File.Delete(settingFilePath);
            }
        }
    }
}
