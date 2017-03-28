BEGIN /* REGION [ EnumChannelType ]*/
	DELETE FROM  [otp].[EnumChannelType]
	INSERT INTO [otp].[EnumChannelType](ChannelType, Name)Values
		('SMS','Short Message Service'),
		('EMAIL','e-Mail')
END

BEGIN /* REGION [ EnumStatus ]*/
	DELETE FROM  [otp].[EnumStatus]
	INSERT INTO [otp].[EnumStatus]([Status], [Description])Values
		('Pending','The code ready to use.'),
		('Expired','The code has expired.'),
		('Locked','The code is locked by entered wrong more than limitation.'),
		('Verified','The code is used.'),
		('Deleted','The code has been deleted by client or system.'),
		('Canceled','The code has been cancelled by system because client re-generate code again with same suid.')
END

BEGIN /* REGION [ Setting ]*/
	DELETE FROM  [otp].[Setting]
	INSERT INTO [otp].[Setting]([Key], [Value])Values
		('Expiration','900'),
		('RetryCount','3')
END

BEGIN /* REGION [ Account ]*/
	DELETE FROM  [otp].[Account]
	/*
	Raw ApiKey is 
	1phM4nLk14tefH8cntFJfuINtH0w_POg1zdKO9EPiu28TYjwLH0mOWzvcFiD0h3pPvf9wlhxhYk5hA6Ur0BHg8InK91GwhfCbW4kQU_6KkbKTb1H9gkOqTnFZxY4lPyl
	*/
	INSERT INTO [otp].[Account]([AccountId], [ApiKey], [Name], [IpAddress], [IsActive], [Description], [Salt], [Email])Values
		('5861F73F-CAD5-419D-96D4-56BD07211297','lLTHykjZ3oZ5ORx1ePayit2+orgbNxI8d5N3296EhT7lSawnoygnfjKbyQdTrxVFLlZ8AfDjn1OnW5Hy2oAgWg==', 'AccountTest', '192.168.1.69', 1, 'This is for testing', 'ALzIYz8BIgvLsk1q', 'Test@mail.com')
		
END
GO
