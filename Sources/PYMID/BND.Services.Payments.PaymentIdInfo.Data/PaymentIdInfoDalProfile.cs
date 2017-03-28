using AutoMapper;
using BND.Services.Payments.PaymentIdInfo.Entities;
using BND.Services.Payments.PaymentIdInfo.Models;

namespace BND.Services.Payments.PaymentIdInfo.Data
{
    /// <summary>
    /// Class PaymentInfoProfile.
    /// </summary>
    public class PaymentIdInfoDalProfile : Profile
    {
        /// <summary>
        /// Override this method in a derived class and call the CreateMap method to associate that map with this profile.
        /// Avoid calling the <see cref="T:AutoMapper.Mapper" /> class from this method.
        /// </summary>
        protected override void Configure()
        {
            CreateMap<p_iDealTransaction, iDealTransaction>();
            CreateMap<p_iDealTransactionStatusHistory, iDealTransactionStatusHistory>();
        }
    }
}
