using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BND.Services.Payments.eMandates.Domain.Interfaces
{
    /// <summary>
    /// Interface ILogRepository performing log table in database.
    /// </summary>
    public interface ILogRepository
    {
        /// <summary>
        /// Inserts the specified log to database.
        /// </summary>
        /// <param name="log">The log.</param>
        /// <returns>total number of affected row(s)</returns>
        int Insert(Models.Log log);
    }
}
