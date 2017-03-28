using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eMandates.Merchant.Website.Models
{
    public class BaseViewModel
    {
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
    }
}