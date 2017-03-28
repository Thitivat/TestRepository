using BND.Services.Security.OTP.Repositories;
using BND.Services.Security.OTP.Repositories.Interfaces;
using BND.Services.Security.OTP.Repositories.Models;
//using MsTest = Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BND.Services.Security.OTP.ApiUnitTest
{
    public static class MockRepository
    {
        public static IRepository<TPocoEntity> MockRepo<TPocoEntity>(
            List<TPocoEntity> getExpectedValue, TPocoEntity getByIdExpectedValue, int ExpectedCount)
            where TPocoEntity : class
        {
            Mock<IRepository<TPocoEntity>> repoMock = new Mock<IRepository<TPocoEntity>>();
            repoMock.Setup(r => r.Get(It.IsAny<Expression<Func<TPocoEntity, bool>>>(),
                                      It.IsAny<Page<TPocoEntity>>(),
                                      It.IsAny<Func<IQueryable<TPocoEntity>, IOrderedQueryable<TPocoEntity>>>(),
                                      It.IsAny<string[]>()))
                    .Returns(getExpectedValue);
            repoMock.Setup(r => r.GetById(It.IsAny<object[]>()))
                    .Returns(getByIdExpectedValue);
            repoMock.Setup(r => r.Insert(It.IsAny<TPocoEntity>()));
            repoMock.Setup(r => r.Insert(It.IsAny<IEnumerable<TPocoEntity>>()));
            repoMock.Setup(r => r.Update(It.IsAny<TPocoEntity>()));
            repoMock.Setup(r => r.Delete(It.IsAny<TPocoEntity>()));
            repoMock.Setup(r => r.Delete(It.IsAny<object[]>()));
            repoMock.Setup(r => r.Delete(It.IsAny<IEnumerable<TPocoEntity>>()));
            repoMock.Setup(r => r.GetQueryable(It.IsAny<Expression<Func<TPocoEntity, bool>>>(),
                                               It.IsAny<Page<TPocoEntity>>(),
                                               It.IsAny<Func<IQueryable<TPocoEntity>, IOrderedQueryable<TPocoEntity>>>(),
                                               It.IsAny<string[]>()))
                    .Returns(getExpectedValue.AsQueryable());
            repoMock.Setup(r => r.GetCount(It.IsAny<Expression<Func<TPocoEntity, bool>>>())).Returns(ExpectedCount);
            return repoMock.Object;
        }
    }
}
