using AutoMapper;
using BND.Services.IbanStore.Models;
using BND.Services.IbanStore.Service.Dal.Pocos;

namespace BND.Services.IbanStore.Service.Bll
{
    public class MapperConfig
    {
        public static void CreateMapper()
        {
             // Sets mapping of all models and pocos.
            Mapper.Initialize(conf =>
            {
                conf.CreateMap<p_Iban, Iban>().ForMember(d => d.IbanId, o => o.MapFrom(s => s.IbanId))
                                               .ForMember(d => d.Uid, o => o.MapFrom(s => s.Uid))
                                               .ForMember(d => d.UidPrefix, o => o.MapFrom(s => s.UidPrefix))
                                               .ForMember(d => d.ReservedTime, o => o.MapFrom(s => s.ReservedTime))
                                               .ForMember(d => d.CountryCode, o => o.MapFrom(s => s.CountryCode))
                                               .ForMember(d => d.CheckSum, o => o.MapFrom(s => s.CheckSum))
                                               .ForMember(d => d.BankCode, o => o.MapFrom(s => s.BankCode))
                                               .ForMember(d => d.Bban, o => o.MapFrom(s => s.Bban))
                                               .ForMember(d => d.BbanFileName, o => o.MapFrom(s => s.BbanFile.Name));

                conf.CreateMap<Iban, p_Iban>().ForMember(d => d.IbanId, o => o.MapFrom(s => s.IbanId))
                                               .ForMember(d => d.Uid, o => o.MapFrom(s => s.Uid));

                conf.CreateMap<p_Iban, Iban>();
            });
        }
    }
}
