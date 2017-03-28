using System.Web;
using System.Web.Mvc;

namespace BND.Services.Payments.PaymentIdInfo.IntegrationTests
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}