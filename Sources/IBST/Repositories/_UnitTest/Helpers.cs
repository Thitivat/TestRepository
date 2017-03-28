using System.Collections.Generic;

namespace BND.Services.IbanStore.RepositoryTest
{
    internal static class Helpers
    {
        public static List<Poco1> MOCK_POCO1_DATA = new List<Poco1>
            {
                new Poco1 { Id = 1, Name = "Poco1-01" },
                new Poco1 { Id = 2, Name = "Poco1-02" },
                new Poco1 { Id = 3, Name = "Poco1-03" },
                new Poco1 { Id = 4, Name = "Poco1-04" },
                new Poco1 { Id = 5, Name = "Poco1-05" },
                new Poco1 { Id = 6, Name = "Poco1-06" },
                new Poco1 { Id = 7, Name = "Poco1-07" }
            };

        public static List<Poco2> MOCK_POCO2_DATA = new List<Poco2>
            {
                new Poco2 { Email = "Poco2.01@Poco1.com", Id = 1 },
                new Poco2 { Email = "Poco2.02@Poco1.com", Id = 1 },
                new Poco2 { Email = "Poco2.03@Poco1.com", Id = 2 },
                new Poco2 { Email = "Poco2.04@Poco1.com", Id = 2 },
                new Poco2 { Email = "Poco2.05@Poco1.com", Id = 3 },
                new Poco2 { Email = "Poco2.06@Poco1.com", Id = 4 },
                new Poco2 { Email = "Poco2.07@Poco1.com", Id = 4 }
            };
    }
}
