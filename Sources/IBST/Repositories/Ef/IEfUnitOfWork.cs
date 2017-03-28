using System.Collections.Generic;
namespace BND.Services.IbanStore.Repository.Interfaces
{
    /// <summary>
    /// Interface IStoredProcedureExecutable represents property type of <see cref="IStoredProcedure"/>.
    /// </summary>
    public interface IEfUnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Use this operation to execute any arbitrary SQL statements in SQL Server if you do not want any result set to be returned.
        /// You can use this operation to create database objects or change data in a database by executing UPDATE, INSERT, or DELETE statements.
        /// The return value of this operation is of Int32 data type, and:
        /// For the UPDATE, INSERT, and DELETE statements, the return value is the number of rows affected by the SQL statement.
        /// For all other types of statements, the return value is -1.
        /// </summary>
        /// <param name="nameOrSqlScript">The name or SQL script.</param>
        /// <param name="parameters">The parameters of store procedure.</param>
        /// <returns>System.Int32.</returns>
        int ExecuteNonQuery(string nameOrSqlScript, Dictionary<string, object> parameters = null);

        /// <summary>
        /// Use this operation to execute any arbitrary SQL statements in SQL Server if you want the result set to be returned,
        /// <see cref="System.Collections.Generic.IEnumerable" /> of TModel.
        /// </summary>
        /// <typeparam name="TResult">The model class that you want to return.</typeparam>
        /// <param name="nameOrSqlScript">The name or SQL script.</param>
        /// <param name="parameters">The parameters of store procedure.</param>
        /// <returns>IEnumerable of TResult generic type.</returns>
        IEnumerable<TResult> ExecuteReader<TResult>(string nameOrSqlScript, Dictionary<string, object> parameters = null) where TResult : class;

    }
}
