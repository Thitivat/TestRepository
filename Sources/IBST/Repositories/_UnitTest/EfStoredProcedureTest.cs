using BND.Services.IbanStore.Repository.Ef;
using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace BND.Services.IbanStore.RepositoryTest
{
    [TestFixture]
    public class EfStoredProcedureTest
    {
        //private MockDbContext _dbContext;

        //[Setup]
        //public void Test_Initialize()
        //{
        //    if (_dbContext != null)
        //    {
        //        _dbContext.Dispose();
        //    }
        //    _dbContext = null;
        //    _dbContext = new MockDbContext();
        //}

        //#region [ExecuteScalar]

        //[Test]
        //public void Test_ExecuteScalar_Success()
        //{
        //    var actual = new PrivateObject(new EfStoredProcedure(_dbContext));

        //    // Mock parameters
        //    var parameters = new Dictionary<string, object>();
        //    parameters.Add("parameter_name", "value_object");

        //    // Mock DbCommand
        //    Mock<IDbCommand> mockCommand = new Mock<IDbCommand>();
        //    object expcetedResult = "Executed result";
        //    mockCommand.Setup(m => m.ExecuteScalar()).Returns(expcetedResult);
        //    mockCommand.Setup(m => m.CreateParameter()).Returns(new SqlParameter());

        //    // Mock for _dbCommand.Parameters.Add(dbParam) 
        //    var paramsMock = new Mock<IDataParameterCollection>();
        //    var valueMock = new Mock<IDataParameter>();
        //    valueMock.Setup(o => o.Value);
        //    paramsMock.Object.Add(valueMock.Object);
        //    mockCommand.Setup(o => o.Parameters).Returns(paramsMock.Object);

        //    actual.SetField("_dbCommand", mockCommand.Object);

        //    object result = actual.Invoke("ExecuteScalar", "storedProcedureName", parameters);

        //    Assert.AreEqual(expcetedResult, result);

        //}

        //[Test]
        //public void Test_ExecuteScalar_Success_NoParameters()
        //{
        //    var actual = new PrivateObject(new EfStoredProcedure(_dbContext));

        //    // Mock parameters
        //    Dictionary<string, object> parameters = null;

        //    // Mock DbCommand
        //    Mock<IDbCommand> mockCommand = new Mock<IDbCommand>();
        //    object expcetedResult = "Executed result";
        //    mockCommand.Setup(m => m.ExecuteScalar()).Returns(expcetedResult);
        //    mockCommand.Setup(m => m.CreateParameter()).Returns(new SqlParameter());

        //    // Mock for _dbCommand.Parameters.Add(dbParam) 
        //    var paramsMock = new Mock<IDataParameterCollection>();
        //    var valueMock = new Mock<IDataParameter>();
        //    valueMock.Setup(o => o.Value);
        //    paramsMock.Object.Add(valueMock.Object);
        //    mockCommand.Setup(o => o.Parameters).Returns(paramsMock.Object);

        //    actual.SetField("_dbCommand", mockCommand.Object);

        //    object result = actual.Invoke("ExecuteScalar", "storedProcedureName", parameters);

        //    Assert.AreEqual(expcetedResult, result);

        //}

        //[Test]
        //[ExpectedException(typeof(ArgumentNullException))]
        //public void Test_ExecuteScalar_Exception_ArgumentNull()
        //{
        //    var actual = new PrivateObject(new EfStoredProcedure(_dbContext));

        //    var parameters = new Dictionary<string, object>();

        //    // Mock DbCommand
        //    Mock<IDbCommand> mockCommand = new Mock<IDbCommand>();
        //    object expcetedResult = "Executed result";
        //    mockCommand.Setup(m => m.ExecuteScalar()).Returns(expcetedResult);

        //    actual.SetField("_dbCommand", mockCommand.Object);
        //    string parameterNull = null;
        //    object result = actual.Invoke("ExecuteScalar", parameterNull, parameters);
        //}

        //[Test]
        //[ExpectedException(typeof(NotSupportedException))]
        //public void Test_ExecuteScalar_Exception_CommandCannotExecute()
        //{
        //    var actual = new PrivateObject(new EfStoredProcedure(_dbContext));

        //    var parameters = new Dictionary<string, object>();

        //    // Not Mock DbCommand test case will throw exception.

        //    object result = actual.Invoke("ExecuteScalar", "storedProcedureName", parameters);
        //}

        //#endregion

        //#region [ExecuteNonQuery]

        //[Test]
        //public void Test_ExecuteNonQuery_Success()
        //{
        //    var actual = new PrivateObject(new EfStoredProcedure(_dbContext));

        //    // Mock parameters
        //    var parameters = new Dictionary<string, object>();
        //    parameters.Add("parameter_name", "value_object");

        //    // Mock DbCommand
        //    Mock<IDbCommand> mockCommand = new Mock<IDbCommand>();
        //    int expcetedResult = 1;
        //    mockCommand.Setup(m => m.ExecuteNonQuery()).Returns(expcetedResult);
        //    mockCommand.Setup(m => m.CreateParameter()).Returns(new SqlParameter());

        //    // Mock for _dbCommand.Parameters.Add(dbParam) 
        //    var paramsMock = new Mock<IDataParameterCollection>();
        //    var valueMock = new Mock<IDataParameter>();
        //    valueMock.Setup(o => o.Value);
        //    paramsMock.Object.Add(valueMock.Object);
        //    mockCommand.Setup(o => o.Parameters).Returns(paramsMock.Object);

        //    actual.SetField("_dbCommand", mockCommand.Object);

        //    object result = actual.Invoke("ExecuteNonQuery", "storedProcedureName", parameters);

        //    Assert.AreEqual(expcetedResult, result);

        //}


        //#endregion

        //#region [ExecuteReader]

        //class MockModel
        //{
        //    public string Fields1 { get; set; }
        //}

        //[Test]
        //public void Test_ExecuteReader_Success()
        //{
        //    var actual = new PrivateObject(new EfStoredProcedure(_dbContext));

        //    // Mock parameters
        //    var parameters = new Dictionary<string, object>();
        //    parameters.Add("parameter_name", "value_object");

        //    // Mock reader
        //    var mockReader = new Mock<IDataReader>();
        //    // This var stores current position in 'ojectsToEmulate' list
        //    bool readFlag = true;
        //    mockReader.Setup(x => x.Read()).Returns(() => readFlag).Callback(() => readFlag = false);
        //    mockReader.Setup(x => x.FieldCount).Returns(1);
        //    mockReader.Setup(x => x.GetName(0)).Returns("Fields1");
        //    mockReader.SetupGet<object>(x => x[0]).Returns("result data");

        //    // Mock DbCommand
        //    Mock<IDbCommand> mockCommand = new Mock<IDbCommand>();
        //    mockCommand.Setup(m => m.ExecuteReader()).Returns(mockReader.Object);
        //    mockCommand.Setup(m => m.CreateParameter()).Returns(new SqlParameter());

        //    // Mock for _dbCommand.Parameters.Add(dbParam) 
        //    var paramsMock = new Mock<IDataParameterCollection>();
        //    var valueMock = new Mock<IDataParameter>();
        //    valueMock.Setup(m => m.Value);
        //    paramsMock.Object.Add(valueMock.Object);
        //    mockCommand.Setup(m => m.Parameters).Returns(paramsMock.Object);

        //    actual.SetField("_dbCommand", mockCommand.Object);

        //    IEnumerable<MockModel> expectedResult = new List<MockModel>() 
        //    {
        //        new MockModel
        //        {
        //            Fields1 = "result data"
        //        }
        //    };
        //    var result = ((EfStoredProcedure)actual.Target).ExecuteReader<MockModel>("storedProcedureName", parameters);
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(expectedResult.Count(), result.Count());
        //    Assert.AreEqual(expectedResult.First().Fields1, result.First().Fields1);

        //}

        //#endregion
    }
}
