using BND.Services.Security.OTP.Mef;

namespace BND.Services.Security.OTP.MefTest
{
    public class PluginMetadataMock : IPluginMetaData
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public PluginMetadataMock(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}