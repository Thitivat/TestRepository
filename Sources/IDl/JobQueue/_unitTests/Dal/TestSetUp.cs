using NUnit.Framework;

namespace BND.Services.Payments.iDeal.JobQueue.Tests.Dal
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
