
namespace BND.Services.Security.OTP.Plugins.LibsTest
{
    public class TestPluginChannel : PluginChannelBase
    {
        public override PluginChannelResult Send(ChannelParams parameters)
        {
            base.SetSendingMessage(parameters);
            base.GetCurrentDirectory();
            return null;
        }
    }
}
