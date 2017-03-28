using BND.Services.IbanStore.Service;
using System;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using NUnit.Framework;

namespace BND.Services.IbanStore.ServiceTest
{
    
    [TestFixture]
    public class DbExceptionHelperTest
    {
        //[Test]
        //public void Test_DbExceptionHelper_Success()
        //{
        //    DbUpdateException dbException = new DbUpdateException();
        //    var pt = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType("BND.Services.IbanStore.Service", "BND.Services.IbanStore.Service.Bll.DbExceptionHelper");
        //    IbanOperationException actual = (IbanOperationException)pt.InvokeStatic("ThrowException", dbException, "Name", "Process");

        //    Assert.IsNotNull(actual);
        //    Assert.AreEqual(MessageLibs.MSG_CANNOT_PROCESS_DATABASE.Code, actual.ErrorCode);
        //}

        //[Test]
        //public void Test_DbExceptionHelper_Success_WithSqlException()
        //{
        //    try
        //    {
        //        SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=fakeddatabase");
        //        conn.Open();

        //        Assert.Fail();
        //    }
        //    catch (SqlException ex)
        //    {
        //        DbUpdateException dbException = new DbUpdateException("Error message", new Exception("Inner exception", ex));
        //        var pt = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType("BND.Services.IbanStore.Service", "BND.Services.IbanStore.Service.Bll.DbExceptionHelper");
        //        IbanOperationException actual = (IbanOperationException)pt.InvokeStatic("ThrowException", dbException, "Name", "Process");

        //        Assert.IsNotNull(actual);
        //        Assert.AreEqual(MessageLibs.MSG_CANNOT_PROCESS_DATABASE.Code, actual.ErrorCode);
        //    }
        //}

        //[Test]
        //public void Test_DbExceptionHelper_Success_WithSqlExceptionUniqueConstraintError()
        //{
        //    using (ShimsContext.Create())
        //    {
        //        System.Data.SqlClient.Fakes.ShimSqlException.AllInstances.NumberGet = (ex) => 2627;

        //        try
        //        {
        //            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=fakeddatabase");
        //            conn.Open();

        //            Assert.Fail();
        //        }
        //        catch (SqlException ex)
        //        {
        //            DbUpdateException dbException = new DbUpdateException("Error message", new Exception("Inner exception", ex));
        //            var pt = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType("BND.Services.IbanStore.Service", "BND.Services.IbanStore.Service.Bll.DbExceptionHelper");
        //            IbanOperationException actual = (IbanOperationException)pt.InvokeStatic("ThrowException", dbException, "Name", "Process");

        //            Assert.IsNotNull(actual);
        //            Assert.AreEqual(MessageLibs.MSG_ALREADY_EXIST_DATABASE.Code, actual.ErrorCode);
        //        }
        //    }
        //}
    }
}
