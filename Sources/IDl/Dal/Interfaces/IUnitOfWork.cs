
namespace BND.Services.Payments.iDeal.Dal
{
    /// <summary>
    /// Interface IUnitOfWork for unit of work.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Gets a repository by passing type of poco entity.
        /// </summary>
        /// <typeparam name="TPocoEntity">The type of the poco entity.</typeparam>
        /// <returns>A repository as <see cref="Bndb.Common.Repositories.Interfaces.IRepository{TPocoEntity}"/> interface.</returns>
        IRepository<TPocoEntity> GetRepository<TPocoEntity>() where TPocoEntity : class;

        /// <summary>
        /// Commit all changes by using repository to database.
        /// </summary>
        /// <returns>The number of objects written to the underlying database.</returns>
        int CommitChanges();

    }
}
