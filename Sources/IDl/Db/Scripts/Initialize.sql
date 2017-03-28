USE [$(DatabaseName)];
GO

/* Initialize roles */
IF Exists(SELECT * FROM sys.server_principals WHERE name = '$(RoleName)' And type = 'R')
	DROP ROLE [$(RoleName)]
CREATE ROLE [$(RoleName)]
GO

GRANT INSERT ON [ideal].[Directory] TO [$(RoleName)]
GO
GRANT SELECT ON [ideal].[Directory] TO [$(RoleName)]
GO
GRANT UPDATE ON [ideal].[Directory] TO [$(RoleName)]
GO
GRANT DELETE ON [ideal].[Directory] TO [$(RoleName)]
GO

GRANT INSERT ON [ideal].[Log] TO [$(RoleName)]
GO
GRANT SELECT ON [ideal].[Log] TO [$(RoleName)]
GO
GRANT UPDATE ON [ideal].[Log] TO [$(RoleName)]
GO
GRANT DELETE ON [ideal].[Log] TO [$(RoleName)]
GO

GRANT INSERT ON [ideal].[Transaction] TO [$(RoleName)]
GO
GRANT SELECT ON [ideal].[Transaction] TO [$(RoleName)]
GO
GRANT UPDATE ON [ideal].[Transaction] TO [$(RoleName)]
GO
GRANT DELETE ON [ideal].[Transaction] TO [$(RoleName)]
GO

GRANT INSERT ON [ideal].[TransactionStatusHistory] TO [$(RoleName)]
GO
GRANT SELECT ON [ideal].[TransactionStatusHistory] TO [$(RoleName)]
GO
GRANT UPDATE ON [ideal].[TransactionStatusHistory] TO [$(RoleName)]
GO
GRANT DELETE ON [ideal].[TransactionStatusHistory] TO [$(RoleName)]
GO

GRANT INSERT ON [ideal].[Setting] TO [$(RoleName)]
GO
GRANT SELECT ON [ideal].[Setting] TO [$(RoleName)]
GO
GRANT UPDATE ON [ideal].[Setting] TO [$(RoleName)]
GO
GRANT DELETE ON [ideal].[Setting] TO [$(RoleName)]
GO

GRANT INSERT ON [ideal].[Issuer] TO [$(RoleName)]
GO
GRANT SELECT ON [ideal].[Issuer] TO [$(RoleName)]
GO
GRANT UPDATE ON [ideal].[Issuer] TO [$(RoleName)]
GO
GRANT DELETE ON [ideal].[Issuer] TO [$(RoleName)]
GO
/* -------------- */

USE master;
GO

BEGIN /* Initialize user */
	-- Checks login account on machine.
	IF Exists(SELECT * FROM sys.server_principals WHERE name = '$(UserName)')
	  BEGIN
		-- Creates login account on sql server.
		IF Not Exists(SELECT * FROM syslogins WHERE name = '$(UserName)' and dbname = 'master')
			CREATE LOGIN [$(UserName)] FROM WINDOWS;

		USE [$(DatabaseName)];
		-- Creates user on database.
		IF Not Exists(SELECT * FROM sys.database_principals WHERE name = '$(UserName)')
			CREATE USER [$(UserName)] FOR LOGIN [$(UserName)];
	
		-- Sets role.
		EXEC sp_addrolemember [$(RoleName)], [$(UserName)];
	  END
	ELSE
	  PRINT 'There is no $(UserName) in your system, please create account first!'
END
GO

BEGIN /* Initilaize data */
	DELETE FROM [ideal].[Setting]
	INSERT INTO [ideal].[Setting]([Key], [Value])Values
		('MinExpirationPeriodSecond','60'),
		('DefaultExpirationPeriodSecond','900'),
		('MaxExpirationPeriodSecond','3600'),
		('MaxRetriesPerDays','5'),
		('DirectoryRequestInterval','1')
END
GO