using Bndb.Kyc.Common.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Bndb.Kyc.SanctionLists.SanctionListsDalTest
{
    internal static class Helpers
    {
        public const string CONNECTION_STRING =
            "Data Source=.;Initial Catalog=Bndb.Kyc.SanctionLists.SanctionListsDb;Integrated Security=True;Pooling=False";

        public static bool InitializeDatabase()
        {
            bool result = false;
            string tempFile = Path.GetTempFileName();

            try
            {
                File.WriteAllText(tempFile, Properties.Resources.SanctionListsDb_Script);

                // Run sql script by using SQLCMD to create database and mock data for testing.
                using (Process p = new Process())
                {
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.CreateNoWindow = true;
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    p.StartInfo.FileName = "SQLCMD";
                    p.StartInfo.Arguments = "-s . -i " + tempFile;
                    p.StartInfo.RedirectStandardError = true;
                    p.Start();
                    p.WaitForExit();

                    string error = p.StandardError.ReadToEnd();
                    if (!String.IsNullOrEmpty(error))
                    {
                        throw new InvalidOperationException(error);
                    }

                    result = true;

                    // Deletes temp file.
                    File.Delete(tempFile);
                }
            }
            catch { }

            return result;
        }

        public static void VerifyUnitOfWork<TPocoEntity>(
            IUnitOfWork unitOfWork,
            IEnumerable<TPocoEntity> pocoEntities,
            GetParameters<TPocoEntity> getParameters,
            Func<TPocoEntity, object[]> onGetIds,
            Action<TPocoEntity> onChangingPocoEntity)
            where TPocoEntity : class
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }
            if (pocoEntities == null)
            {
                throw new ArgumentNullException("pocoEntities");
            }
            if (pocoEntities.Count() < 3)
            {
                throw new ArgumentOutOfRangeException("pocoEntities", "It must has at least 3 elements.");
            }
            if (onChangingPocoEntity == null)
            {
                throw new ArgumentNullException("onChangingPocoEntity");
            }
            if (getParameters == null)
            {
                throw new ArgumentNullException("getParameters");
            }
            if (onGetIds == null)
            {
                throw new ArgumentNullException("onGetIds");
            }

            TPocoEntity poco = pocoEntities.First();
            IEnumerable<TPocoEntity> pocos;
            int rowsAffected;

            // Tests Insert.
            unitOfWork.GetRepository<TPocoEntity>().Insert(poco);
            rowsAffected = unitOfWork.Execute();
            if (rowsAffected < 1)
            {
                throw new InvalidOperationException("[Insert][Single]: Failed.");
            }
            unitOfWork.GetRepository<TPocoEntity>().Insert(pocoEntities.Skip(1));
            rowsAffected = unitOfWork.Execute();
            if (rowsAffected < 1)
            {
                throw new InvalidOperationException("[Insert][Multiple]: Failed.");
            }

            // Tests Get by id.
            poco = unitOfWork.GetRepository<TPocoEntity>().GetById(onGetIds(poco));
            if (poco == null)
            {
                throw new InvalidOperationException("[GetById]: Failed.");
            }

            // Tests Get.
            pocos = unitOfWork.GetRepository<TPocoEntity>().Get(getParameters.Filter, getParameters.Page,
                                                                getParameters.OrderBy, getParameters.IncludeProperties);
            if (!pocos.Any())
            {
                throw new InvalidOperationException("[Get]: Failed.");
            }

            // Tests Update.
            onChangingPocoEntity(poco);
            unitOfWork.GetRepository<TPocoEntity>().Update(poco);
            rowsAffected = unitOfWork.Execute();
            if (rowsAffected < 1)
            {
                throw new InvalidOperationException("[Update]: Failed.");
            }

            // Tests Delete.
            unitOfWork.GetRepository<TPocoEntity>().Delete(onGetIds(poco));
            rowsAffected = unitOfWork.Execute();
            if (rowsAffected < 1)
            {
                throw new InvalidOperationException("[Delete][ById]: Failed.");
            }
            unitOfWork.GetRepository<TPocoEntity>().Delete(pocoEntities.ElementAt(1));
            rowsAffected = unitOfWork.Execute();
            if (rowsAffected < 1)
            {
                throw new InvalidOperationException("[Delete][Single]: Failed.");
            }
            unitOfWork.GetRepository<TPocoEntity>().Delete(pocoEntities.Skip(2));
            rowsAffected = unitOfWork.Execute();
            if (rowsAffected < 1)
            {
                throw new InvalidOperationException("[Delete][Multiple]: Failed.");
            }
        }
    }
}
