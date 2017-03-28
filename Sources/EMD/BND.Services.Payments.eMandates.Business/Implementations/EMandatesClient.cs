using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BND.Services.Payments.eMandates.Business.Interfaces;
using BND.Services.Payments.eMandates.Business.Models;
using eMandates.Merchant.Library;
using eMandates.Merchant.Library.Configuration;
using eMandates.Merchant.Library.Misc;
using eMandates.Merchant.Library.XML.Schemas.iDx;

namespace BND.Services.Payments.eMandates.Business.Implementations
{
    public class EMandatesClient : IEMandatesClient
    {
        private readonly CoreCommunicator _coreCommunicator;

        public EMandatesClient(CoreCommunicator coreCommunicator)
        {
            _coreCommunicator = coreCommunicator;
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DebtorBank, DebtorBankModel>();                
                cfg.CreateMap<DirectoryResponse, DirectoryResponseModel>()
                   .ForMember(s => s.DebtorBanks, c => c.MapFrom(m => m.DebtorBanks));
                cfg.CreateMap<EnumSequenceType, SequenceType>().ReverseMap();
                cfg.CreateMap<ErrorResponse, ErrorResponseModel>().ReverseMap();
                cfg.CreateMap<NewMandateRequest, NewMandateRequestModel>().ReverseMap();
                cfg.CreateMap<NewMandateResponse, NewMandateResponseModel>()
                    .ForMember(x => x.Error, c => c.ResolveUsing(v => Mapper.Map<ErrorResponseModel>(v.Error)));
                cfg.CreateMap<StatusResponse, StatusResponseModel>(); 
            });
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            //Mapper.AssertConfigurationIsValid();
        }

        /// <summary>
        /// Sends the directory request to get debtor bank list from eMandates service.
        /// </summary>
        /// <returns>DirectoryResponseModel.</returns>
        public DirectoryResponseModel SendDirectoryRequest()
        {
            // Call eMandates
            DirectoryResponse response = _coreCommunicator.Directory();

            // Map response
            DirectoryResponseModel directory = Mapper.Map<DirectoryResponseModel>(response);


            return directory;
        }

        public NewMandateResponseModel SendTransactionRequest(NewMandateRequestModel newMandateRequest)
        {
            // Map request
            NewMandateRequest nmr = Mapper.Map<NewMandateRequest>(newMandateRequest);
            
            // Call eMandates
            NewMandateResponse newMandateResponse = _coreCommunicator.NewMandate(nmr);

            // Map response
            NewMandateResponseModel nmrm = Mapper.Map<NewMandateResponseModel>(newMandateResponse);

            return nmrm;
        }

        public StatusResponseModel SendStatusRequest(string transactionId)
        {
            // Map request
            StatusRequest sr = new StatusRequest(transactionId);

            // Call eMandates
            StatusResponse statusResponse = _coreCommunicator.GetStatus(sr);

            // Map response
            StatusResponseModel response = Mapper.Map<StatusResponseModel>(statusResponse);

            return response;
        }


        public string GenerateMessageId()
        {
            return MessageIdGenerator.New();
        }
    }
}
