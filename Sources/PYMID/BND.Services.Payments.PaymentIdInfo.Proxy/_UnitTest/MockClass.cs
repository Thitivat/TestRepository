using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BND.Services.Payments.PaymentIdInfo.Proxy.Test
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

        public void SetResult<TResult>(HttpResponseMessage responseMessage)
            where TResult : class
        {
            base.SetResult<TResult>(responseMessage);
        }
    }
}
