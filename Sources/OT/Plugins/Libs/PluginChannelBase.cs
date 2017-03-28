using System.IO;

namespace BND.Services.Security.OTP.Plugins
{
    public abstract class PluginChannelBase : IPluginChannel
    {
        private const string PlaceHolderRefCode = "{RefCode}";
        private const string PlaceHolderOtpCode = "{Otp}";

        public abstract PluginChannelResult Send(ChannelParams parameters);

        protected string GetCurrentDirectory()
        {
            return Path.GetDirectoryName((GetType().Assembly).Location);
        }

        protected void SetSendingMessage(ChannelParams parameters)
        {
            parameters.Message = parameters.Message.Replace(PlaceHolderRefCode, parameters.RefCode)
                                                   .Replace(PlaceHolderOtpCode, parameters.OtpCode);
        }
    }
}
