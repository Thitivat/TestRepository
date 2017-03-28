using NUnit.Framework;

namespace BND.Services.Security.OTP.ApiUnitTest
{
    [SetUpFixture]
    public class NUnitSetUp
    {
        [OneTimeSetUp]
        public static void AssemblyInitialize()
        {
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
        }
    }
}
