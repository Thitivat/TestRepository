CREATE TABLE [ideal].[Directory]
(
	[AcquirerID] CHAR(4) NOT NULL , 
    [DirectoryDateTimestamp] DATETIME NOT NULL, 
    [LastDirectoryRequestDateTimestamp] DATETIME NOT NULL, 
    PRIMARY KEY ([AcquirerID]) 
)
