using BND.Services.Payments.eMandates.Entities;
using System;

namespace BND.Services.Payments.eMandates.Proxy.Interfaces
{
    /// <summary>
    /// Interface IDirectories
    /// </summary>
    public interface IDirectories : IDisposable
    {
        /// <summary>
        /// Gets the Debtor Banks.
        /// </summary>
        /// <returns>IEnumerable&lt;DirectoryModel&gt;.</returns>
        DirectoryModel GetDirectories();
    }
}
