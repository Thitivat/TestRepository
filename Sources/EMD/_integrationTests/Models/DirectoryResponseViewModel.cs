using BND.Services.Payments.eMandates.Entities;
using eMandates.Merchant.Library;
using eMandates.Merchant.Library.Configuration;
using System.Collections.Generic;

namespace eMandates.Merchant.Website.Models
{
    public class DirectoryResponseViewModel : BaseViewModel
    {
        //public DirectoryResponse Source { get; set; }

        /// <summary>
        /// Maximum amount. Not allowed for Core, optional for B2B.
        /// </summary>
        public Instrumentation Instrumentation { get; set; }

        public IEnumerable<DirectoryModel> Source { get; set; }
    }
}