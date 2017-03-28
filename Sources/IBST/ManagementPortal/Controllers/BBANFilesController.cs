using BND.Services.IbanStore.ManagementPortal.Attribute;
using BND.Services.IbanStore.ManagementPortal.Helper;
using BND.Services.IbanStore.Models;
using BND.Services.IbanStore.Service.Bll;
using BND.Services.IbanStore.Service.Bll.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace BND.Services.IbanStore.ManagementPortal.Controllers
{
    /// <summary>
    /// Class BBANFilesController.
    /// </summary>
    [ValidateToken]
    [ErrorHandler]
    public class BBANFilesController : Controller
    {
        #region [Fields]
        /// <summary>
        /// The _bban file
        /// </summary>
        private IBbanFileManager _bbanFile;
        /// <summary>
        /// The token
        /// </summary>
        private string token = UserHelper.GetUserName();
        /// <summary>
        /// The context
        /// </summary>
        private string context = "WEB";
        #endregion

        #region [Constructor]
        /// <summary>
        /// Initializes a new instance of the <see cref="BBANFilesController"/> class.
        /// </summary>
        public BBANFilesController()
        {
            _bbanFile = new BbanFileManager(token, context, ConfigurationManager.AppSettings.Get("ConnectionString"), XmlHelper.ReadXmlToString("Config/emailSettings.xml"));
        }
        #endregion

        #region [Public Method]
        /// <summary>
        /// call BBANFiles page 
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
            List<BbanFile> model = _bbanFile.Get(offSet, 10, out total, null).ToList();

            //table paging count
            total = (total / 10) + 1;

            //set total record paging
            ViewBag.TotalPage = total;

            //current paging
            ViewBag.Currentpage = currentPage;

            return View(model);
        }

        /// <summary>
        /// routing bbanFile status try again action
        /// </summary>
        /// <param name="bbanFileId">The bban file identifier.</param>
        /// <param name="status">current status</param>
        /// <returns>ActionResult.</returns>
        /// <exception cref="System.Exception"></exception>
        public ActionResult RedirectStatus(int bbanFileId, string status)
        {
            if (status == EnumBbanFileStatus.Uploaded.ToString() || status == EnumBbanFileStatus.VerifiedFailed.ToString())
            {
                return RedirectToAction("BBANList", "UploadBBAN", new { BbanFileId = bbanFileId });
            }
            else if (status == EnumBbanFileStatus.UploadCanceled.ToString())
            {
                return RedirectToAction("Index", "UploadBBAN");
            }
            else if (status == EnumBbanFileStatus.Verified.ToString())
            {
                return RedirectToAction("Verification", "UploadBBAN", new { BbanFileId = bbanFileId });
            }

            throw new Exception("Invalid BBAN file status");
        }
        #endregion


        #region [Api]
        /// <summary>
        /// Gets the bban file history.
        /// </summary>
        /// <param name="bbanFileId">The bban file identifier.</param>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetBbanFileHistory(int bbanFileId)
        {
            //Call service.
            List<BbanFileHistory> bBanFileHistories = _bbanFile.GetHistory(bbanFileId).ToList();
            return Json(new { Success = true, Data = bBanFileHistories }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the iban by bban file id.
        /// </summary>
        /// <param name="bbanFileId">The bban file id.</param>
        /// <param name="page">The page.</param>
        /// <returns>JsonResult.</returns>
        [HttpPost]
        public JsonResult GetIBanByBBanFileId(int bbanFileId, int? page)
        {
            int currentPage = page ?? 1;
            // table paging
            int offSet = (currentPage - 1) * 10;

            int total = 0;
            List<Iban> model = _bbanFile.GetIbans(bbanFileId, offSet, 10, out total).ToList();

            total = (total / 10) + 1;

            return Json(new { Success = true, Data = model, TotalPage = total, Currentpage = currentPage }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}