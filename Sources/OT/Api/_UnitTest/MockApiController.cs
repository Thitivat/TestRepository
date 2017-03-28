using BND.Services.Security.OTP.Api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace BND.Services.Security.OTP.ApiUnitTest
{
    [RoutePrefix("TestPrefix")]
    public class MockApiController : ApiControllerBase
    {
        [Route("Get")]
        public IHttpActionResult Get()
        {
            return Ok();
        }
    }
}
