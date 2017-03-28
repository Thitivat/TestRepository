CREATE TABLE [otp].[Account]
(
	[AccountId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(), 
	[ApiKey] VARCHAR(128) NOT NULL , 
    [Name] VARCHAR(64) NOT NULL, 
    [IpAddress] VARCHAR(64) NOT NULL, 
    [IsActive] BIT NOT NULL, 
    [Description] TEXT NULL, 
    [Salt] CHAR(16) NOT NULL, 
    [Email] VARCHAR(256) NOT NULL
)
