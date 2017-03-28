using BND.Services.IbanStore.Repository.Interfaces;
using BND.Services.IbanStore.Repository.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BND.Services.IbanStore.ServiceTest
{
    public static class MockRepository
    {
        public static IRepository<TPocoEntity> MockRepo<TPocoEntity>(
            List<TPocoEntity> getExpectedValue, TPocoEntity getByIdExpectedValue, int expectedCount)
            where TPocoEntity : class
        {
            Mock<IRepository<TPocoEntity>> repoMock = new Mock<IRepository<TPocoEntity>>();
            repoMock.Setup(r => r.Get(It.IsAny<Expression<Func<TPocoEntity, bool>>>(),
                                      It.IsAny<Page<TPocoEntity>>(),
                                      It.IsAny<Func<IQueryable<TPocoEntity>, IOrderedQueryable<TPocoEntity>>>(),
                                      It.IsAny<string[]>()))
                    .Returns(getExpectedValue);
            repoMock.Setup(r => r.GetQueryable(It.IsAny<Expression<Func<TPocoEntity, bool>>>(),
                                      It.IsAny<Page<TPocoEntity>>(),
                                      It.IsAny<Func<IQueryable<TPocoEntity>, IOrderedQueryable<TPocoEntity>>>(),
                                      It.IsAny<string[]>()))
                    .Returns(getExpectedValue.AsQueryable());
            repoMock.Setup(r => r.GetById(It.IsAny<object[]>()))
                    .Returns(getByIdExpectedValue);
            repoMock.Setup(r => r.Insert(It.IsAny<TPocoEntity>()));
            repoMock.Setup(r => r.Insert(It.IsAny<IEnumerable<TPocoEntity>>()));
            repoMock.Setup(r => r.Update(It.IsAny<TPocoEntity>()));
            repoMock.Setup(r => r.Delete(It.IsAny<TPocoEntity>()));
            repoMock.Setup(r => r.Delete(It.IsAny<object[]>()));
            repoMock.Setup(r => r.Delete(It.IsAny<IEnumerable<TPocoEntity>>()));
            repoMock.Setup(r => r.GetCount(It.IsAny<Expression<Func<TPocoEntity, bool>>>())).Returns(expectedCount);
            return repoMock.Object;

        }


        public static void SetupIQueryable<TRepository, TEntity>(this Mock<TRepository> mock, IQueryable<TEntity> queryable)
            where TRepository : class, IQueryable<TEntity>
        {
            mock.Setup(r => r.GetEnumerator()).Returns(queryable.GetEnumerator());
            mock.Setup(r => r.Provider).Returns(queryable.Provider);
            mock.Setup(r => r.ElementType).Returns(queryable.ElementType);
            mock.Setup(r => r.Expression).Returns(queryable.Expression);
        }
    }
}
