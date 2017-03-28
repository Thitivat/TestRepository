using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BND.Services.Payments.iDeal.Models
{
    /// <summary>
    /// Class DirectoryModel that contains the properties of Directory data from iDeal.
    /// </summary>
    public class DirectoryModel
    {
        /// <summary>
        /// Gets or sets the country names in the official languages of the country, separated by 
        /// a '/' symbol (e.g. 'België/Belgique')
        /// </summary>
        /// <value>The country names.</value>
        public string CountryNames { get; set; }

        /// <summary>
        /// Gets or sets the issuers.
        /// </summary>
        /// <value>The issuers.</value>
        public List<IssuerModel> Issuers { get; set; }

    }
}
