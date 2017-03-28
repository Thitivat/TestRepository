using System.Web.Mvc;

namespace BND.Websites.BackOffice.SanctionListsManagement.Web.Controllers.Mvc.Dashboard
{
    /// <summary>
    /// Contains logic of dashboard of KYC Consoles
    /// </summary>
    public class DashboardController : SanctionListsBaseController
    {
        /// <summary>
        /// Main page
        /// </summary>
        /// <returns>Rendered view</returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}