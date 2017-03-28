using System;
using System.Web.Http;
using BND.Websites.BackOffice.SanctionListsManagement.Business.Implementations;
using BND.Websites.BackOffice.SanctionListsManagement.Web.Common;

namespace BND.Websites.BackOffice.SanctionListsManagement.Web.Controllers.Api
{
    /// <summary>
    /// Base controller for API controllers
    /// </summary>
    public class SanctionListsBaseController : ApiController
    {
        /// <summary>
        /// SanctionListsBll that will be used across the controller.
        /// </summary>
        protected SanctionListsBll _sanctionListsBll =
            new SanctionListsBll(System.Configuration.ConfigurationManager.ConnectionStrings["SanctionListsDb"].ConnectionString);

        /// <summary>
        /// Encapsulates logging.
        /// </summary>
        /// <param name="listTypeId">List type id.</param>
        /// <param name="description">Description of the action to be logged.</param>
        protected void LogActivity(int listTypeId, string description)
        {

            Logger logger = Logger.GetLogger(
                System.Configuration.ConfigurationManager.ConnectionStrings["SanctionListsDb"].ConnectionString);

            logger.LogActivity(
                User.Identity.Name,
                1,
                listTypeId,
                description);
        }


        protected void Log(Exception ex)
        {
            string filePath = System.Configuration.ConfigurationManager.AppSettings["LogFile"];

            // Generate file name
            string filename = DateTime.UtcNow.ToString("yyyy-MM-dd-_HH-mm-ss") + "_" + DateTime.UtcNow.Ticks.ToString() + ".log";

            // Write log information to the file
            System.IO.File.AppendAllText(filePath + "/" + filename, ex.ToString());
        }
    }
}
