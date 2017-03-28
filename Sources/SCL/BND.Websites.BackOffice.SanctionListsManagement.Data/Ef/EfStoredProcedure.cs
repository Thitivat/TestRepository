using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using BND.Websites.BackOffice.SanctionListsManagement.Domain.Interfaces;

namespace BND.Websites.BackOffice.SanctionListsManagement.Domain.Ef
{
    public class EfStoredProcedure : IStoredProcedure
    {
        /// <summary>
        /// The database context that retrieved from entity framework for using in inherited class to modify.
        /// </summary>
        protected DbContext _context;

        /// <summary>
        /// Instance of IDbCommand that Represents an SQL statement or stored procedure to execute against a data source. 
        /// </summary>
        protected IDbCommand _dbCommand;

        public EfStoredProcedure(DbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Use this operation to execute any arbitrary SQL statements in SQL Server if you want the result set to be returned,
        /// <see cref="IEnumerable{T}" /> of TModel.
        /// </summary>
        /// <typeparam name="TResult">The model class that you want to return.</typeparam>
        /// <param name="storeProcedureName">Name of the store procedure.</param>
        /// <param name="parameters">The parameters of store procedure.</param>
        /// <returns>IEnumerable of TResult generic type.</returns>
        /// <exception cref="System.ObjectDisposedException">When object has been disposed.</exception>
        /// <exception cref="System.ArgumentNullException">storeProcedureName</exception>
        public virtual IEnumerable<TResult> ExecuteReader<TResult>(string storeProcedureName, Dictionary<string, object> parameters = null)
            where TResult : class
        {
            // setting _dbCommand parameters.
            SetDbCommandParameters(storeProcedureName, parameters);

            IDataReader reader = null;

            try
            {
                // Check connection state it should be open before execute.
                if (_context.Database.Connection.State != ConnectionState.Open)
                {
                    (_context as IObjectContextAdapter).ObjectContext.Connection.Open();
                }

                reader = _dbCommand.ExecuteReader();
                List<TResult> result = (List<TResult>)Activator.CreateInstance(typeof(List<>).MakeGenericType(typeof(TResult)));

                string[] fields = new string[reader.FieldCount];

                // Get all db's fields to compare with TModel properties.
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    fields[i] = reader.GetName(i);
                }

                // Read data and assign to List<TModel>
                while (reader.Read())
                {
                    // Create TModel instance
                    TResult model = Activator.CreateInstance<TResult>();
                    foreach (PropertyInfo prop in typeof(TResult).GetProperties())
                    {
                        int index;
                        if ((index = Array.FindIndex(fields, f => f.Equals(prop.Name))) != -1)
                        {
                            prop.SetValue(model, reader[index]);
                        }
                    }

                    result.Add(model);
                }

                return result;
            }
            finally
            {
                // Release all resource after finished.
                if (reader != null)
                {
                    reader.Close();
                    reader.Dispose();
                }
                if (_dbCommand != null)
                {
                    _dbCommand.Dispose();
                    _dbCommand = null;
                }
                (_context as IObjectContextAdapter).ObjectContext.Connection.Close();
                GC.SuppressFinalize(this);
            }

        }

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
        /// <exception cref="System.ObjectDisposedException"></exception>
        /// <exception cref="System.ArgumentNullException">storeProcedureName</exception>
        public virtual int ExecuteNonQuery(string storeProcedureName, Dictionary<string, object> parameters = null)
        {
            // setting _dbCommand parameters.
            SetDbCommandParameters(storeProcedureName, parameters);

            try
            {
                // Check connection state it should be open before execute.
                if (_context.Database.Connection.State != ConnectionState.Open)
                {
                    (_context as IObjectContextAdapter).ObjectContext.Connection.Open();
                }

                return _dbCommand.ExecuteNonQuery();
            }
            finally
            {
                // Release all resource after finished.
                if (_dbCommand != null)
                {
                    _dbCommand.Dispose();
                    _dbCommand = null;
                }
                (_context as IObjectContextAdapter).ObjectContext.Connection.Close();
                GC.SuppressFinalize(this);
            }
        }

        /// <summary>
        /// Use this operation to execute any arbitrary SQL statements in SQL Server to return a single value.
        /// This operation returns the value only in the first column of the first row in the result set returned by the SQL statement.
        /// </summary>
        /// <param name="storeProcedureName">Name of the store procedure.</param>
        /// <param name="parameters">The parameters of store procedure.</param>
        /// <returns>System.Object.</returns>
        /// <exception cref="System.ObjectDisposedException"></exception>
        /// <exception cref="System.ArgumentNullException">storeProcedureName</exception>
        public virtual object ExecuteScalar(string storeProcedureName, Dictionary<string, object> parameters = null)
        {
            // setting _dbCommand parameters.
            SetDbCommandParameters(storeProcedureName, parameters);

            try
            {
                // Check connection state it should be open before execute.
                if (_context.Database.Connection.State != ConnectionState.Open)
                {
                    (_context as IObjectContextAdapter).ObjectContext.Connection.Open();
                }

                return _dbCommand.ExecuteScalar();
            }
            finally
            {
                // Release all resource after finished.
                if (_dbCommand != null)
                {
                    _dbCommand.Dispose();
                    _dbCommand = null;
                }
                (_context as IObjectContextAdapter).ObjectContext.Connection.Close();
                GC.SuppressFinalize(this);
            }
        }

        /// <summary>
        /// Setting up parameters for _dbCommand field.
        /// </summary>
        /// <param name="storeProcedureName">Name of the store procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <exception cref="System.ArgumentNullException">storeProcedureName</exception>
        protected void SetDbCommandParameters(string storeProcedureName, Dictionary<string, object> parameters)
        {
            if (String.IsNullOrEmpty(storeProcedureName))
            {
                throw new ArgumentNullException("storeProcedureName");
            }

            if (_dbCommand == null)
            {
                _dbCommand = _context.Database.Connection.CreateCommand();
            }

            _dbCommand.CommandText = storeProcedureName;
            _dbCommand.CommandType = CommandType.StoredProcedure;

            if (parameters != null && parameters.Any())
            {
                foreach (var param in parameters)
                {
                    DbParameter dbParam = _dbCommand.CreateParameter() as DbParameter;
                    if (dbParam != null)
                    {
                        dbParam.ParameterName = param.Key;
                        dbParam.Value = param.Value;

                        _dbCommand.Parameters.Add(dbParam);
                    }
                }
            }
        }


    }
}
