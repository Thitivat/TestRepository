using BND.Services.Payments.eMandates.Entities;
using BND.Services.Payments.eMandates.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BND.Services.Payments.eMandates.Business.Models;
using eMandates.Merchant.Library;
using eMandates.Merchant.Library.XML.Schemas.iDx;

namespace BND.Services.Payments.eMandates.Business.Interfaces
{
    public interface IEMandatesClient
    {
        DirectoryResponseModel SendDirectoryRequest();

        NewMandateResponseModel SendTransactionRequest(NewMandateRequestModel newMandateRequest);

        StatusResponseModel SendStatusRequest(string transactionId);

        string GenerateMessageId();
    }
}
