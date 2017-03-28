using BND.Services.Security.OTP.Repositories.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Pocos = BND.Services.Security.OTP.Dal.Pocos;

namespace BND.Services.Security.OTP.DalTest
{
    [TestFixture]
    public class OtpUnitOfWorkTest
    {
        private bool _HasInitializedCompletely = false;
        private string _connectionString = "Data Source=.;Initial Catalog=BND.Services.Security.OTP.Db.Test;Integrated Security=True;Pooling=False;Connection Timeout=60";
        private MockDbUnitOfWork uof;
        
        [SetUp]
        public void Test_Initialize()
        {
            uof = new MockDbUnitOfWork();
            uof.GetRepository<Pocos.EnumStatus>().Insert(new Pocos.EnumStatus()
            {
                Status = "Pending",
                Description = "The code ready to use."
            });
            uof.GetRepository<Pocos.EnumStatus>().Insert(new Pocos.EnumStatus()
            {
                Status = "Expired",
                Description = "The code has expired."
            });
            uof.GetRepository<Pocos.EnumStatus>().Insert(new Pocos.EnumStatus()
            {
                Status = "Locked",
                Description = "The code is locked by entered wrong more than limitation."
            });
            uof.GetRepository<Pocos.EnumStatus>().Insert(new Pocos.EnumStatus()
            {
                Status = "Verified",
                Description = "The code is used."
            });
            uof.GetRepository<Pocos.EnumStatus>().Insert(new Pocos.EnumStatus()
            {
                Status = "Deleted",
                Description = "The code has been deleted by client or system."
            });
            uof.GetRepository<Pocos.EnumStatus>().Insert(new Pocos.EnumStatus()
            {
                Status = "Canceled",
                Description = "The code has been cancelled by system because client re-generate code again with same suid."
            });
            uof.GetRepository<Pocos.EnumChannelType>().Insert(new Pocos.EnumChannelType()
            {
                ChannelType = "SMS",
                Name = "Short Message Service"
            });
            uof.GetRepository<Pocos.EnumChannelType>().Insert(new Pocos.EnumChannelType()
            {
                ChannelType = "EMAIL",
                Name = "e-Mail"
            });
            uof.GetRepository<Pocos.Setting>().Insert(new Pocos.Setting()
            {
                Key = "Expiration",
                Value = "900"
            });
            uof.GetRepository<Pocos.Setting>().Insert(new Pocos.Setting()
            {
                Key = "RetryCount",
                Value = "3"
            });
            uof.GetRepository<Pocos.Account>().Insert(new Pocos.Account()
            {
                AccountId = new Guid("5861F73F-CAD5-419D-96D4-56BD07211297"),
                ApiKey = "lLTHykjZ3oZ5ORx1ePayit2+orgbNxI8d5N3296EhT7lSawnoygnfjKbyQdTrxVFLlZ8AfDjn1OnW5Hy2oAgWg==",
                Name = "AccountTest",
                IpAddress = "192.168.1.69",
                IsActive = true,
                Description = "This is for testing",
                Salt = "ALzIYz8BIgvLsk1q",
                Email = "Test@mail.com"
            });

            uof.Execute();
        }



        [Test]
        public void Test_Attempt_Success()
        {
            #region Create Otp for unit test
            Pocos.OneTimePassword Otp = new Pocos.OneTimePassword
            {
                OtpId = Guid.NewGuid().ToString(),
                AccountId = Guid.Parse("5861F73F-CAD5-419D-96D4-56BD07211297"),
                Suid = "System Unique Id",
                ChannelType = "EMAIL",
                ChannelSender = "BND",
                ChannelAddress = "customer1@bnd.com",
                ChannelMessage = "you have to approve this action",
                ExpiryDate = DateTime.Now.AddMinutes(5),
                Payload = null,
                RefCode = "1234",
                Code = "1000",
                Status = "Pending"
            };

            // Unit test insert.
            uof.GetRepository<Pocos.OneTimePassword>().Insert(Otp);
            uof.Execute();
            string OtpId = Otp.OtpId;
            #endregion

            Pocos.Attempt insertItem = new Pocos.Attempt
            {
                OtpId = OtpId,
                Date = DateTime.Now,
                IpAddress = "127.0.0.1",
                UserAgent = "Chrome"
            };

            // Unit test insert.
            uof.GetRepository<Pocos.Attempt>().Insert(insertItem);
            int rowAffected = uof.Execute();
            Assert.AreEqual(1, rowAffected);

            // keep id to another test case.
            int attemptId1 = insertItem.AttemptId;

            // Unit test get by id .
            var resultPoco = uof.GetRepository<Pocos.Attempt>().GetById(attemptId1);
            Assert.IsNotNull(resultPoco);
            Assert.AreEqual(insertItem, resultPoco);

            // Creates list for insert range.
            List<Pocos.Attempt> insertList = new List<Pocos.Attempt>
            {
                new Pocos.Attempt
                {
                    OtpId = OtpId,
                    Date = DateTime.Now,
                    IpAddress = "127.0.0.1",
                    UserAgent = "IE"
                },
                new Pocos.Attempt
                {
                    OtpId = OtpId,
                    Date = DateTime.Now,
                    IpAddress = "127.0.0.1",
                    UserAgent = "Firefox"
                },
                new Pocos.Attempt
                {
                    OtpId = OtpId,
                    Date = DateTime.Now,
                    IpAddress = "127.0.0.1",
                    UserAgent = "Safary"
                }
            };

            // Unit test insert range.
            uof.GetRepository<Pocos.Attempt>().Insert(insertList);
            rowAffected = uof.Execute();
            Assert.AreEqual(3, rowAffected);

            // Unit test get count.
            int count = uof.GetRepository<Pocos.Attempt>().GetCount();
            Assert.AreEqual(4, count);

            // Unit test all data are in the database.
            var resultPocos = uof.GetRepository<Pocos.Attempt>().Get();
            Assert.IsNotNull(resultPocos);
            Assert.AreEqual(4, resultPocos.Count());
            resultPoco = resultPocos.SingleOrDefault(s => s.AttemptId.Equals(insertItem.AttemptId));
            Assert.IsNotNull(resultPoco);
            resultPoco = resultPocos.SingleOrDefault(s => s.AttemptId.Equals(insertList[0].AttemptId));
            Assert.IsNotNull(resultPoco);
            resultPoco = resultPocos.SingleOrDefault(s => s.AttemptId.Equals(insertList[1].AttemptId));
            Assert.IsNotNull(resultPoco);
            resultPoco = resultPocos.SingleOrDefault(s => s.AttemptId.Equals(insertList[2].AttemptId));
            Assert.IsNotNull(resultPoco);

            // Creates page object
            Page<Pocos.Attempt> page = new Page<Pocos.Attempt>
            {
                Limit = 2,
                Offset = 0,
                OrderBy = p => p.OrderBy(o => o.AttemptId)
            };

            // Unit test get with page.
            resultPocos = uof.GetRepository<Pocos.Attempt>().Get(page: page);
            Assert.IsNotNull(resultPocos);
            Assert.AreEqual(2, resultPocos.Count());

            // Unit test get with orderBy.
            resultPocos = uof.GetRepository<Pocos.Attempt>().Get(orderBy: p => p.OrderByDescending(o => o.AttemptId));
            Assert.IsNotNull(resultPocos);
            Assert.AreEqual(4, resultPocos.Count());
            Assert.AreEqual(insertList.OrderByDescending(o => o.AttemptId).First(), resultPocos.First());

            // Modify object to update to db.
            insertItem.Date = DateTime.Now.AddDays(5);
            insertItem.IpAddress = "192.168.1.1";
            insertItem.UserAgent = "Chrome update";

            // Unit test update.
            uof.GetRepository<Pocos.Attempt>().Update(insertItem);
            rowAffected = uof.Execute();
            Assert.AreEqual(1, rowAffected);

            // Unit test get with filter to check record is updated.
            resultPocos = uof.GetRepository<Pocos.Attempt>().Get(p => p.IpAddress.Equals("192.168.1.1"));
            Assert.IsNotNull(resultPocos);
            Assert.AreEqual(1, resultPocos.Count());
            Assert.AreEqual(insertItem, resultPocos.First());

            // Unit test delete with id
            uof.GetRepository<Pocos.Attempt>().Delete(attemptId1);
            uof.Execute();
            count = uof.GetRepository<Pocos.Attempt>().GetCount();
            Assert.AreEqual(3, count);

            // Unit test delete with entity
            uof.GetRepository<Pocos.Attempt>().Delete(insertList[0]);
            uof.Execute();
            count = uof.GetRepository<Pocos.Attempt>().GetCount();
            Assert.AreEqual(2, count);

            // remove item in list that alredy delete from database.
            insertList.RemoveAt(0);

            // Unit test delete with collections of entity.
            uof.GetRepository<Pocos.Attempt>().Delete(insertList);
            uof.Execute();
            count = uof.GetRepository<Pocos.Attempt>().GetCount();
            Assert.AreEqual(0, count);
        }

        [Test]
        public void Test_LogRepository_Success()
        {
            Pocos.Log insertItem = new Pocos.Log
            {
                Prival = 1,
                Version = 1,
                Timestamp = DateTime.Now,
                Hostname = "BND server",
                AppName = "Portal",
                ProcId = "0001",
                MsgId = "Message Id",
                StructuredData = "[Structure Data]",
                Msg = "Message Log"
            };

            // Unit test insert.
            uof.GetRepository<Pocos.Log>().Insert(insertItem);
            int rowAffected = uof.Execute();
            Assert.AreEqual(1, rowAffected);

            // keep id to another test case.
            int logId1 = insertItem.LogId;

            // Unit test get by id .
            var resultPoco = uof.GetRepository<Pocos.Log>().GetById(logId1);
            Assert.IsNotNull(resultPoco);
            Assert.AreEqual(insertItem, resultPoco);

            // Creates list for insert range.
            List<Pocos.Log> insertList = new List<Pocos.Log>
            {
                new Pocos.Log
                {
                    Prival = 1,
                    Version = 1,
                    Timestamp = DateTime.Now,
                    Hostname = "BND server",
                    AppName = "Portal",
                    ProcId = "0002",
                    MsgId = "Message Id",
                    StructuredData = "[Structure Data]",
                    Msg = "Message Log"
                },
                new Pocos.Log
                {
                    Prival = 1,
                    Version = 1,
                    Timestamp = DateTime.Now,
                    Hostname = "BND server",
                    AppName = "Portal",
                    ProcId = "0003",
                    MsgId = "Message Id",
                    StructuredData = "[Structure Data]",
                    Msg = "Message Log"
                },
                new Pocos.Log
                {
                    Prival = 1,
                    Version = 1,
                    Timestamp = DateTime.Now,
                    Hostname = "BND server",
                    AppName = "Portal",
                    ProcId = "0004",
                    MsgId = "Message Id",
                    StructuredData = "[Structure Data]",
                    Msg = "Message Log"
                },
            };

            // Unit test insert range.
            uof.GetRepository<Pocos.Log>().Insert(insertList);
            rowAffected = uof.Execute();
            Assert.AreEqual(3, rowAffected);

            // Unit test get count.
            int count = uof.GetRepository<Pocos.Log>().GetCount();
            Assert.AreEqual(4, count);

            // Unit test get count with filter.
            count = uof.GetRepository<Pocos.Log>().GetCount(p => p.ProcId.Equals("0001"));
            Assert.AreEqual(1, count);

            // Unit test get count with filter that no result.
            count = uof.GetRepository<Pocos.Log>().GetCount(p => p.ProcId.Equals("No return value"));
            Assert.AreEqual(0, count);

            // Unit test all data are in the database.
            var resultPocos = uof.GetRepository<Pocos.Log>().Get();
            Assert.IsNotNull(resultPocos);
            Assert.AreEqual(4, resultPocos.Count());
            resultPoco = resultPocos.SingleOrDefault(s => s.ProcId.Equals(insertItem.ProcId));
            Assert.IsNotNull(resultPoco);
            resultPoco = resultPocos.SingleOrDefault(s => s.ProcId.Equals(insertList[0].ProcId));
            Assert.IsNotNull(resultPoco);
            resultPoco = resultPocos.SingleOrDefault(s => s.ProcId.Equals(insertList[1].ProcId));
            Assert.IsNotNull(resultPoco);
            resultPoco = resultPocos.SingleOrDefault(s => s.ProcId.Equals(insertList[2].ProcId));
            Assert.IsNotNull(resultPoco);


            // Unit test get with filter.
            resultPocos = uof.GetRepository<Pocos.Log>().Get(p => p.ProcId.Equals("0002"));
            Assert.IsNotNull(resultPocos);
            Assert.AreEqual(1, resultPocos.Count());
            Assert.AreEqual(insertList.First(), resultPocos.First());

            // Unit test get with filter that no result.
            resultPocos = uof.GetRepository<Pocos.Log>().Get(p => p.ProcId.Equals("No return value"));
            Assert.IsNotNull(resultPocos);
            Assert.AreEqual(0, resultPocos.Count());

            // Creates page object
            Page<Pocos.Log> page = new Page<Pocos.Log>
            {
                Limit = 2,
                Offset = 0,
                OrderBy = p => p.OrderBy(o => o.LogId)
            };

            // Unit test get with page.
            resultPocos = uof.GetRepository<Pocos.Log>().Get(page: page);
            Assert.IsNotNull(resultPocos);
            Assert.AreEqual(2, resultPocos.Count());

            // Unit test get with orderBy.
            resultPocos = uof.GetRepository<Pocos.Log>().Get(orderBy: p => p.OrderByDescending(o => o.LogId));
            Assert.IsNotNull(resultPocos);
            Assert.AreEqual(4, resultPocos.Count());
            Assert.AreEqual(insertList.OrderByDescending(o => o.LogId).First(), resultPocos.First());

            // Modify object to update to db.
            insertItem.Prival = 1;
            insertItem.Version = 1;
            insertItem.Timestamp = DateTime.Now;
            insertItem.Hostname = "BND server";
            insertItem.AppName = "Portal";
            insertItem.ProcId = "0004";
            insertItem.MsgId = "Message Id";
            insertItem.StructuredData = "[Structure Data]";
            insertItem.Msg = "Message Log is updated";

            // Unit test update.
            uof.GetRepository<Pocos.Log>().Update(insertItem);
            rowAffected = uof.Execute();
            Assert.AreEqual(1, rowAffected);

            // Unit test get with filter to check record is updated.
            resultPocos = uof.GetRepository<Pocos.Log>().Get(p => p.LogId.Equals(logId1));
            Assert.IsNotNull(resultPocos);
            Assert.AreEqual(1, resultPocos.Count());
            Assert.AreEqual(insertItem, resultPocos.First());

            // Unit test delete with id
            uof.GetRepository<Pocos.Log>().Delete(logId1);
            uof.Execute();
            count = uof.GetRepository<Pocos.Log>().GetCount();
            Assert.AreEqual(3, count);

            // Unit test delete with entity
            uof.GetRepository<Pocos.Log>().Delete(insertList[0]);
            uof.Execute();
            count = uof.GetRepository<Pocos.Log>().GetCount();
            Assert.AreEqual(2, count);

            // remove item in list that alredy delete from database.
            insertList.RemoveAt(0);

            // Unit test delete with collections of entity.
            uof.GetRepository<Pocos.Log>().Delete(insertList);
            uof.Execute();
            count = uof.GetRepository<Pocos.Log>().GetCount();
            Assert.AreEqual(0, count);
        }

        [Test]
        public void Test_OtpRepository_Success()
        {
            // remove initilize data.
            foreach (Pocos.OneTimePassword p in uof.GetRepository<Pocos.OneTimePassword>().Get())
            {
                uof.GetRepository<Pocos.OneTimePassword>().Delete(p);
            }
            uof.Execute();

            Pocos.OneTimePassword insertItem = new Pocos.OneTimePassword
            {
                OtpId = Guid.NewGuid().ToString(),
                AccountId = Guid.Parse("5861F73F-CAD5-419D-96D4-56BD07211297"),
                Suid = "System Unique Id",
                ChannelType = "EMAIL",
                ChannelSender = "BND",
                ChannelAddress = "customer1@bnd.com",
                ChannelMessage = "you have to approve this action",
                ExpiryDate = DateTime.Now.AddMinutes(5),
                Payload = null,
                RefCode = "1234",
                Code = "1000",
                Status = "Pending"
            };

            // Unit test insert.
            uof.GetRepository<Pocos.OneTimePassword>().Insert(insertItem);
            int rowAffected = uof.Execute();
            Assert.AreEqual(1, rowAffected);

            // keep id to another test case.
            string otpId1 = insertItem.OtpId;

            // Unit test get by id .
            var resultPoco = uof.GetRepository<Pocos.OneTimePassword>().GetById(otpId1);
            Assert.IsNotNull(resultPoco);
            Assert.AreEqual(insertItem, resultPoco);

            // Creates list for insert range.
            List<Pocos.OneTimePassword> insertList = new List<Pocos.OneTimePassword>
            {
                new Pocos.OneTimePassword
                {
                    OtpId = Guid.NewGuid().ToString(),
                    AccountId = Guid.Parse("5861F73F-CAD5-419D-96D4-56BD07211297"),
                    Suid = "System Unique Id",
                    ChannelType = "EMAIL",
                    ChannelSender ="BND",
                    ChannelAddress ="customer1@bnd.com",
                    ChannelMessage ="you have to approve this action",
                    ExpiryDate = DateTime.Now.AddMinutes(5),
                    Payload = null,
                    RefCode = "1234",
                    Code = "2000",
                    Status = "Deleted"
                },
                new Pocos.OneTimePassword
                {
                    OtpId = Guid.NewGuid().ToString(),
                    AccountId = Guid.Parse("5861F73F-CAD5-419D-96D4-56BD07211297"),
                    Suid = "System Unique Id",
                    ChannelType = "EMAIL",
                    ChannelSender ="BND",
                    ChannelAddress ="customer1@bnd.com",
                    ChannelMessage ="you have to approve this action",
                    ExpiryDate = DateTime.Now.AddMinutes(5),
                    Payload = null,
                    RefCode = "1234",
                    Code = "3000",
                    Status = "Expired"
                },
                new Pocos.OneTimePassword
                {
                    OtpId = Guid.NewGuid().ToString(),
                    AccountId = Guid.Parse("5861F73F-CAD5-419D-96D4-56BD07211297"),
                    Suid = "System Unique Id",
                    ChannelType = "EMAIL",
                    ChannelSender ="BND",
                    ChannelAddress ="customer1@bnd.com",
                    ChannelMessage ="you have to approve this action",
                    ExpiryDate = DateTime.Now.AddMinutes(5),
                    Payload = null,
                    RefCode = "1234",
                    Code = "4000",
                    Status = "Locked"
                },
            };

            // Unit test insert range.
            uof.GetRepository<Pocos.OneTimePassword>().Insert(insertList);
            rowAffected = uof.Execute();
            Assert.AreEqual(3, rowAffected);

            // Unit test get count.
            int count = uof.GetRepository<Pocos.OneTimePassword>().GetCount();
            Assert.AreEqual(4, count);

            // Unit test get count with filter.
            count = uof.GetRepository<Pocos.OneTimePassword>().GetCount(p => p.Code.Equals("1000"));
            Assert.AreEqual(1, count);

            // Unit test get count with filter that no result.
            count = uof.GetRepository<Pocos.OneTimePassword>().GetCount(p => p.Code.Equals("0"));
            Assert.AreEqual(0, count);

            // Unit test all data are in the database.
            var resultPocos = uof.GetRepository<Pocos.OneTimePassword>().Get();
            Assert.IsNotNull(resultPocos);
            Assert.AreEqual(4, resultPocos.Count());
            resultPoco = resultPocos.SingleOrDefault(s => s.OtpId.Equals(insertItem.OtpId));
            Assert.IsNotNull(resultPoco);
            resultPoco = resultPocos.SingleOrDefault(s => s.OtpId.Equals(insertList[0].OtpId));
            Assert.IsNotNull(resultPoco);
            resultPoco = resultPocos.SingleOrDefault(s => s.OtpId.Equals(insertList[1].OtpId));
            Assert.IsNotNull(resultPoco);
            resultPoco = resultPocos.SingleOrDefault(s => s.OtpId.Equals(insertList[2].OtpId));
            Assert.IsNotNull(resultPoco);

            // Unit test get with filter.
            resultPocos = uof.GetRepository<Pocos.OneTimePassword>().Get(p => p.Code.Equals("2000"));
            Assert.IsNotNull(resultPocos);
            Assert.AreEqual(1, resultPocos.Count());
            Assert.AreEqual(insertList.First(), resultPocos.First());

            // Unit test get with filter that no result.
            resultPocos = uof.GetRepository<Pocos.OneTimePassword>().Get(p => p.Code.Equals("0"));
            Assert.IsNotNull(resultPocos);
            Assert.AreEqual(0, resultPocos.Count());

            // Creates page object
            Page<Pocos.OneTimePassword> page = new Page<Pocos.OneTimePassword>
            {
                Limit = 2,
                Offset = 0,
                OrderBy = p => p.OrderBy(o => o.OtpId)
            };

            // Unit test get with page.
            resultPocos = uof.GetRepository<Pocos.OneTimePassword>().Get(page: page);
            Assert.IsNotNull(resultPocos);
            Assert.AreEqual(2, resultPocos.Count());

            // Unit test get with orderBy.
            resultPocos = uof.GetRepository<Pocos.OneTimePassword>().Get(orderBy: p => p.OrderByDescending(o => o.OtpId));
            Assert.IsNotNull(resultPocos);
            Assert.AreEqual(4, resultPocos.Count());
            Assert.AreEqual(insertList.OrderByDescending(o => o.Code).First(), resultPocos.OrderByDescending(o => o.Code).First());

            // Modify object to update to db.
            insertItem.Status = "Verified";

            // Unit test update.
            uof.GetRepository<Pocos.OneTimePassword>().Update(insertItem);
            rowAffected = uof.Execute();
            Assert.AreEqual(1, rowAffected);

            // Unit test get with filter to check record is updated.
            resultPocos = uof.GetRepository<Pocos.OneTimePassword>().Get(p => p.Status.Equals("Verified"));
            Assert.IsNotNull(resultPocos);
            Assert.AreEqual(1, resultPocos.Count());
            Assert.AreEqual(insertItem, resultPocos.First());

            // Unit test delete with id
            uof.GetRepository<Pocos.OneTimePassword>().Delete(otpId1);
            uof.Execute();
            count = uof.GetRepository<Pocos.OneTimePassword>().GetCount();
            Assert.AreEqual(3, count);

            // Unit test delete with entity
            uof.GetRepository<Pocos.OneTimePassword>().Delete(insertList[0]);
            uof.Execute();
            count = uof.GetRepository<Pocos.OneTimePassword>().GetCount();
            Assert.AreEqual(2, count);

            // remove item in list that alredy delete from database.
            insertList.RemoveAt(0);

            // Unit test delete with collections of entity.
            uof.GetRepository<Pocos.OneTimePassword>().Delete(insertList);
            uof.Execute();
            count = uof.GetRepository<Pocos.OneTimePassword>().GetCount();
            Assert.AreEqual(0, count);
        }

        [Test]
        public void Test_Setting_Success()
        {
            Pocos.Setting insertItem = new Pocos.Setting
            {
                Key = "Setting1",
                Value = "This is test no.1"
            };

            // Unit test insert.
            uof.GetRepository<Pocos.Setting>().Insert(insertItem);
            int rowAffected = uof.Execute();
            Assert.AreEqual(1, rowAffected);

            // keep id to another test case.
            string channelType1 = insertItem.Key;

            // Unit test get by id .
            var resultPoco = uof.GetRepository<Pocos.Setting>().GetById(channelType1);
            Assert.IsNotNull(resultPoco);
            Assert.AreEqual(insertItem, resultPoco);

            // Creates list for insert range.
            List<Pocos.Setting> insertList = new List<Pocos.Setting>
            {
                new Pocos.Setting
                {
                    Key = "Setting2",
                    Value = "This is test no.2"
                },
                new Pocos.Setting
                {
                    Key = "Setting3",
                    Value = "This is test no.3"
                },
                new Pocos.Setting
                {
                    Key = "Setting4",
                    Value = "This is test no.4"
                }
            };

            // Unit test insert range.
            uof.GetRepository<Pocos.Setting>().Insert(insertList);
            rowAffected = uof.Execute();
            Assert.AreEqual(3, rowAffected);

            // Unit test get count.
            int count = uof.GetRepository<Pocos.Setting>().GetCount();
            Assert.AreEqual(6, count);

            // Unit test get count with filter.
            count = uof.GetRepository<Pocos.Setting>().GetCount(p => p.Key.Equals("Setting1"));
            Assert.AreEqual(1, count);

            // Unit test get count with filter that no result.
            count = uof.GetRepository<Pocos.Setting>().GetCount(p => p.Key.Equals("No Result Condition"));
            Assert.AreEqual(0, count);

            // Unit test all data are in the database.
            var resultPocos = uof.GetRepository<Pocos.Setting>().Get();
            Assert.IsNotNull(resultPocos);
            Assert.AreEqual(6, resultPocos.Count());
            resultPoco = resultPocos.SingleOrDefault(s => s.Key.Equals(insertItem.Key));
            Assert.IsNotNull(resultPoco);
            resultPoco = resultPocos.SingleOrDefault(s => s.Key.Equals(insertList[0].Key));
            Assert.IsNotNull(resultPoco);
            resultPoco = resultPocos.SingleOrDefault(s => s.Key.Equals(insertList[1].Key));
            Assert.IsNotNull(resultPoco);
            resultPoco = resultPocos.SingleOrDefault(s => s.Key.Equals(insertList[2].Key));
            Assert.IsNotNull(resultPoco);

            // Unit test get with filter.
            resultPocos = uof.GetRepository<Pocos.Setting>().Get(p => p.Key.Equals("Setting2"));
            Assert.IsNotNull(resultPocos);
            Assert.AreEqual(1, resultPocos.Count());
            Assert.AreEqual(insertList.First(), resultPocos.First());

            // Unit test get with filter that no result.
            resultPocos = uof.GetRepository<Pocos.Setting>().Get(p => p.Key.Equals("No Result Condition"));
            Assert.IsNotNull(resultPocos);
            Assert.AreEqual(0, resultPocos.Count());

            // Creates page object
            Page<Pocos.Setting> page = new Page<Pocos.Setting>
            {
                Limit = 2,
                Offset = 0,
                OrderBy = p => p.OrderBy(o => o.Key)
            };

            // Unit test get with page.
            resultPocos = uof.GetRepository<Pocos.Setting>().Get(page: page);
            Assert.IsNotNull(resultPocos);
            Assert.AreEqual(2, resultPocos.Count());

            // Unit test get with orderBy.
            resultPocos = uof.GetRepository<Pocos.Setting>().Get(orderBy: p => p.OrderByDescending(o => o.Key));
            Assert.IsNotNull(resultPocos);
            Assert.AreEqual(6, resultPocos.Count());
            Assert.AreEqual(insertList.OrderByDescending(o => o.Key).First(), resultPocos.First());

            // Modify object to update to db.
            insertItem.Value = "This is test no.4 is updated";

            // Unit test update.
            uof.GetRepository<Pocos.Setting>().Update(insertItem);
            rowAffected = uof.Execute();
            Assert.AreEqual(1, rowAffected);

            // Unit test get with filter to check record is updated.
            resultPocos = uof.GetRepository<Pocos.Setting>().Get(p => p.Key.Equals("Setting1"));
            Assert.IsNotNull(resultPocos);
            Assert.AreEqual(1, resultPocos.Count());
            Assert.AreEqual(insertItem, resultPocos.First());

            // Unit test delete with id
            uof.GetRepository<Pocos.Setting>().Delete(channelType1);
            uof.Execute();
            count = uof.GetRepository<Pocos.Setting>().GetCount();
            Assert.AreEqual(5, count);

            // Unit test delete with entity
            uof.GetRepository<Pocos.Setting>().Delete(insertList[0]);
            uof.Execute();
            count = uof.GetRepository<Pocos.Setting>().GetCount();
            Assert.AreEqual(4, count);

            // remove item in list that already delete from database.
            insertList.RemoveAt(0);

            // Unit test delete with collections of entity.
            uof.GetRepository<Pocos.Setting>().Delete(insertList);
            uof.Execute();
            count = uof.GetRepository<Pocos.Setting>().GetCount();
            Assert.AreEqual(2, count);
        }

        [Test]
        public void Test_AccountRepository_Success()
        {
            // remove initilize data.
            foreach (Pocos.Account p in uof.GetRepository<Pocos.Account>().Get())
            {
                uof.GetRepository<Pocos.Account>().Delete(p);
            }
            uof.Execute();

            Pocos.Account insertItem = new Pocos.Account
            {
                AccountId = Guid.NewGuid(),
                ApiKey = "Apikey Item 1",
                Description = "Description",
                Email = "test@mail.com",
                IpAddress = "127.0.0.1",
                IsActive = true,
                Name = "Account Name 1",
                Salt = "1234567890123456"
            };

            // Unit test insert.
            uof.GetRepository<Pocos.Account>().Insert(insertItem);
            int rowAffected = uof.Execute();
            Assert.AreEqual(1, rowAffected);

            // keep id to another test case.
            Guid accountId1 = insertItem.AccountId;

            // Unit test get by id .
            var resultPoco = uof.GetRepository<Pocos.Account>().GetById(accountId1);
            Assert.IsNotNull(resultPoco);
            Assert.AreEqual(insertItem, resultPoco);

            // Creates list for insert range.
            List<Pocos.Account> insertList = new List<Pocos.Account>
            {
                new Pocos.Account
                {
                    AccountId = Guid.NewGuid(),
                    ApiKey = "Apikey Item 2",
                    Description = "Description",
                    Email = "test@mail.com",
                    IpAddress = "127.0.0.1",
                    IsActive = true,
                    Name = "Account Name 2",
                    Salt = "1234567890123457"
                },
                new Pocos.Account
                {
                    AccountId = Guid.NewGuid(),
                    ApiKey = "Apikey Item 3",
                    Description = "Description",
                    Email = "test@mail.com",
                    IpAddress = "127.0.0.1",
                    IsActive = true,
                    Name = "Account Name 3",
                    Salt = "1234567890123458"
                },
                new Pocos.Account
                {
                    AccountId = Guid.NewGuid(),
                    ApiKey = "Apikey Item 4",
                    Description = "Description",
                    Email = "test@mail.com",
                    IpAddress = "127.0.0.1",
                    IsActive = true,
                    Name = "Account Name 4",
                    Salt = "1234567890123459"
                }
            };

            // Unit test insert range.
            uof.GetRepository<Pocos.Account>().Insert(insertList);
            rowAffected = uof.Execute();
            Assert.AreEqual(3, rowAffected);

            // Unit test get count.
            int count = uof.GetRepository<Pocos.Account>().GetCount();
            Assert.AreEqual(4, count);

            // Unit test get count with filter.
            count = uof.GetRepository<Pocos.Account>().GetCount(p => p.Name.Equals("Account Name 1"));
            Assert.AreEqual(1, count);

            // Unit test get count with filter that no result.
            count = uof.GetRepository<Pocos.Account>().GetCount(p => p.Name.Equals("No Result Condition"));
            Assert.AreEqual(0, count);

            // Unit test all data are in the database.
            var resultPocos = uof.GetRepository<Pocos.Account>().Get();
            Assert.IsNotNull(resultPocos);
            Assert.AreEqual(4, resultPocos.Count());
            resultPoco = resultPocos.SingleOrDefault(s => s.AccountId.Equals(insertItem.AccountId));
            Assert.IsNotNull(resultPoco);
            resultPoco = resultPocos.SingleOrDefault(s => s.AccountId.Equals(insertList[0].AccountId));
            Assert.IsNotNull(resultPoco);
            resultPoco = resultPocos.SingleOrDefault(s => s.AccountId.Equals(insertList[1].AccountId));
            Assert.IsNotNull(resultPoco);
            resultPoco = resultPocos.SingleOrDefault(s => s.AccountId.Equals(insertList[2].AccountId));
            Assert.IsNotNull(resultPoco);

            // Unit test get with filter.
            resultPocos = uof.GetRepository<Pocos.Account>().Get(p => p.Name.Equals("Account Name 2"));
            Assert.IsNotNull(resultPocos);
            Assert.AreEqual(1, resultPocos.Count());
            Assert.AreEqual(insertList.First(), resultPocos.First());

            // Unit test get with filter that no result.
            resultPocos = uof.GetRepository<Pocos.Account>().Get(p => p.Name.Equals("No Result Condition"));
            Assert.IsNotNull(resultPocos);
            Assert.AreEqual(0, resultPocos.Count());

            // Creates page object
            Page<Pocos.Account> page = new Page<Pocos.Account>
            {
                Limit = 2,
                Offset = 0,
                OrderBy = p => p.OrderBy(o => o.Name)
            };

            // Unit test get with page.
            resultPocos = uof.GetRepository<Pocos.Account>().Get(page: page);
            Assert.IsNotNull(resultPocos);
            Assert.AreEqual(2, resultPocos.Count());

            // Unit test get with orderBy.
            resultPocos = uof.GetRepository<Pocos.Account>().Get(orderBy: p => p.OrderByDescending(o => o.Name));
            Assert.IsNotNull(resultPocos);
            Assert.AreEqual(4, resultPocos.Count());
            Assert.AreEqual(insertList.OrderByDescending(o => o.Name).First(), resultPocos.First());

            // Modify object to update to db.
            //insertItem.AccountId = Guid.NewGuid(),
            insertItem.ApiKey = "Apikey Item 1 is updated";
            insertItem.Description = "Description is updated";
            insertItem.Email = "update@mail.com";
            insertItem.IpAddress = "192.168.1.1";
            insertItem.IsActive = false;
            insertItem.Name = "Account Name 1 is updated";
            insertItem.Salt = "1234567890123450";

            // Unit test update.
            uof.GetRepository<Pocos.Account>().Update(insertItem);
            rowAffected = uof.Execute();
            Assert.AreEqual(1, rowAffected);

            // Unit test get with filter to check record is updated.
            resultPocos = uof.GetRepository<Pocos.Account>().Get(p => p.IpAddress.Equals("192.168.1.1"));
            Assert.IsNotNull(resultPocos);
            Assert.AreEqual(1, resultPocos.Count());
            Assert.AreEqual(insertItem, resultPocos.First());

            // Unit test delete with id
            uof.GetRepository<Pocos.Account>().Delete(accountId1);
            uof.Execute();
            count = uof.GetRepository<Pocos.Account>().GetCount();
            Assert.AreEqual(3, count);

            // Unit test delete with entity
            uof.GetRepository<Pocos.Account>().Delete(insertList[0]);
            uof.Execute();
            count = uof.GetRepository<Pocos.Account>().GetCount();
            Assert.AreEqual(2, count);

            // remove item in list that alredy delete from database.
            insertList.RemoveAt(0);

            // Unit test delete with collections of entity.
            uof.GetRepository<Pocos.Account>().Delete(insertList);
            uof.Execute();
            count = uof.GetRepository<Pocos.Account>().GetCount();
            Assert.AreEqual(0, count);
        }

        [Test]
        public void Test_EnumChannelType_Success()
        {
            // remove initilize data.
            foreach (Pocos.EnumChannelType p in uof.GetRepository<Pocos.EnumChannelType>().Get())
            {
                uof.GetRepository<Pocos.EnumChannelType>().Delete(p);
            }
            uof.Execute();

            Pocos.EnumChannelType insertItem = new Pocos.EnumChannelType
            {
                ChannelType = "Type1",
                Name = "This is test no.1"
            };

            // Unit test insert.
            uof.GetRepository<Pocos.EnumChannelType>().Insert(insertItem);
            int rowAffected = uof.Execute();
            Assert.AreEqual(1, rowAffected);

            // keep id to another test case.
            string channelType1 = insertItem.ChannelType;

            // Unit test get by id .
            var resultPoco = uof.GetRepository<Pocos.EnumChannelType>().GetById(channelType1);
            Assert.IsNotNull(resultPoco);
            Assert.AreEqual(insertItem, resultPoco);

            // Creates list for insert range.
            List<Pocos.EnumChannelType> insertList = new List<Pocos.EnumChannelType>
            {
                new Pocos.EnumChannelType
                {
                    ChannelType = "Type2",
                    Name = "This is test no.2"
                },
                new Pocos.EnumChannelType
                {
                    ChannelType = "Type3",
                    Name = "This is test no.3"
                },
                new Pocos.EnumChannelType
                {
                    ChannelType = "Type4",
                    Name = "This is test no.4"
                }
            };

            // Unit test insert range.
            uof.GetRepository<Pocos.EnumChannelType>().Insert(insertList);
            rowAffected = uof.Execute();
            Assert.AreEqual(3, rowAffected);

            // Unit test get count.
            int count = uof.GetRepository<Pocos.EnumChannelType>().GetCount();
            Assert.AreEqual(4, count);

            // Unit test get count with filter.
            count = uof.GetRepository<Pocos.EnumChannelType>().GetCount(p => p.ChannelType.Equals("Type1"));
            Assert.AreEqual(1, count);

            // Unit test get count with filter that no result.
            count = uof.GetRepository<Pocos.EnumChannelType>().GetCount(p => p.ChannelType.Equals("No Result Condition"));
            Assert.AreEqual(0, count);

            // Unit test all data are in the database.
            var resultPocos = uof.GetRepository<Pocos.EnumChannelType>().Get();
            Assert.IsNotNull(resultPocos);
            Assert.AreEqual(4, resultPocos.Count());
            resultPoco = resultPocos.SingleOrDefault(s => s.ChannelType.Equals(insertItem.ChannelType));
            Assert.IsNotNull(resultPoco);
            resultPoco = resultPocos.SingleOrDefault(s => s.ChannelType.Equals(insertList[0].ChannelType));
            Assert.IsNotNull(resultPoco);
            resultPoco = resultPocos.SingleOrDefault(s => s.ChannelType.Equals(insertList[1].ChannelType));
            Assert.IsNotNull(resultPoco);
            resultPoco = resultPocos.SingleOrDefault(s => s.ChannelType.Equals(insertList[2].ChannelType));
            Assert.IsNotNull(resultPoco);

            // Unit test get with filter.
            resultPocos = uof.GetRepository<Pocos.EnumChannelType>().Get(p => p.ChannelType.Equals("Type2"));
            Assert.IsNotNull(resultPocos);
            Assert.AreEqual(1, resultPocos.Count());
            Assert.AreEqual(insertList.First(), resultPocos.First());

            // Unit test get with filter that no result.
            resultPocos = uof.GetRepository<Pocos.EnumChannelType>().Get(p => p.ChannelType.Equals("No Result Condition"));
            Assert.IsNotNull(resultPocos);
            Assert.AreEqual(0, resultPocos.Count());

            // Creates page object
            Page<Pocos.EnumChannelType> page = new Page<Pocos.EnumChannelType>
            {
                Limit = 2,
                Offset = 0,
                OrderBy = p => p.OrderBy(o => o.ChannelType)
            };

            // Unit test get with page.
            resultPocos = uof.GetRepository<Pocos.EnumChannelType>().Get(page: page);
            Assert.IsNotNull(resultPocos);
            Assert.AreEqual(2, resultPocos.Count());

            // Unit test get with orderBy.
            resultPocos = uof.GetRepository<Pocos.EnumChannelType>().Get(orderBy: p => p.OrderByDescending(o => o.ChannelType));
            Assert.IsNotNull(resultPocos);
            Assert.AreEqual(4, resultPocos.Count());
            Assert.AreEqual(insertList.OrderByDescending(o => o.ChannelType).First(), resultPocos.First());

            // Modify object to update to db.
            insertItem.Name = "This is test no.4 is updated";

            // Unit test update.
            uof.GetRepository<Pocos.EnumChannelType>().Update(insertItem);
            rowAffected = uof.Execute();
            Assert.AreEqual(1, rowAffected);

            // Unit test get with filter to check record is updated.
            resultPocos = uof.GetRepository<Pocos.EnumChannelType>().Get(p => p.Name.Equals("This is test no.4 is updated"));
            Assert.IsNotNull(resultPocos);
            Assert.AreEqual(1, resultPocos.Count());
            Assert.AreEqual(insertItem, resultPocos.First());

            // Unit test delete with id
            uof.GetRepository<Pocos.EnumChannelType>().Delete(channelType1);
            uof.Execute();
            count = uof.GetRepository<Pocos.EnumChannelType>().GetCount();
            Assert.AreEqual(3, count);

            // Unit test delete with entity
            uof.GetRepository<Pocos.EnumChannelType>().Delete(insertList[0]);
            uof.Execute();
            count = uof.GetRepository<Pocos.EnumChannelType>().GetCount();
            Assert.AreEqual(2, count);

            // remove item in list that alredy delete from database.
            insertList.RemoveAt(0);

            // Unit test delete with collections of entity.
            uof.GetRepository<Pocos.EnumChannelType>().Delete(insertList);
            uof.Execute();
            count = uof.GetRepository<Pocos.EnumChannelType>().GetCount();
            Assert.AreEqual(0, count);
        }

        [Test]
        public void Test_EnumStatus_Success()
        {
            // remove initialize data.
            foreach (Pocos.EnumStatus p in uof.GetRepository<Pocos.EnumStatus>().Get())
            {
                uof.GetRepository<Pocos.EnumStatus>().Delete(p);
            }
            uof.Execute();

            Pocos.EnumStatus insertItem = new Pocos.EnumStatus
            {
                Status = "Status1",
                Description = "This is test no.1"
            };

            // Unit test insert.
            uof.GetRepository<Pocos.EnumStatus>().Insert(insertItem);
            int rowAffected = uof.Execute();
            Assert.AreEqual(1, rowAffected);

            // keep id to another test case.
            string channelType1 = insertItem.Status;

            // Unit test get by id .
            var resultPoco = uof.GetRepository<Pocos.EnumStatus>().GetById(channelType1);
            Assert.IsNotNull(resultPoco);
            Assert.AreEqual(insertItem, resultPoco);

            // Creates list for insert range.
            List<Pocos.EnumStatus> insertList = new List<Pocos.EnumStatus>
            {
                new Pocos.EnumStatus
                {
                    Status = "Status2",
                    Description = "This is test no.2"
                },
                new Pocos.EnumStatus
                {
                    Status = "Status3",
                    Description = "This is test no.3"
                },
                new Pocos.EnumStatus
                {
                    Status = "Status4",
                    Description = "This is test no.4"
                }
            };

            // Unit test insert range.
            uof.GetRepository<Pocos.EnumStatus>().Insert(insertList);
            rowAffected = uof.Execute();
            Assert.AreEqual(3, rowAffected);

            // Unit test get count.
            int count = uof.GetRepository<Pocos.EnumStatus>().GetCount();
            Assert.AreEqual(4, count);

            // Unit test get count with filter.
            count = uof.GetRepository<Pocos.EnumStatus>().GetCount(p => p.Status.Equals("Status1"));
            Assert.AreEqual(1, count);

            // Unit test get count with filter that no result.
            count = uof.GetRepository<Pocos.EnumStatus>().GetCount(p => p.Status.Equals("No Result Condition"));
            Assert.AreEqual(0, count);

            // Unit test all data are in the database.
            var resultPocos = uof.GetRepository<Pocos.EnumStatus>().Get();
            Assert.IsNotNull(resultPocos);
            Assert.AreEqual(4, resultPocos.Count());
            resultPoco = resultPocos.SingleOrDefault(s => s.Status.Equals(insertItem.Status));
            Assert.IsNotNull(resultPoco);
            resultPoco = resultPocos.SingleOrDefault(s => s.Status.Equals(insertList[0].Status));
            Assert.IsNotNull(resultPoco);
            resultPoco = resultPocos.SingleOrDefault(s => s.Status.Equals(insertList[1].Status));
            Assert.IsNotNull(resultPoco);
            resultPoco = resultPocos.SingleOrDefault(s => s.Status.Equals(insertList[2].Status));
            Assert.IsNotNull(resultPoco);

            // Unit test get with filter.
            resultPocos = uof.GetRepository<Pocos.EnumStatus>().Get(p => p.Status.Equals("Status2"));
            Assert.IsNotNull(resultPocos);
            Assert.AreEqual(1, resultPocos.Count());
            Assert.AreEqual(insertList.First(), resultPocos.First());

            // Unit test get with filter that no result.
            resultPocos = uof.GetRepository<Pocos.EnumStatus>().Get(p => p.Status.Equals("No Result Condition"));
            Assert.IsNotNull(resultPocos);
            Assert.AreEqual(0, resultPocos.Count());

            // Creates page object
            Page<Pocos.EnumStatus> page = new Page<Pocos.EnumStatus>
            {
                Limit = 2,
                Offset = 0,
                OrderBy = p => p.OrderBy(o => o.Status)
            };

            // Unit test get with page.
            resultPocos = uof.GetRepository<Pocos.EnumStatus>().Get(page: page);
            Assert.IsNotNull(resultPocos);
            Assert.AreEqual(2, resultPocos.Count());

            // Unit test get with orderBy.
            resultPocos = uof.GetRepository<Pocos.EnumStatus>().Get(orderBy: p => p.OrderByDescending(o => o.Status));
            Assert.IsNotNull(resultPocos);
            Assert.AreEqual(4, resultPocos.Count());
            Assert.AreEqual(insertList.OrderByDescending(o => o.Status).First(), resultPocos.First());

            // Modify object to update to db.
            insertItem.Description = "This is test no.4 is updated";

            // Unit test update.
            uof.GetRepository<Pocos.EnumStatus>().Update(insertItem);
            rowAffected = uof.Execute();
            Assert.AreEqual(1, rowAffected);

            // Unit test get with filter to check record is updated.
            resultPocos = uof.GetRepository<Pocos.EnumStatus>().Get(p => p.Status.Equals("Status1"));
            Assert.IsNotNull(resultPocos);
            Assert.AreEqual(1, resultPocos.Count());
            Assert.AreEqual(insertItem, resultPocos.First());

            // Unit test delete with id
            uof.GetRepository<Pocos.EnumStatus>().Delete(channelType1);
            uof.Execute();
            count = uof.GetRepository<Pocos.EnumStatus>().GetCount();
            Assert.AreEqual(3, count);

            // Unit test delete with entity
            uof.GetRepository<Pocos.EnumStatus>().Delete(insertList[0]);
            uof.Execute();
            count = uof.GetRepository<Pocos.EnumStatus>().GetCount();
            Assert.AreEqual(2, count);

            // remove item in list that alredy delete from database.
            insertList.RemoveAt(0);

            // Unit test delete with collections of entity.
            uof.GetRepository<Pocos.EnumStatus>().Delete(insertList);
            uof.Execute();
            count = uof.GetRepository<Pocos.EnumStatus>().GetCount();
            Assert.AreEqual(0, count);
        }
    }
}
