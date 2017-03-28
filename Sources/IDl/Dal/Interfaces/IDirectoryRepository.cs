using BND.Services.Payments.iDeal.Dal.Pocos;
using System.Collections.Generic;

namespace BND.Services.Payments.iDeal.Dal
{
    /// <summary>
    /// Interface IDirectoryRepository performing directory table in database.
    /// </summary>
    public interface IDirectoryRepository
    {
        /// <summary>
        /// Gets the directory.
        /// </summary>
        /// <returns>p_Directory.</returns>
        p_Directory Get();

        /// <summary>
        /// Updates the directory.
        /// </summary>
        /// <param name="newDirectory">The new directory.</param>
        /// <returns>Affected row count.</returns>
        int UpdateDirectory(p_Directory newDirectory);
    }
}
