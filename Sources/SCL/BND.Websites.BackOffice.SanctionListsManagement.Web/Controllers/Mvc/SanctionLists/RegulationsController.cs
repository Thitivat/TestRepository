using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BND.Websites.BackOffice.SanctionListsManagement.Entities;
using Newtonsoft.Json;

namespace BND.Websites.BackOffice.SanctionListsManagement.Web.Controllers.Mvc.SanctionLists
{
    public class RegulationsController : SanctionListsBaseController
    {
        [HttpGet]
        [Route("SanctionLists/{listTypeId:int}/Regulations")]
        public ActionResult Index(int listTypeId)
        {
            try
            {
                List<object> selectRegulationsOptions = new List<object>();

                ListType[] listTypes = base._sanctionListsBll.ListTypesGet();

                foreach (ListType lt in listTypes)
                {
                    selectRegulationsOptions.Add(new { id = lt.ListTypeId.ToString(), value = lt.Name });
                }
                ViewBag.SelectRegulationsOptions = JsonConvert.SerializeObject(selectRegulationsOptions.ToArray());
                ViewBag.CurrentList = listTypes.First(f => f.ListTypeId == listTypeId);
            }
            catch (System.Exception ex)
            {
                base.Log(ex);
            }

            return View();
        }
    }
}