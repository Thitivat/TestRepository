using BND.Services.IbanStore.Proxy.NET4.Models;

namespace BND.Services.IbanStore.Proxy.NET4.Interfaces
{
    /// <summary>
    /// Interface IIbanResource
    /// </summary>
    public interface IIbanResource
    {
        /// <summary>
        /// Reserves the Iban which is the next available one.
        /// </summary>
        /// <param name="uidPrefix">The uid prefix.</param>
        /// <param name="uid">The uid.</param>
        /// <returns>Latest IBAN Id</returns>
        int ReserveNextAvailable(string uidPrefix, string uid);

        /// <summary>
        /// Assigns status of Iban
        /// </summary>
        /// <param name="uidPrefix">The uid prefix.</param>
        /// <param name="uid">The uid.</param>
        /// <param name="ibanId">The iban identifier.</param>
        /// <returns>Assigned IBAN Url</returns>
        string Assign(string uidPrefix, string uid, int ibanId);

        /// <summary>
        /// Gets the specified iban by iban parameter
        /// </summary>
        /// <param name="uidPrefix">The uid prefix.</param>
        /// <param name="uid">The uid.</param>
        /// <returns>Next available IBAN</returns>
        NextAvailable Get(string uidPrefix, string uid);

        /// <summary>
        /// Reservs the and asssign.
        /// </summary>
        /// <param name="uidPrefix">The uid prefix.</param>
        /// <param name="uid">The uid.</param>
        /// <returns>Next available IBAN.</returns>
        NextAvailable ReserveAndAssign(string uidPrefix, string uid);
    }
}
