using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BND.Services.Payments.eMandates.Domain.Interfaces
{
    /// <summary>
    /// Interface ISettingRepository
    /// </summary>
    public interface ISettingRepository
    {
        /// <summary>
        /// Gets the value by key.
        /// </summary>
        /// <param name="key">The key for retrieve data.</param>
        /// <returns>System.String.</returns>
        string GetValueByKey(string key);
    }
}
