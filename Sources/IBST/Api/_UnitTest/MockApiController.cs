using BND.Services.IbanStore.Api.Controllers;
using BND.Services.IbanStore.Api.Helpers;
using System.Web.Http;

namespace BND.Services.IbanStore.ApiTest
{
    [RoutePrefix("TestPrefix")]
    public class MockApiController : ApiControllerBase
    {
        public MockApiController()
            : base()
        { }

        [Route("Get")]
        public IHttpActionResult Get()
        {
            return Ok();
        }
    }
}
