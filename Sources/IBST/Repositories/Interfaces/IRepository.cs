﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BND.Services.IbanStore.Repository.Models;

namespace BND.Services.IbanStore.Repository.Interfaces
{
    public interface IRepository<TPocoEntity>
        where TPocoEntity : class 
    {
        /// <summary>
        /// Gets poco entity representing data in database.
        /// </summary>
        /// <param name="filter">The filter data.</param>
        /// <param name="page">The pagination for retrieving scoped data.</param>
        /// <param name="orderBy">The ordering data.</param>
        /// <param name="includeProperties">The collection of property names of related poco entity for including data.</param>
        /// <returns>Collection of data as IEnumerable of poco entity.</returns>
        IEnumerable<TPocoEntity> Get(
            Expression<Func<TPocoEntity, bool>> filter = null,
            Page<TPocoEntity> page = null,
            Func<IQueryable<TPocoEntity>, IOrderedQueryable<TPocoEntity>> orderBy = null,
            params string[] includeProperties);

        /// <summary>
        /// Gets the query that allow outside to execute a query by them self, IQuery will not get data from database
        /// it will waiting when called methods that will the collection or object then will connect to database and get the data.
        /// </summary>
        /// <param name="filter">The filter data.</param>
        /// <param name="page">The pagination for retrieving scoped data.</param>
        /// <param name="orderBy">The ordering data.</param>
        /// <param name="includeProperties">The collection of property names of related poco entity for including data.</param>
        /// <returns> <see cref="System.Linq.IQueryable"/> </returns>
        IQueryable<TPocoEntity> GetQueryable(
            Expression<Func<TPocoEntity, bool>> filter = null,
            Page<TPocoEntity> page = null,
            Func<IQueryable<TPocoEntity>, IOrderedQueryable<TPocoEntity>> orderBy = null,
            params string[] includeProperties);

        /// <summary>
        /// Gets poco entity representing data in database by using primary key.
        /// </summary>
        /// <param name="ids">The primary key.</param>
        /// <returns>A data as poco entity. It will be null if could not found data.</returns>
        TPocoEntity GetById(params object[] ids);

        /// <summary>
        /// Gets the amount of data with filter if any.
        /// </summary>
        /// <param name="filter">The filter data.</param>
        /// <returns>Amount of data.</returns>
        int GetCount(Expression<Func<TPocoEntity, bool>> filter = null);

        /// <summary>
        /// Inserts a data as poco entity to database.
        /// </summary>
        /// <param name="entityToInsert">The entity to insert.</param>
        void Insert(TPocoEntity entityToInsert);

        /// <summary>
        /// Inserts the multiple data as collection of poco entities to database.
        /// </summary>
        /// <param name="entitiesToInsert">The collection of entities to insert.</param>
        void Insert(IEnumerable<TPocoEntity> entitiesToInsert);

        /// <summary>
        /// Updates a data as poco entity in database.
        /// </summary>
        /// <param name="entityToUpdate">The entity to update.</param>
        void Update(TPocoEntity entityToUpdate);

        /// <summary>
        /// Deletes a data by using primary key.
        /// </summary>
        /// <param name="ids">The primary key.</param>
        void Delete(params object[] ids);

        /// <summary>
        /// Deletes a data as poco entity from database.
        /// </summary>
        /// <param name="entityToDelete">The entity to delete.</param>
        void Delete(TPocoEntity entityToDelete);

        /// <summary>
        /// Deletes the multiple data as collection of poco entities from database.
        /// </summary>
        /// <param name="entitiesToDelete">The collection of entities to delete.</param>
        void Delete(IEnumerable<TPocoEntity> entitiesToDelete);
    }
}
