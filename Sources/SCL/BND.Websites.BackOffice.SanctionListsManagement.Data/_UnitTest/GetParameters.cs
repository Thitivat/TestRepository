using Bndb.Kyc.Common.Repositories.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Bndb.Kyc.SanctionLists.SanctionListsDalTest
{
    internal class GetParameters<TPocoEntity>
    {
        public Expression<Func<TPocoEntity, bool>> Filter { get; set; }
        public Page<TPocoEntity> Page { get; set; }
        public Func<IQueryable<TPocoEntity>, IOrderedQueryable<TPocoEntity>> OrderBy { get; set; }
        public string[] IncludeProperties { get; set; }
    }
}
