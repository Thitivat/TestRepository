using BND.Services.Payments.eMandates.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BND.Services.Payments.eMandates.UnitTests.Proxy
{
    public class MockClass : ResourceBase
    {
        public MockClass(string baseAddress, string token)
            : base(baseAddress, token)
        {

        }

        public void MapModelTest<TModel>(TModel model) where TModel : class
        {
            base.MapModelToFormUrlEncodedContent(model);
        }
    }
}
