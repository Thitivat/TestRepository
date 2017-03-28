USE [BND.Services.Security.OTP.Db]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[otp].[FK_OneTimePassword_ToSystem]') AND parent_object_id = OBJECT_ID(N'[otp].[OneTimePassword]'))
ALTER TABLE [otp].[OneTimePassword] DROP CONSTRAINT [FK_OneTimePassword_ToSystem]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[otp].[FK_OneTimePassword_ToEnumStatus]') AND parent_object_id = OBJECT_ID(N'[otp].[OneTimePassword]'))
ALTER TABLE [otp].[OneTimePassword] DROP CONSTRAINT [FK_OneTimePassword_ToEnumStatus]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[otp].[FK_OneTimePassword_ToEnumChannelType]') AND parent_object_id = OBJECT_ID(N'[otp].[OneTimePassword]'))
ALTER TABLE [otp].[OneTimePassword] DROP CONSTRAINT [FK_OneTimePassword_ToEnumChannelType]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[otp].[FK_Attempt_ToOneTimePassword]') AND parent_object_id = OBJECT_ID(N'[otp].[Attempt]'))
ALTER TABLE [otp].[Attempt] DROP CONSTRAINT [FK_Attempt_ToOneTimePassword]
GO
PRINT('DROP FOREIGN_KEY SUCCESS')

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[otp].[DF__Log__Timestamp__1CF15040]') AND type = 'D')
BEGIN
	ALTER TABLE [otp].[Log] DROP CONSTRAINT [DF__Log__Timestamp__1CF15040]
PRINT('DROP DEFAULT VALUE IN [otp].[[Log]]')
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[otp].[DF__Account__Account__1BFD2C07]') AND type = 'D')
BEGIN
	ALTER TABLE [otp].[Account] DROP CONSTRAINT [DF__Account__Account__1BFD2C07]
PRINT('DROP DEFAULT VALUE IN [otp].[Account]')
END
GO

-- drop table && schema
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[otp].[Setting]') AND type in (N'U'))
DROP TABLE [otp].[Setting]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[otp].[OneTimePassword]') AND type in (N'U'))
DROP TABLE [otp].[OneTimePassword]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[otp].[Log]') AND type in (N'U'))
DROP TABLE [otp].[Log]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[otp].[EnumStatus]') AND type in (N'U'))
DROP TABLE [otp].[EnumStatus]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[otp].[EnumChannelType]') AND type in (N'U'))
DROP TABLE [otp].[EnumChannelType]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[otp].[Attempt]') AND type in (N'U'))
DROP TABLE [otp].[Attempt]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[otp].[Account]') AND type in (N'U'))
DROP TABLE [otp].[Account]
GO
PRINT('DROP TABLE SUCCESS')

IF  EXISTS (SELECT * FROM sys.schemas WHERE name = N'otp')
BEGIN
	DROP SCHEMA [otp]
	PRINT('DROP SCHEMA SUCCESS')
END
GO


-- create schema
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'otp')
BEGIN
	EXEC sys.sp_executesql N'CREATE SCHEMA [otp]'
	PRINT('CREATE SCHEMA SUCCESS')
END
GO


/****** Object:  Table [otp].[Account]    Script Date: 30/5/2559 12:27:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[otp].[Account]') AND type in (N'U'))
BEGIN
	CREATE TABLE [otp].[Account](
		[AccountId] [uniqueidentifier] NOT NULL,
		[ApiKey] [varchar](128) NOT NULL,
		[Name] [varchar](64) NOT NULL,
		[IpAddress] [varchar](64) NOT NULL,
		[IsActive] [bit] NOT NULL,
		[Description] [text] NULL,
		[Salt] [char](16) NOT NULL,
		[Email] [varchar](256) NOT NULL,
	PRIMARY KEY CLUSTERED ( [AccountId] ASC )
	WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] )
	ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

	PRINT('CREATE TABLE [otp].[Account] SUCCESS')
END
ELSE BEGIN
	PRINT('TABLE [otp].[Account] HAS EXISTS')
END
GO
SET ANSI_PADDING ON
GO

/****** Object:  Table [otp].[Attempt]    Script Date: 30/5/2559 12:27:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[otp].[Attempt]') AND type in (N'U'))
BEGIN
	CREATE TABLE [otp].[Attempt](
		[AttemptId] [int] IDENTITY(1,1) NOT NULL,
		[OtpId] [varchar](128) NOT NULL,
		[Date] [datetime2](7) NOT NULL,
		[IpAddress] [varchar](64) NOT NULL,
		[UserAgent] [varchar](64) NOT NULL,
	PRIMARY KEY CLUSTERED ( [AttemptId] ASC )
	WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] )
	ON [PRIMARY]

	PRINT('CREATE TABLE [otp].[Attempt] SUCCESS')
END
ELSE BEGIN
	PRINT('TABLE [otp].[Attempt] HAS EXISTS')
END
GO
SET ANSI_PADDING ON
GO

/****** Object:  Table [otp].[EnumChannelType]    Script Date: 30/5/2559 12:27:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[otp].[EnumChannelType]') AND type in (N'U'))
BEGIN
	CREATE TABLE [otp].[EnumChannelType](
		[ChannelType] [varchar](16) NOT NULL,
		[Name] [varchar](64) NOT NULL,
	PRIMARY KEY CLUSTERED ( [ChannelType] ASC )
	WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] )
	ON [PRIMARY]

	PRINT('CREATE TABLE [otp].[EnumChannelType] SUCCESS')
END
ELSE BEGIN
	PRINT('TABLE [otp].[EnumChannelType] HAS EXISTS')
END
GO
SET ANSI_PADDING ON
GO

/****** Object:  Table [otp].[EnumStatus]    Script Date: 30/5/2559 12:27:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[otp].[EnumStatus]') AND type in (N'U'))
BEGIN
	CREATE TABLE [otp].[EnumStatus](
		[Status] [varchar](16) NOT NULL,
		[Description] [text] NULL,
	PRIMARY KEY CLUSTERED ( [Status] ASC )
	WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] )
	ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

	PRINT('CREATE TABLE [otp].[EnumStatus] SUCCESS')
END
ELSE BEGIN
	PRINT('TABLE [otp].[EnumStatus] HAS EXISTS')
END
GO
SET ANSI_PADDING ON
GO

/****** Object:  Table [otp].[Log]    Script Date: 30/5/2559 12:27:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[otp].[Log]') AND type in (N'U'))
BEGIN
	CREATE TABLE [otp].[Log](
		[LogId] [int] IDENTITY(1,1) NOT NULL,
		[Prival] [tinyint] NOT NULL,
		[Version] [tinyint] NOT NULL,
		[Timestamp] [datetime2](7) NOT NULL,
		[Hostname] [varchar](255) NULL,
		[AppName] [varchar](48) NULL,
		[ProcId] [varchar](128) NULL,
		[MsgId] [varchar](32) NULL,
		[StructuredData] [varchar](max) NULL,
		[Msg] [varchar](max) NULL,
	PRIMARY KEY CLUSTERED ( [LogId] ASC )
	WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] )
	ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

	PRINT('CREATE TABLE [otp].[Log] SUCCESS')
END
ELSE BEGIN
	PRINT('TABLE [otp].[Log] HAS EXISTS')
END
GO
SET ANSI_PADDING ON
GO

/****** Object:  Table [otp].[OneTimePassword]    Script Date: 30/5/2559 12:27:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[otp].[OneTimePassword]') AND type in (N'U'))
BEGIN
	CREATE TABLE [otp].[OneTimePassword](
		[OtpId] [varchar](128) NOT NULL,
		[AccountId] [uniqueidentifier] NOT NULL,
		[Suid] [varchar](128) NOT NULL,
		[ChannelType] [varchar](16) NOT NULL,
		[ChannelSender] [varchar](64) NOT NULL,
		[ChannelAddress] [varchar](256) NOT NULL,
		[ChannelMessage] [varchar](max) NOT NULL,
		[ExpiryDate] [datetime] NOT NULL,
		[Payload] [varchar](max) NULL,
		[RefCode] [varchar](8) NULL,
		[Code] [varchar](8) NOT NULL,
		[Status] [varchar](16) NOT NULL,
	PRIMARY KEY CLUSTERED ( [OtpId] ASC )
	WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] )
	ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

	PRINT('CREATE TABLE [otp].[OneTimePassword] SUCCESS')
END
ELSE BEGIN
	PRINT('TABLE [otp].[OneTimePassword] HAS EXISTS')
END
GO
SET ANSI_PADDING ON
GO

/****** Object:  Table [otp].[Setting]    Script Date: 30/5/2559 12:27:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[otp].[Setting]') AND type in (N'U'))
BEGIN
	CREATE TABLE [otp].[Setting](
		[Key] [varchar](16) NOT NULL,
		[Value] [varchar](32) NOT NULL,
	PRIMARY KEY CLUSTERED (	[Key] ASC )
	WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] )
	ON [PRIMARY]

	PRINT('CREATE TABLE [otp].[Setting] SUCCESS')
END
ELSE BEGIN
	PRINT('TABLE [otp].[Setting] HAS EXISTS')
END
GO

-- default value
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[otp].[DF__Account__Account__1BFD2C07]') AND type = 'D')
BEGIN
	ALTER TABLE [otp].[Account] ADD  DEFAULT (newid()) FOR [AccountId]
	PRINT('ADD DEFAULT VALUE IN [opt].[Account]')
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[otp].[DF__Log__Timestamp__1CF15040]') AND type = 'D')
BEGIN
	ALTER TABLE [otp].[Log] ADD  DEFAULT (getdate()) FOR [Timestamp]
	PRINT('ADD DEFAULT VALUE IN [opt].[Log]')
END
GO

-- create relation
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[otp].[FK_Attempt_ToOneTimePassword]') AND parent_object_id = OBJECT_ID(N'[otp].[Attempt]'))
ALTER TABLE [otp].[Attempt]  WITH CHECK ADD CONSTRAINT [FK_Attempt_ToOneTimePassword] FOREIGN KEY([OtpId])
REFERENCES [otp].[OneTimePassword] ([OtpId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[otp].[FK_Attempt_ToOneTimePassword]') AND parent_object_id = OBJECT_ID(N'[otp].[Attempt]'))
ALTER TABLE [otp].[Attempt] CHECK CONSTRAINT [FK_Attempt_ToOneTimePassword]
GO
PRINT('ADD FOREIGN KEY IN [opt].[Attempt]')

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[otp].[FK_OneTimePassword_ToEnumChannelType]') AND parent_object_id = OBJECT_ID(N'[otp].[OneTimePassword]'))
ALTER TABLE [otp].[OneTimePassword]  WITH CHECK ADD CONSTRAINT [FK_OneTimePassword_ToEnumChannelType] FOREIGN KEY([ChannelType])
REFERENCES [otp].[EnumChannelType] ([ChannelType])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[otp].[FK_OneTimePassword_ToEnumChannelType]') AND parent_object_id = OBJECT_ID(N'[otp].[OneTimePassword]'))
ALTER TABLE [otp].[OneTimePassword] CHECK CONSTRAINT [FK_OneTimePassword_ToEnumChannelType]
GO
PRINT('ADD FOREIGN KEY IN [opt].[OneTimePassword]')

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[otp].[FK_OneTimePassword_ToEnumStatus]') AND parent_object_id = OBJECT_ID(N'[otp].[OneTimePassword]'))
ALTER TABLE [otp].[OneTimePassword]  WITH CHECK ADD CONSTRAINT [FK_OneTimePassword_ToEnumStatus] FOREIGN KEY([Status])
REFERENCES [otp].[EnumStatus] ([Status])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[otp].[FK_OneTimePassword_ToEnumStatus]') AND parent_object_id = OBJECT_ID(N'[otp].[OneTimePassword]'))
ALTER TABLE [otp].[OneTimePassword] CHECK CONSTRAINT [FK_OneTimePassword_ToEnumStatus]
GO
PRINT('ADD FOREIGN KEY IN [opt].[OneTimePassword]')

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[otp].[FK_OneTimePassword_ToSystem]') AND parent_object_id = OBJECT_ID(N'[otp].[OneTimePassword]'))
ALTER TABLE [otp].[OneTimePassword]  WITH CHECK ADD CONSTRAINT [FK_OneTimePassword_ToSystem] FOREIGN KEY([AccountId])
REFERENCES [otp].[Account] ([AccountId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[otp].[FK_OneTimePassword_ToSystem]') AND parent_object_id = OBJECT_ID(N'[otp].[OneTimePassword]'))
ALTER TABLE [otp].[OneTimePassword] CHECK CONSTRAINT [FK_OneTimePassword_ToSystem]
GO
PRINT('ADD FOREIGN KEY IN [opt].[OneTimePassword]')