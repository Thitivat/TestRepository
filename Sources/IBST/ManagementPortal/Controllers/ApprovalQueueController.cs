using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using BND.Services.IbanStore.ManagementPortal.Attribute;
using BND.Services.IbanStore.ManagementPortal.Helper;
using BND.Services.IbanStore.Models;
using BND.Services.IbanStore.Service.Bll;
using BND.Services.IbanStore.Service.Bll.Interfaces;

namespace BND.Services.IbanStore.ManagementPortal.Controllers
{
    /// <summary>
    /// Class ApprovalQueueController.
    /// </summary>
    [ErrorHandler]
    public class ApprovalQueueController : Controller
    {

        #region [Fields]
        /// <summary>
        /// The _bban file
        /// </summary>
        private readonly IBbanFileManager _bbanFile;
        /// <summary>
        /// The iban manager
        /// </summary>
        private readonly IIbanManager _iBanManager;
        /// <summary>
        /// The _token
        /// </summary>
        private readonly string username = UserHelper.GetUserName();
        /// <summary>
        /// The context
        /// </summary>
        private string context = "WEB";

        #endregion


        #region [Constructor]
        /// <summary>
        /// Initializes a new instance of the <see cref="ApprovalQueueController"/> class.
        /// </summary>
        public ApprovalQueueController()
        {
            _bbanFile = new BbanFileManager(username, context, ConfigurationManager.AppSettings.Get("ConnectionString"),
                XmlHelper.ReadXmlToString("Config/emailSettings.xml"));
            _iBanManager = new IbanManager(username, context, ConfigurationManager.AppSettings.Get("ConnectionString"));
        }

        #endregion


        #region [Public Method]
        /// <summary>
        /// call BBANFiles page
        /// </summary>
        /// <param name="page">The page. current page number</param>
        /// <param name="bbanFileId">The bban file identifier.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult Index(int? page, int? bbanFileId)
        {
            //if page number equal zero set as one
            int currentPage = page ?? 1;
            // table paging
            int offSet = (currentPage - 1) * 10;

            int total;

            // call service method.
            List<BbanFile> model = _bbanFile.Get(offSet, 10, out total, bbanFileId,
                    EnumBbanFileStatus.WaitingForApproval.ToString()).ToList();

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
        /// Gets the bbans.
        /// </summary>
        /// <param name="bbanFileId">The bban file identifier.</param>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetBbans(int bbanFileId, int? page)
        {
            int currentPage = page ?? 1;
            // table paging
            int offSet = (currentPage - 1) * 10;

            int total = 0;
            //get BBAN by bbanfileId
            var result = _bbanFile.GetBbans(offSet, 10, out total, bbanFileId).ToList();
            total = (total / 10) + 1;
            return Json(new { Success = true, Data = result, TotalPage = total, Currentpage = currentPage }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Approves the bban.
        /// </summary>
        /// <param name="bbanFileId">The bban file identifier.</param>
        /// <param name="remark">The remark.</param>
        /// <returns>JsonResult.</returns>
        [HttpPost]
        public JsonResult ApproveBban(int bbanFileId, string remark)
        {
            _bbanFile.Approve(bbanFileId, remark);
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Denies the bban require remark.
        /// </summary>
        /// <param name="bbanFileId">The bban file identifier.</param>
        /// <param name="remark">The remark.</param>
        /// <returns>JsonResult.</returns>
        [HttpPost]
        public JsonResult DenyBban(int bbanFileId, string remark)
        {
            _bbanFile.Deny(bbanFileId, remark);
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the histories.
        /// </summary>
        /// <param name="ibanId">The iban identifier.</param>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetHistories(int ibanId)
        {
            List<IbanHistory> model = _iBanManager.GetHistory(ibanId).ToList();
            return Json(new { Success = true ,Data=model}, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}