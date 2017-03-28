CREATE TABLE [otp].[Log]
(
	[LogId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Prival] TINYINT NOT NULL, 
    [Version] TINYINT NOT NULL, 
    [Timestamp] DATETIME2 NOT NULL DEFAULT getDate(), 
    [Hostname] VARCHAR(255) NULL , 
    [AppName] VARCHAR(48) NULL , 
    [ProcId] VARCHAR(128) NULL , 
    [MsgId] VARCHAR(32) NULL , 
    [StructuredData] VARCHAR(MAX) NULL , 
    [Msg] VARCHAR(MAX) NULL
)
