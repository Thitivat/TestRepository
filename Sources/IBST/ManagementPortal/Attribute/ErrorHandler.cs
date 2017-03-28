using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BND.Services.IbanStore.Service;

namespace BND.Services.IbanStore.ManagementPortal.Attribute
{

    public class ErrorHandlerAttribute : FilterAttribute, IExceptionFilter
    {
        /// <summary>
        /// Called when an exception occurs.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public void OnException(ExceptionContext filterContext)
        {

            //get message error in exception
            filterContext.ExceptionHandled = true;
            //var messageError = filterContext.Exception.InnerException != null
            //    ? filterContext.Exception.InnerException.Message
            //    : filterContext.Exception.Message;
            var messageError = filterContext.Exception.Message;

            
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                //format json error response
                filterContext.Result = new JsonResult
                {
                    Data = new { Success = false, Error = messageError },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                
                filterContext.ExceptionHandled = true;

                //if call by redirect action return view page
                filterContext.Result = new ViewResult()
                {
                    ViewName =   "~/Views/Error/Index.cshtml",
                    ViewData = new ViewDataDictionary(messageError)
                };
            }
        }
    }
}