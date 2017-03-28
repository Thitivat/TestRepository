using BND.Services.Payments.iDeal.Api.Helpers;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.ModelBinding;

namespace BND.Services.Payments.iDeal.Api.Tests
{
    [TestFixture]
    public class ExtensionValidateTest
    {
        [Test]
        public void Validate_Should_ThrowArgumentException_When_Invalid()
        {
            Dictionary<string, string> expectedErrors = new AttributeDictionary
            {
                { "PurchaseID", "Value is required" },
                { "Language", "Characters are not allowed" }
            };

            ModelStateDictionary modelState = new ModelStateDictionary();

            foreach (string key in expectedErrors.Keys)
            {
                modelState.AddModelError(key, expectedErrors[key]);
            }

            Assert.AreEqual(String.Format("{0}\r\nParameter name: modelState",
                String.Join(Environment.NewLine, modelState.Values.SelectMany(m => m.Errors.Select(e => e.ErrorMessage)))),
                Assert.Throws<ArgumentException>(() => modelState.Validate("modelState")).Message);
        }

        [Test]
        public void Validate_Should_DoNotThrowAnyException_When_Valid()
        {
            ModelStateDictionary modelState = new ModelStateDictionary();

            modelState.Add("PurchaseID", new ModelState());

            Assert.DoesNotThrow(() => modelState.Validate("modelState"));
        }
    }
}