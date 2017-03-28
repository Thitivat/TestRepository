/* Create schema */
IF NOT EXISTS ( SELECT  *
                FROM    sys.schemas
                WHERE   name = N'sl' ) 
    EXEC('CREATE SCHEMA [sl] AUTHORIZATION [dbo]')

GO

IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'WebUserV2')
BEGIN
	CREATE USER [WebUserV2] FOR LOGIN [WebUserV2] WITH DEFAULT_SCHEMA=[dbo]
END
GO

GO
IF  NOT EXISTS
(SELECT *
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[EnumActionTypes]')
	 AND type IN (N'U'))
BEGIN
	PRINT N'Creating [sl].[EnumActionTypes]...'
	CREATE TABLE [sl].[EnumActionTypes] (
		[ActionTypeId] INT          IDENTITY (1, 1) NOT NULL,
		[Name]         VARCHAR (50) NOT NULL,
		[Description]  TEXT         NOT NULL,
		PRIMARY KEY CLUSTERED ([ActionTypeId] ASC)
	)
END


GO
IF  NOT EXISTS
(SELECT *
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[ContactInfo]')
	 AND type IN (N'U'))
BEGIN
	PRINT N'Creating [sl].[ContactInfo]...';
	CREATE TABLE [sl].[ContactInfo] (
		[ContactInfoId]         INT            IDENTITY (1, 1) NOT NULL,
		[OriginalContactInfoId] INT            NULL,
		[EntityId]              INT            NOT NULL,
		[RegulationId]          INT            NOT NULL,
		[ContactInfoTypeId]     INT            NOT NULL,
		[Value]                 NVARCHAR (256) NOT NULL,
		[RemarkId]              INT            NULL,
		PRIMARY KEY CLUSTERED ([ContactInfoId] ASC)
	)
END


GO
IF  NOT EXISTS
(SELECT *
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[Updates]')
	 AND type IN (N'U'))
BEGIN
	PRINT N'Creating [sl].[Updates]...';
	CREATE TABLE [sl].[Updates] (
		[UpdateId]      INT          IDENTITY (1, 1) NOT NULL,
		[UpdatedDate]   DATETIME     NOT NULL,
		[PublicDate]    DATETIME     NOT NULL,
		[ListTypeId]    INT          NOT NULL,
		[Username]      VARCHAR (96) NOT NULL,
		[ListArchiveId] INT          NULL,
		PRIMARY KEY CLUSTERED ([UpdateId] ASC)
	)
END

GO
IF  NOT EXISTS
(SELECT *
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[Remarks]')
	 AND type IN (N'U'))
BEGIN
	PRINT N'Creating [sl].[Remarks]...';
	CREATE TABLE [sl].[Remarks] (
		[RemarkId] INT  IDENTITY (1, 1) NOT NULL,
		[Value]    TEXT NOT NULL,
		PRIMARY KEY CLUSTERED ([RemarkId] ASC)
	)
END

GO
IF  NOT EXISTS
(SELECT *
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[Regulations]')
	 AND type IN (N'U'))
BEGIN
	PRINT N'Creating [sl].[Regulations]...';
	CREATE TABLE [sl].[Regulations] (
		[RegulationId]     INT            IDENTITY (1, 1) NOT NULL,
		[RegulationTitle]  NVARCHAR (128) NOT NULL,
		[RegulationDate]   DATE           NULL,
		[PublicationDate]  DATE           NOT NULL,
		[PublicationTitle] NVARCHAR (128) NOT NULL,
		[PublicationUrl]   NVARCHAR (256) NULL,
		[RemarkId]         INT            NULL,
		[Programme]        NVARCHAR (20)  NOT NULL,
		[ListTypeId]       INT            NOT NULL,
		PRIMARY KEY CLUSTERED ([RegulationId] ASC)
	)
END

GO
IF  NOT EXISTS
(SELECT *
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[NameAliases]')
	 AND type IN (N'U'))
BEGIN
	PRINT N'Creating [sl].[NameAliases]...';
	CREATE TABLE [sl].[NameAliases] (
		[NameAliasId]         INT             IDENTITY (1, 1) NOT NULL,
		[OriginalNameAliasId] INT             NULL,
		[EntityId]            INT             NOT NULL,
		[RegulationId]        INT             NOT NULL,
		[LastName]            NVARCHAR (256)  NULL,
		[FirstName]           NVARCHAR (256)  NULL,
		[MiddleName]          NVARCHAR (256)  NULL,
		[WholeName]           NVARCHAR (512)  NOT NULL,
		[PrefixName]          NVARCHAR (20)   NULL,
		[GenderId]            INT             NOT NULL,
		[Title]               NVARCHAR (2048) NULL,
		[LanguageIso3]        CHAR (3)        NULL,
		[RemarkId]            INT             NULL,
		[Quality]             SMALLINT        NULL,
		[Function]            NVARCHAR (2048) NULL,
		PRIMARY KEY CLUSTERED ([NameAliasId] ASC)
	)
END

GO
IF  NOT EXISTS
(SELECT *
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[Logs]')
	 AND type IN (N'U'))
BEGIN
	PRINT N'Creating [sl].[Logs]...';
	CREATE TABLE [sl].[Logs] (
		[LogId]        INT          IDENTITY (1, 1) NOT NULL,
		[LogDate]      DATETIME     NOT NULL,
		[ListTypeId]   INT          NULL,
		[Username]     VARCHAR (96) NOT NULL,
		[Description]  TEXT         NULL,
		[ActionTypeId] INT          NOT NULL,
		PRIMARY KEY CLUSTERED ([LogId] ASC)
	)
END


GO
IF  NOT EXISTS
(SELECT *
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[Identifications]')
	 AND type IN (N'U'))
BEGIN
	PRINT N'Creating [sl].[Identifications]...';
	CREATE TABLE [sl].[Identifications] (
		[IdentificationId]         INT             IDENTITY (1, 1) NOT NULL,
		[OriginalIdentificationId] INT             NULL,
		[EntityId]                 INT             NOT NULL,
		[RegulationId]             INT             NOT NULL,
		[IdentificationTypeId]     INT             NOT NULL,
		[DocumentNumber]           NVARCHAR (2048) NOT NULL,
		[IssueCity]                NVARCHAR (256)  NULL,
		[IssueCountryIso3]         CHAR (3)        NULL,
		[IssueDate]                DATE            NULL,
		[ExpiryDate]               DATE            NULL,
		[RemarkId]                 INT             NULL,
		PRIMARY KEY CLUSTERED ([IdentificationId] ASC)
	)
END

GO
IF  NOT EXISTS
(SELECT *
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[EnumSubjectTypes]')
	 AND type IN (N'U'))
BEGIN
	PRINT N'Creating [sl].[EnumSubjectTypes]...';
	CREATE TABLE [sl].[EnumSubjectTypes] (
		[SubjectTypeId] INT           IDENTITY (1, 1) NOT NULL,
		[Name]          VARCHAR (128) NOT NULL,
		PRIMARY KEY CLUSTERED ([SubjectTypeId] ASC)
	)
END


GO
IF  NOT EXISTS
(SELECT *
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[EnumStatuses]')
	 AND type IN (N'U'))
BEGIN
	PRINT N'Creating [sl].[EnumStatuses]...';
	CREATE TABLE [sl].[EnumStatuses] (
		[StatusId] INT           NOT NULL,
		[Name]     VARCHAR (128) NOT NULL,
		PRIMARY KEY CLUSTERED ([StatusId] ASC)
	)
END


GO
IF  NOT EXISTS
(SELECT *
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[EnumListTypes]')
	 AND type IN (N'U'))
BEGIN
	PRINT N'Creating [sl].[EnumListTypes]...';
	CREATE TABLE [sl].[EnumListTypes] (
		[ListTypeId]  INT           IDENTITY (1, 1) NOT NULL,
		[Name]        VARCHAR (128) NOT NULL,
		[Description] TEXT          NOT NULL,
		PRIMARY KEY CLUSTERED ([ListTypeId] ASC)
	)
END

GO
IF  NOT EXISTS
(SELECT *
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[EnumLanguages]')
	 AND type IN (N'U'))
BEGIN
	PRINT N'Creating [sl].[EnumLanguages]...';
	CREATE TABLE [sl].[EnumLanguages] (
		[Iso3] CHAR (3)      NOT NULL,
		[Iso2] CHAR (2)      NOT NULL,
		[Name] VARCHAR (128) NOT NULL,
		PRIMARY KEY CLUSTERED ([Iso3] ASC)
	)
END


GO
IF  NOT EXISTS
(SELECT *
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[EnumIdentificationTypes]')
	 AND type IN (N'U'))
BEGIN
	PRINT N'Creating [sl].[EnumIdentificationTypes]...';
	CREATE TABLE [sl].[EnumIdentificationTypes] (
		[IdentificationTypeId] INT           IDENTITY (1, 1) NOT NULL,
		[Name]                 VARCHAR (128) NOT NULL,
		[Description]          TEXT          NOT NULL,
		PRIMARY KEY CLUSTERED ([IdentificationTypeId] ASC)
	)
END

GO
IF  NOT EXISTS
(SELECT *
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[EnumGenders]')
	 AND type IN (N'U'))
BEGIN
	PRINT N'Creating [sl].[EnumGenders]...';
	CREATE TABLE [sl].[EnumGenders] (
		[GenderId] INT          NOT NULL,
		[Name]     VARCHAR (20) NOT NULL,
		PRIMARY KEY CLUSTERED ([GenderId] ASC)
	)
END


GO
IF  NOT EXISTS
(SELECT *
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[EnumCountries]')
	 AND type IN (N'U'))
BEGIN
	PRINT N'Creating [sl].[EnumCountries]...';
	CREATE TABLE [sl].[EnumCountries] (
		[Iso3]      CHAR (3)       NOT NULL,
		[Iso2]      CHAR (2)       NOT NULL,
		[Name]      NVARCHAR (128) NOT NULL,
		[NiceName]  NVARCHAR (128) NOT NULL,
		[NumCode]   INT            NULL,
		[PhoneCode] INT            NULL,
		PRIMARY KEY CLUSTERED ([Iso3] ASC)
	)
END


GO
IF  NOT EXISTS
(SELECT *
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[EnumContactInfoTypes]')
	 AND type IN (N'U'))
BEGIN
	PRINT N'Creating [sl].[EnumContactInfoTypes]...';
	CREATE TABLE [sl].[EnumContactInfoTypes] (
		[ContactInfoTypeId] INT           IDENTITY (1, 1) NOT NULL,
		[Name]              VARCHAR (128) NOT NULL,
		PRIMARY KEY CLUSTERED ([ContactInfoTypeId] ASC)
	)
END


GO
IF  NOT EXISTS
(SELECT *
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[Entities]')
	 AND type IN (N'U'))
BEGIN
	PRINT N'Creating [sl].[Entities]...';
	CREATE TABLE [sl].[Entities] (
		[EntityId]         INT IDENTITY (1, 1) NOT NULL,
		[OriginalEntityId] INT NULL,
		[RegulationId]     INT NOT NULL,
		[SubjectTypeId]    INT NOT NULL,
		[StatusId]         INT NOT NULL,
		[RemarkId]         INT NULL,
		[ListTypeId]       INT NOT NULL,
		[ListArchiveId]    INT NULL,
		PRIMARY KEY CLUSTERED ([EntityId] ASC)
	)	
	ALTER TABLE [sl].[Entities]
		ADD DEFAULT ident_current('sl.Entities') FOR [OriginalEntityId];
END


GO
IF  NOT EXISTS
(SELECT *
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[Births]')
	 AND type IN (N'U'))
BEGIN
	PRINT N'Creating [sl].[Births]...';
	CREATE TABLE [sl].[Births] (
		[BirthId]         INT             IDENTITY (1, 1) NOT NULL,
		[OriginalBirthId] INT             NULL,
		[EntityId]        INT             NOT NULL,
		[RegulationId]    INT             NOT NULL,
		[Year]            INT             NULL,
		[Month]           INT             NULL,
		[Day]             INT             NULL,
		[Place]           NVARCHAR (2048) NULL,
		[CountryIso3]     CHAR (3)        NULL,
		[RemarkId]        INT             NULL,
		PRIMARY KEY CLUSTERED ([BirthId] ASC)
	)
END


GO
IF  NOT EXISTS
(SELECT *
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[Banks]')
	 AND type IN (N'U'))
BEGIN
	PRINT N'Creating [sl].[Banks]...';
	CREATE TABLE [sl].[Banks] (
		[BankId]            INT            IDENTITY (1, 1) NOT NULL,
		[OriginalBankId]    INT            NULL,
		[EntityId]          INT            NOT NULL,
		[BankName]          NVARCHAR (256) NOT NULL,
		[AccountHolderName] NVARCHAR (512) NOT NULL,
		[AccountNumber]     NVARCHAR (20)  NOT NULL,
		[CountryIso3]       CHAR (3)       NULL,
		[Swift]             NVARCHAR (11)  NULL,
		[Iban]              NVARCHAR (40)  NULL,
		[RemarkId]          INT            NULL,
		PRIMARY KEY CLUSTERED ([BankId] ASC)
	)
END

GO
IF  NOT EXISTS
(SELECT *
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[Addresses]')
	 AND type IN (N'U'))
BEGIN
	PRINT N'Creating [sl].[Addresses]...';
	CREATE TABLE [sl].[Addresses] (
		[AddressId]         INT             IDENTITY (1, 1) NOT NULL,
		[OriginalAddressId] INT             NULL,
		[EntityId]          INT             NOT NULL,
		[RegulationId]      INT             NOT NULL,
		[Number]            NVARCHAR (20)   NULL,
		[Street]            NVARCHAR (256)  NULL,
		[Zipcode]           NVARCHAR (20)   NULL,
		[City]              NVARCHAR (128)  NULL,
		[CountryIso3]       CHAR (3)        NULL,
		[RemarkId]          INT             NULL,
		[Other]             NVARCHAR (2048) NULL,
		PRIMARY KEY CLUSTERED ([AddressId] ASC)
	)
END


GO
IF  NOT EXISTS
(SELECT *
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[Citizenships]')
	 AND type IN (N'U'))
BEGIN
	PRINT N'Creating [sl].[Citizenships]...';
	CREATE TABLE [sl].[Citizenships] (
		[CitizenshipId]         INT      IDENTITY (1, 1) NOT NULL,
		[OriginalCitizenshipId] INT      NULL,
		[EntityId]              INT      NOT NULL,
		[RegulationId]          INT      NOT NULL,
		[CountryIso3]           CHAR (3) NULL,
		[RemarkId]              INT      NULL,
		PRIMARY KEY CLUSTERED ([CitizenshipId] ASC)
	)
END

GO
IF  NOT EXISTS
(SELECT *
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[ListArchive]')
	 AND type IN (N'U'))
BEGIN
	PRINT N'Creating [sl].[ListArchive]...';
	CREATE TABLE [sl].[ListArchive] (
		[ListArchiveId] INT             IDENTITY (1, 1) NOT NULL,
		[Date]          DATETIME        NOT NULL,
		[File]          VARBINARY (MAX) NOT NULL,
		PRIMARY KEY CLUSTERED ([ListArchiveId] ASC)
	)
END

GO
IF  NOT EXISTS
(SELECT *
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[Settings]')
	 AND type IN (N'U'))
BEGIN
	PRINT N'Creating [sl].[Settings]...';
	CREATE TABLE [sl].[Settings] (
		[SettingId]  INT          IDENTITY (1, 1) NOT NULL,
		[Key]        VARCHAR (50) NOT NULL,
		[Value]      TEXT         NOT NULL,
		[ListTypeId] INT          NULL,
		PRIMARY KEY CLUSTERED ([SettingId] ASC),
		CONSTRAINT [AK_Settings_Key_ListTypeId] UNIQUE NONCLUSTERED ([Key] ASC, [ListTypeId] ASC)
	)
END


GO
IF NOT EXISTS
(SELECT * 
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[FK_ContactInfo_To_Entities]')
	 AND type = 'F')
BEGIN	
	PRINT N'Creating [sl].[FK_ContactInfo_To_Entities]...';
	ALTER TABLE [sl].[ContactInfo]
		ADD CONSTRAINT [FK_ContactInfo_To_Entities] FOREIGN KEY ([EntityId]) REFERENCES [sl].[Entities] ([EntityId])
END
	

GO
IF NOT EXISTS
(SELECT * 
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[FK_ContactInfo_To_Regulations]')
	 AND type = 'F')
BEGIN	
	PRINT N'Creating [sl].[FK_ContactInfo_To_Regulations]...';
	ALTER TABLE [sl].[ContactInfo]
		ADD CONSTRAINT [FK_ContactInfo_To_Regulations] FOREIGN KEY ([RegulationId]) REFERENCES [sl].[Regulations] ([RegulationId])
END


GO
IF NOT EXISTS
(SELECT * 
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[FK_ContactInfo_To_ContactInfoTypes]')
	 AND type = 'F')
BEGIN	
	PRINT N'Creating [sl].[FK_ContactInfo_To_ContactInfoTypes]...';
	ALTER TABLE [sl].[ContactInfo]
		ADD CONSTRAINT [FK_ContactInfo_To_ContactInfoTypes] FOREIGN KEY ([ContactInfoTypeId]) REFERENCES [sl].[EnumContactInfoTypes] ([ContactInfoTypeId])
END

GO
IF NOT EXISTS
(SELECT * 
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[FK_ContactInfo_To_Remarks]')
	 AND type = 'F')
BEGIN
	PRINT N'Creating [sl].[FK_ContactInfo_To_Remarks]...';
	ALTER TABLE [sl].[ContactInfo]
		ADD CONSTRAINT [FK_ContactInfo_To_Remarks] FOREIGN KEY ([RemarkId]) REFERENCES [sl].[Remarks] ([RemarkId])
END

GO
IF NOT EXISTS
(SELECT * 
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[FK_Updates_To_ListTypes]')
	 AND type = 'F')
BEGIN
	PRINT N'Creating [sl].[FK_Updates_To_ListTypes]...';
	ALTER TABLE [sl].[Updates]
		ADD CONSTRAINT [FK_Updates_To_ListTypes] FOREIGN KEY ([ListTypeId]) REFERENCES [sl].[EnumListTypes] ([ListTypeId])
END

GO
IF NOT EXISTS
(SELECT * 
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[FK_Updates_To_ListArchive]')
	 AND type = 'F')
BEGIN
	PRINT N'Creating [sl].[FK_Updates_To_ListArchive]...';
	ALTER TABLE [sl].[Updates]
		ADD CONSTRAINT [FK_Updates_To_ListArchive] FOREIGN KEY ([ListArchiveId]) REFERENCES [sl].[ListArchive] ([ListArchiveId])
END

GO
IF NOT EXISTS
(SELECT * 
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[FK_Regulations_To_Remarks]')
	 AND type = 'F')
BEGIN
	PRINT N'Creating [sl].[FK_Regulations_To_Remarks]...';
	ALTER TABLE [sl].[Regulations]
		ADD CONSTRAINT [FK_Regulations_To_Remarks] FOREIGN KEY ([RemarkId]) REFERENCES [sl].[Remarks] ([RemarkId])
END

GO
IF NOT EXISTS
(SELECT * 
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[FK_Regulations_To_ListTypes]')
	 AND type = 'F')
BEGIN
	PRINT N'Creating [sl].[FK_Regulations_To_ListTypes]...';
	ALTER TABLE [sl].[Regulations]
		ADD CONSTRAINT [FK_Regulations_To_ListTypes] FOREIGN KEY ([ListTypeId]) REFERENCES [sl].[EnumListTypes] ([ListTypeId]);
END

GO
IF NOT EXISTS
(SELECT * 
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[FK_NameAliases_To_Entities]')
	 AND type = 'F')
BEGIN
	PRINT N'Creating [sl].[FK_NameAliases_To_Entities]...';
	ALTER TABLE [sl].[NameAliases]
		ADD CONSTRAINT [FK_NameAliases_To_Entities] FOREIGN KEY ([EntityId]) REFERENCES [sl].[Entities] ([EntityId]);
END

GO
IF NOT EXISTS
(SELECT * 
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[FK_NameAliases_To_Regulations]')
	 AND type = 'F')
BEGIN
	PRINT N'Creating [sl].[FK_NameAliases_To_Regulations]...';
	ALTER TABLE [sl].[NameAliases]
		ADD CONSTRAINT [FK_NameAliases_To_Regulations] FOREIGN KEY ([RegulationId]) REFERENCES [sl].[Regulations] ([RegulationId]);
END

GO
IF NOT EXISTS
(SELECT * 
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[FK_NameAliases_To_Genders]')
	 AND type = 'F')
BEGIN
	PRINT N'Creating [sl].[FK_NameAliases_To_Genders]...';
	ALTER TABLE [sl].[NameAliases]
		ADD CONSTRAINT [FK_NameAliases_To_Genders] FOREIGN KEY ([GenderId]) REFERENCES [sl].[EnumGenders] ([GenderId]);
END

GO
IF NOT EXISTS
(SELECT * 
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[FK_NameAliases_To_Languages]')
	 AND type = 'F')
BEGIN
	PRINT N'Creating [sl].[FK_NameAliases_To_Languages]...';
	ALTER TABLE [sl].[NameAliases]
		ADD CONSTRAINT [FK_NameAliases_To_Languages] FOREIGN KEY ([LanguageIso3]) REFERENCES [sl].[EnumLanguages] ([Iso3]);
END

GO
IF NOT EXISTS
(SELECT * 
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[FK_NameAliases_To_Remarks]')
	 AND type = 'F')
BEGIN
	PRINT N'Creating [sl].[FK_NameAliases_To_Remarks]...';
	ALTER TABLE [sl].[NameAliases]
		ADD CONSTRAINT [FK_NameAliases_To_Remarks] FOREIGN KEY ([RemarkId]) REFERENCES [sl].[Remarks] ([RemarkId]);
END

GO
IF NOT EXISTS
(SELECT * 
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[FK_Logs_To_ListTypes]')
	 AND type = 'F')
BEGIN
	PRINT N'Creating [sl].[FK_Logs_To_ListTypes]...';
	ALTER TABLE [sl].[Logs]
		ADD CONSTRAINT [FK_Logs_To_ListTypes] FOREIGN KEY ([ListTypeId]) REFERENCES [sl].[EnumListTypes] ([ListTypeId]);
END

GO
IF NOT EXISTS
(SELECT * 
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[FK_Logs_To_EnumActionTypes]')
	 AND type = 'F')
BEGIN
	PRINT N'Creating [sl].[FK_Logs_To_EnumActionTypes]...';
	ALTER TABLE [sl].[Logs]
		ADD CONSTRAINT [FK_Logs_To_EnumActionTypes] FOREIGN KEY ([ActionTypeId]) REFERENCES [sl].[EnumActionTypes] ([ActionTypeId]);
END

GO
IF NOT EXISTS
(SELECT * 
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[FK_Identifications_To_Entities]')
	 AND type = 'F')
BEGIN
	PRINT N'Creating [sl].[FK_Identifications_To_Entities]...';
	ALTER TABLE [sl].[Identifications]
		ADD CONSTRAINT [FK_Identifications_To_Entities] FOREIGN KEY ([EntityId]) REFERENCES [sl].[Entities] ([EntityId]);
END

GO
IF NOT EXISTS
(SELECT * 
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[FK_Identifications_To_Regulations]')
	 AND type = 'F')
BEGIN
	PRINT N'Creating [sl].[FK_Identifications_To_Regulations]...';
	ALTER TABLE [sl].[Identifications]
		ADD CONSTRAINT [FK_Identifications_To_Regulations] FOREIGN KEY ([RegulationId]) REFERENCES [sl].[Regulations] ([RegulationId]);
END

GO
IF NOT EXISTS
(SELECT * 
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[FK_Identifications_To_IdentificationTypes]')
	 AND type = 'F')
BEGIN
	PRINT N'Creating [sl].[FK_Identifications_To_IdentificationTypes]...';
	ALTER TABLE [sl].[Identifications]
		ADD CONSTRAINT [FK_Identifications_To_IdentificationTypes] FOREIGN KEY ([IdentificationTypeId]) REFERENCES [sl].[EnumIdentificationTypes] ([IdentificationTypeId]);
END

GO
IF NOT EXISTS
(SELECT * 
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[FK_Identifications_To_Countries]')
	 AND type = 'F')
BEGIN
	PRINT N'Creating [sl].[FK_Identifications_To_Countries]...';
	ALTER TABLE [sl].[Identifications]
		ADD CONSTRAINT [FK_Identifications_To_Countries] FOREIGN KEY ([IssueCountryIso3]) REFERENCES [sl].[EnumCountries] ([Iso3]);
END

GO
IF NOT EXISTS
(SELECT * 
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[FK_Identifications_To_Remarks]')
	 AND type = 'F')
BEGIN
	PRINT N'Creating [sl].[FK_Identifications_To_Remarks]...';
	ALTER TABLE [sl].[Identifications]
		ADD CONSTRAINT [FK_Identifications_To_Remarks] FOREIGN KEY ([RemarkId]) REFERENCES [sl].[Remarks] ([RemarkId]);
END

GO
IF NOT EXISTS
(SELECT * 
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[FK_Entities_To_Regulations]')
	 AND type = 'F')
BEGIN
	PRINT N'Creating [sl].[FK_Entities_To_Regulations]...';
	ALTER TABLE [sl].[Entities]
		ADD CONSTRAINT [FK_Entities_To_Regulations] FOREIGN KEY ([RegulationId]) REFERENCES [sl].[Regulations] ([RegulationId]);
END

GO
IF NOT EXISTS
(SELECT * 
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[FK_Entities_To_SubjectTypes]')
	 AND type = 'F')
BEGIN
	PRINT N'Creating [sl].[FK_Entities_To_SubjectTypes]...';
	ALTER TABLE [sl].[Entities]
		ADD CONSTRAINT [FK_Entities_To_SubjectTypes] FOREIGN KEY ([SubjectTypeId]) REFERENCES [sl].[EnumSubjectTypes] ([SubjectTypeId]);
END

GO
IF NOT EXISTS
(SELECT * 
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[FK_Entities_To_Statuses]')
	 AND type = 'F')
BEGIN
	PRINT N'Creating [sl].[FK_Entities_To_Statuses]...';
	ALTER TABLE [sl].[Entities]
		ADD CONSTRAINT [FK_Entities_To_Statuses] FOREIGN KEY ([StatusId]) REFERENCES [sl].[EnumStatuses] ([StatusId]);
END

GO
IF NOT EXISTS
(SELECT * 
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[FK_Entities_To_Remarks]')
	 AND type = 'F')
BEGIN
	PRINT N'Creating [sl].[FK_Entities_To_Remarks]...';
	ALTER TABLE [sl].[Entities]
		ADD CONSTRAINT [FK_Entities_To_Remarks] FOREIGN KEY ([RemarkId]) REFERENCES [sl].[Remarks] ([RemarkId]);
END

GO
IF NOT EXISTS
(SELECT * 
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[FK_Entities_To_ListTypes]')
	 AND type = 'F')
BEGIN
	PRINT N'Creating [sl].[FK_Entities_To_ListTypes]...';
	ALTER TABLE [sl].[Entities]
		ADD CONSTRAINT [FK_Entities_To_ListTypes] FOREIGN KEY ([ListTypeId]) REFERENCES [sl].[EnumListTypes] ([ListTypeId]);
END

GO
IF NOT EXISTS
(SELECT * 
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[FK_Entities_To_ListArchive]')
	 AND type = 'F')
BEGIN
	PRINT N'Creating [sl].[FK_Entities_To_ListArchive]...';
	ALTER TABLE [sl].[Entities]
		ADD CONSTRAINT [FK_Entities_To_ListArchive] FOREIGN KEY ([ListArchiveId]) REFERENCES [sl].[ListArchive] ([ListArchiveId]);
END

GO
IF NOT EXISTS
(SELECT * 
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[FK_Births_To_Entities]')
	 AND type = 'F')
BEGIN
	PRINT N'Creating [sl].[FK_Births_To_Entities]...';
	ALTER TABLE [sl].[Births]
		ADD CONSTRAINT [FK_Births_To_Entities] FOREIGN KEY ([EntityId]) REFERENCES [sl].[Entities] ([EntityId]);
END

GO
IF NOT EXISTS
(SELECT * 
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[FK_Births_To_Regulations]')
	 AND type = 'F')
BEGIN
	PRINT N'Creating [sl].[FK_Births_To_Regulations]...';
	ALTER TABLE [sl].[Births]
		ADD CONSTRAINT [FK_Births_To_Regulations] FOREIGN KEY ([RegulationId]) REFERENCES [sl].[Regulations] ([RegulationId]);
END

GO
IF NOT EXISTS
(SELECT * 
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[FK_Births_To_Countries]')
	 AND type = 'F')
BEGIN
	PRINT N'Creating [sl].[FK_Births_To_Countries]...';
	ALTER TABLE [sl].[Births]
		ADD CONSTRAINT [FK_Births_To_Countries] FOREIGN KEY ([CountryIso3]) REFERENCES [sl].[EnumCountries] ([Iso3]);
END

GO
IF NOT EXISTS
(SELECT * 
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[FK_BirthDates_To_Remarks]')
	 AND type = 'F')
BEGIN
	PRINT N'Creating [sl].[FK_BirthDates_To_Remarks]...';
	ALTER TABLE [sl].[Births]
		ADD CONSTRAINT [FK_BirthDates_To_Remarks] FOREIGN KEY ([RemarkId]) REFERENCES [sl].[Remarks] ([RemarkId]);
END

GO
IF NOT EXISTS
(SELECT * 
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[FK_Banks_To_Entities]')
	 AND type = 'F')
BEGIN
	PRINT N'Creating [sl].[FK_Banks_To_Entities]...';
	ALTER TABLE [sl].[Banks]
		ADD CONSTRAINT [FK_Banks_To_Entities] FOREIGN KEY ([EntityId]) REFERENCES [sl].[Entities] ([EntityId]);
END

GO
IF NOT EXISTS
(SELECT * 
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[FK_Banks_To_Countries]')
	 AND type = 'F')
BEGIN
	PRINT N'Creating [sl].[FK_Banks_To_Countries]...';
	ALTER TABLE [sl].[Banks]
		ADD CONSTRAINT [FK_Banks_To_Countries] FOREIGN KEY ([CountryIso3]) REFERENCES [sl].[EnumCountries] ([Iso3]);
END

GO
IF NOT EXISTS
(SELECT * 
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[FK_Banks_To_Remarks]')
	 AND type = 'F')
BEGIN
	PRINT N'Creating [sl].[FK_Banks_To_Remarks]...';
	ALTER TABLE [sl].[Banks]
		ADD CONSTRAINT [FK_Banks_To_Remarks] FOREIGN KEY ([RemarkId]) REFERENCES [sl].[Remarks] ([RemarkId]);
END

GO
IF NOT EXISTS
(SELECT * 
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[FK_Addresses_To_Entities]')
	 AND type = 'F')
BEGIN
	PRINT N'Creating [sl].[FK_Addresses_To_Entities]...';
	ALTER TABLE [sl].[Addresses]
		ADD CONSTRAINT [FK_Addresses_To_Entities] FOREIGN KEY ([EntityId]) REFERENCES [sl].[Entities] ([EntityId]);
END

GO
IF NOT EXISTS
(SELECT * 
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[FK_Addresses_To_Regulations]')
	 AND type = 'F')
BEGIN
	PRINT N'Creating [sl].[FK_Addresses_To_Regulations]...';
	ALTER TABLE [sl].[Addresses]
		ADD CONSTRAINT [FK_Addresses_To_Regulations] FOREIGN KEY ([RegulationId]) REFERENCES [sl].[Regulations] ([RegulationId]);
END

GO
IF NOT EXISTS
(SELECT * 
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[FK_Addresses_To_Countries]')
	 AND type = 'F')
BEGIN
	PRINT N'Creating [sl].[FK_Addresses_To_Countries]...';
	ALTER TABLE [sl].[Addresses]
		ADD CONSTRAINT [FK_Addresses_To_Countries] FOREIGN KEY ([CountryIso3]) REFERENCES [sl].[EnumCountries] ([Iso3]);
END

GO
IF NOT EXISTS
(SELECT * 
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[FK_Addresses_To_Remarks]')
	 AND type = 'F')
BEGIN
	PRINT N'Creating [sl].[FK_Addresses_To_Remarks]...';
	ALTER TABLE [sl].[Addresses]
		ADD CONSTRAINT [FK_Addresses_To_Remarks] FOREIGN KEY ([RemarkId]) REFERENCES [sl].[Remarks] ([RemarkId]);
END

GO
IF NOT EXISTS
(SELECT * 
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[FK_Citizenships_To_Entities]')
	 AND type = 'F')
BEGIN
	PRINT N'Creating [sl].[FK_Citizenships_To_Entities]...';
	ALTER TABLE [sl].[Citizenships]
		ADD CONSTRAINT [FK_Citizenships_To_Entities] FOREIGN KEY ([EntityId]) REFERENCES [sl].[Entities] ([EntityId]);
END

GO
IF NOT EXISTS
(SELECT * 
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[FK_Citizens_To_Regulations]')
	 AND type = 'F')
BEGIN
	PRINT N'Creating [sl].[FK_Citizens_To_Regulations]...';
	ALTER TABLE [sl].[Citizenships]
		ADD CONSTRAINT [FK_Citizens_To_Regulations] FOREIGN KEY ([RegulationId]) REFERENCES [sl].[Regulations] ([RegulationId]);
END

GO
IF NOT EXISTS
(SELECT * 
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[FK_Citizens_To_Countries]')
	 AND type = 'F')
BEGIN
	PRINT N'Creating [sl].[FK_Citizens_To_Countries]...';
	ALTER TABLE [sl].[Citizenships]
		ADD CONSTRAINT [FK_Citizens_To_Countries] FOREIGN KEY ([CountryIso3]) REFERENCES [sl].[EnumCountries] ([Iso3]);
END

GO
IF NOT EXISTS
(SELECT * 
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[FK_Citizens_To_Remarks]')
	 AND type = 'F')
BEGIN
	PRINT N'Creating [sl].[FK_Citizens_To_Remarks]...';
	ALTER TABLE [sl].[Citizenships]
		ADD CONSTRAINT [FK_Citizens_To_Remarks] FOREIGN KEY ([RemarkId]) REFERENCES [sl].[Remarks] ([RemarkId]);
END

GO
IF NOT EXISTS
(SELECT * 
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[sl].[FK_Settings_To_ListTypes]')
	 AND type = 'F')
BEGIN
	PRINT N'Creating [sl].[FK_Settings_To_ListTypes]...';
	ALTER TABLE [sl].[Settings]
		ADD CONSTRAINT [FK_Settings_To_ListTypes] FOREIGN KEY ([ListTypeId]) REFERENCES [sl].[EnumListTypes] ([ListTypeId]);
END
