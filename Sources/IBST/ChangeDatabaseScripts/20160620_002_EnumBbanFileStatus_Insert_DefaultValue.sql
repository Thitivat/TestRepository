IF NOT EXISTS(SELECT 1 FROM [ib].[EnumBbanFileStatus] WHERE [StatusId] = '11')
	INSERT INTO [ib].[EnumBbanFileStatus] ([StatusId], [BbanFileStatus])
	VALUES (11, 'Uploaded')
GO

IF NOT EXISTS(SELECT 1 FROM [ib].[EnumBbanFileStatus] WHERE [StatusId] = '12')
	INSERT INTO [ib].[EnumBbanFileStatus] ([StatusId], [BbanFileStatus])
	VALUES (12, 'UploadCanceled')
GO

IF NOT EXISTS(SELECT 1 FROM [ib].[EnumBbanFileStatus] WHERE [StatusId] = '13')
	INSERT INTO [ib].[EnumBbanFileStatus] ([StatusId], [BbanFileStatus])
	VALUES (13, 'Verifying')
GO

IF NOT EXISTS(SELECT 1 FROM [ib].[EnumBbanFileStatus] WHERE [StatusId] = '14')
	INSERT INTO [ib].[EnumBbanFileStatus] ([StatusId], [BbanFileStatus])
	VALUES (14, 'Verified')
GO

IF NOT EXISTS(SELECT 1 FROM [ib].[EnumBbanFileStatus] WHERE [StatusId] = '15')
	INSERT INTO [ib].[EnumBbanFileStatus] ([StatusId], [BbanFileStatus])
	VALUES (15, 'VerifiedFailed')
GO

IF NOT EXISTS(SELECT 1 FROM [ib].[EnumBbanFileStatus] WHERE [StatusId] = '16')
	INSERT INTO [ib].[EnumBbanFileStatus] ([StatusId], [BbanFileStatus])
	VALUES (16, 'WaitingForApproval')
GO

IF NOT EXISTS(SELECT 1 FROM [ib].[EnumBbanFileStatus] WHERE [StatusId] = '17')
	INSERT INTO [ib].[EnumBbanFileStatus] ([StatusId], [BbanFileStatus])
	VALUES (17, 'Approved')
GO

IF NOT EXISTS(SELECT 1 FROM [ib].[EnumBbanFileStatus] WHERE [StatusId] = '18')
	INSERT INTO [ib].[EnumBbanFileStatus] ([StatusId], [BbanFileStatus])
	VALUES (18, 'ApprovalDenied')
GO

IF NOT EXISTS(SELECT 1 FROM [ib].[EnumBbanFileStatus] WHERE [StatusId] = '19')
	INSERT INTO [ib].[EnumBbanFileStatus] ([StatusId], [BbanFileStatus])
	VALUES (19, 'Importing')
GO

IF NOT EXISTS(SELECT 1 FROM [ib].[EnumBbanFileStatus] WHERE [StatusId] = '20')
	INSERT INTO [ib].[EnumBbanFileStatus] ([StatusId], [BbanFileStatus])
	VALUES (20, 'Imported')
GO

IF NOT EXISTS(SELECT 1 FROM [ib].[EnumBbanFileStatus] WHERE [StatusId] = '21')
	INSERT INTO [ib].[EnumBbanFileStatus] ([StatusId], [BbanFileStatus])
	VALUES (21, 'ImportFailed')
GO