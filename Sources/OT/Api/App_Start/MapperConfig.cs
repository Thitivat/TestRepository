using AutoMapper;
using BND.Services.Security.OTP.Dal.Pocos;
using BND.Services.Security.OTP.Models;
using BND.Services.Security.OTP.Plugins;

namespace BND.Services.Security.OTP.Api
{
    /// <summary>
    /// Class MapperConfig is a class for initializing AutoMapper component.
    /// </summary>
    public static class MapperConfig
    {
        /// <summary>
        /// Initializes AutoMapper component.
        /// </summary>
        public static void Register()
        {
            // Sets mapping of all models and pocos.
            Mapper.Initialize(conf => {
                conf.CreateMap<OtpRequestModel, OneTimePassword>().ForMember(d => d.ChannelType, o => o.MapFrom(s => s.Channel.Type))
                                                                  .ForMember(d => d.ChannelSender, o => o.MapFrom(s => s.Channel.Sender))
                                                                  .ForMember(d => d.ChannelAddress, o => o.MapFrom(s => s.Channel.Address))
                                                                  .ForMember(d => d.ChannelMessage, o => o.MapFrom(s => s.Channel.Message));

                conf.CreateMap<OneTimePassword, OtpModel>().ForMember(d => d.Id, o => o.MapFrom(s => s.OtpId))
                                                           .AfterMap((s, d) => {
                                                                                   d.Channel = new ChannelModel
                                                                                   {
                                                                                       Type = s.ChannelType,
                                                                                       Sender = s.ChannelSender,
                                                                                       Address = s.ChannelAddress,
                                                                                       Message = s.ChannelMessage
                                                                                   };
                                                           });

                conf.CreateMap<OneTimePassword, ChannelParams>().ForMember(d => d.Type, o => o.MapFrom(s => s.ChannelType))
                                                                .ForMember(d => d.Sender, o => o.MapFrom(s => s.ChannelSender))
                                                                .ForMember(d => d.Address, o => o.MapFrom(s => s.ChannelAddress))
                                                                .ForMember(d => d.Message, o => o.MapFrom(s => s.ChannelMessage))
                                                                .ForMember(d => d.OtpCode, o => o.MapFrom(s => s.Code));

                conf.CreateMap<OneTimePassword, OtpResultModel>();
            });
        }
    }
}