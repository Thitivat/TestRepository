using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BND.Services.IbanStore.ManagementPortal.Controllers;
using BND.Services.IbanStore.Models;
using BND.Services.IbanStore.Service.Bll.Interfaces;
using Moq;
using NUnit.Framework;

namespace BND.Services.IbanStore.ManagementPortalTest
{
    [TestFixture]
    public class BBANFileControllerTest
    {
        private List<Iban> _ibans;
        [SetUp]
        public void Init()
        {

            HttpContext.Current = new HttpContext(
            new HttpRequest("", "http://tempuri.org", ""),
            new HttpResponse(new StringWriter())
            ); ;
            Mock<IIdentity> identity = new Mock<IIdentity>();
            identity.Setup(i => i.Name).Returns("COM1\\Test");
            Mock<IPrincipal> iprincipal = new Mock<IPrincipal>();
            iprincipal.Setup(p => p.Identity).Returns(identity.Object);
            HttpContext.Current.User = iprincipal.Object;
            _ibans = new List<Iban>()
            {
                new Iban("NL","BNDA","637751000","17")
            };

        }

        //[Test]
        //public void BBanFile_Indexpage()
        //{
        //    Mock<IBbanFileManager> mockBbanFileManager = new Mock<IBbanFileManager>();
        //    int total = 0;
        //    mockBbanFileManager.Setup(
        //        s => s.Get(It.IsAny<int>(), It.IsAny<int>(), out total, It.IsAny<int>(), It.IsAny<string>()))
        //        .Returns(It.IsAny<List<BbanFile>>());
        //    BBANFilesController controller = new BBANFilesController();
        //    var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(controller);
        //    privateObject.SetField("_bbanFile", mockBbanFileManager.Object);
        //    ViewResult result = controller.Index(1) as ViewResult;
        //    Assert.IsNotNull(result);
        //}

        //[Test]
        //public void BBANFile_Index_Success_WithOut_Page()
        //{
        //    Mock<IBbanFileManager> mockBbanFileManager = new Mock<IBbanFileManager>();
        //    int total = 0;
        //    mockBbanFileManager.Setup(
        //        s => s.Get(It.IsAny<int>(), It.IsAny<int>(), out total, It.IsAny<int>(), It.IsAny<string>()))
        //        .Returns(It.IsAny<List<BbanFile>>());
        //    BBANFilesController controller = new BBANFilesController();
        //    var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(controller);
        //    privateObject.SetField("_bbanFile", mockBbanFileManager.Object);
        //    ViewResult result = controller.Index(null) as ViewResult;
        //    Assert.IsNotNull(result);
        //}

        [Test]
        public void BBANFile_RedirectStatus_Status_Upload()
        {
            BBANFilesController controller = new BBANFilesController();

            RedirectToRouteResult result = (RedirectToRouteResult)controller.RedirectStatus(1, EnumBbanFileStatus.Uploaded.ToString());

            Assert.AreEqual("BBANList", result.RouteValues["action"]);
            Assert.AreEqual("UploadBBAN", result.RouteValues["controller"]);
        }

        [Test]
        public void BBANFile_RedirectStatus_Status_UploadCancel()
        {
            BBANFilesController controller = new BBANFilesController();

            RedirectToRouteResult result = (RedirectToRouteResult)controller.RedirectStatus(1, EnumBbanFileStatus.UploadCanceled.ToString());

            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("UploadBBAN", result.RouteValues["controller"]);
        }

        [Test]
        public void BBANFile_RedirectStatus_Status_Verified()
        {
            BBANFilesController controller = new BBANFilesController();

            RedirectToRouteResult result = (RedirectToRouteResult)controller.RedirectStatus(1, EnumBbanFileStatus.Verified.ToString());

            Assert.AreEqual("Verification", result.RouteValues["action"]);
            Assert.AreEqual("UploadBBAN", result.RouteValues["controller"]);
        }

        [Test]
        public void BBANFile_RedirectStatus_Throw_Exception()
        {
            BBANFilesController controller = new BBANFilesController();
            Assert.Throws<Exception>(() =>
            {
                controller.RedirectStatus(1, "I AM NOT EnumBbanFileStatus");
            });
        }

        //[Test]
        //public void BBANFile_GetIBanByBBanFileId_Return_Success()
        //{
        //    int total = 0;
        //    Mock<IBbanFileManager> mockBbanFileManager = new Mock<IBbanFileManager>();
        //    mockBbanFileManager.Setup(s => s.GetIbans(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), out total))
        //        .Returns(_ibans);


        //    BBANFilesController controller = new BBANFilesController();
        //    var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(controller);
        //    privateObject.SetField("_bbanFile", mockBbanFileManager.Object);

        //    JsonResult result = controller.GetIBanByBBanFileId(1, 1);
        //    Assert.IsNotNull(result);
        //}

        //[Test]
        //public void BBANFile_GetIBanByBBanFileId_Return_Success_WithOut_Page()
        //{
        //    int total = 0;
        //    Mock<IBbanFileManager> mockBbanFileManager = new Mock<IBbanFileManager>();
        //    mockBbanFileManager.Setup(s => s.GetIbans(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), out total))
        //        .Returns(_ibans);


        //    BBANFilesController controller = new BBANFilesController();
        //    var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(controller);
        //    privateObject.SetField("_bbanFile", mockBbanFileManager.Object);

        //    JsonResult result = controller.GetIBanByBBanFileId(1, null);
        //    Assert.IsNotNull(result);
        //}

        //[Test]
        //public void BBANFile_GetBbanFileHistory_Success()
        //{
        //    Mock<IBbanFileManager> mockBbanFileManager = new Mock<IBbanFileManager>();
        //    mockBbanFileManager.Setup(s => s.GetHistory(It.IsAny<int>()))
        //        .Returns(new List<BbanFileHistory>()
        //        {
        //            new BbanFileHistory() { Id = 1 },
        //            new BbanFileHistory() { Id = 2 },
        //        });

        //    BBANFilesController controller = new BBANFilesController();
        //    var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(controller);
        //    privateObject.SetField("_bbanFile", mockBbanFileManager.Object);

        //    JsonResult result = controller.GetBbanFileHistory(0);
        //    Assert.IsNotNull(result);
        //}
    }
}
