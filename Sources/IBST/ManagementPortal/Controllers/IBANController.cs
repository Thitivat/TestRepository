using BND.Services.IbanStore.ManagementPortal.Attribute;
using BND.Services.IbanStore.ManagementPortal.Helper;
using BND.Services.IbanStore.Models;
using BND.Services.IbanStore.Service.Bll.Interfaces;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using IBan = BND.Services.IbanStore.Service.Bll.IbanManager;

namespace BND.Services.IbanStore.ManagementPortal.Controllers
{
    /// <summary>
    /// Class IBANController provides function to manipulate with iban data.
    /// </summary>
    [ErrorHandler]
    public class IBANController : Controller
    {

        #region [Fields]
        /// <summary>
        /// The iban manager.
        /// </summary>
        private IIbanManager _iban;
        /// <summary>
        /// The token.
        /// </summary>
        private string token = UserHelper.GetUserName();
        /// <summary>
        /// The context
        /// </summary>
        private string context = "WEB";
        #endregion


        #region [Constructor]
        /// <summary>
        /// Initializes a new instance of the <see cref="IBANController"/> class.
        /// </summary>
        public IBANController()
        {
            _iban = new IBan(token, context, ConfigurationManager.AppSettings.Get("ConnectionString"));
        }
        #endregion


        #region [Public Method]
        /// <summary>
        /// The main page.
        /// </summary>
        /// <param name="page">The page. current page number</param>
        /// <returns>ActionResult.</returns>
        public ActionResult Index(int? page)
        {
            //if page number equal zero set as one
            int currentPage = page ?? 1;
            // table paging
            int offSet = (currentPage - 1) * 10;

            int total = 0;
            // call service method
            List<Iban> model = _iban.Get(offSet, 10, out total).ToList();

            //table paging count
            total = (total / 10) + 1;

            //set total record paging
            ViewBag.TotalPage = total;

            //current paging
            ViewBag.Currentpage = currentPage;

            return View(model);
        }
        #endregion


        #region [Api]
        /// <summary>
        /// Gets the iban.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="iban">The iban.</param>
        /// <param name="page">The page.</param>
        /// <returns>JsonResult.</returns>
        public JsonResult GetIban(string status, string iban, int? page)
        {
            //if page number equal zero set as one
            int currentPage = page ?? 1;
            // table paging
            int offSet = (currentPage - 1) * 10;

            int total = 0;
            // call service method
            List<Iban> model = _iban.Get(offSet, 10, out total, status, iban).ToList();

            //table paging count
            total = (total / 10) + 1;

            return Json(new { Success = true, Data = model, TotalPage = total, Currentpage = currentPage }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}