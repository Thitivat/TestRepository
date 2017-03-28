using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BND.Services.Payments.iDeal.IntegrationTests.ViewModels
{
    public class TransactionStatusRequestViewModel
    {
        public string TransactionId { get; set; }
        public string EntranceCode { get; set; }
        public string ApiToken { get; set; }
    }
}