CREATE TABLE [ib].[BbanFile]
(
	[BbanFileId]			INT				NOT NULL PRIMARY KEY IDENTITY (1, 1), 
    [Name]					NVARCHAR(255)	NOT NULL, 
    [RawFile]				VARBINARY(MAX)	NOT NULL, 
    [Hash]					VARCHAR(64)		NOT NULL, 
    [CurrentStatusHistoryId]	INT				NULL
)
