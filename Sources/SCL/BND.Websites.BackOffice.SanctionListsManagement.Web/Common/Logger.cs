using System;
using BND.Websites.BackOffice.SanctionListsManagement.Business.Implementations;
using BND.Websites.BackOffice.SanctionListsManagement.Entities;

namespace BND.Websites.BackOffice.SanctionListsManagement.Web.Common
{
    /// <summary>
    /// Class Logger responsible to save all activity to database, to make the easy way for another class to save log.
    /// This class follow the Singleton design pattern for log.
    /// </summary>
    public class Logger
    {
        #region [Fields]

        /// <summary>
        /// The instance object of SanctionListsBll class.
        /// </summary>
        private SanctionListsBll _sanctionListBll;

        /// <summary>
        /// The instance object of Logger class.
        /// </summary>
        private static Logger _loggerInstance;

        #endregion

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns>Logger.</returns>
        public static Logger GetLogger(string connectionString)
        {
            if (_loggerInstance == null)
            {
                _loggerInstance = new Logger(connectionString);
            }

            return _loggerInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        private Logger(string connectionString)
        {
            _sanctionListBll = new SanctionListsBll(connectionString);  
        }

        /// <summary>
        /// Logs the activity to database.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="actionTypeId">The action type identifier.</param>
        /// <param name="listTypeId">The list type identifier.</param>
        /// <param name="description">The description.</param>
        public void LogActivity(string username, int actionTypeId, int? listTypeId, string description)
        {
            Log log = new Log()
            {
                ActionType = new ActionType() { ActionTypeId = actionTypeId },
                ListType = (listTypeId != null) ? new ListType() { ListTypeId = (Int32)listTypeId } : null,
                Description = description,
                LogDate = DateTime.UtcNow,
                Username = username
            };

            _sanctionListBll.LogsAdd(log);
        }

    }
}