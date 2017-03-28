using System.Web.Mvc;

namespace BND.Services.IbanStore.ManagementPortal.Controllers
{
    /// <summary>
    /// Class ErrorController.
    /// </summary>
    public class ErrorController : Controller
    {
        // GET: Error
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}