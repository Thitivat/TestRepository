using System.Web.Http;
using BND.Websites.BackOffice.SanctionListsManagement.Entities;

namespace BND.Websites.BackOffice.SanctionListsManagement.Web.Controllers.Api.SanctionLists
{
    public class LogsController : SanctionListsBaseController
    {
        /// <summary>
        /// Returns list of logs.
        /// </summary>
        /// <param name="listTypeId">List ype id (optional)</param>
        /// <param name="offset">Offset(optional)</param>
        /// <param name="limit">Limit of returned set(optional)</param>
        /// <returns></returns>
        [HttpGet]
        [System.Web.Mvc.Route("api/logs/{listTypeId:int?}")]
        public object GetLogs(int? listTypeId = null, int? offset = null, int? limit = null)
        {
            Log[] logs = _sanctionListsBll.LogsGet(listTypeId, offset, limit);
            int logsCount = _sanctionListsBll.LogsCount(listTypeId);

            return new {logs, LogsCount = logsCount };
        }
    }
}
