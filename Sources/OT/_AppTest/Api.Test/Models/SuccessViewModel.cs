using BND.Services.Security.OTP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BND.Services.Security.OTP.ApiTest.Models
{
    /// <summary>
    /// Class SuccessViewModel contains the OtpModel object that return from verify otp process to show on Success page.
    /// </summary>
    public class SuccessViewModel
    {
        /// <summary>
        /// The success result model.
        /// </summary>
        public OtpModel SuccessModel { get; set; }
    }
}