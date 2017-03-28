using BND.Services.IbanStore.Models;
using BND.Services.IbanStore.Service.Dal.Pocos;
using BND.Services.IbanStore.ServiceTest.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BND.Services.IbanStore.ServiceTest
{
    public static class MockDataTest
    {
        public static List<p_BbanFile> GenerateBbanFiles()
        {
            return new List<p_BbanFile>(new[]
            {
                new p_BbanFile
                {
                    BbanFileId = 1,
                    Name = "Test.xlsx",
                    RawFile = Encoding.UTF8.GetBytes(Resources.BBANFile_Sample),
                    Hash = "fghjkl;tyuioppfghjkl;yuiop[",
                    CurrentStatusHistoryId = 1,
                    BbanFileHistory = new List<p_BbanFileHistory>(new[]
                    {
                        new p_BbanFileHistory
                        {
                            HistoryId = 1,
                            BbanFileId = 1,
                            BbanFileStatusId = p_EnumBbanFileStatus.Uploaded,
                            Remark = "Test",
                            Context = "Service",
                            ChangedDate = Convert.ToDateTime("10/03/2016"),
                            ChangedBy = "Test"
                        }
                    })
                },

                new p_BbanFile
                {
                    BbanFileId = 2,
                    Name = "Test2.CSV",
                    RawFile = Encoding.UTF8.GetBytes(Resources.BBANFile_Sample),
                    Hash = "fghjkl;tyuioppfghjkl;yuiop[",
                    CurrentStatusHistoryId = 1,
                    BbanFileHistory = new List<p_BbanFileHistory>(new[]
                    {
                        new p_BbanFileHistory
                        {
                            HistoryId = 1,
                            BbanFileId = 1,
                            BbanFileStatusId = p_EnumBbanFileStatus.Uploaded,
                            Remark = "Test",
                            Context = "Service",
                            ChangedDate = Convert.ToDateTime("10/03/2016"),
                            ChangedBy = "Test"
                        }
                    })
                }
            });
        }
        
        public static List<p_BbanImport> GenerateBbanImport()
        {
            return new List<p_BbanImport>(new[]
            { 
                new p_BbanImport
                {
                    BbanImportId = 1,
                    BbanFileId = 1,
                    Bban = "341603562",
                    IsImported = false,
                    BbanFile = new p_BbanFile
                    {
                        BbanFileId = 1,
                        Name = "Test.xlsx",
                        RawFile = Encoding.UTF8.GetBytes(Resources.BBANFile_Sample),
                        Hash = "fghjkl;tyuioppfghjkl;yuiop[",
                        CurrentStatusHistoryId = 1,
                        BbanFileHistory = new List<p_BbanFileHistory>(new[]
                        {
                            new p_BbanFileHistory
                            {
                                HistoryId = 1,
                                BbanFileId = 1,
                                BbanFileStatusId = p_EnumBbanFileStatus.Uploaded,
                                Remark = "Test",
                                Context = "Service",
                                ChangedDate = Convert.ToDateTime("10/03/2016"),
                                ChangedBy = "Test"
                            }
                        })
                    }
                }
            });
        }

        public static List<Bban> GenerateBban()
        {
            p_BbanImport bbanImport = GenerateBbanImport().FirstOrDefault();
            if (bbanImport != null)
            {
                return new List<Bban>(new[]
                {
                    new Bban
                    {
                        ImportId = bbanImport.BbanImportId,
                        BbanCode = bbanImport.Bban,
                        IsImported = bbanImport.IsImported
                    }
                });
            }

            return null;
        }

        public static List<p_BbanFileHistory> GenerateBbanFilesHistory()
        {
            return new List<p_BbanFileHistory>(new[]
            {
                new p_BbanFileHistory
                {
                    HistoryId = 1,
                    BbanFileId = 1,
                    BbanFileStatusId = p_EnumBbanFileStatus.Verified,
                    Remark = "Test",
                    Context = "Service",
                    ChangedDate = Convert.ToDateTime("10/03/2016"),
                    ChangedBy = "Test"
                },
                new p_BbanFileHistory
                {
                    HistoryId = 2,
                    BbanFileId = 1,
                    BbanFileStatusId = p_EnumBbanFileStatus.Verifying,
                    Remark = "Test",
                    Context = "Service",
                    ChangedDate = Convert.ToDateTime("10/03/2016"),
                    ChangedBy = "Test"
                },
                new p_BbanFileHistory
                {
                    HistoryId = 3,
                    BbanFileId = 1,
                    BbanFileStatusId = p_EnumBbanFileStatus.Uploaded,
                    Remark = "Test",
                    Context = "Service",
                    ChangedDate = Convert.ToDateTime("10/03/2016"),
                    ChangedBy = "Test"
                }
               
            });
        }
    }
}
