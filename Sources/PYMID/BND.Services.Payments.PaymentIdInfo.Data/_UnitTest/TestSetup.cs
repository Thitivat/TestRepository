using NUnit.Framework;
using System;

namespace BND.Services.Payments.PaymentIdInfo.Dal.Test
{
    [SetUpFixture]
    public class TestSetup
    {
        [OneTimeSetUp]
        public void SetUp()
        {
            // Registers Effort provider for unit testing.
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
        }

        [OneTimeTearDown]
        public void TearDown()
        {

        }
    }
}
