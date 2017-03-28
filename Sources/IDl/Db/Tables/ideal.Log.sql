CREATE TABLE [ideal].[Log]
(
	[LogID] INT NOT NULL PRIMARY KEY IDENTITY,
    [Prival] TINYINT NULL, 
    [Version] TINYINT NULL, 
    [Timestamp] DATETIME2 NOT NULL, 
    [Hostname] VARCHAR(255) NULL, 
    [AppName] VARCHAR(48) NULL, 
    [ProcId] VARCHAR(128) NULL, 
    [MsgId] VARCHAR(32) NULL, 
    [StructuredData] VARCHAR(MAX) NULL, 
    [Msg] VARCHAR(MAX) NULL
)
