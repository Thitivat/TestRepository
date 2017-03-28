using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using BND.Services.IbanStore.Api.Helpers;

namespace BND.Services.IbanStore.ApiTest
{
    [TestFixture]
    public class LoggerTest
    {
        [Test]
        public void Test_Info_Success()
        {
            var logger = new Logger();
            string message = "LOG MESSAGE";
            logger.Info(message);
        }

        [Test]
        public void Test_Warn_Success()
        {
            var logger = new Logger();
            string message = "LOG MESSAGE";
            logger.Warn(message);
        }

        [Test]
        public void Test_Error_Success()
        {
            var logger = new Logger();
            Exception exception = new Exception("LOG EXCEPTION");
            logger.Error(exception);
        }
    }
}
