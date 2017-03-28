using BND.Services.Payments.eMandates.Entities;
using System;
using System.Collections.Generic;

namespace BND.Services.Payments.eMandates.Proxy.NET4.Interfaces
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
        IEnumerable<DirectoryModel> GetDirectories();
    }
}
