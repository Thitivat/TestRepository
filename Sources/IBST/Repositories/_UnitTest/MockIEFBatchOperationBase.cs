using EntityFramework.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BND.Services.IbanStore.RepositoryTest
{
    internal class MockIEFBatchOperationBase<T> : IEFBatchOperationBase<DbContext, T> where T : class
    {
        public int TotalOperatedRow { get; set; }
        public void InsertAll<TEntity>(IEnumerable<TEntity> items, DbConnection connection = null, int? batchSize = null) where TEntity : class, T
        {
            TotalOperatedRow = items.Count();
        }

        public void UpdateAll<TEntity>(IEnumerable<TEntity> items, Action<UpdateSpecification<TEntity>> updateSpecification, DbConnection connection = null, int? batchSize = null) where TEntity : class, T
        {
            throw new NotImplementedException();
        }

        public IEFBatchOperationFiltered<DbContext, T> Where(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
