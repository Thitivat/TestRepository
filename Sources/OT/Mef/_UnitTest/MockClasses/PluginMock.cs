using System;
using System.ComponentModel.Composition;

namespace BND.Services.Security.OTP.MefTest
{
    [MockExport("ExtensionMock1", "My Plugin 1")]
    internal class ExtensionMock1 : IExtensionMock
    {
        public string GetData()
        {
            return "This is ExtensionMock1";
        }
    }

    [MockExport("ExtensionMock2", "My Plugin 2")]
    internal class ExtensionMock2 : IExtensionMock
    {
        public string GetData()
        {
            return "This is ExtensionMock2";
        }
    }

    [MockExport("ExtensionMock3", "My Plugin 3")]
    internal class ExtensionMock3 : IExtensionMock
    {
        public string GetData()
        {
            return "This is ExtensionMock3";
        }
    }

    

    /// <summary>
    /// Represent Extension interface
    /// </summary>
    internal interface IExtensionMock
    {
        string GetData();
    }

    

    /// <summary>
    /// Represent Extension export attribute
    /// </summary>
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    internal class MockExportAttribute : ExportAttribute
    {
        public string Name { get; private set; }

        public string Description { get; private set; }

        [ImportingConstructor]
        public MockExportAttribute(string pluginName, string description)
            : base(typeof(IExtensionMock))
        {
            if (String.IsNullOrEmpty(pluginName))
            {
                throw new ArgumentNullException("pluginName");
            }
            if (String.IsNullOrEmpty(description))
            {
                throw new ArgumentNullException("description");
            }

            Name = pluginName;
            Description = description;
        }
    }


    [MockExport2("ExtensionMock2_1", "My Plugin 2/1")]
    internal class ExtensionMock21 : IExtensionMock2
    {
    }

    [MockExport2("ExtensionMock2_1", "My Plugin 2/2")]
    internal class ExtensionMock22 : IExtensionMock2
    {
    }

    internal interface IExtensionMock2
    {
    }


    /// <summary>
    /// Represent Extension export attribute
    /// </summary>
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    internal class MockExport2Attribute : ExportAttribute
    {
        public string Name { get; private set; }

        public string Description { get; private set; }

        [ImportingConstructor]
        public MockExport2Attribute(string pluginName, string description)
            : base(typeof(IExtensionMock2))
        {
            if (String.IsNullOrEmpty(pluginName))
            {
                throw new ArgumentNullException("pluginName");
            }
            if (String.IsNullOrEmpty(description))
            {
                throw new ArgumentNullException("description");
            }

            Name = pluginName;
            Description = description;
        }
    }


    [MockExport3("ExtensionMock3_1", "My Plugin 3/1")]
    internal class ExtensionMock31 : IExtensionMock3
    {
    }

    //This is not class will make unit test error
    internal class IExtensionMock3
    {
    }

    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    internal class MockExport3Attribute : ExportAttribute
    {
        public string Name { get; private set; }

        public string Description { get; private set; }

        [ImportingConstructor]
        public MockExport3Attribute(string pluginName, string description)
            : base(typeof(IExtensionMock3))
        {
            if (String.IsNullOrEmpty(pluginName))
            {
                throw new ArgumentNullException("pluginName");
            }
            if (String.IsNullOrEmpty(description))
            {
                throw new ArgumentNullException("description");
            }

            Name = pluginName;
            Description = description;
        }
    }
}