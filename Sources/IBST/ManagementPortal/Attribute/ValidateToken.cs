using System.Web.Mvc;
using BND.Services.IbanStore.ManagementPortal.Helper;

namespace BND.Services.IbanStore.ManagementPortal.Attribute
{
    public class ValidateToken : ActionFilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (!ValidateTokenHelper.IsValid())
            {
                //if invalid token
                filterContext.Result = new ViewResult()
                {
                    ViewName = "~/Views/Error/Index.cshtml",
                    ViewData = new ViewDataDictionary("Invalid token")
                };
            }
            this.OnActionExecuting(filterContext);
        }
    }
}