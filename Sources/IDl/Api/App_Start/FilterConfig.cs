using System.Web.Http.Filters;

namespace BND.Services.Payments.iDeal.Api
{
    /// <summary>
    /// Class FilterConfig is class for register filter.
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// Registers the specified filters.
        /// </summary>
        /// <param name="filters">The filters.</param>
        public static void Register(HttpFilterCollection filters)
        {
            filters.Add(new ErrorHandlerConfigAttribute());
            filters.Add(new SecurityConfigAttribute());
            filters.Add(new ModelStateValidationConfigAttribute());
        }
    }
}