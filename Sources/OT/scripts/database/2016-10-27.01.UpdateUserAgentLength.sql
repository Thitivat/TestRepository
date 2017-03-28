USE [test_bnd_services_security_otp]
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[otp].[Attempt]') AND type in (N'U'))
BEGIN
	ALTER TABLE [otp].[Attempt]
	ALTER COLUMN [UserAgent] [varchar](max) NOT NULL

	PRINT('UPDATE TABLE [otp].[Attempt] SUCCESS')
END

GO