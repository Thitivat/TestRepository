using System.Web.Http;
using BND.Websites.BackOffice.SanctionListsManagement.Entities;

namespace BND.Websites.BackOffice.SanctionListsManagement.Web.Controllers.Api.SanctionLists
{
    /// <summary>
    /// API actions on Regulations.
    /// </summary>
    public class RegulationsController : SanctionListsBaseController
    {
        /// <summary>
        /// Adds new regulation to the database.
        /// </summary>
        /// <param name="regulation">Regulation entity</param>
        /// <param name="listTypeId">List type id</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/SanctionLists/{listTypeId}/Regulations")]
        public Regulation Post([FromBody]Regulation regulation, int listTypeId)
        {
            // TODO should we overwrite listId in regulation by Id from request, or raise error, 
            Regulation newRegulation = _sanctionListsBll.RegulationsAdd(regulation);

            // Log
            LogActivity(regulation.ListType.ListTypeId, "Added new entity. Entity Id: " + newRegulation.RegulationId);

            return newRegulation;
        }

        [HttpGet]
        [Route("api/SanctionLists/{listTypeId}/Regulations")]
        public object RegulationsGet(int listTypeId, int? offset = null, int? limit = null)
        {
            Regulation[] newRegulation = _sanctionListsBll.RegulationsGet(listTypeId, offset, limit);

            // Log
            LogActivity(listTypeId, "Gets regulations");

            int count = RegulationsGetCount(listTypeId);

            return new { Regulations = newRegulation, RegulationsCount = count };
        }

        [HttpGet]
        [Route("api/SanctionLists/{listTypeId}/Regulations/Count")]
        public int RegulationsGetCount(int listTypeId)
        {
            return _sanctionListsBll.RegulationsCount(listTypeId);
        }
    }
}