using System;
using System.Collections.Generic;

namespace Bndb.Kyc.SanctionLists.SanctionListsDalTest
{
    internal class UnitOfWorkTestObject<TPocoEntity>
        where TPocoEntity : class
    {
        public IEnumerable<TPocoEntity> PocoEntities { get; set; }
        public GetParameters<TPocoEntity> GetParameters { get; set; }
        public Func<TPocoEntity, object[]> OnGetIds { get; set; }
        public Action<TPocoEntity> OnChangingPocoEntity { get; set; }
    }
}
