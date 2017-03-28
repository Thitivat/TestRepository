using System;
//using MsTest = Microsoft.VisualStudio.TestTools.UnitTesting;
using BND.Services.Security.OTP.Mef;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Security.AccessControl;
using NUnit.Framework;

namespace BND.Services.Security.OTP.MefTest
{
    [TestFixture]
    public class PluginManagerTest
    {
        #region [Fields]
        private string _tempPluginDirectory;
        #endregion


        [SetUp]
        public void Init()
        {
            _tempPluginDirectory = Path.Combine( AppDomain.CurrentDomain.BaseDirectory ,"_Temp_PluginFolder");
            if (!Directory.Exists(_tempPluginDirectory)) Directory.CreateDirectory(_tempPluginDirectory);
        }

        [TearDown]
        public void CleanUp()
        {
            // Remove the access control entry from the directory.
            RemoveDirectorySecurity(_tempPluginDirectory, Environment.UserName, FileSystemRights.ReadData, AccessControlType.Allow);

            if (Directory.Exists(_tempPluginDirectory)) Directory.Delete(_tempPluginDirectory);
        }

        #region [Constructor]

        [Test]
        public void Test_Constructor_Success()
        {
            PluginManager<IExtensionMock> extensionManager = new PluginManager<IExtensionMock>(AppDomain.CurrentDomain.BaseDirectory);

            Assert.IsNotNull(extensionManager);
        }

        [Test]
        public void Test_Constructor_Fail_DirectoryIsNull()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                string pluginPath = String.Empty;
                PluginManager<IExtensionMock> extensionManager = new PluginManager<IExtensionMock>(pluginPath);
            });
        }

        [Test]
        public void Test_Constructor_Fail_DirectoryNotFound()
        {
            Assert.Throws<PluginLoadException>(() =>
            {
                //This case wrap DirectoryNotFoundException with PluginLoadException
                string pluginPath = @"C:\DIRECTORY_DOES_NOT_FOUND";
                PluginManager<IExtensionMock> extensionManager = new PluginManager<IExtensionMock>(pluginPath);
            });
        }

        [Test]
        public void Test_Constructor_Fail_Directory_Invalid()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                string pluginPath = @"C:\DIRECTORY Is Invalid" + Environment.NewLine;
                PluginManager<IExtensionMock> extensionManager = new PluginManager<IExtensionMock>(pluginPath);
            });
        }

        [Test]
        public void Test_GetCatalog_Fail_Directory_InValid()
        {
            Assert.Throws<PluginLoadException>(() =>
            {
                //This case wrap PathTooLongException with PluginLoadException
                string pluginPath = @"C:\TO_TEST_DIRECTORY_IS_TOO_LONG\TO_TEST_DIRECTORY_IS_TOO_LONG\" +
                                    @"TO_TEST_DIRECTORY_IS_TOO_LONG\TO_TEST_DIRECTORY_IS_TOO_LONG\TO_TEST_DIRECTORY_IS_TOO_LONG\" +
                                    @"TO_TEST_DIRECTORY_IS_TOO_LONG\TO_TEST_DIRECTORY_IS_TOO_LONG\TO_TEST_DIRECTORY_IS_TOO_LONG\" +
                                    @"TO_TEST_DIRECTORY_IS_TOO_LONG\TO_TEST_DIRECTORY_IS_TOO_LONG\TO_TEST_DIRECTORY_IS_TOO_LONG\" +
                                    @"TO_TEST_DIRECTORY_IS_TOO_LONG\TO_TEST_DIRECTORY_IS_TOO_LONG\TO_TEST_DIRECTORY_IS_TOO_LONG";
                PluginManager<IExtensionMock> extensionManager = new PluginManager<IExtensionMock>(pluginPath);
            });
        }

        [Test]
        public void Test_GetCatalog_Fail_Directory_Unauthorized()
        {
            Assert.Throws<PluginLoadException>(() =>
            {
                //This case wrap UnauthorizedAccessException with PluginLoadException
                string pluginPath = _tempPluginDirectory;
                // Add the access control entry to the directory.
                AddDirectorySecurity(pluginPath, Environment.UserName, FileSystemRights.ReadData, AccessControlType.Deny);

                PluginManager<IExtensionMock> extensionManager = new PluginManager<IExtensionMock>(pluginPath);
            });
        }

        
        #endregion

        #region [Private Methods]

        // Adds an ACL entry on the specified directory for the specified account.
        private void AddDirectorySecurity(string FileName, string Account, FileSystemRights Rights, AccessControlType ControlType)
        {
            // Create a new DirectoryInfo object.
            DirectoryInfo dInfo = new DirectoryInfo(FileName);

            // Get a DirectorySecurity object that represents the 
            // current security settings.
            DirectorySecurity dSecurity = dInfo.GetAccessControl();

            // Add the FileSystemAccessRule to the security settings. 
            dSecurity.AddAccessRule(new FileSystemAccessRule(Account,
                                                            Rights,
                                                            ControlType));

            // Set the new access settings.
            dInfo.SetAccessControl(dSecurity);

        }

        // Removes an ACL entry on the specified directory for the specified account.
        private void RemoveDirectorySecurity(string FileName, string Account, FileSystemRights Rights, AccessControlType ControlType)
        {
            // Create a new DirectoryInfo object.
            DirectoryInfo dInfo = new DirectoryInfo(FileName);

            // Get a DirectorySecurity object that represents the 
            // current security settings.
            DirectorySecurity dSecurity = dInfo.GetAccessControl();

            // Add the FileSystemAccessRule to the security settings. 
            dSecurity.RemoveAccessRule(new FileSystemAccessRule(Account,
                                                            Rights,
                                                            ControlType));

            // Set the new access settings.
            dInfo.SetAccessControl(dSecurity);

        }

        #endregion

        #region [LoadPluginNames]

        [Test]
        public void Test_LoadPluginNames_Success()
        {
            PluginManager<IExtensionMock> extensionManager = new PluginManager<IExtensionMock>(AppDomain.CurrentDomain.BaseDirectory);

            //Call method LoadPluginNames to get the list of plugins name.
            IEnumerable<string> pluginNames = extensionManager.LoadPluginNames();

            Assert.AreEqual(3, pluginNames.Count());
            Assert.AreEqual("ExtensionMock1", pluginNames.Single(p => p == "ExtensionMock1"));
            Assert.AreEqual("ExtensionMock2", pluginNames.Single(p => p == "ExtensionMock2"));
            Assert.AreEqual("ExtensionMock3", pluginNames.Single(p => p == "ExtensionMock3"));
        }

        [Test]
        public void Test_LoadPluginNames_Exception_LoadFailed()
        {
            //Assert.Throws<PluginLoadException>(() =>
            //{
            //    PluginManager<IExtensionMock> extensionManager = new PluginManager<IExtensionMock>(AppDomain.CurrentDomain.BaseDirectory);

            //    //MsTest.PrivateObject po = new MsTest.PrivateObject(extensionManager);
            //    //po.SetField("_catalog", null);

            //    ////Call method LoadPluginNames to get the list of plugins name.
            //    //IEnumerable<string> pluginNames = extensionManager.LoadPluginNames();
            //});
        }

        [Test]
        public void Test_LoadPluginNames_Fail_Disposed()
        {
            //Assert.Throws<ObjectDisposedException>(() =>
            //{
            //    PluginManager<IExtensionMock> extensionManager = new PluginManager<IExtensionMock>(AppDomain.CurrentDomain.BaseDirectory);

            //    //MsTest.PrivateObject po = new MsTest.PrivateObject(extensionManager);
            //    //po.SetField("_disposed", true);

            //    ////Call method LoadPluginNames to get the list of plugins name.
            //    //IEnumerable<string> pluginNames = extensionManager.LoadPluginNames();
            //});
        }

        #endregion

        #region [Dispose]

        [Test]
        public void Test_Dispose_Success()
        {
            PluginManager<IExtensionMock> extensionManager = new PluginManager<IExtensionMock>(AppDomain.CurrentDomain.BaseDirectory);

            Assert.IsNotNull(extensionManager);

            extensionManager.Dispose();

            // call two times to check method called twice.
            extensionManager.Dispose();
        }

        #endregion

        #region [LoadPlugin]

        [Test]
        public void Test_LoadPlugin_Success()
        {
            PluginManager<IExtensionMock> extensionManager = new PluginManager<IExtensionMock>(AppDomain.CurrentDomain.BaseDirectory);

            // Call method LoadPlugin to get the plugin object
            PluginObject<IExtensionMock> extension = extensionManager.LoadPlugin("ExtensionMock1");

            Assert.IsNotNull(extension);
            Assert.AreEqual("This is ExtensionMock1", extension.Plugin.GetData());
        }

        #endregion

        #region [LoadPlugins]

        [Test]
        public void Test_LoadPlugins_Success_GetOne()
        {
            PluginManager<IExtensionMock> extensionManager = new PluginManager<IExtensionMock>(AppDomain.CurrentDomain.BaseDirectory);

            // Call method LoadPlugin to get the plugin object
            IEnumerable<PluginObject<IExtensionMock>> extensions = extensionManager.LoadPlugins("ExtensionMock1");

            Assert.IsNotNull(extensions);
            Assert.AreEqual("This is ExtensionMock1", extensions.First().Plugin.GetData());
        }

        [Test]
        public void Test_LoadPlugins_Success_GetMany()
        {
            PluginManager<IExtensionMock> extensionManager = new PluginManager<IExtensionMock>(AppDomain.CurrentDomain.BaseDirectory);

            // Call method LoadPlugin to get the plugin object
            IEnumerable<PluginObject<IExtensionMock>> extensions = extensionManager.LoadPlugins("ExtensionMock1", "ExtensionMock2");

            Assert.IsNotNull(extensions);
            Assert.AreEqual("This is ExtensionMock1", extensions.First().Plugin.GetData());
            Assert.AreEqual("This is ExtensionMock2", extensions.Single(s => s.Name.Equals("ExtensionMock2")).Plugin.GetData());
        }

        [Test]
        public void Test_LoadPlugins_Success_GetAll()
        {
            PluginManager<IExtensionMock> extensionManager = new PluginManager<IExtensionMock>(AppDomain.CurrentDomain.BaseDirectory);

            // Call method LoadPlugin to get the plugin object
            IEnumerable<PluginObject<IExtensionMock>> extensions = extensionManager.LoadPlugins();

            Assert.IsNotNull(extensions);
            Assert.AreEqual(3, extensions.Count());
            Assert.AreEqual("This is ExtensionMock1", extensions.First().Plugin.GetData());
            Assert.AreEqual("This is ExtensionMock2", extensions.Single(s => s.Name.Equals("ExtensionMock2")).Plugin.GetData());
            Assert.AreEqual("This is ExtensionMock3", extensions.Single(s => s.Name.Equals("ExtensionMock3")).Plugin.GetData());
        }

        [Test]
        public void Test_LoadPlugins_Success_GetAll_Null()
        {
            PluginManager<IExtensionMock> extensionManager = new PluginManager<IExtensionMock>(AppDomain.CurrentDomain.BaseDirectory);

            // Call method LoadPlugin to get the plugin object
            IEnumerable<PluginObject<IExtensionMock>> extensions = extensionManager.LoadPlugins(null);

            Assert.IsNotNull(extensions);
            Assert.AreEqual(3, extensions.Count());
            Assert.AreEqual("This is ExtensionMock1", extensions.First().Plugin.GetData());
            Assert.AreEqual("This is ExtensionMock2", extensions.Single(s => s.Name.Equals("ExtensionMock2")).Plugin.GetData());
            Assert.AreEqual("This is ExtensionMock3", extensions.Single(s => s.Name.Equals("ExtensionMock3")).Plugin.GetData());
        }

        [Test]
        public void Test_LoadPlugins_Fail_InvalidOperation()
        {
            //Assert.Throws<PluginLoadException>(() =>
            //{
            //    //This test case will throw error because we have same plugin name are "ExtensionMock2_1".
            //    //Look at PluginMock.cs it have 2 class "ExtensionMock21" and "ExtensionMock22"
            //    //both they have attribute Name "ExtensionMock2_1".

            //    PluginManager<IExtensionMock2> extensionManager = new PluginManager<IExtensionMock2>(AppDomain.CurrentDomain.BaseDirectory);

            //    // Call method LoadPlugin to get the plugin object
            //    IEnumerable<PluginObject<IExtensionMock2>> extensions = extensionManager.LoadPlugins("ExtensionMock2_1");
            //});
        }

        [Test]
        public void Test_LoadPlugins_Fail_Disposed()
        {
            //Assert.Throws<ObjectDisposedException>(() =>
            //{
            //    PluginManager<IExtensionMock> extensionManager = new PluginManager<IExtensionMock>(AppDomain.CurrentDomain.BaseDirectory);

            //    //MsTest.PrivateObject po = new MsTest.PrivateObject(extensionManager);
            //    //po.SetField("_disposed", true);

            //    //// Call method LoadPlugin to get the plugin object
            //    //IEnumerable<PluginObject<IExtensionMock>> extensions = extensionManager.LoadPlugins("ExtensionMock1");
            //});
        }
        #endregion

    }
}
