using System.Web.Http;

namespace BND.Services.Security.OTP.Api.Controllers
{
    /// <summary>
    /// ChannelsController class providing channel resource to client..
    /// </summary>
    public class ChannelsController : ApiControllerBase
    {
        /// <summary>
        /// The action to get all channel names.
        /// </summary>
        /// <returns>IHttpActionResult.</returns>
        public IHttpActionResult Get()
        {
            // Calls Process method from base class to perform work properly.
            return Process(() => Ok(ApiManager.GetChannelNames()));
        }
    }
}
