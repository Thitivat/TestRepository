namespace BND.Websites.BackOffice.SanctionListsManagement.Domain.Interfaces
{
    /// <summary>
    /// Interface IStoredProcedureExecutable
    /// </summary>
    public interface IStoredProcedureExecutable
    {
        /// <summary>
        /// Gets the stored procedure.
        /// </summary>
        /// <value>The stored procedure.</value>
        IStoredProcedure StoredProcedure { get; }
    }
}
