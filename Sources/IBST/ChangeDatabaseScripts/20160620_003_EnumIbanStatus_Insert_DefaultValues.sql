IF NOT EXISTS(SELECT 1 FROM [ib].[EnumIBanStatus] WHERE [StatusId] = '11')
	INSERT INTO [ib].[EnumIBanStatus] ([StatusId], [IbanStatus])
	VALUES (11, 'Available')
GO

IF NOT EXISTS(SELECT 1 FROM [ib].[EnumIBanStatus] WHERE [StatusId] = '12')
	INSERT INTO [ib].[EnumIBanStatus] ([StatusId], [IbanStatus])
	VALUES (12, 'Assigned')
GO

IF NOT EXISTS(SELECT 1 FROM [ib].[EnumIBanStatus] WHERE [StatusId] = '13')
	INSERT INTO [ib].[EnumIBanStatus] ([StatusId], [IbanStatus])
	VALUES (13, 'Canceled')
GO

IF NOT EXISTS(SELECT 1 FROM [ib].[EnumIBanStatus] WHERE [StatusId] = '14')
	INSERT INTO [ib].[EnumIBanStatus] ([StatusId], [IbanStatus])
	VALUES (14, 'Active')
GO