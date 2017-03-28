
namespace BND.Services.Payments.iDeal.Proxy.Tests
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
