using System;
using System.Diagnostics;

namespace BND.Services.IbanStore.ServiceTest
{
    public class Helpers
    {
        public static bool InitializeDatabase()
        {
            bool result = false;
            string scriptFile = "Scripts//Bndb.Kyc.Otp.Db_Create.sql";
            string scriptClear = "Scripts//ClearData.sql";

            try
            {
                // Run sql script by using SQLCMD to create database and mock data for testing.
                using (Process p = new Process())
                {
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.CreateNoWindow = true;
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    p.StartInfo.FileName = "SQLCMD";
                    p.StartInfo.Arguments = "-s . -i " + scriptFile;
                    p.StartInfo.RedirectStandardError = true;
                    p.Start();
                    p.WaitForExit();

                    string error = p.StandardError.ReadToEnd();
                    if (!String.IsNullOrEmpty(error))
                    {
                        throw new InvalidOperationException(error);
                    }
                }

                using (Process p = new Process())
                {
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.CreateNoWindow = true;
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    p.StartInfo.FileName = "SQLCMD";
                    p.StartInfo.Arguments = "-s . -i " + scriptClear;
                    p.StartInfo.RedirectStandardError = true;
                    p.Start();
                    p.WaitForExit();

                    string error = p.StandardError.ReadToEnd();
                    if (!String.IsNullOrEmpty(error))
                    {
                        throw new InvalidOperationException(error);
                    }

                    result = true;

                }
            }
            catch { }

            return result;
        }

        public static bool RunSQLScript(string scriptFile)
        {
            bool result = false;
            try
            {
                // Run sql script by using SQLCMD to create database and mock data for testing.
                using (Process p = new Process())
                {
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.CreateNoWindow = true;
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    p.StartInfo.FileName = "SQLCMD";
                    p.StartInfo.Arguments = "-s . -i " + scriptFile;
                    p.StartInfo.RedirectStandardError = true;
                    p.Start();
                    p.WaitForExit();

                    string error = p.StandardError.ReadToEnd();
                    if (!String.IsNullOrEmpty(error))
                    {
                        throw new InvalidOperationException(error);
                    }
                }
            }
            catch { }

            return result;
        }
    }
}
