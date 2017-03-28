using BND.Services.Payments.iDeal.Models;
using System;
using System.Collections.Generic;

namespace BND.Services.Payments.iDeal.Proxy.NET4.Interfaces
{
    /// <summary>
    /// Interface IDirectories is an interface providing Directory resource.
    /// </summary>
    public interface IDirectories : IDisposable
    {
        /// <summary>
        /// Gets the directory.
        /// </summary>
        /// <returns>IEnumerable{DirectoryModel}.</returns>
        IEnumerable<DirectoryModel> GetDirectories();
    }
}
