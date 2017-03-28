﻿using BND.Services.Payments.eMandates.Entities;
using eMandates.Merchant.Library;
using eMandates.Merchant.Library.Configuration;

namespace eMandates.Merchant.Website.Models
{
    public class NewMandateResponseViewModel : BaseViewModel
    {
        //public NewMandateResponse Source { get; set; }

        /// <summary>
        /// The transaction ID
        /// </summary>
        public string TransactionId { get; set; }
        /// <summary>
        /// Maximum amount. Not allowed for Core, optional for B2B.
        /// </summary>
        public Instrumentation Instrumentation { get; set; }

        public TransactionResponseModel Source { get; set; }
    }
}