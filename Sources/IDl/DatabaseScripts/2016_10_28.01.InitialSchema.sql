DECLARE @dbname nvarchar(128)
SET @dbname = N'dev_bnd_services_payments_ideal'
IF (NOT EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE ('[' + name + ']' = @dbname OR name = @dbname)))
BEGIN
	CREATE DATABASE [dev_bnd_services_payments_ideal] COLLATE SQL_Latin1_General_CP1_CI_AS
END
GO

IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'ideal')
EXEC sys.sp_executesql N'CREATE SCHEMA [ideal] AUTHORIZATION [dbo]'
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dev_bnd_services_payments_ideal].[ideal].[Transaction]') AND type in (N'U'))
BEGIN
CREATE TABLE [dev_bnd_services_payments_ideal].[ideal].[Transaction](
	[TransactionID] [varchar](16) NOT NULL,
	[AcquirerID] [varchar](4) NOT NULL,
	[MerchantID] [varchar](9) NOT NULL,
	[SubID] [int] NULL,
	[IssuerID] [varchar](11) NOT NULL,
	[IssuerAuthenticationURL] [varchar](512) NULL,
	[MerchantReturnURL] [varchar](512) NOT NULL,
	[EntranceCode] [varchar](40) NOT NULL,
	[PurchaseID] [varchar](35) NOT NULL,
	[Amount] [decimal](12, 2) NOT NULL,
	[Currency] [varchar](3) NOT NULL,
	[Language] [varchar](2) NOT NULL,
	[Description] [nvarchar](35) NULL,
	[ConsumerName] [nvarchar](70) NULL,
	[ConsumerIBAN] [varchar](34) NULL,
	[ConsumerBIC] [varchar](11) NULL,
	[TransactionRequestDateTimestamp] [datetime] NOT NULL,
	[TransactionResponseDateTimestamp] [datetime] NOT NULL,
	[TransactionCreateDateTimestamp] [datetime] NOT NULL,
	[BNDIBAN] [nvarchar](34) NOT NULL,
	[PaymentType] [varchar](64) NOT NULL,
	[ExpirationSecondPeriod] [int] NOT NULL,
	[ExpectedCustomerIBAN] [varchar](34) NOT NULL,
	[IsSystemFail] [bit] NOT NULL,
	[TodayAttempts] [int] NOT NULL,
	[LatestAttemptsDateTimestamp] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[TransactionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ideal].[Setting]') AND type in (N'U'))
BEGIN
CREATE TABLE [ideal].[Setting](
	[Key] [varchar](32) NOT NULL,
	[Value] [varchar](2048) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Key] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ideal].[Log]') AND type in (N'U'))
BEGIN
CREATE TABLE [ideal].[Log](
	[LogID] [int] IDENTITY(1,1) NOT NULL,
	[Prival] [tinyint] NULL,
	[Version] [tinyint] NULL,
	[Timestamp] [datetime2](7) NOT NULL,
	[Hostname] [varchar](255) NULL,
	[AppName] [varchar](48) NULL,
	[ProcId] [varchar](128) NULL,
	[MsgId] [varchar](32) NULL,
	[StructuredData] [varchar](max) NULL,
	[Msg] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ideal].[Directory]') AND type in (N'U'))
BEGIN
CREATE TABLE [ideal].[Directory](
	[AcquirerID] [char](4) NOT NULL,
	[DirectoryDateTimestamp] [datetime] NOT NULL,
	[LastDirectoryRequestDateTimestamp] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AcquirerID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ideal].[TransactionStatusHistory]') AND type in (N'U'))
BEGIN
CREATE TABLE [ideal].[TransactionStatusHistory](
	[TransactionStatusHistoryID] [int] IDENTITY(1,1) NOT NULL,
	[TransactionID] [varchar](16) NOT NULL,
	[Status] [nvarchar](32) NOT NULL,
	[StatusRequestDateTimeStamp] [datetime] NOT NULL,
	[StatusResponseDateTimeStamp] [datetime] NOT NULL,
	[StatusDateTimeStamp] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[TransactionStatusHistoryID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ideal].[Issuer]') AND type in (N'U'))
BEGIN
CREATE TABLE [ideal].[Issuer](
	[AcquirerID] [char](4) NOT NULL,
	[CountryNames] [nchar](100) NOT NULL,
	[IssuerID] [varchar](11) NOT NULL,
	[IssuerName] [nvarchar](35) NULL,
PRIMARY KEY CLUSTERED 
(
	[AcquirerID] ASC,
	[IssuerID] ASC,
	[CountryNames] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING ON
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[ideal].[FK_TransactionStatusHistory_To_Transaction]') AND parent_object_id = OBJECT_ID(N'[ideal].[TransactionStatusHistory]'))
ALTER TABLE [ideal].[TransactionStatusHistory]  WITH CHECK ADD  CONSTRAINT [FK_TransactionStatusHistory_To_Transaction] FOREIGN KEY([TransactionID])
REFERENCES [ideal].[Transaction] ([TransactionID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[ideal].[FK_TransactionStatusHistory_To_Transaction]') AND parent_object_id = OBJECT_ID(N'[ideal].[TransactionStatusHistory]'))
ALTER TABLE [ideal].[TransactionStatusHistory] CHECK CONSTRAINT [FK_TransactionStatusHistory_To_Transaction]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[ideal].[FK_Issuer_To_Directory]') AND parent_object_id = OBJECT_ID(N'[ideal].[Issuer]'))
ALTER TABLE [ideal].[Issuer]  WITH CHECK ADD  CONSTRAINT [FK_Issuer_To_Directory] FOREIGN KEY([AcquirerID])
REFERENCES [ideal].[Directory] ([AcquirerID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[ideal].[FK_Issuer_To_Directory]') AND parent_object_id = OBJECT_ID(N'[ideal].[Issuer]'))
ALTER TABLE [ideal].[Issuer] CHECK CONSTRAINT [FK_Issuer_To_Directory]
GO
