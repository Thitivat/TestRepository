/* Create schema */
IF NOT EXISTS ( SELECT  *
                FROM    sys.schemas
                WHERE   name = N'ib' ) 
    EXEC('CREATE SCHEMA [ib] AUTHORIZATION [dbo]');
GO

/* 1.IbanHistory */
PRINT N'Creating [ib].[IbanHistory]...';

IF  NOT EXISTS
(SELECT *
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[ib].[IbanHistory]')
	 AND type IN (N'U'))
	 
	BEGIN

		CREATE TABLE [ib].[IbanHistory] (
			[HistoryId]    INT            IDENTITY (1, 1) NOT NULL,
			[IbanId]       INT            NOT NULL,
			[IbanStatusId] INT            NOT NULL,
			[Remark]       NVARCHAR (MAX) NULL,
			[Context]      NVARCHAR (50)  NULL,
			[ChangedDate]  DATETIME       NOT NULL,
			[ChangedBy]    NVARCHAR (50)  NOT NULL,
			PRIMARY KEY CLUSTERED ([HistoryId] ASC)
		);
		
	END
GO

PRINT N'Creating [ib].[Iban]...';

/* 2.Iban */
IF  NOT EXISTS
(SELECT *
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[ib].[Iban]')
	 AND type IN (N'U'))
	 
	BEGIN

		CREATE TABLE [ib].[Iban] (
			[IbanId]                 INT           IDENTITY (1, 1) NOT NULL,
			[BbanFileId]             INT           NOT NULL,
			[Uid]                    VARCHAR (256) NULL,
			[UidPrefix]              VARCHAR (256) NULL,
			[ReservedTime]           DATETIME      NULL,
			[CountryCode]            VARCHAR (2)   NOT NULL,
			[BankCode]               VARCHAR (4)   NOT NULL,
			[CheckSum]               VARCHAR (2)   NOT NULL,
			[Bban]                   VARCHAR (10)  NOT NULL,
			[CurrentStatusHistoryId] INT           NULL,
			[RowVersion]             TIMESTAMP     NOT NULL,
			PRIMARY KEY CLUSTERED ([IbanId] ASC),
			CONSTRAINT [AK_Iban_Bban] UNIQUE NONCLUSTERED ([Bban] ASC)
		);

	END

GO


/* 3.EnumIBanStatus*/
PRINT N'Creating [ib].[EnumIBanStatus]...';

IF  NOT EXISTS
(SELECT *
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[ib].[EnumIBanStatus]')
	 AND type IN (N'U'))
	 
	BEGIN

		CREATE TABLE [ib].[EnumIBanStatus] (
		[StatusId]   INT          NOT NULL,
		[IbanStatus] VARCHAR (50) NOT NULL,
		PRIMARY KEY CLUSTERED ([StatusId] ASC)
		);
		
	END

GO

/* 4.EnumBbanFileStatus*/
PRINT N'Creating [ib].[EnumBbanFileStatus]...';

GO

IF  NOT EXISTS
(SELECT *
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[ib].[EnumBbanFileStatus]')
	 AND type IN (N'U'))
	 
	BEGIN
	
		CREATE TABLE [ib].[EnumBbanFileStatus] (
			[StatusId]       INT          NOT NULL,
			[BbanFileStatus] VARCHAR (50) NOT NULL,
			PRIMARY KEY CLUSTERED ([StatusId] ASC)
		);
		
	END

GO

/* 5.BbanImport */
PRINT N'Creating [ib].[BbanImport]...';

GO

IF  NOT EXISTS
(SELECT *
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[ib].[BbanImport]')
	 AND type IN (N'U'))
	 
	BEGIN

		CREATE TABLE [ib].[BbanImport] (
			[BbanImportId] INT          IDENTITY (1, 1) NOT NULL,
			[BbanFileId]   INT          NOT NULL,
			[Bban]         VARCHAR (10) NOT NULL,
			[IsImported]   BIT          NOT NULL,
			PRIMARY KEY CLUSTERED ([BbanImportId] ASC),
			CONSTRAINT [AK_BbanImport_Bban] UNIQUE NONCLUSTERED ([Bban] ASC)
		);
		
	END

GO

/* 6.BbanFileHistory */
PRINT N'Creating [ib].[BbanFileHistory]...';

GO

IF  NOT EXISTS
(SELECT *
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[ib].[BbanFileHistory]')
	 AND type IN (N'U'))
	 
	BEGIN

		CREATE TABLE [ib].[BbanFileHistory] (
			[HistoryId]        INT            IDENTITY (1, 1) NOT NULL,
			[BbanFileId]       INT            NOT NULL,
			[BbanFileStatusId] INT            NOT NULL,
			[Remark]           NVARCHAR (MAX) NULL,
			[Context]          NVARCHAR (50)  NULL,
			[ChangedDate]      DATETIME       NOT NULL,
			[ChangedBy]        NVARCHAR (50)  NOT NULL,
			PRIMARY KEY CLUSTERED ([HistoryId] ASC)
		);
		
	END

GO

/* 7.BbanFile */
PRINT N'Creating [ib].[BbanFile]...';
GO

IF  NOT EXISTS
(SELECT *
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[ib].[BbanFile]')
	 AND type IN (N'U'))
	 
	BEGIN

		CREATE TABLE [ib].[BbanFile] (
			[BbanFileId]             INT             IDENTITY (1, 1) NOT NULL,
			[Name]                   NVARCHAR (255)  NOT NULL,
			[RawFile]                VARBINARY (MAX) NOT NULL,
			[Hash]                   VARCHAR (64)    NOT NULL,
			[CurrentStatusHistoryId] INT             NULL,
			PRIMARY KEY CLUSTERED ([BbanFileId] ASC),
			CONSTRAINT [AK_BbanFile_Hash] UNIQUE NONCLUSTERED ([Hash] ASC)
		);
		
	END
GO

/* Iban AK_Iban_Uid*/
PRINT N'Creating [ib].[Iban].[AK_Iban_Uid]...';
GO

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'AK_Iban_Uid' AND object_id = OBJECT_ID(N'[ib].[Iban]'))
    BEGIN
        CREATE UNIQUE NONCLUSTERED INDEX [AK_Iban_Uid]
		ON [ib].[Iban]([Uid] ASC, [UidPrefix] ASC) WHERE Uid IS NOT NULL AND UidPrefix IS NOT NULL;
    END
GO

PRINT N'Creating [ib].[DF_BbanImport_IsImported] constraint on [ib].[BbanImport]...';
GO

IF OBJECT_ID(N'[ib].[DF_BbanImport_IsImported]', 'D') IS NULL
BEGIN
    ALTER TABLE [ib].[BbanImport] ADD CONSTRAINT DF_BbanImport_IsImported DEFAULT 0 FOR [IsImported]
END
GO

PRINT N'Creating [ib].[FK_IBanHistory_IBan]...';
GO

IF OBJECT_ID(N'[ib].[FK_IBanHistory_IBan]', 'F') IS NULL
BEGIN
    ALTER TABLE [ib].[IbanHistory] ADD CONSTRAINT [FK_IBanHistory_IBan] FOREIGN KEY ([IbanId]) REFERENCES [ib].[Iban] ([IbanId]);
END
GO

PRINT N'Creating [ib].[FK_IBanHistory_IBanStatus]...';
GO

IF OBJECT_ID(N'[ib].[FK_IBanHistory_IBanStatus]', 'F') IS NULL
BEGIN
	ALTER TABLE [ib].[IbanHistory] ADD CONSTRAINT [FK_IBanHistory_IBanStatus] FOREIGN KEY ([IbanStatusId]) REFERENCES [ib].[EnumIBanStatus] ([StatusId]);
END
GO

PRINT N'Creating [ib].[FK_IBan_BbanFile]...';
GO

IF OBJECT_ID(N'[ib].[FK_IBan_BbanFile]', 'F') IS NULL
BEGIN
	ALTER TABLE [ib].[Iban] ADD CONSTRAINT [FK_IBan_BbanFile] FOREIGN KEY ([BbanFileId]) REFERENCES [ib].[BbanFile] ([BbanFileId]);
END
GO

PRINT N'Creating [ib].[FK_BbanImport_BbanFile]...';
GO

IF OBJECT_ID(N'[ib].[FK_BbanImport_BbanFile]', 'F') IS NULL
BEGIN
ALTER TABLE [ib].[BbanImport]
    ADD CONSTRAINT [FK_BbanImport_BbanFile] FOREIGN KEY ([BbanFileId]) REFERENCES [ib].[BbanFile] ([BbanFileId]);
END
GO

PRINT N'Creating [ib].[FK_BbanFileHistory_BbanFile]...';
GO

IF OBJECT_ID(N'[ib].[FK_BbanFileHistory_BbanFile]', 'F') IS NULL
BEGIN
ALTER TABLE [ib].[BbanFileHistory]
    ADD CONSTRAINT [FK_BbanFileHistory_BbanFile] FOREIGN KEY ([BbanFileId]) REFERENCES [ib].[BbanFile] ([BbanFileId]);
END
GO

PRINT N'Creating [ib].[FK_BbanFileHistory_BbanFileStatus]...';
GO

IF OBJECT_ID(N'[ib].[FK_BbanFileHistory_BbanFile]', 'F') IS NULL
BEGIN
ALTER TABLE [ib].[FK_BbanFileHistory_BbanFileStatus]
    ADD CONSTRAINT [FK_BbanFileHistory_BbanFileStatus] FOREIGN KEY ([BbanFileStatusId]) REFERENCES [ib].[EnumBbanFileStatus] ([StatusId]);
END
GO

	
	