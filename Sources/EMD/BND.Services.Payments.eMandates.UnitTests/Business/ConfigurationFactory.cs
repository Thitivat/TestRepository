using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eMandates.Merchant.Library;
using eMandates.Merchant.Library.Configuration;

namespace BND.Services.Payments.eMandates.UnitTests.Business
{
    public static class ConfigurationFactory
    {

        public static IConfiguration GetCoreCommunicator()
        {
            IConfiguration config = new Configuration(
                "0030000621",
                "http://localhost:8081/Products/Status",
                "9a0aef81ce0066a8d36e1829f5976b3e966f5908",
                "13DB1626664C85554E917DF863A3CAE5DED08B09",
                "https://abnamro-test.incassomachtigen.de/bvn-idx-routing/bvnGateway",
                "https://abnamro-test.incassomachtigen.de/bvn-idx-routing/bvnGateway",
                "https://abnamro-test.incassomachtigen.de/bvn-idx-routing/bvnGateway",
                true,
                @"D:\BrandNewDay\eMandates\BND.Services.Payments.eMandates.UnitTests\bin" );

            return config;
        }
    }
}
