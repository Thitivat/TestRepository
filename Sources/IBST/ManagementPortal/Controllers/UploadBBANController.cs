using BND.Services.IbanStore.ManagementPortal.Attribute;
using BND.Services.IbanStore.ManagementPortal.Helper;
using BND.Services.IbanStore.ManagementPortal.Models;
using BND.Services.IbanStore.Service.Bll;
using BND.Services.IbanStore.Service.Bll.Interfaces;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BND.Services.IbanStore.ManagementPortal.Controllers
{
    /// <summary>
    /// Class UploadBBANController.
    /// </summary>
    [ErrorHandler]
    public class UploadBBANController : Controller
    {
        #region [Fields]
        /// <summary>
        /// The bban file manager.
        /// </summary>
        private IBbanFileManager _bbanFile;

        /// <summary>
        /// fix token authentication
        /// </summary>
        private string token = UserHelper.GetUserName();

        /// <summary>
        /// fix context
        /// </summary>
        private string Context = "WEB";
        #endregion

        #region [Contructor]
        /// <summary>
        /// Initializes a new instance of the <see cref="UploadBBANController"/> class.
        /// </summary>
        public UploadBBANController()
        {
            // create instance pass token connection stirng and email config(xml)
            _bbanFile = new BbanFileManager(token, Context, ConfigurationManager.AppSettings.Get("ConnectionString"), XmlHelper.ReadXmlToString("Config/emailSettings.xml"));
        }
        #endregion

        #region [Public Method]
        
        /// <summary>
        /// render page
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// render page 
        /// </summary>
        /// <param name="BbanFileId">The bban file identifier.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult BBANList(int BbanFileId, int? page)
        {
            int currentPage = page ?? 1;
            // table paging
            int offSet = (currentPage - 1) * 10;

            int total = 0;
            //get BBAN by bbanfileId
            var Model=_bbanFile.GetBbans(offSet,10,out total,BbanFileId).ToList();
            total = (total / 10) + 1;
            ViewBag.TotalPage = total;

            //current paging
            ViewBag.Currentpage = currentPage;

            return View(Model); 
        }

        /// <summary>
        /// render page 
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Verification()
        {
            return View();
        }
        #endregion

        #region [API]
        /// <summary>
        /// Uploads the bban contentType Content-Type:multipart/form-data
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>JsonResult.</returns>
        /// <exception cref="System.ArgumentException">File not found</exception>
        [HttpPost]
        public JsonResult UploadBBAN(HttpPostedFileBase file)
        {
            //require file
            if (file == null)
            {
                throw new ArgumentException("File not found");
            }
            //stream HttpPostedFileBase to byte[]
            MemoryStream target = new MemoryStream();
            file.InputStream.CopyTo(target);
            byte[] data = target.ToArray();

            //call service
            var bbanFileId = _bbanFile.Save(file.FileName, data);

            return Json(new { Success = true, Data = bbanFileId }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Verifies the bban exist .
        /// </summary>
        /// <param name="bbanFileId">The bban file identifier.</param>
        /// <returns>JsonResult.</returns>
        [HttpPost]
        public JsonResult VerifyBbanExist(int bbanFileId)
        {
            // call service
             _bbanFile.VerifyBbanExist(bbanFileId);
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Sends to approve .
        /// </summary>
        /// <param name="bbanFileId">The bban file identifier.</param>
        /// <param name="emailReceiver">The email receiver.</param>
        /// <param name="emailMessage">The email message.</param>
        /// <returns>JsonResult.</returns>
        [HttpPost]
        public JsonResult SendToApprove(int bbanFileId, string emailReceiver, string emailMessage)
        {
            // Creates email body.
            EmailBody bodyModel = new EmailBody
            {
                ApproveUrl = Url.Action("Index", "ApprovalQueue", new { page = 1, bbanFileId = bbanFileId }, Request.Url.Scheme),
                Message = emailMessage,
                SendDate = DateTime.Now,
                SendName = Request.RequestContext.HttpContext.User.Identity.Name
            };

            // Generate the body message via render the template with model.
            string bodyHtml = RenderViewToString(ControllerContext,
                              "~/Views/UploadBBAN/_templateApprove.cshtml",
                              bodyModel, true);

            //call service
            _bbanFile.SendToApprove(bbanFileId, emailReceiver, bodyHtml);
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Cancels the bban file (Verification Page).
        /// </summary>
        /// <param name="bbanFileId">The bban file identifier.</param>
        /// <returns>JsonResult.</returns>
        [HttpPost]
        public JsonResult CancelBbanFile(int bbanFileId)
        {
            //call service 
          _bbanFile.Cancel(bbanFileId);
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region [Private Methods]
        /// <summary>
        /// Renders the view to string.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="viewPath">The view path.</param>
        /// <param name="model">The model.</param>
        /// <param name="partial">if set to <c>true</c> [partial].</param>
        /// <returns>System.String.</returns>
        /// <exception cref="System.IO.FileNotFoundException">View cannot be found.</exception>
        private string RenderViewToString(ControllerContext context,
                                    string viewPath,
                                    object model = null,
                                    bool partial = false)
        {
            // first find the ViewEngine for this view
            ViewEngineResult viewEngineResult = null;
            if (partial)
                viewEngineResult = ViewEngines.Engines.FindPartialView(context, viewPath);
            else
                viewEngineResult = ViewEngines.Engines.FindView(context, viewPath, null);

            if (viewEngineResult == null)
                throw new FileNotFoundException("View cannot be found.");

            // get the view and attach the model to view data
            var view = viewEngineResult.View;
            context.Controller.ViewData.Model = model;

            string result = null;

            using (var sw = new StringWriter())
            {
                var ctx = new ViewContext(context, view,
                                            context.Controller.ViewData,
                                            context.Controller.TempData,
                                            sw);
                view.Render(ctx, sw);
                result = sw.ToString();
            }

            return result;
        }
        #endregion
    }
}