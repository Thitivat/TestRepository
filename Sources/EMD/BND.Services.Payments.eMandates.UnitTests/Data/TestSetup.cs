using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BND.Services.Payments.eMandates.UnitTests.Data
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
