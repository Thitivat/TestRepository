namespace BND.Services.Payments.iDeal.Api.Helpers
{
    /// <summary>
    /// Interface ISecurity provides the method to validate token
    /// </summary>
    public interface ISecurity
    {
        /// <summary>
        /// Validates the token.
        /// </summary>
        /// <param name="token">The token.</param>
        void ValidateToken(string token);
    }
}