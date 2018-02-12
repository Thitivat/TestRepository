using System.Collections.Generic;

using BND.Services.Matrix.Entities.Interfaces;

namespace BND.Services.Matrix.Entities
{
    /// <summary>
    /// The class that encapsulates the list of values for the 'types' querystring parameter that can be additionally specified in an api request
    /// </summary>
    public class AccountTypeList : IQueryStringModel
    {
        /// <summary>
        /// Gets or sets the sysIds.
        /// </summary>
        public List<string> sysIds { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountTypeList"/> class.
        /// </summary>
        public AccountTypeList()
        {
            sysIds = new List<string>();
        }
    }
}
