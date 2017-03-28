using BND.Services.Security.OTP.Dal;
using BND.Services.Security.OTP.Repositories.Ef;

namespace BND.Services.Security.OTP.DalTest
{
    public class MockDbUnitOfWork:EfUnitOfWorkBase
    {
        public MockDbUnitOfWork() : base(new OtpContext(Effort.DbConnectionFactory.CreateTransient()),Effort.Provider.EffortProviderServices.Instance,false,false)
        {
        }
    }
}
