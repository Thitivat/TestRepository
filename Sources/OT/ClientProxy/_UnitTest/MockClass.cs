using BND.Services.Security.OTP.ClientProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BND.Services.Security.OTP.ClientProxyTest
{
    public class MockClass : ResourceBase
    {
        public MockClass(string baseAddress, string apiKey, string accountId)
            : base(baseAddress, apiKey, accountId)
        {

        }

        public void MapModelTest<TModel>(TModel model) where TModel : class
        {
            base.MapModelToFormUrlEncodedContent(model);
        }
    }
}
