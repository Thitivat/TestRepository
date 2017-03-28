IF NOT EXISTS(SELECT 1 FROM [ideal].[Setting] WHERE [Key] = 'MinExpirationPeriodSecond')
BEGIN
	INSERT INTO [ideal].[Setting]([Key], [Value])Values
		('MinExpirationPeriodSecond','60')
END
GO

IF NOT EXISTS(SELECT 1 FROM [ideal].[Setting] WHERE [Key] = 'DefaultExpirationPeriodSecond')
BEGIN
	INSERT INTO [ideal].[Setting]([Key], [Value])Values
		('DefaultExpirationPeriodSecond','900')
END
GO

IF NOT EXISTS(SELECT 1 FROM [ideal].[Setting] WHERE [Key] = 'MaxExpirationPeriodSecond')
BEGIN
	INSERT INTO [ideal].[Setting]([Key], [Value])Values
		('MaxExpirationPeriodSecond','3600')
END
GO

IF NOT EXISTS(SELECT 1 FROM [ideal].[Setting] WHERE [Key] = 'MaxRetriesPerDays')
BEGIN
	INSERT INTO [ideal].[Setting]([Key], [Value])Values
		('MaxRetriesPerDays','5')
END
GO
