use [dev_bnd_services_payments_portals]
/* Initilaize data */
IF NOT EXISTS(SELECT 1 FROM [emandates].[Setting] WHERE [Key] = 'DayCheckPeriod')
BEGIN
	INSERT INTO [emandates].[Setting]([Key], [Value])Values
		('DayCheckPeriod','7')
END
GO

IF NOT EXISTS(SELECT 1 FROM [emandates].[Setting] WHERE [Key] = 'MinExpirationPeriodSecond')
BEGIN
	INSERT INTO [emandates].[Setting]([Key], [Value])Values
		('MinExpirationPeriodSecond','60')
END
GO

IF NOT EXISTS(SELECT 1 FROM [emandates].[Setting] WHERE [Key] = 'DefaultExpirationPeriodSecond')
BEGIN
	INSERT INTO [emandates].[Setting]([Key], [Value])Values
		('DefaultExpirationPeriodSecond','900')
END
GO

IF NOT EXISTS(SELECT 1 FROM [emandates].[Setting] WHERE [Key] = 'MaxExpirationPeriodSecond')
BEGIN
	INSERT INTO [emandates].[Setting]([Key], [Value])Values
		('MaxExpirationPeriodSecond','3600')
END
GO

IF NOT EXISTS(SELECT 1 FROM [emandates].[Setting] WHERE [Key] = 'MaxRetriesPerDays')
BEGIN
	INSERT INTO [emandates].[Setting]([Key], [Value])Values
		('MaxRetriesPerDays','5')
END
GO

IF NOT EXISTS(SELECT 1 FROM [emandates].[EnumSequenceType] WHERE [SequenceTypeName] = 'Rcur')
BEGIN
	INSERT INTO [emandates].[EnumSequenceType]([SequenceTypeName], [Description])Values
		('Rcur','Recurring type')
END
GO

IF NOT EXISTS(SELECT 1 FROM [emandates].[EnumSequenceType] WHERE [SequenceTypeName] = 'Ooff')
BEGIN
	INSERT INTO [emandates].[EnumSequenceType]([SequenceTypeName], [Description])Values
		('Ooff','one-off Direct Debit type')
END
GO

IF NOT EXISTS(SELECT 1 FROM [emandates].[EnumTransactionType] WHERE [TransactionTypeName] = 'Issuing')
BEGIN
	INSERT INTO [emandates].[EnumTransactionType]([TransactionTypeName], [Description])Values
		('Issuing','Create new emandate')
END
GO

IF NOT EXISTS(SELECT 1 FROM [emandates].[EnumTransactionType] WHERE [TransactionTypeName] = 'Amending')
BEGIN
	INSERT INTO [emandates].[EnumTransactionType]([TransactionTypeName], [Description])Values
		('Amending','Update an existing emandate')
END
GO

IF NOT EXISTS(SELECT 1 FROM [emandates].[EnumTransactionType] WHERE [TransactionTypeName] = 'Cancelling')
BEGIN
	INSERT INTO [emandates].[EnumTransactionType]([TransactionTypeName], [Description])Values
		('Cancelling','Cancelling of an existing emandate')
END
GO