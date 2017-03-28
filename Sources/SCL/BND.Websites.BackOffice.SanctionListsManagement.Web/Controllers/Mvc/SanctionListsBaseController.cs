using System;
using System.Web.Mvc;
using BND.Websites.BackOffice.SanctionListsManagement.Business.Implementations;
using BND.Websites.BackOffice.SanctionListsManagement.Business.Interfaces;
using BND.Websites.BackOffice.SanctionListsManagement.Web.Common;

namespace BND.Websites.BackOffice.SanctionListsManagement.Web.Controllers.Mvc
{
    /// <summary>
    /// Abstract controller for all MVC controllers. 
    /// </summary>
    public abstract class SanctionListsBaseController : Controller
    {
        /// <summary>
        /// SanctionListsBl lthat will be used across the controller.
        /// </summary>
        protected ISanctionListsBll _sanctionListsBll =
            new SanctionListsBll(System.Configuration.ConfigurationManager.ConnectionStrings["SanctionListsDb"].ConnectionString);


        /// <summary>
        /// Method executed on every action call. It logs access.
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            Logger logger = Logger.GetLogger(
                System.Configuration.ConfigurationManager.ConnectionStrings["SanctionListsDb"].ConnectionString);

            logger.LogActivity(
                Request.RequestContext.HttpContext.User.Identity.Name,
                1,
                null,
                "Accessed url: " + filterContext.HttpContext.Request.FilePath);
        }

        /// <summary>
        /// Encapsulates activity logging functionality.
        /// </summary>
        /// <param name="listTypeId">Id of current sanctionList.</param>
        /// <param name="description">Description of action to be logged.</param>
        protected void LogActivity(int listTypeId, string description)
        {
            Logger logger = Logger.GetLogger(
                System.Configuration.ConfigurationManager.ConnectionStrings["SanctionListsDb"].ConnectionString);

            logger.LogActivity(
                Request.RequestContext.HttpContext.User.Identity.Name,
                1,
                listTypeId,
                description);
        }

        /// <summary>
        /// Dumps exception to the log file
        /// </summary>
        /// <param name="ex"></param>
        protected void Log(Exception ex)
        {
            // Directory path
            string filePath = System.Configuration.ConfigurationManager.AppSettings["LogFile"];

            // Generate file name
            string filename = DateTime.UtcNow.ToString("yyyy-MM-dd-_HH-mm-ss") + "_" + DateTime.UtcNow.Ticks.ToString() + ".log";

            // Write log information to the file
            System.IO.File.AppendAllText(filePath + "/" + filename, ex.ToString());
        }
    }
}