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
    public class UploadBBANFileControllerTest
    {
        private List<Bban> _bban;

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

            _bban = new List<Bban>()
            {
                new Bban()
                {
                    BbanCode = "458578965",
                    ImportId = 1,
                    IsImported = false
                },
                new Bban()
                {
                    BbanCode = "458578961",
                    ImportId = 1,
                    IsImported = false
                },
                new Bban()
                {
                    BbanCode = "458578962",
                    ImportId = 1,
                    IsImported = false
                },
            };
        }

        [Test]
        public void UploadBBAN_IndexPage()
        {
            UploadBBANController controller = new UploadBBANController();
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [Test]
        public void UploadBBAN_BBANList_ReturnView()
        {
            UploadBBANController controller = new UploadBBANController();
            Mock<IBbanFileManager> mockBBanFileManager = new Mock<IBbanFileManager>();
            int total = 0;
            mockBBanFileManager.Setup(s => s.GetBbans(It.IsAny<int>(), It.IsAny<int>(), out total, It.IsAny<int>())).Returns(_bban);

            //var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(controller);
            //privateObject.SetField("_bbanFile", mockBBanFileManager.Object);

            //ViewResult result = controller.BBANList(BbanFileId: 1, page: 1) as ViewResult;
            //var model = (List<Bban>)result.ViewData.Model;
            //Assert.IsNotNull(model);
            //Assert.IsTrue(model.Count > 1);
        }

        [Test]
        public void UploadBBan_BBANList_WithOut_Page_Success()
        {
            UploadBBANController controller = new UploadBBANController();
            Mock<IBbanFileManager> mockBBanFileManager = new Mock<IBbanFileManager>();
            int total = 0;
            mockBBanFileManager.Setup(s => s.GetBbans(It.IsAny<int>(), It.IsAny<int>(), out total, It.IsAny<int>())).Returns(_bban);

            //var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(controller);
            //privateObject.SetField("_bbanFile", mockBBanFileManager.Object);

            //ViewResult result = controller.BBANList(BbanFileId: 1, page: null) as ViewResult;
            //var model = (List<Bban>)result.ViewData.Model;
            //Assert.IsNotNull(model);
            //Assert.IsTrue(model.Count > 1);
        }

        [Test]
        public void UploadBBAN_BBANList_Return_Verification_View()
        {
            UploadBBANController controller = new UploadBBANController();
            ViewResult result = controller.Verification() as ViewResult;

            Assert.IsNotNull(result);
        }

        [Test]
        public void Test_CancelBbanFile_Success()
        {
            UploadBBANController controller = new UploadBBANController();

            Mock<IBbanFileManager> mockBBanFileManager = new Mock<IBbanFileManager>();
            mockBBanFileManager.Setup(s => s.Cancel(It.IsAny<int>()));
            //var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(controller);
            //privateObject.SetField("_bbanFile", mockBBanFileManager.Object);

            //JsonResult result = controller.CancelBbanFile(12);

            //Assert.IsNotNull(result);
        }

        [Test]
        public void Test_UploadBBAN_Success()
        {
            UploadBBANController controller = new UploadBBANController();

            Mock<IBbanFileManager> mockBBanFileManager = new Mock<IBbanFileManager>();
            mockBBanFileManager.Setup(s => s.Save(It.IsAny<string>(), It.IsAny<byte[]>()));
            //var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(controller);
            //privateObject.SetField("_bbanFile", mockBBanFileManager.Object);

            //UTF8Encoding enc = new UTF8Encoding();
            //Mock<HttpPostedFileBase> file = new Mock<HttpPostedFileBase>();
            //file.Setup(d => d.InputStream).Returns(new MemoryStream(enc.GetBytes("")));

            //JsonResult result = controller.UploadBBAN(file.Object);

            //Assert.IsNotNull(result);
        }

        [Test]
        public void Test_UploadBBAN_Fail_File_Is_Null()
        {
            UploadBBANController controller = new UploadBBANController();

            Mock<IBbanFileManager> mockBBanFileManager = new Mock<IBbanFileManager>();
            mockBBanFileManager.Setup(s => s.Save(It.IsAny<string>(), It.IsAny<byte[]>()));
            //var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(controller);
            //privateObject.SetField("_bbanFile", mockBBanFileManager.Object);

            //Assert.Throws<ArgumentException>(() =>
            //{
            //    controller.UploadBBAN(null);
            //});

        }

        //[Test]
        //public void Test_SendToApprove_Success()
        //{
        //    UploadBBANController controller = new UploadBBANController();

        //    Mock<IBbanFileManager> mockBBanFileManager = new Mock<IBbanFileManager>();
        //    mockBBanFileManager.Setup(s => s.SendToApprove(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()));
        //    var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(controller);
        //    privateObject.SetField("_bbanFile", mockBBanFileManager.Object);

        //    var result = controller.SendToApprove(12, "test@test.com", "TEST MESSAGE");
        //    Assert.IsNotNull(result);
        //}

        //[Test]
        //public void Test_RenderViewToString_Success()
        //{
        //    UploadBBANController controller = new UploadBBANController();
        //    var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(controller);

        //    string actualResult = (string)privateObject.Invoke("RenderViewToString", controller.HttpContext, "", null, null);

        //    Assert.AreNotEqual(0, actualResult.Length);
        //}
    }
}
