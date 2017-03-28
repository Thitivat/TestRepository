

using System.Collections.Generic;
using BND.Services.IbanStore.Models;

namespace BND.Services.IbanStore.Service.Bll.Interfaces
{
    /// <summary>
    /// Interface IIBan
    /// </summary>
    public interface  IIbanManager
    {
        /// <summary>
        /// Reserves the specified uid.
        /// </summary>
        /// <param name="uid">The uid.</param>
        /// <param name="uidPrefix">The context.</param>
        /// <returns>Iban.</returns>
        Iban Reserve(string uid, string uidPrefix);

        /// <summary>
        /// Assigns the specified uid.
        /// </summary>
        /// <param name="ibanId">The iban identifier.</param>
        /// <param name="uid">The uid.</param>
        /// <param name="uidPrefix">The context.</param>
        /// <returns>Iban.</returns>
        Iban Assign(int ibanId, string uid, string uidPrefix);

        /// <summary>
        /// Gets all ibans.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="total">The total.</param>
        /// <param name="status">The status.</param>
        /// <param name="iban">The iban.</param>
        /// <returns>IEnumerable&lt;IBan&gt;.</returns>
        IEnumerable<Iban> Get(int? offset, int? limit, out int total, string status = null, string iban = null);

        /// <summary>
        /// Gets the specified uid.
        /// </summary>
        /// <param name="uid">The uid.</param>
        /// <param name="uidPrefix">The context.</param>
        /// <returns>Iban.</returns>
        Iban Get(string uid, string uidPrefix);

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Iban.</returns>
        Iban Get(int id);

        /// <summary>
        /// Gets the specified iban.
        /// </summary>
        /// <param name="iban">The iban.</param>
        /// <returns>Iban.</returns>
        Iban Get(string iban);

        /// <summary>
        /// Gets the history.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>IbanHistory.</returns>
        IEnumerable<Models.IbanHistory> GetHistory(int id);

    }
}
