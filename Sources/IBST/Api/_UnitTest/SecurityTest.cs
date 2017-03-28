using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using BND.Services.IbanStore.Api.Helpers;
using BND.Services.IbanStore.Api.Models;
using System.Web;
using Moq;

namespace BND.Services.IbanStore.ApiTest
{
    [TestFixture]
    public class SecurityTest
    {
        private string _authorization = "AUTHORIZATION_TOKEN";
        private string _uidPrefix = "UID_PREFIX";

        [Test]
        public void Test_Authenticate_Success()
        {
            var security = new Security();
            var credential = new CredentialsModel("AUTHEN_HEADER");
            var actualResult = security.Authenticate(credential);
            var expectedResult = true;
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void Test_GetUserCredential_Success()
        {
            string token = _authorization;
            string uidPrefix = _uidPrefix;

            var security = new Security();
            var actualResult = security.GetUserCredential(token, uidPrefix);

            var expectedResult = new CredentialsModel(_authorization);
            expectedResult.Token = _authorization;
            expectedResult.UidPrefix = _uidPrefix;

            Assert.AreEqual(actualResult.Token, expectedResult.Token);
            Assert.AreEqual(actualResult.UidPrefix, expectedResult.UidPrefix);
        }
    }
}
