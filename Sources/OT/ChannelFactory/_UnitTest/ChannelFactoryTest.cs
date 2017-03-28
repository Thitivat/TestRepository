using BND.Services.Security.OTP.Mef;
using BND.Services.Security.OTP.Plugins;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
//using MsTest = Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BND.Services.Security.OTP.ChannelFactoryTest
{
    [TestFixture]
    public class ChannelFactoryTest
    {
        Mock<IPluginManager<IPluginChannel>> _pluginManagerMock;
        ChannelFactory _channelFactoryMock;

        [SetUp]
        public void Test_Init()
        {
            _pluginManagerMock = new Mock<IPluginManager<IPluginChannel>>();

            //MsTest.PrivateObject channelFactoryObject = new MsTest.PrivateObject(new ChannelFactory(Environment.CurrentDirectory));
            //channelFactoryObject.SetField("_pluginManager", _pluginManagerMock.Object);

            //_channelFactoryMock = (ChannelFactory)channelFactoryObject.Target;
        }

        [Test]
        public void Test_Ctor_Success()
        {
            ChannelFactory channelFactory = new ChannelFactory(Environment.CurrentDirectory);

            Assert.IsNotNull(channelFactory);
            Assert.IsInstanceOf<ChannelFactory>(channelFactory);
        }

        [Test]
        public void Test_Ctor_Exception()
        {
            Assert.Throws(Is.TypeOf<ChannelOperationException>(), delegate { new ChannelFactory(null); });
        }

        [Test]
        public void Test_GetChannel_Success()
        {
            //MsTest.PrivateObject pluginObject =
            //    new MsTest.PrivateObject(typeof(PluginObject<IPluginChannel>),
            //                      new Lazy<IPluginChannel, IPluginMetaData>(() => new PluginChannelMock(),
            //                                                                new PluginMetaDataMock("x", "y")));

            //_pluginManagerMock.Setup(p => p.LoadPlugin(It.IsAny<string>()))
            //                  .Returns(() => (PluginObject<IPluginChannel>)pluginObject.Target);

            //IPluginChannel actual = _channelFactoryMock.GetChannel("");

            //Assert.IsNotNull(actual);
            //Assert.IsInstanceOf<PluginChannelMock>(actual);
        }

        [Test]
        public void Test_GetChannel_Exception()
        {
            _pluginManagerMock.Setup(p => p.LoadPlugin(It.IsAny<string>())).Throws(new Exception());

            //Assert.Throws(Is.TypeOf<ChannelOperationException>(), delegate { _channelFactoryMock.GetChannel(""); });
        }

        [Test]
        public void Test_GetAllChannelTypeNames_Success()
        {
            List<string> expected = new List<string> { "Channel1", "Channel2" };
            _pluginManagerMock.Setup(p => p.LoadPluginNames()).Returns(() => expected);

            //List<string> actual = _channelFactoryMock.GetAllChannelTypeNames().ToList();

            //Assert.IsNotNull(actual);
            //Assert.AreEqual(expected.Count, actual.Count);
            //Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [Test]
        public void Test_GetAllChannelTypeNames_Exception()
        {
            _pluginManagerMock.Setup(p => p.LoadPluginNames()).Throws(new Exception());

            //Assert.Throws(Is.TypeOf<ChannelOperationException>(), delegate { _channelFactoryMock.GetAllChannelTypeNames().ToList(); });
        }

        [Test]
        public void Test_Dispose_Success()
        {
            //MsTest.PrivateObject channelFactory = new MsTest.PrivateObject(new ChannelFactory(Environment.CurrentDirectory));
            //((ChannelFactory)channelFactory.Target).Dispose();
            //((ChannelFactory)channelFactory.Target).Dispose();

            //Assert.IsNull(channelFactory.GetField("_pluginManager"));
            //Assert.IsTrue((bool)channelFactory.GetField("Disposed"));
        }
    }
}
