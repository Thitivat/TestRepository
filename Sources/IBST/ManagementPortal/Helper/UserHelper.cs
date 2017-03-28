using System.Web;

namespace BND.Services.IbanStore.ManagementPortal.Helper
{
    /// <summary>
    /// Class UserHelper.
    /// </summary>
    public class UserHelper
    {
        /// <summary>
        /// Gets the name of the user.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string GetUserName()
        {
            var username = HttpContext.Current.User.Identity.Name.Split('\\')[1];
            return username;
        }
    }
}