using BND.Services.Payments.iDeal.ClientData.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace BND.Services.Payments.iDeal.ClientData.Dal.Ef
{
    /// <summary>
    /// Class EfUnitOfWorkBase provides ef unit of work template for any unit of work.
    /// </summary>
    public abstract class EfUnitOfWorkBase : IUnitOfWork
    {
        #region [Fields]

        /// <summary>
        /// The _disposed flag.
        /// </summary>
        private bool _disposed = false;
        /// <summary>
        /// The database context that retrieved from entity framework for using in inherited class to modify.
        /// </summary>
        protected DbContext _context;

        /// <summary>
        /// Repositories's container that store the repositories to prevent to create new instance many times.
        /// </summary>
        protected Dictionary<string, object> _repositoriesContainer = null;

        #endregion


        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="EfUnitOfWorkBase" /> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="databaseProvider">The database provider that inherited from
        /// <see cref="System.Data.Entity.Core.Common.DbProviderServices" /> abstract class.</param>
        /// <param name="checkDbExists">if set to <c>true</c> check database exists, it will throw exception if not exists.</param>
        /// <param name="lazyLoad">if set to <c>true</c> EF will not load related entities, you should manual load that by eager loading.
        /// by default EF set this flag is <c>false</c>.</param>
        /// <exception cref="System.ArgumentNullException">When context parameter is null.</exception>
        /// <exception cref="System.ArgumentException">When database does not exist. This exception will occur when set checkDbExists to true.</exception>
        public EfUnitOfWorkBase(DbContext context, System.Data.Entity.Core.Common.DbProviderServices databaseProvider, bool checkDbExists = true, bool lazyLoad = true)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (checkDbExists && !context.Database.Exists())
            {
                throw new ArgumentException("The database does not exist. Please check your connection string.", "context");
            }

            _context = context;
            // Sets to false for high performance but has to use with ChangeTracker.DetectChanges method to detect later when you want.
            _context.Configuration.AutoDetectChangesEnabled = false;

            // Sets to false for high performance but has to use with eager loading for all navigation properties 
            // you want to use (Include on ObjectQuery), by default EF will set to false and no need to use eager loading
            // EF will automatic lading all related entities at first times.
            // <a href="https://msdn.microsoft.com/en-us/data/hh949853.aspx" target="_blank"> For more information.</a>
            _context.Configuration.LazyLoadingEnabled = lazyLoad;

            _repositoriesContainer = new Dictionary<string, object>();
        }

        #endregion


        #region [Methods]

        /// <summary>
        /// Gets a repository by passing type of poco entity.
        /// </summary>
        /// <typeparam name="TPocoEntity">The type of the poco entity.</typeparam>
        /// <returns>A repository as <see cref="Bndb.Kyc.Common.Repositories.Interfaces.IRepository&lt;TPocoEntity&gt;"/> interface.</returns>
        /// <exception cref="System.ObjectDisposedException">When object has been disposed.</exception>
        public virtual IRepository<TPocoEntity> GetRepository<TPocoEntity>()
            where TPocoEntity : class
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }


            string pocoName = typeof(TPocoEntity).Name;
            if (!_repositoriesContainer.ContainsKey(pocoName))
            {
                // Creates new Repository and store to private dictionary field.
                _repositoriesContainer.Add(pocoName, new EfRepository<TPocoEntity>(_context));
            }

            // return respository that keep in container field.
            return _repositoriesContainer[pocoName] as IRepository<TPocoEntity>;
        }

        /// <summary>
        /// Commit all changes by using repository to database.
        /// </summary>
        /// <returns>The number of objects written to the underlying database.</returns>
        /// <exception cref="System.ObjectDisposedException">When object has been disposed.</exception>
        public virtual int CommitChanges()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            try
            {
                _context.ChangeTracker.DetectChanges();

                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return 0;
            }
            finally
            {
                // Detaches all entities after SaveChanges in case of error will fine for next save.
                DetachAll();
            }
        }

        /// <summary>
        /// Detaches all of the DbEntityEntry objects that have been added to the ChangeTracker.
        /// </summary>
        private void DetachAll()
        {
            foreach (DbEntityEntry pocoEntity in this._context.ChangeTracker.Entries().Where(x => x.Entity != null))
            {
                if (pocoEntity.State != System.Data.Entity.EntityState.Unchanged)
                {
                    pocoEntity.State = System.Data.Entity.EntityState.Detached;
                }
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources;
        /// <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                if (_repositoriesContainer.Count() > 0)
                {
                    _repositoriesContainer.Clear();
                }
                _repositoriesContainer = null;

                _context.Dispose();
                _context = null;
            }

            _disposed = true;
        }

        #endregion
    }
}
