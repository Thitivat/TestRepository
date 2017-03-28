using BND.Services.Payments.iDeal.Dal.Pocos;

namespace BND.Services.Payments.iDeal.Dal
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
        int Insert(p_Log log);
    }
}
