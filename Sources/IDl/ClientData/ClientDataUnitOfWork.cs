using BND.Services.Payments.iDeal.ClientData.Dal.Ef;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;

namespace BND.Services.Payments.iDeal.ClientData
{
    public class ClientDataUnitOfWork : EfUnitOfWorkBase
    {
        public ClientDataUnitOfWork(string connectionString, bool checkDbExists = true)
            : base(new ClientDataDbContext(new SqlConnection(connectionString)),
                   System.Data.Entity.SqlServer.SqlProviderServices.Instance,
                   checkDbExists)
        { }
    }
}
