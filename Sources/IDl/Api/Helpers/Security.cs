using System;

namespace BND.Services.Payments.iDeal.Api.Helpers
{
    /// <summary>
    /// Class Security is implements ISecurity for validate token
    /// </summary>
    public class Security : ISecurity
    {
        /// <summary>
        /// Validates the token.
        /// </summary>
        /// <param name="token">The token.</param>
        public void ValidateToken(string token)
        {
            // simple logic, we didn't implement authentication it yet.
            //if(token != "BND123456")
            //{
            //    throw new UnauthorizedAccessException("Unauthorized");
            //}
        }
    }
}