using System;
//using MsTest = Microsoft.VisualStudio.TestTools.UnitTesting;
using BND.Services.Security.OTP.Mef;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;

namespace BND.Services.Security.OTP.MefTest
{
    [TestFixture]
    public class PluginObjectTest
    {
        private PluginMetadataMock _metadata;
        private ExtensionMock1 _plugin;

        [SetUp]
        public void Init()
        {
            //Fake the plugin and metadata
            _metadata = new PluginMetadataMock("Plugin name", "Plugin description");
            _plugin = new ExtensionMock1();
        }

        [Test]
        public void Test_Constructor_Success()
        {
            var pluginItem = new Lazy<IExtensionMock, IPluginMetaData>(() => _plugin, _metadata);

            //Create instance of PluginObject via PirvateObject
            //MsTest.PrivateObject po = new MsTest.PrivateObject(typeof(PluginObject<IExtensionMock>), pluginItem);
            //PluginObject<IExtensionMock> pluginObj = (PluginObject<IExtensionMock>)po.Target;

            //Assert.AreEqual(pluginItem.Metadata.Name, pluginObj.Name);
            //Assert.AreEqual(pluginItem.Metadata.Description, pluginObj.Description);
            //Assert.AreEqual(pluginItem.Value, pluginObj.Plugin);

        }

        [Test]
        public void Test_Constructor_Fail_T_IsNotInterface()
        {
            //Assert.Throws<NotSupportedException>(() =>
            //{
            //    var pluginItem = new Lazy<ExtensionMock1, IPluginMetaData>(() => _plugin, _metadata);

            //    try
            //    {
            //        //Create instance of PluginObject via PirvateObject
            //        //MsTest.PrivateObject po = new MsTest.PrivateObject(typeof(PluginObject<ExtensionMock1>), pluginItem);
            //        ////Sent wrong generic type (it not Interface) to PluginbOject for receive an error.
            //        //PluginObject<ExtensionMock1> pluginObj = (PluginObject<ExtensionMock1>)po.Target;
            //    }
            //    catch (TargetInvocationException ex)
            //    {
            //        throw ex.InnerException;
            //    }
            //});
        }

        [Test]
        public void Test_Constructor_Fail_Value_IsNull()
        {
            //Assert.Throws<ArgumentNullException>(() =>
            //{
            //    var pluginItem = new Lazy<IExtensionMock, IPluginMetaData>(() => null, _metadata);

            //    try
            //    {
            //        //Create instance of PluginObject via PirvateObject
            //        //MsTest.PrivateObject po = new MsTest.PrivateObject(typeof(PluginObject<IExtensionMock>), pluginItem);
            //        //PluginObject<IExtensionMock> pluginObj = (PluginObject<IExtensionMock>)po.Target;
            //    }
            //    catch (TargetInvocationException ex)
            //    {
            //        throw ex.InnerException;
            //    }
            //});
        }

        [Test]
        public void Test_Constructor_Fail_Metadata_IsNull()
        {
            //Assert.Throws<ArgumentNullException>(() =>
            //{
            //    var pluginItem = new Lazy<IExtensionMock, IPluginMetaData>(() => _plugin, null);

            //    try
            //    {
            //        //Create instance of PluginObject via PirvateObject
            //        //MsTest.PrivateObject po = new MsTest.PrivateObject(typeof(PluginObject<IExtensionMock>), pluginItem);
            //        //PluginObject<IExtensionMock> pluginObj = (PluginObject<IExtensionMock>)po.Target;
            //    }
            //    catch (TargetInvocationException ex)
            //    {
            //        throw ex.InnerException;
            //    }
            //});
        }

        [Test]
        public void Test_Constructor_Fail_Reference_IsNull()
        {
            //Assert.Throws<ArgumentNullException>(() =>
            //{
            //    PluginObject<IExtensionMock> pluginItem = null;
            //    try
            //    {
            //        //Create instance of PluginObject via PirvateObject
            //        //MsTest.PrivateObject po = new MsTest.PrivateObject(typeof(PluginObject<IExtensionMock>), pluginItem);
            //        //PluginObject<IExtensionMock> pluginObj = (PluginObject<IExtensionMock>)po.Target;
            //    }
            //    catch (TargetInvocationException ex)
            //    {
            //        throw ex.InnerException;
            //    }
            //});
        }

        [Test]
        public void Test_Constructor_Fail_NameIsNull()
        {
            //Assert.Throws<ArgumentNullException>(() =>
            //{
            //    PluginMetadataMock metadata = new PluginMetadataMock("", "Plugin description");
            //    var pluginItem = new Lazy<IExtensionMock, IPluginMetaData>(() => _plugin, metadata);

            //    try
            //    {
            //        //Create instance of PluginObject via PirvateObject
            //        //MsTest.PrivateObject po = new MsTest.PrivateObject(typeof(PluginObject<IExtensionMock>), pluginItem);
            //        //PluginObject<IExtensionMock> pluginObj = (PluginObject<IExtensionMock>)po.Target;
            //    }
            //    catch (TargetInvocationException ex)
            //    {
            //        throw ex.InnerException;
            //    }
            //});
        }

        [Test]
        public void Test_Constructor_Fail_DescriptionIsNull()
        {
            //Assert.Throws<ArgumentNullException>(() =>
            //{
            //    PluginMetadataMock metadata = new PluginMetadataMock("Plugin Name", "");
            //    var pluginItem = new Lazy<IExtensionMock, IPluginMetaData>(() => _plugin, metadata);

            //    try
            //    {
            //        //Create instance of PluginObject via PirvateObject
            //        //MsTest.PrivateObject po = new MsTest.PrivateObject(typeof(PluginObject<IExtensionMock>), pluginItem);
            //        //PluginObject<IExtensionMock> pluginObj = (PluginObject<IExtensionMock>)po.Target;
            //    }
            //    catch (TargetInvocationException ex)
            //    {
            //        throw ex.InnerException;
            //    }
            //});
        }
    }
}
