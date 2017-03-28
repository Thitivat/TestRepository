CREATE TABLE [ib].[BbanImport]
(
	[BbanImportId]		INT			PRIMARY KEY IDENTITY (1, 1) NOT NULL, 
    [BbanFileId]		INT			NOT NULL, 
    [Bban]				Varchar(10)	NOT NULL , 
    [IsImported]		BIT			NOT NULL DEFAULT 0, 
)
