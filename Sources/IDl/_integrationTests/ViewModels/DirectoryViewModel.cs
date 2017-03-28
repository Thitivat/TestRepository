using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BND.Services.Payments.iDeal.IntegrationTests.ViewModels
{
    /// <summary>
    /// Class DirectoryViewModel to reflect data that Directory View Needed
    /// </summary>
    public class DirectoryViewModel
    {
        /// <summary>
        /// Gets or sets the directories.
        /// </summary>
        /// <value>The directories.</value>
        public IEnumerable<BND.Services.Payments.iDeal.Models.DirectoryModel> Directories { get; set; }
    }
}