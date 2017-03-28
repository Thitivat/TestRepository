using System.Collections.Generic;
using System.Web.Mvc;
using BND.Websites.BackOffice.SanctionListsManagement.Entities;
using Newtonsoft.Json;

namespace BND.Websites.BackOffice.SanctionListsManagement.Web.Controllers.Mvc.SanctionLists
{
    /// <summary>
    /// ActivityLog controller for managing and displaying system activity logs. It extends <see cref="SanctionListsBaseController"/>
    /// </summary>
	public class ActivityLogsController : SanctionListsBaseController
	{
        /// <summary>
        /// Action that handles displaying activity logs
        /// </summary>
        /// <param name="listTypeId">Specify list type of which logs will be displayed. If it is empty, all logs are displayed.</param>
        /// <returns>Rendered view</returns>
		[HttpGet]
        [Route("SanctionLists/ActivityLog/{listTypeId:int}", Name = "ActivityLogById")]
        [Route("SanctionLists/ActivityLog/All", Name = "ActivityLogAll")]
		public ActionResult Index(int? listTypeId)
		{
            try
            {
                List<object> SelectLogsOptions = new List<object>();

                ListType[] listTypes = base._sanctionListsBll.ListTypesGet();

                SelectLogsOptions.Add(new { id = "All", value = "All" });
                foreach (ListType lt in listTypes)
                {
                    SelectLogsOptions.Add(new { id = lt.ListTypeId.ToString(), value = lt.Name });
                }

                ViewBag.Logs = JsonConvert.SerializeObject(base._sanctionListsBll.LogsGet(listTypeId, 0, 25));
                string count = JsonConvert.SerializeObject(base._sanctionListsBll.LogsCount(listTypeId));
                ViewBag.LogsCount = count;
                ViewBag.SelectLogsOptions = JsonConvert.SerializeObject(SelectLogsOptions.ToArray());
                ViewBag.CurrentList = JsonConvert.SerializeObject((listTypeId == null) ? "All" : listTypeId.ToString());
                ViewBag.ListTypeDescription = "";
            }
            catch (System.Exception ex)
            {
                base.Log(ex);
            }

			return View();
		}
	}
}