
USE [master];

DECLARE @dbname nvarchar(128)
SET @dbname = N'dev_bnd_services_payments_portals'
IF (NOT EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE ('[' + name + ']' = @dbname OR name = @dbname)))
BEGIN
	PRINT N'Creating DATABASE [dev_bnd_services_payments_portals]...'
	CREATE DATABASE [dev_bnd_services_payments_portals] COLLATE SQL_Latin1_General_CP1_CI_AS
END

GO
USE [dev_bnd_services_payments_portals]


GO
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'ideal')
BEGIN	
	PRINT N'Creating SCHEMA [ideal]...'
	EXEC('CREATE SCHEMA [ideal] AUTHORIZATION [dbo]')
END

GO
IF  NOT EXISTS
(SELECT *
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[ideal].[TransactionStatusHistory]')
	 AND type IN (N'U'))
BEGIN
	PRINT N'Creating [ideal].[TransactionStatusHistory]...'
	CREATE TABLE [ideal].[TransactionStatusHistory] (
		[TransactionStatusHistoryID]  INT           IDENTITY (1, 1) NOT NULL,
		[TransactionID]               VARCHAR (16)  NOT NULL,
		[Status]                      NVARCHAR (32) NOT NULL,
		[StatusRequestDateTimeStamp]  DATETIME      NOT NULL,
		[StatusResponseDateTimeStamp] DATETIME      NOT NULL,
		[StatusDateTimeStamp]         DATETIME      NULL,
		PRIMARY KEY CLUSTERED ([TransactionStatusHistoryID] ASC)
	)
END

GO
IF  NOT EXISTS
(SELECT *
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[ideal].[Setting]')
	 AND type IN (N'U'))
BEGIN
	PRINT N'Creating [ideal].[Setting]...'
	CREATE TABLE [ideal].[Setting] (
		[Key]   VARCHAR (32)   NOT NULL,
		[Value] VARCHAR (2048) NOT NULL,
		PRIMARY KEY CLUSTERED ([Key] ASC)
	)
END

GO
IF  NOT EXISTS
(SELECT *
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[ideal].[Transaction]')
	 AND type IN (N'U'))
BEGIN
	PRINT N'Creating [ideal].[Transaction]...'
	CREATE TABLE [ideal].[Transaction] (
		[TransactionID]                    VARCHAR (16)    NOT NULL,
		[AcquirerID]                       VARCHAR (4)     NOT NULL,
		[MerchantID]                       VARCHAR (9)     NOT NULL,
		[SubID]                            INT             NULL,
		[IssuerID]                         VARCHAR (11)    NOT NULL,
		[IssuerAuthenticationURL]          VARCHAR (512)   NULL,
		[MerchantReturnURL]                VARCHAR (512)   NOT NULL,
		[EntranceCode]                     VARCHAR (40)    NOT NULL,
		[PurchaseID]                       VARCHAR (35)    NOT NULL,
		[Amount]                           DECIMAL (12, 2) NOT NULL,
		[Currency]                         VARCHAR (3)     NOT NULL,
		[Language]                         VARCHAR (2)     NOT NULL,
		[Description]                      NVARCHAR (35)   NULL,
		[ConsumerName]                     NVARCHAR (70)   NULL,
		[ConsumerIBAN]                     VARCHAR (34)    NULL,
		[ConsumerBIC]                      VARCHAR (11)    NULL,
		[TransactionRequestDateTimestamp]  DATETIME        NOT NULL,
		[TransactionResponseDateTimestamp] DATETIME        NOT NULL,
		[TransactionCreateDateTimestamp]   DATETIME        NOT NULL,
		[BNDIBAN]                          NVARCHAR (34)   NOT NULL,
		[PaymentType]                      VARCHAR (64)    NOT NULL,
		[ExpirationSecondPeriod]           INT             NOT NULL,
		[ExpectedCustomerIBAN]             VARCHAR (34)    NOT NULL,
		[IsSystemFail]                     BIT             NOT NULL,
		[TodayAttempts]                    INT             NOT NULL,
		[LatestAttemptsDateTimestamp]      DATETIME        NULL,
		PRIMARY KEY CLUSTERED ([TransactionID] ASC)
	)
END

GO
IF  NOT EXISTS
(SELECT *
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[ideal].[Log]')
	 AND type IN (N'U'))
BEGIN
	PRINT N'Creating [ideal].[Log]...'
	CREATE TABLE [ideal].[Log] (
		[LogID]          INT           IDENTITY (1, 1) NOT NULL,
		[Prival]         TINYINT       NULL,
		[Version]        TINYINT       NULL,
		[Timestamp]      DATETIME2 (7) NOT NULL,
		[Hostname]       VARCHAR (255) NULL,
		[AppName]        VARCHAR (48)  NULL,
		[ProcId]         VARCHAR (128) NULL,
		[MsgId]          VARCHAR (32)  NULL,
		[StructuredData] VARCHAR (MAX) NULL,
		[Msg]            VARCHAR (MAX) NULL,
		PRIMARY KEY CLUSTERED ([LogID] ASC)
	)
END

GO
IF  NOT EXISTS
(SELECT *
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[ideal].[Directory]')
	 AND type IN (N'U'))
BEGIN
	PRINT N'Creating [ideal].[Directory]...'
	CREATE TABLE [ideal].[Directory] (
		[AcquirerID]                        CHAR (4) NOT NULL,
		[DirectoryDateTimestamp]            DATETIME NOT NULL,
		[LastDirectoryRequestDateTimestamp] DATETIME NOT NULL,
		PRIMARY KEY CLUSTERED ([AcquirerID] ASC)
	)
END

GO
IF  NOT EXISTS
(SELECT *
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[ideal].[Issuer]')
	 AND type IN (N'U'))
BEGIN
	PRINT N'Creating [ideal].[Issuer]...'
	CREATE TABLE [ideal].[Issuer] (
		[AcquirerID]   CHAR (4)      NOT NULL,
		[CountryNames] NCHAR (100)   NOT NULL,
		[IssuerID]     VARCHAR (11)  NOT NULL,
		[IssuerName]   NVARCHAR (35) NULL,
		PRIMARY KEY CLUSTERED ([AcquirerID] ASC, [IssuerID] ASC, [CountryNames] ASC)
	);
END

GO
IF NOT EXISTS
(SELECT * 
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[ideal].[FK_TransactionStatusHistory_To_Transaction]')
	 AND type = 'F')
BEGIN	
	PRINT N'Creating [ideal].[FK_TransactionStatusHistory_To_Transaction]...'
	ALTER TABLE [ideal].[TransactionStatusHistory]
		ADD CONSTRAINT [FK_TransactionStatusHistory_To_Transaction] FOREIGN KEY ([TransactionID]) REFERENCES [ideal].[Transaction] ([TransactionID]);
END

GO
IF NOT EXISTS
(SELECT * 
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[ideal].[FK_Issuer_To_Directory]')
	 AND type = 'F')
BEGIN	
	PRINT N'Creating [ideal].[FK_Issuer_To_Directory]...'
	ALTER TABLE [ideal].[Issuer]
		ADD CONSTRAINT [FK_Issuer_To_Directory] FOREIGN KEY ([AcquirerID]) REFERENCES [ideal].[Directory] ([AcquirerID]) ON DELETE CASCADE;
END

