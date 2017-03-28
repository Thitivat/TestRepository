USE [BND.Services.Payments.iDeal.Db]
GO

-- DROP CONSTRAINTS
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[ideal].[FK_TransactionStatusHistory_To_Transaction]') 
	AND parent_object_id = OBJECT_ID(N'[ideal].[TransactionStatusHistory]'))
ALTER TABLE [ideal].[TransactionStatusHistory] DROP CONSTRAINT [FK_TransactionStatusHistory_To_Transaction]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[ideal].[FK_Issuer_To_Directory]') 
	AND parent_object_id = OBJECT_ID(N'[ideal].[Issuer]'))
ALTER TABLE [ideal].[Issuer] DROP CONSTRAINT [FK_Issuer_To_Directory]
GO

-- DROP TABLE
/****** Object:  Table [ideal].[TransactionStatusHistory]    Script Date: 6/1/2016 4:23:54 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ideal].[TransactionStatusHistory]') AND type in (N'U'))
DROP TABLE [ideal].[TransactionStatusHistory]
GO
/****** Object:  Table [ideal].[Transaction]    Script Date: 6/1/2016 4:23:54 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ideal].[Transaction]') AND type in (N'U'))
DROP TABLE [ideal].[Transaction]
GO
/****** Object:  Table [ideal].[Setting]    Script Date: 6/1/2016 4:23:54 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ideal].[Setting]') AND type in (N'U'))
DROP TABLE [ideal].[Setting]
GO
/****** Object:  Table [ideal].[Log]    Script Date: 6/1/2016 4:23:54 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ideal].[Log]') AND type in (N'U'))
DROP TABLE [ideal].[Log]
GO
/****** Object:  Table [ideal].[Issuer]    Script Date: 6/1/2016 4:23:54 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ideal].[Issuer]') AND type in (N'U'))
DROP TABLE [ideal].[Issuer]
GO
/****** Object:  Table [ideal].[Directory]    Script Date: 6/1/2016 4:23:54 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ideal].[Directory]') AND type in (N'U'))
DROP TABLE [ideal].[Directory]
GO

-- SCHEMAS
/****** Object:  Schema [ideal]    Script Date: 6/1/2016 4:23:54 PM ******/
IF  EXISTS (SELECT * FROM sys.schemas WHERE name = N'ideal')
DROP SCHEMA [ideal]
GO
/****** Object:  Schema [ideal]    Script Date: 6/1/2016 4:23:54 PM ******/
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'ideal')
EXEC sys.sp_executesql N'CREATE SCHEMA [ideal]'
GO

-- Tables
/****** Object:  Table [ideal].[Directory]    Script Date: 6/1/2016 3:13:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ideal].[Directory]') AND type in (N'U'))
BEGIN
	CREATE TABLE [ideal].[Directory](
		[AcquirerID] [char](4) NOT NULL,
		[DirectoryDateTimestamp] [datetime] NOT NULL,
		[LastDirectoryRequestDateTimestamp] [datetime] NOT NULL,
	PRIMARY KEY CLUSTERED (	[AcquirerID] ASC )
	WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) 
	ON [PRIMARY]) ON [PRIMARY]

	PRINT('[ideal].[Directoyry] has been created')
END
ELSE BEGIN
	PRINT('[ideal].[Directory] already existed')
END
GO
SET ANSI_PADDING ON
GO

/****** Object:  Table [ideal].[Issuer]    Script Date: 6/1/2016 3:24:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ideal].[Issuer]') AND type in (N'U'))
BEGIN
	CREATE TABLE [ideal].[Issuer](
		[AcquirerID] [char](4) NOT NULL,
		[CountryNames] [nchar](100) NOT NULL,
		[IssuerID] [varchar](11) NOT NULL,
		[IssuerName] [nvarchar](35) NULL,
	PRIMARY KEY CLUSTERED ( [AcquirerID] ASC, [IssuerID] ASC, [CountryNames] ASC )
	WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) 
	ON [PRIMARY]) ON [PRIMARY]

	PRINT('[ideal].[Issuer] has been created')	
END
ELSE BEGIN
	PRINT('[ideal].[Issuer] already existed')
END
GO
SET ANSI_PADDING ON
GO

/****** Object:  Table [ideal].[Setting]    Script Date: 6/1/2016 3:31:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ideal].[Setting]') AND type in (N'U'))
BEGIN
	CREATE TABLE [ideal].[Setting](
		[Key] [varchar](32) NOT NULL,
		[Value] [varchar](2048) NOT NULL,
	PRIMARY KEY CLUSTERED (	[Key] ASC )
	WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) 
	ON [PRIMARY]) ON [PRIMARY]

	PRINT('[ideal].[Setting] has been created')
END
ELSE BEGIN
	PRINT('[ideal].[Setting] already existed')
END
GO
SET ANSI_PADDING ON
GO

/****** Object:  Table [ideal].[Transaction]    Script Date: 6/1/2016 3:34:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ideal].[Transaction]') AND type in (N'U'))
BEGIN
	CREATE TABLE [ideal].[Transaction](
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
	PRIMARY KEY CLUSTERED (	[TransactionID] ASC )
	WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) 
	ON [PRIMARY]) ON [PRIMARY]

	PRINT('[ideal].[Transaction] has been created')
END
ELSE BEGIN
	PRINT('[ideal].[Transaction] already existed')
END
GO
SET ANSI_PADDING ON
GO

/****** Object:  Table [ideal].[TransactionStatusHistory]    Script Date: 6/1/2016 3:37:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
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
	PRIMARY KEY CLUSTERED (	[TransactionStatusHistoryID] ASC )
	WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) 
	ON [PRIMARY] ) ON [PRIMARY]

	PRINT('[ideal].[TransactionStatusHistory] has been created')
END
ELSE BEGIN
	PRINT('[ideal].[TransactionStatusHistory] already existed')
END
GO
SET ANSI_PADDING ON
GO

/****** Object:  Table [ideal].[Log]    Script Date: 6/1/2016 3:41:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
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
	PRIMARY KEY CLUSTERED (	[LogID] ASC )
	WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) 
	ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

	PRINT('[ideal].[Log] has been created')
END
ELSE BEGIN
	PRINT('[ideal].[Log] already existed')
END
GO
SET ANSI_PADDING ON
GO

-- CREATE RELATION
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[ideal].[FK_Issuer_To_Directory]') 
	AND parent_object_id = OBJECT_ID(N'[ideal].[Issuer]'))
ALTER TABLE [ideal].[Issuer]  WITH CHECK ADD  CONSTRAINT [FK_Issuer_To_Directory] FOREIGN KEY([AcquirerID])
REFERENCES [ideal].[Directory] ([AcquirerID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[ideal].[FK_Issuer_To_Directory]') 
	AND parent_object_id = OBJECT_ID(N'[ideal].[Issuer]'))
ALTER TABLE [ideal].[Issuer] CHECK CONSTRAINT [FK_Issuer_To_Directory]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[ideal].[FK_TransactionStatusHistory_To_Transaction]') 
	AND parent_object_id = OBJECT_ID(N'[ideal].[TransactionStatusHistory]'))
ALTER TABLE [ideal].[TransactionStatusHistory]  WITH CHECK ADD  CONSTRAINT [FK_TransactionStatusHistory_To_Transaction] FOREIGN KEY([TransactionID])
REFERENCES [ideal].[Transaction] ([TransactionID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[ideal].[FK_TransactionStatusHistory_To_Transaction]') 
	AND parent_object_id = OBJECT_ID(N'[ideal].[TransactionStatusHistory]'))
ALTER TABLE [ideal].[TransactionStatusHistory] CHECK CONSTRAINT [FK_TransactionStatusHistory_To_Transaction]
GO