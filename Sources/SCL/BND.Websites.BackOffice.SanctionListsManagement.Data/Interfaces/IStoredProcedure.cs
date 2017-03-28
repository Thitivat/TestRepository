using System.Collections.Generic;

namespace BND.Websites.BackOffice.SanctionListsManagement.Domain.Interfaces
{
    /// <summary>
    /// Interface IStoredProcedure represents methods to execute stored procedure.
    /// </summary>
    public interface IStoredProcedure
    {
        /// <summary>
        /// Use this operation to execute any arbitrary SQL statements in SQL Server if you do not want any result set to be returned.
        /// You can use this operation to create database objects or change data in a database by executing UPDATE, INSERT, or DELETE statements.
        /// The return value of this operation is of Int32 data type, and:
        /// For the UPDATE, INSERT, and DELETE statements, the return value is the number of rows affected by the SQL statement.
        /// For all other types of statements, the return value is -1.
        /// </summary>
        /// <param name="storeProcedureName">Name of the store procedure.</param>
        /// <param name="parameters">The parameters of store procedure.</param>
        /// <returns>System.Int32.</returns>
        int ExecuteNonQuery(string storeProcedureName, Dictionary<string, object> parameters = null);

        /// <summary>
        /// Use this operation to execute any arbitrary SQL statements in SQL Server if you want the result set to be returned,
        /// <see cref="IEnumerable{T}" /> of TModel.
        /// </summary>
        /// <typeparam name="TResult">The model class that you want to return.</typeparam>
        /// <param name="storeProcedureName">Name of the store procedure.</param>
        /// <param name="parameters">The parameters of store procedure.</param>
        /// <returns>IEnumerable of TResult generic type.</returns>
        IEnumerable<TResult> ExecuteReader<TResult>(string storeProcedureName, Dictionary<string, object> parameters = null) where TResult : class;
        
        /// <summary>
        /// Use this operation to execute any arbitrary SQL statements in SQL Server to return a single value.
        /// This operation returns the value only in the first column of the first row in the result set returned by the SQL statement.
        /// </summary>
        /// <param name="storeProcedureName">Name of the store procedure.</param>
        /// <param name="parameters">The parameters of store procedure.</param>
        /// <returns>System.Object.</returns>
        object ExecuteScalar(string storeProcedureName, Dictionary<string, object> parameters = null);
    }
}
