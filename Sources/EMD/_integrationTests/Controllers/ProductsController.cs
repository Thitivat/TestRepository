using System;
using System.Collections.Generic;
using System.Web.Mvc;
using eMandates.Merchant.Library;
using eMandates.Merchant.Library.Misc;
using eMandates.Merchant.Website.Models;
using eMandates.Merchant.Library.Configuration;
using BND.Services.Payments.eMandates.Proxy;
using BND.Services.Payments.eMandates.Entities;
using System.Web.Configuration;

namespace eMandates.Merchant.Website.Controllers
{
    public class ProductsController : Controller
    {
        private readonly List<ProductViewModel> products = new List<ProductViewModel>(new[]
        {
            new ProductViewModel { Id = "1", Price = 20, Name = "Product 1" },
            new ProductViewModel { Id = "2", Price = 20, Name = "Product 2" },
            new ProductViewModel { Id = "3", Price = 20, Name = "Product 3" },
            new ProductViewModel { Id = "4", Price = 20, Name = "Product 4" },
        });

        private string ApiBaseAddress { get { return WebConfigurationManager.AppSettings["ApiBaseUrl"]; } }
        private string Token { get { return "BND123456"; } }

        public ActionResult Index()
        {
            return View(products);
        }

        public ActionResult Buy(string id, string instrumentation)
        {
            try
            {
                var product = products.Find(p => p.Id == id);
                ViewBag.Product = product;
                var response = new DirectoryResponseViewModel { Source = new List<DirectoryModel>() { new Directories(ApiBaseAddress, Token).GetDirectories() }, Instrumentation = Instrumentation.Core };

                return View(response);

                //if (instrumentation == "B2B") return View(new DirectoryResponseViewModel { Source = new B2BCommunicator().Directory(), Instrumentation = Instrumentation.B2B });

                //throw new Exception("Instrumentation should either be Core or B2B.");
            }
            catch (Exception ex)
            {
                var response = new DirectoryResponseViewModel { IsError = true, ErrorMessage = ex.ToString() };

                return View(response);
            }
        }

        [HttpPost]
        public ActionResult Transaction(string issuer)
        {
            var response = new NewMandateRequestViewModel { Instrumentation = Instrumentation.Core };
            response.Source = new NewMandateRequest();
            response.Source.EntranceCode = Guid.NewGuid().ToString("N");
            response.Source.MessageId = MessageIdGenerator.New();
            response.Source.EMandateId = Guid.NewGuid().ToString("N");
            response.Source.DebtorBankId = issuer;
            response.Source.PurchaseId = "123456";

            return View(response);
        }

        [HttpPost]
        public ActionResult NewMandateResult(NewMandateRequestViewModel model)
        {
            try
            {
                if (String.IsNullOrEmpty(model.Source.MessageId))
                {
                    model.Source.MessageId = MessageIdGenerator.New();
                }

                NewTransactionModel newTransaction = new NewTransactionModel();
                newTransaction.DebtorBankId = model.Source.DebtorBankId;
                newTransaction.DebtorReference = model.Source.DebtorReference;
                newTransaction.EMandateId = model.Source.EMandateId;
                newTransaction.EMandateReason = model.Source.EMandateReason;
                newTransaction.ExpirationPeriod = model.Source.ExpirationPeriod;
                newTransaction.Language = model.Source.Language;
                newTransaction.MaxAmount = model.Source.MaxAmount;
                newTransaction.PurchaseId = model.Source.PurchaseId;
                newTransaction.SequenceType = model.Source.SequenceType.ToString();

                return View(new NewMandateResponseViewModel { Source = new Transaction(ApiBaseAddress, Token).CreateTransaction(newTransaction), Instrumentation = Instrumentation.Core });

                //if (model.Instrumentation == Instrumentation.B2B) return View(new NewMandateResponseViewModel { Source = new B2BCommunicator().NewMandate(model.Source), Instrumentation = Instrumentation.B2B });

                //throw new Exception("Instrumentation should either be Core or B2B.");
            }
            catch (Exception ex)
            {
                var response = new NewMandateResponseViewModel { IsError = true, ErrorMessage = ex.ToString() };

                return View(response);
            }
        }

        [HttpGet]
        public ActionResult Status(string trxid, string instrumentation)
        {
            if (String.IsNullOrEmpty(trxid)) throw new Exception("trxid must have a value.");

            return GetStatusResponse(trxid, Instrumentation.Core);

            //if (instrumentation == "B2B") return GetStatusResponse(trxid, Instrumentation.B2B);

            //throw new Exception("Instrumentation should either be Core or B2B.");
        }

        [HttpPost]
        public ActionResult Status(StatusRequestViewModel model)
        {
            return GetStatusResponse(model.Source.TransactionId, model.Instrumentation);
        }

        private ActionResult GetStatusResponse(string trxid, Instrumentation instrumentation)
        {
            try
            {
                var response = new StatusResponseViewModel();
                response.Status = new Transaction(ApiBaseAddress, Token).GetTransactionStatus(trxid).ToString();

                return View(response);

                //if (instrumentation == Instrumentation.B2B) return View(new B2BCommunicator().GetStatus(new StatusRequest(trxid)));

                //throw new Exception("Instrumentation should either be Core or B2B.");
            }
            catch (Exception ex)
            {
                var response = new StatusResponseViewModel { IsError = true, ErrorMessage = ex.ToString() };

                return View(response);
            }
        }


        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Amend()
        {
            return View(new AmendmentRequestViewModel { Instrumentation = Instrumentation.Core });
        }

        [HttpPost]
        public ActionResult AmendMandateResult(AmendmentRequestViewModel model)
        {
            if (String.IsNullOrEmpty(model.Source.MessageId))
            {
                model.Source.MessageId = MessageIdGenerator.New();
            }

            if (model.Instrumentation == Instrumentation.Core) return View(new AmendmentResponseViewModel { Source = new CoreCommunicator().Amend(model.Source), Instrumentation = Instrumentation.Core });
            if (model.Instrumentation == Instrumentation.B2B) return View(new AmendmentResponseViewModel { Source = new B2BCommunicator().Amend(model.Source), Instrumentation = Instrumentation.B2B });

            throw new Exception("Instrumentation should either be Core or B2B.");
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Cancel()
        {
            return View(new CancellationRequest());
        }

        [HttpPost]
        public ActionResult CancelMandateResult(CancellationRequest model)
        {
            if (String.IsNullOrEmpty(model.MessageId))
            {
                model.MessageId = MessageIdGenerator.New();
            }

            var response = new B2BCommunicator().Cancel(model);
            return View(response);
        }
    }
}