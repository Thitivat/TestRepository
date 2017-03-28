using BND.Services.Security.OTP.Mef;

namespace BND.Services.Security.OTP.ChannelFactoryTest
{
    public class PluginMetaDataMock : IPluginMetaData
    {
        public string Name { get; private set; }

        public string Description { get; private set; }

        public PluginMetaDataMock(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
