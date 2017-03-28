using BND.Services.Security.OTP.ApiTest.Models;
using BND.Services.Security.OTP.ClientProxy;
using BND.Services.Security.OTP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using System.Web.Mvc;

namespace BND.Services.Security.OTP.ApiTest.Controllers
{
    /// <summary>
    /// Class HomeController contains the functions to respresent the view of otp system. These view provide the function
    /// for request otp and verify otp.
    /// </summary>
    public class HomeController : Controller
    {
        // GET: Home
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Index()
        {
            ViewBag.Title = "OTP Request";
            return View();
        }

        /// <summary>
        /// This method provides function for requests the otp code.
        /// </summary>
        /// <param name="request">The request model.</param>
        /// <exception cref="System.Exception"></exception>
        [HttpPost]
        public ActionResult VerifyRequest(HomeViewModel request)
        {
            ApiErrorModel error = null;
            try
            {
                // set data to address
                request.OtpRequest.Channel.Address =
                    (request.OtpRequest.Channel.Type.ToUpper() == "SMS") ? request.Mobile
                                                                         : request.Email;
                OneTimePasswords otp = new OneTimePasswords(WebConfigurationManager.AppSettings["ApiOtpBase"],
                        request.ApiKey, request.AccountId);
                OtpResultModel model = otp.NewCode(new OtpRequestModel
                                    {
                                        Channel = request.OtpRequest.Channel,
                                        Payload = request.OtpRequest.Payload,
                                        Suid = request.OtpRequest.Suid
                                    });
                if (model != null)
                {
                    ViewBag.Title = "Verify Code";
                    return View("VerifyOtp", new VerifyOtpModel
                                            {
                                                ApiKey = request.ApiKey,
                                                OtpId = model.OtpId,
                                                RefCode = model.RefCode,
                                                AccountId = request.AccountId
                                            }
                                );
                }
                throw new Exception();

            }
            catch (Exception ex)
            {
                if (error == null)
                {
                    error = new ApiErrorModel
                    {
                        StatusCode = System.Net.HttpStatusCode.InternalServerError,
                        Messages = new ErrorMessageModel[] { new ErrorMessageModel { Message = ex.Message } },
                    };
                }
                request.Error = error;
                TempData["OtpRequest"] = request.OtpRequest;
                TempData["Error"] = request.Error;
                return RedirectToAction("RequestOtp", request);
            }
        }

        /// <summary>
        /// The main page for request otp.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult RequestOtp(HomeViewModel request)
        {
            ViewBag.Title = "OTP Request";
            request.OtpRequest = TempData["OtpRequest"] as OtpRequestViewModel;
            request.Error = TempData["Error"] as ApiErrorModel;
            TempData["Error"] = null;
            return View(request);
        }

        /// <summary>
        /// Verifies the otp.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult VerifyOtp(VerifyOtpModel request)
        {
            ViewBag.Title = "Verify Code";
            request.Error = TempData["Error"] as ApiErrorModel;
            TempData["Error"] = null;
            request.RefCode = TempData["RefCode"] as string;
            return View(request);
        }

        /// <summary>
        /// Confirms the code.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>ActionResult.</returns>
        /// <exception cref="System.Exception"></exception>
        public ActionResult ConfirmCode(VerifyOtpModel request)
        {
            ApiErrorModel error = null;
            try
            {
                OneTimePasswords otp = new OneTimePasswords(WebConfigurationManager.AppSettings["ApiOtpBase"], request.ApiKey, request.AccountId);

                OtpModel model = otp.VerifyCode(request.OtpId, request.Code);
                if (model != null)
                {
                    ViewBag.Title = "Verify Success";
                    return View("SuccessResult", new SuccessViewModel { SuccessModel = model });
                }
                throw new Exception();
            }
            catch (Exception ex)
            {
                if (error == null)
                {
                    error = new ApiErrorModel
                    {
                        StatusCode = System.Net.HttpStatusCode.InternalServerError,
                        Messages = new ErrorMessageModel[] { new ErrorMessageModel { Message = ex.Message },
                                                             new ErrorMessageModel { Message = ex.Message } }
                    };
                }
                request.Error = error;
                TempData["Error"] = request.Error;
                TempData["RefCode"] = request.RefCode;
                return RedirectToAction("VerifyOtp", request);
            }
        }

        /// <summary>
        /// Gets the channel.
        /// </summary>
        /// <returns>ActionResult.</returns>
        /// <exception cref="System.Exception">Cannot Retrieve Plugin names.</exception>
        public ActionResult GetChannel()
        {
            ApiErrorModel error = null;
            try
            {
                Channels channel = new Channels(WebConfigurationManager.AppSettings["ApiOtpBase"],
                                                       WebConfigurationManager.AppSettings["ApiKey"],
                                                       WebConfigurationManager.AppSettings["AccountId"]);

                List<string> channels = channel.GetAllChannelTypeNames().ToList();
                if (channels.Any())
                {
                    return Json(channels, JsonRequestBehavior.AllowGet);
                }
                throw new Exception("Cannot Retrieve Plugin names.");
            }
            catch (Exception ex)
            {
                if (error == null)
                {
                    error = new ApiErrorModel
                    {
                        StatusCode = System.Net.HttpStatusCode.InternalServerError,
                        Messages = new ErrorMessageModel[] { new ErrorMessageModel { Message = ex.Message } },
                    };
                }
                HomeViewModel request = new HomeViewModel();
                request.Error = error;
                TempData["Error"] = request.Error;
                return RedirectToAction("RequestOtp", request);
            }
        }
    }
}