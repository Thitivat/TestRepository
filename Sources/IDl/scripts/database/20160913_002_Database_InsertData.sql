/* Initilaize data */
IF NOT EXISTS(SELECT 1 FROM [ideal].[Setting] WHERE [Key] = 'MinExpirationPeriodSecond')
BEGIN
	INSERT INTO [ideal].[Setting]([Key], [Value])Values
		('MinExpirationPeriodSecond','60'),
		('DefaultExpirationPeriodSecond','900'),
		('MaxExpirationPeriodSecond','3600'),
		('MaxRetriesPerDays','5'),
		('DirectoryRequestInterval','1')
END
GO