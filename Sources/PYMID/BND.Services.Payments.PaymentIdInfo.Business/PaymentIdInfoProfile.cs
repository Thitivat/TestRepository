using AutoMapper;
using BND.Services.Payments.PaymentIdInfo.Entities;

namespace BND.Services.Payments.PaymentIdInfo.Business
{
    /// <summary>
    /// Class PaymentInfoProfile.
    /// </summary>
    public class PaymentIdInfoProfile : Profile
    {
        /// <summary>
        /// Override this method in a derived class and call the CreateMap method to associate that map with this profile.
        /// Avoid calling the <see cref="T:AutoMapper.Mapper" /> class from this method.
        /// </summary>
        protected override void Configure()
        {
            CreateMap<iDealTransaction, PaymentIdInfoModel>()
                .ForMember(d => d.TransactionDate, s => s.MapFrom(x => x.TransactionResponseDateTimestamp))
                .ForMember(d => d.TransactionType, s => s.MapFrom(x => x.PaymentType))
                .ForMember(d => d.TransactionId, s => s.MapFrom(x => x.TransactionID))
                .ForMember(d => d.BndIban, s => s.MapFrom(x => x.BNDIBAN))
                .ForMember(d => d.SourceAccountHolderName, s => s.MapFrom(x => x.ConsumerName))
                .ForMember(d => d.SourceIban, s => s.MapFrom(x => x.ConsumerIBAN))
                .ForMember(d => d.SourceBic, s => s.MapFrom(x => x.ConsumerBIC));
        }
    }
}
