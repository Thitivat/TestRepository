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
    public class IBanControllerTest
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
        //public void IBan_Index_Success()
        //{
        //    IBANController controller = new IBANController();
        //    var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(controller);
        //    Mock<IIbanManager> ibanmangerMock = new Mock<IIbanManager>();
        //    int total = 0;
        //    ibanmangerMock.Setup(
        //        s => s.Get(It.IsAny<int>(), It.IsAny<int>(), out total, It.IsAny<string>(), It.IsAny<string>()))
        //        .Returns(_ibans);

        //    privateObject.SetField("_iban", ibanmangerMock.Object);
        //    ViewResult viewResult = controller.Index(1) as ViewResult;
        //    Assert.IsNotNull(viewResult);
        //}

        //[Test]
        //public void IBan_Index_WithOut_Page_Success()
        //{
        //    IBANController controller = new IBANController();
        //    var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(controller);
        //    Mock<IIbanManager> ibanmangerMock = new Mock<IIbanManager>();
        //    int total = 0;
        //    ibanmangerMock.Setup(
        //        s => s.Get(It.IsAny<int>(), It.IsAny<int>(), out total, It.IsAny<string>(), It.IsAny<string>()))
        //        .Returns(_ibans);

        //    privateObject.SetField("_iban", ibanmangerMock.Object);
        //    ViewResult viewResult = controller.Index(null) as ViewResult;
        //    Assert.IsNotNull(viewResult);
        //}

        //[Test]
        //public void IBan_GetIban_Return_Success()
        //{
        //    IBANController controller = new IBANController();
        //    var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(controller);
        //    Mock<IIbanManager> ibanmangerMock = new Mock<IIbanManager>();
        //    int total = 0;
        //    ibanmangerMock.Setup(
        //        s => s.Get(It.IsAny<int>(), It.IsAny<int>(), out total, It.IsAny<string>(), It.IsAny<string>()))
        //        .Returns(_ibans);

        //    privateObject.SetField("_iban", ibanmangerMock.Object);

        //    JsonResult jsonResult = controller.GetIban(EnumIbanStatus.Active.ToString(), "NL53BNDA0768641667", 1);
        //    Assert.IsNotNull(jsonResult);
        //}

        //[Test]
        //public void IBan_GetIban_WithOut_Page_Success()
        //{
        //    IBANController controller = new IBANController();
        //    var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(controller);
        //    Mock<IIbanManager> ibanmangerMock = new Mock<IIbanManager>();
        //    int total = 0;
        //    ibanmangerMock.Setup(
        //        s => s.Get(It.IsAny<int>(), It.IsAny<int>(), out total, It.IsAny<string>(), It.IsAny<string>()))
        //        .Returns(_ibans);

        //    privateObject.SetField("_iban", ibanmangerMock.Object);

        //    JsonResult jsonResult = controller.GetIban(EnumIbanStatus.Active.ToString(), "NL53BNDA0768641667", null);
        //    Assert.IsNotNull(jsonResult);
        //}
    }
}
