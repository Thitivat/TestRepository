using System.Web.Mvc;

namespace BND.Services.Payments.iDeal.IntegrationTests.Controllers
{
    /// <summary>
    /// Class HomeController, the main controller of website.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Index page, the only one page in website.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}
