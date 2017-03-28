using System;
using System.Diagnostics;
using System.IO;

namespace Bndb.Kyc.SanctionLists.SanctionListsBllTest
{
    internal static class Helpers
    {

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
    }
}
