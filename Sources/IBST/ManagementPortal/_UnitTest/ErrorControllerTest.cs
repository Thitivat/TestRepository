using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using BND.Services.IbanStore.ManagementPortal.Controllers;
using System.Web.Mvc;

namespace BND.Services.IbanStore.ManagementPortalTest
{
    [TestFixture]
    public class ErrorControllerTest
    {
        [Test]
        public void Test_Index_Success()
        {
            var controller = new ErrorController();
            ViewResult index = controller.Index() as ViewResult;
            Assert.IsNotNull(index);
        }
    }
}
