using BND.Services.Payments.eMandates.Models;
using System;
using System.Collections.Generic;

namespace BND.Services.Payments.eMandates.Domain.Interfaces
{
    public interface IDirectoryRepository : IDisposable
    {
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>Directory.</returns>
        Directory Get();

        /// <summary>
        /// Updates the directory.
        /// </summary>
        /// <param name="newDirectory">The new directory.</param>
        /// <returns>System.Int32.</returns>
        int UpdateDirectory(Directory newDirectory);
    }
}
