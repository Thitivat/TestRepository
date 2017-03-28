USE [master];

DECLARE @dbname nvarchar(128)
SET @dbname = N'dev_bnd_services_payments_portals'
IF (NOT EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE ('[' + name + ']' = @dbname OR name = @dbname)))
BEGIN
	CREATE DATABASE [dev_bnd_services_payments_portals] COLLATE SQL_Latin1_General_CP1_CI_AS
END
GO

USE [dev_bnd_services_payments_portals]
GO

/****** Object:  Schema [emandates]    Script Date: 09/23/2016 17:28:43 ******/
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'emandates')
EXEC sys.sp_executesql N'CREATE SCHEMA [emandates] AUTHORIZATION [dbo]'
GO
/****** Object:  Table [emandates].[Setting]    Script Date: 09/23/2016 17:28:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[emandates].[Setting]') AND type in (N'U'))
BEGIN
CREATE TABLE [emandates].[Setting](
	[Key] [varchar](32) NOT NULL,
	[Value] [varchar](2048) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Key] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [emandates].[RawMessage]    Script Date: 09/23/2016 17:28:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[emandates].[RawMessage]') AND type in (N'U'))
BEGIN
CREATE TABLE [emandates].[RawMessage](	
	[RawMessageID] [int] IDENTITY(1,1) NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_RawMessage] PRIMARY KEY CLUSTERED 
(
	[RawMessageID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [emandates].[Log]    Script Date: 09/23/2016 17:28:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[emandates].[Log]') AND type in (N'U'))
BEGIN
CREATE TABLE [emandates].[Log](
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
SET ANSI_PADDING OFF
GO
/****** Object:  Table [emandates].[EnumTransactionType]    Script Date: 09/23/2016 17:28:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[emandates].[EnumTransactionType]') AND type in (N'U'))
BEGIN
CREATE TABLE [emandates].[EnumTransactionType](
	[TransactionTypeName] [varchar](50) NOT NULL,
	[Description] [varchar](50) NULL,
 CONSTRAINT [PK_EnumTransactionType_1] PRIMARY KEY CLUSTERED 
(
	[TransactionTypeName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [emandates].[EnumSequenceType]    Script Date: 09/23/2016 17:28:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[emandates].[EnumSequenceType]') AND type in (N'U'))
BEGIN
CREATE TABLE [emandates].[EnumSequenceType](
	[SequenceTypeName] [varchar](4) NOT NULL,
	[Description] [varchar](50) NULL,
 CONSTRAINT [PK_EnumSequenceType] PRIMARY KEY CLUSTERED 
(
	[SequenceTypeName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [emandates].[Transaction]    Script Date: 09/23/2016 17:28:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[emandates].[Transaction]') AND type in (N'U'))
BEGIN
CREATE TABLE [emandates].[Transaction](
	[TransactionID] [varchar](16) NOT NULL,
	[DebtorBankID] [varchar](11) NOT NULL,
	[DebtorReference] [nvarchar](35) NULL,
	[EMandateID] [varchar](35) NOT NULL,
	[EMandateReason] [varchar](70) NULL,
	[EntranceCode] [varchar](40) NOT NULL,
	[ExpirationPeriod] [bigint] NULL,
	[Language] [varchar](2) NOT NULL,
	[MaxAmount] [decimal](11, 2) NULL,
	[MessageID] [varchar](35) NOT NULL,
	[PurchaseID] [varchar](35) NULL,
	[OriginalDebtorBankID] [varchar](11) NOT NULL,
	[OriginalIban] [varchar](34) NOT NULL,
	[TransactionType] [varchar](50) NOT NULL,
	[SequenceType] [varchar](4) NOT NULL,
	[TransactionCreateDateTimestamp] [datetime] NOT NULL,
	[IssuerAuthenticationUrl] [nvarchar](512) NOT NULL,
	[MerchantReturnUrl] [nvarchar](512) NOT NULL,
	[RawMessageID] [int] NULL,
	[TodayAttempts] [int] NOT NULL,
	[LatestAttemptsDateTimestamp] [datetime] NULL,
	[IsSystemFail] [bit] NOT NULL,
 CONSTRAINT [PK__Transact__55433A4B07020F21] PRIMARY KEY CLUSTERED 
(
	[TransactionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [emandates].[Directory]    Script Date: 09/23/2016 17:28:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[emandates].[Directory]') AND type in (N'U'))
BEGIN
CREATE TABLE [emandates].[Directory](
	[DirectoryID] [int] IDENTITY(1,1) NOT NULL,
	[DirectoryDateTimestamp] [datetime] NOT NULL,
	[LastDirectoryRequestDateTimestamp] [datetime] NOT NULL,
	[RawMessageID] [int] NULL,
 CONSTRAINT [PK__Director__052818260EA330E9] PRIMARY KEY CLUSTERED 
(
	[DirectoryID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [emandates].[TransactionStatusHistory]    Script Date: 09/23/2016 17:28:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[emandates].[TransactionStatusHistory]') AND type in (N'U'))
BEGIN
CREATE TABLE [emandates].[TransactionStatusHistory](
	[TransactionStatusHistoryID] [int] IDENTITY(1,1) NOT NULL,
	[TransactionID] [varchar](16) NOT NULL,
	[Status] [varchar](9) NOT NULL,
	[StatusDateTimestamp] [datetime] NULL,
	[RawMessageID] [int] NULL,
 CONSTRAINT [PK__Transact__A06DBB1C7F60ED59] PRIMARY KEY CLUSTERED 
(
	[TransactionStatusHistoryID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [emandates].[DebtorBank]    Script Date: 09/23/2016 17:28:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[emandates].[DebtorBank]') AND type in (N'U'))
BEGIN
CREATE TABLE [emandates].[DebtorBank](
	[DirectoryID] [int] NOT NULL,
	[DebtorBankCountry] [nvarchar](128) NOT NULL,
	[DebtorBankId] [varchar](11) NOT NULL,
	[DebtorBankName] [nvarchar](35) NULL,
 CONSTRAINT [PK_Issuer] PRIMARY KEY CLUSTERED 
(
	[DirectoryID] ASC,
	[DebtorBankCountry] ASC,
	[DebtorBankId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [emandates].[EmanDate]    Script Date: 09/23/2016 17:28:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[emandates].[EMandate]') AND type in (N'U'))
BEGIN
CREATE TABLE [emandates].[EMandate](
	[AcceptanceReportID] [int] IDENTITY(1,1) NOT NULL,
	[AcceptedResult] [bit] NOT NULL,
	[CreditorAddressLine] [varchar](255) NOT NULL,
	[CreditorCountry] [nvarchar](100) NOT NULL,
	[CreditorID] [varchar](35) NOT NULL,
	[CreditorName] [nvarchar](70) NOT NULL,
	[CreditorTradeName] [nvarchar](70) NULL,
	[DateTime] [datetime] NOT NULL,
	[DebtorAccountName] [nvarchar](70) NOT NULL,
	[DebtorBankID] [varchar](11) NOT NULL,
	[DebtorIban] [varchar](34) NOT NULL,
	[DebtorReference] [nvarchar](35) NULL,
	[DebtorSignerName] [nvarchar](70) NOT NULL,
	[EMandateReason] [nvarchar](70) NULL,
	[LocalInstrumentCode] [varchar](4) NOT NULL,
	[MandateRequestID] [varchar](16) NOT NULL,
	[MaxAmount] [decimal](11, 2) NULL,
	[MessageID] [varchar](35) NOT NULL,
	[MessageNameID] [varchar](9) NOT NULL,
	[OriginalMandateID] [varchar](35) NOT NULL,
	[OriginalMessageID] [varchar](16) NOT NULL,
	[RawMessageID] [int] NULL,
	[SchemeName] [varchar](4) NOT NULL,
	[SequenceType] [varchar](4) NOT NULL,
	[ServiceLevelCode] [varchar](4) NOT NULL,
	[ValidationReference] [nvarchar](128) NOT NULL,
	[TransactionStatusHistoryID] [int] NOT NULL,
	[FrequencyCount] [bigint] NULL,
	[FrequencyPeriod] [char](4) NULL,
	[AmendmentReason] [nvarchar](70) NULL,
 CONSTRAINT [PK_AcceptanceReport] PRIMARY KEY CLUSTERED 
(
	[AcceptanceReportID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  ForeignKey [FK_Transaction_EnumSequenceType]    Script Date: 09/23/2016 17:28:44 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[emandates].[FK_Transaction_EnumSequenceType]') AND parent_object_id = OBJECT_ID(N'[emandates].[Transaction]'))
ALTER TABLE [emandates].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_EnumSequenceType] FOREIGN KEY([SequenceType])
REFERENCES [emandates].[EnumSequenceType] ([SequenceTypeName])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[emandates].[FK_Transaction_EnumSequenceType]') AND parent_object_id = OBJECT_ID(N'[emandates].[Transaction]'))
ALTER TABLE [emandates].[Transaction] CHECK CONSTRAINT [FK_Transaction_EnumSequenceType]
GO
/****** Object:  ForeignKey [FK_Transaction_EnumTransactionType]    Script Date: 09/23/2016 17:28:44 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[emandates].[FK_Transaction_EnumTransactionType]') AND parent_object_id = OBJECT_ID(N'[emandates].[Transaction]'))
ALTER TABLE [emandates].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_EnumTransactionType] FOREIGN KEY([TransactionType])
REFERENCES [emandates].[EnumTransactionType] ([TransactionTypeName])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[emandates].[FK_Transaction_EnumTransactionType]') AND parent_object_id = OBJECT_ID(N'[emandates].[Transaction]'))
ALTER TABLE [emandates].[Transaction] CHECK CONSTRAINT [FK_Transaction_EnumTransactionType]
GO
/****** Object:  ForeignKey [FK_Transaction_RawMessage]    Script Date: 09/23/2016 17:28:44 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[emandates].[FK_Transaction_RawMessage]') AND parent_object_id = OBJECT_ID(N'[emandates].[Transaction]'))
ALTER TABLE [emandates].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_RawMessage] FOREIGN KEY([RawMessageID])
REFERENCES [emandates].[RawMessage] ([RawMessageID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[emandates].[FK_Transaction_RawMessage]') AND parent_object_id = OBJECT_ID(N'[emandates].[Transaction]'))
ALTER TABLE [emandates].[Transaction] CHECK CONSTRAINT [FK_Transaction_RawMessage]
GO
/****** Object:  ForeignKey [FK_Directory_RawMessage]    Script Date: 09/23/2016 17:28:44 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[emandates].[FK_Directory_RawMessage]') AND parent_object_id = OBJECT_ID(N'[emandates].[Directory]'))
ALTER TABLE [emandates].[Directory]  WITH CHECK ADD  CONSTRAINT [FK_Directory_RawMessage] FOREIGN KEY([RawMessageID])
REFERENCES [emandates].[RawMessage] ([RawMessageID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[emandates].[FK_Directory_RawMessage]') AND parent_object_id = OBJECT_ID(N'[emandates].[Directory]'))
ALTER TABLE [emandates].[Directory] CHECK CONSTRAINT [FK_Directory_RawMessage]
GO
/****** Object:  ForeignKey [FK_TransactionStatusHistory_RawMessage]    Script Date: 09/23/2016 17:28:44 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[emandates].[FK_TransactionStatusHistory_RawMessage]') AND parent_object_id = OBJECT_ID(N'[emandates].[TransactionStatusHistory]'))
ALTER TABLE [emandates].[TransactionStatusHistory]  WITH CHECK ADD  CONSTRAINT [FK_TransactionStatusHistory_RawMessage] FOREIGN KEY([RawMessageID])
REFERENCES [emandates].[RawMessage] ([RawMessageID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[emandates].[FK_TransactionStatusHistory_RawMessage]') AND parent_object_id = OBJECT_ID(N'[emandates].[TransactionStatusHistory]'))
ALTER TABLE [emandates].[TransactionStatusHistory] CHECK CONSTRAINT [FK_TransactionStatusHistory_RawMessage]
GO
/****** Object:  ForeignKey [FK_TransactionStatusHistory_To_Transaction]    Script Date: 09/23/2016 17:28:44 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[emandates].[FK_TransactionStatusHistory_To_Transaction]') AND parent_object_id = OBJECT_ID(N'[emandates].[TransactionStatusHistory]'))
ALTER TABLE [emandates].[TransactionStatusHistory]  WITH CHECK ADD  CONSTRAINT [FK_TransactionStatusHistory_To_Transaction] FOREIGN KEY([TransactionID])
REFERENCES [emandates].[Transaction] ([TransactionID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[emandates].[FK_TransactionStatusHistory_To_Transaction]') AND parent_object_id = OBJECT_ID(N'[emandates].[TransactionStatusHistory]'))
ALTER TABLE [emandates].[TransactionStatusHistory] CHECK CONSTRAINT [FK_TransactionStatusHistory_To_Transaction]
GO
/****** Object:  ForeignKey [FK_Issuer_To_Directory]    Script Date: 09/23/2016 17:28:44 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[emandates].[FK_Issuer_To_Directory]') AND parent_object_id = OBJECT_ID(N'[emandates].[DebtorBank]'))
ALTER TABLE [emandates].[DebtorBank]  WITH CHECK ADD  CONSTRAINT [FK_Issuer_To_Directory] FOREIGN KEY([DirectoryID])
REFERENCES [emandates].[Directory] ([DirectoryID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[emandates].[FK_Issuer_To_Directory]') AND parent_object_id = OBJECT_ID(N'[emandates].[DebtorBank]'))
ALTER TABLE [emandates].[DebtorBank] CHECK CONSTRAINT [FK_Issuer_To_Directory]
GO
/****** Object:  ForeignKey [FK_Emandate_EnumSequenceType]    Script Date: 09/23/2016 17:28:44 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[emandates].[FK_Emandate_EnumSequenceType]') AND parent_object_id = OBJECT_ID(N'[emandates].[EMandate]'))
ALTER TABLE [emandates].[EMandate]  WITH CHECK ADD  CONSTRAINT [FK_Emandate_EnumSequenceType] FOREIGN KEY([SequenceType])
REFERENCES [emandates].[EnumSequenceType] ([SequenceTypeName])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[emandates].[FK_Emandate_EnumSequenceType]') AND parent_object_id = OBJECT_ID(N'[emandates].[EMandate]'))
ALTER TABLE [emandates].[EMandate] CHECK CONSTRAINT [FK_Emandate_EnumSequenceType]
GO
/****** Object:  ForeignKey [FK_Emandate_RawMessage]    Script Date: 09/23/2016 17:28:44 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[emandates].[FK_Emandate_RawMessage]') AND parent_object_id = OBJECT_ID(N'[emandates].[Emandate]'))
ALTER TABLE [emandates].[Emandate]  WITH CHECK ADD  CONSTRAINT [FK_Emandate_RawMessage] FOREIGN KEY([RawMessageID])
REFERENCES [emandates].[RawMessage] ([RawMessageID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[emandates].[FK_Emandate_RawMessage]') AND parent_object_id = OBJECT_ID(N'[emandates].[Emandate]'))
ALTER TABLE [emandates].[EMandate] CHECK CONSTRAINT [FK_Emandate_RawMessage]
GO
/****** Object:  ForeignKey [FK_Emandate_TransactionStatusHistory]    Script Date: 09/23/2016 17:28:44 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[emandates].[FK_Emandate_TransactionStatusHistory]') AND parent_object_id = OBJECT_ID(N'[emandates].[EMandate]'))
ALTER TABLE [emandates].[EMandate]  WITH CHECK ADD  CONSTRAINT [FK_Emandate_TransactionStatusHistory] FOREIGN KEY([TransactionStatusHistoryID])
REFERENCES [emandates].[TransactionStatusHistory] ([TransactionStatusHistoryID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[emandates].[FK_Emandate_TransactionStatusHistory]') AND parent_object_id = OBJECT_ID(N'[emandates].[EMandate]'))
ALTER TABLE [emandates].[EMandate] CHECK CONSTRAINT [FK_Emandate_TransactionStatusHistory]
GO
