using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BND.Services.IbanStore.ManagementPortal.Models
{
    public class EmailBody
    {
        public DateTime SendDate { get; set; }
        public string Message { get; set; }
        public string SendName { get; set; }
        public string ApproveUrl { get; set; }
    }
}