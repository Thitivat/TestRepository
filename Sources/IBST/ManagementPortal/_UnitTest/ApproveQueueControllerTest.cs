using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using BND.Services.IbanStore.ManagementPortal.Controllers;
using BND.Services.IbanStore.ManagementPortal.Helper;
using BND.Services.IbanStore.Models;
using BND.Services.IbanStore.Service.Bll;
using BND.Services.IbanStore.Service.Bll.Interfaces;
using Moq;
using NUnit.Framework;

namespace BND.Services.IbanStore.ManagementPortalTest
{
    [TestFixture]
    public class ApproveQueueControllerTest
    {
        private List<BbanFile> _bbanfile;
        private List<Bban> _bban;
        private List<IbanHistory> _ibanHistories;

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

            _ibanHistories = new List<IbanHistory>()
            {
                new IbanHistory()
                {
                    Context = "Web",
                    ChangedBy = "Test",
                    ChangedDate = DateTime.Now,
                    Id = 1,
                    Remark = "",
                    Status = "Available"
                }
            };

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
            _bbanfile = new List<BbanFile>()
            {
                new BbanFile()
                {
                    CurrentStatus = new BbanFileHistory()
                    {
                        ChangedBy = "Test",
                        ChangedDate = DateTime.Now,
                        Context = "Web",
                        Id = 1,
                        Remark = "Remark",
                        Status = "WaitingForApproval"

                    },
                    Hash = "A1111",
                    Id = 1,
                    Name = "BbanFile.csv"
                },
                new BbanFile()
                {
                    CurrentStatus = new BbanFileHistory()
                    {
                        ChangedBy = "Test",
                        ChangedDate = DateTime.Now,
                        Context = "Web",
                        Id = 2,
                        Remark = "Remark",
                        Status = "WaitingForApproval"

                    },
                    Hash = "A1112",
                    Id = 2,
                    Name = "BbanFile2.csv"
                },
                new BbanFile()
                {
                    CurrentStatus = new BbanFileHistory()
                    {
                        ChangedBy = "Test",
                        ChangedDate = DateTime.Now,
                        Context = "Web",
                        Id = 3,
                        Remark = "Remark",
                        Status = "WaitingForApproval"

                    },
                    Hash = "A1111",
                    Id = 3,
                    Name = "BbanFile3.csv"
                }

            };

        }

        [Test]
        public void Test_Index_Success()
        {

            ApprovalQueueController controller = new ApprovalQueueController();

            //var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(controller);


            //Mock<IBbanFileManager> mockBbanFileManager = new Mock<IBbanFileManager>();
            //int total = 0;
            //mockBbanFileManager.Setup(
            //    s => s.Get(It.IsAny<int>(), It.IsAny<int>(), out total, It.IsAny<int>(), It.IsAny<string>()))
            //    .Returns(_bbanfile);
            //privateObject.SetFieldOrProperty("_bbanFile", mockBbanFileManager.Object);

            //var viewResult = controller.Index(1, 12) as ViewResult;

            //Assert.IsNotNull(viewResult);


        }

        [Test]
        public void Test_Index_Success_WithOut_Page()
        {
            ApprovalQueueController controller = new ApprovalQueueController();

            //var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(controller);

            //Mock<IBbanFileManager> mockBbanFileManager = new Mock<IBbanFileManager>();
            //int total = 0;
            //mockBbanFileManager.Setup(
            //    s => s.Get(It.IsAny<int>(), It.IsAny<int>(), out total, It.IsAny<int>(), It.IsAny<string>()))
            //    .Returns(_bbanfile);
            //privateObject.SetFieldOrProperty("_bbanFile", mockBbanFileManager.Object);

            //var viewResult = controller.Index(null, 12) as ViewResult;

            //Assert.IsNotNull(viewResult);
        }

        [Test]
        public void Test_Index_Return_Paging()
        {
            ApprovalQueueController controller = new ApprovalQueueController();

            //var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(controller);


            //Mock<IBbanFileManager> mockBbanFileManager = new Mock<IBbanFileManager>();
            //int total = 0;
            //mockBbanFileManager.Setup(
            //    s => s.Get(It.IsAny<int>(), It.IsAny<int>(), out total, It.IsAny<int>(), It.IsAny<string>()))
            //    .Returns(_bbanfile);
            //privateObject.SetFieldOrProperty("_bbanFile", mockBbanFileManager.Object);
            //var viewResult = controller.Index(1, 12) as ViewResult;
            //var currentPage = viewResult.ViewBag.Currentpage;
            //Assert.IsNotNull(currentPage);
            //Assert.AreEqual(1, currentPage);
        }

        [Test]
        public void Test_Index_Return_Model()
        {
            ApprovalQueueController controller = new ApprovalQueueController();

            //var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(controller);


            //Mock<IBbanFileManager> mockBbanFileManager = new Mock<IBbanFileManager>();
            //int total = 0;
            //mockBbanFileManager.Setup(
            //    s => s.Get(It.IsAny<int>(), It.IsAny<int>(), out total, It.IsAny<int>(), It.IsAny<string>()))
            //    .Returns(_bbanfile);
            //privateObject.SetFieldOrProperty("_bbanFile", mockBbanFileManager.Object);

            //var viewResult = controller.Index(1, 12) as ViewResult;

            //var model = (List<BbanFile>)viewResult.ViewData.Model;
            //Assert.IsNotNull(model);
            //Assert.AreEqual(3, model.Count);
        }

        [Test]
        public void Test_GetBbans_Success()
        {
            ApprovalQueueController controller = new ApprovalQueueController();

            //var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(controller);
            //Mock<IBbanFileManager> mockBbanFileManager = new Mock<IBbanFileManager>();
            //int total = 0;
            //mockBbanFileManager.Setup(s => s.GetBbans(It.IsAny<int>(), It.IsAny<int>(), out total, It.IsAny<int>())).Returns(_bban);
            //privateObject.SetFieldOrProperty("_bbanFile", mockBbanFileManager.Object);

            //dynamic result = controller.GetBbans(bbanFileId: 1, page: 1).Data;

            //Assert.IsNotNull(result);

        }

        [Test]
        public void Test_GetBbans_WithOut_Page_Success()
        {
            ApprovalQueueController controller = new ApprovalQueueController();

            //var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(controller);
            //Mock<IBbanFileManager> mockBbanFileManager = new Mock<IBbanFileManager>();
            //int total = 0;
            //mockBbanFileManager.Setup(s => s.GetBbans(It.IsAny<int>(), It.IsAny<int>(), out total, It.IsAny<int>())).Returns(_bban);
            //privateObject.SetFieldOrProperty("_bbanFile", mockBbanFileManager.Object);

            //dynamic result = controller.GetBbans(bbanFileId: 1, page: null).Data;

            //Assert.IsNotNull(result);
        }

        [Test]
        public void Test_GetHistory_Success()
        {
            ApprovalQueueController controller = new ApprovalQueueController();

            //var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(controller);
            //Mock<IIbanManager> mockBbanFileManager = new Mock<IIbanManager>();
            //mockBbanFileManager.Setup(s => s.GetHistory(It.IsAny<int>())).Returns(_ibanHistories);
            //privateObject.SetFieldOrProperty("_iBanManager", mockBbanFileManager.Object);

            //dynamic result = controller.GetHistories(12).Data;

            //Assert.IsNotNull(result);
        }

        [Test]
        public void Test_ApproveBban_Success()
        {
            ApprovalQueueController controller = new ApprovalQueueController();

            //var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(controller);
            //Mock<IBbanFileManager> mockBbanFileManager = new Mock<IBbanFileManager>();
            //mockBbanFileManager.Setup(s => s.Approve(It.IsAny<int>(), It.IsAny<string>()));
            //privateObject.SetFieldOrProperty("_bbanFile", mockBbanFileManager.Object);

            //var result = controller.ApproveBban(12, "REMARK").Data;

            //Assert.IsNotNull(result);
        }

        [Test]
        public void Test_DenyBban_Success()
        {
            ApprovalQueueController controller = new ApprovalQueueController();

            //var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(controller);
            //Mock<IBbanFileManager> mockBbanFileManager = new Mock<IBbanFileManager>();
            //mockBbanFileManager.Setup(s => s.Deny(It.IsAny<int>(), It.IsAny<string>()));
            //privateObject.SetFieldOrProperty("_bbanFile", mockBbanFileManager.Object);

            //var result = controller.DenyBban(12, "REMARK").Data;

            //Assert.IsNotNull(result);
        }

    }

}
