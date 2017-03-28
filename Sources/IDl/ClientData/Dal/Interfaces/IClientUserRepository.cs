
namespace BND.Services.Payments.iDeal.ClientData.Dal.Interfaces
{
    /// <summary>
    /// Interface IProductBankAccountRepository
    /// </summary>
    public interface IClientUserRepository
    {
        /// <summary>
        /// Gets the name of the client.
        /// </summary>
        /// <param name="iban">The iban.</param>
        /// <returns>System.String.</returns>
        string GetClientName(string iban);
    }
}
