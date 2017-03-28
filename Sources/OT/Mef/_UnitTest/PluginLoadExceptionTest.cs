using BND.Services.Security.OTP.Mef;
//using MsTest = Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using NUnit.Framework;

namespace BND.Services.Security.OTP.MefTest
{
    [TestFixture]
    public class PluginLoadExceptionTest
    {
        [Test]
        public void Test_Constructor_2Parameter()
        {
            string pluginDirectory = AppDomain.CurrentDomain.BaseDirectory;

            PluginLoadException exception1 = new PluginLoadException(pluginDirectory, typeof(ExtensionMock1));
            Assert.IsNotNull(exception1);
            Assert.AreEqual(pluginDirectory, exception1.PluginDirectory);
            Assert.AreEqual(typeof(ExtensionMock1), exception1.PluginContractType);
        }

        [Test]
        public void Test_Constructor_3Parameter()
        {
            string pluginDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string message = "error message that send from outside";

            PluginLoadException exception1 = new PluginLoadException(pluginDirectory, typeof(ExtensionMock1), message);
            Assert.IsNotNull(exception1);
            Assert.AreEqual(pluginDirectory, exception1.PluginDirectory);
            Assert.AreEqual(typeof(ExtensionMock1), exception1.PluginContractType);
            Assert.AreEqual(message, exception1.Message);
        }

        [Test]
        public void Test_Constructor_4Parameter()
        {
            string pluginDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string message = "error message that send from outside";

            PluginLoadException exception1 = new PluginLoadException(pluginDirectory, typeof(ExtensionMock1), message, new ArgumentException());
            Assert.IsNotNull(exception1);
            Assert.AreEqual(pluginDirectory, exception1.PluginDirectory);
            Assert.AreEqual(typeof(ExtensionMock1), exception1.PluginContractType);
            Assert.AreEqual(message, exception1.Message);
            Assert.IsTrue(exception1.InnerException is ArgumentException);
        }
    }
}