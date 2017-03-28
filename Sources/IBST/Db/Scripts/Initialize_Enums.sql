BEGIN /* REGION [ EnumBbanFileStatus ]*/
	DELETE FROM [ib].[EnumBbanFileStatus]
	INSERT INTO [ib].[EnumBbanFileStatus] (StatusId, BbanFileStatus) VALUES
	(11, 'Uploaded'),
	(12, 'UploadCanceled'),
	(13, 'Verifying'),
	(14, 'Verified'),
	(15, 'VerifiedFailed'),
	(16, 'WaitingForApproval'),
	(17, 'Approved'),
	(18, 'ApprovalDenied'),
	(19, 'Importing'),
	(20, 'Imported'),
	(21, 'ImportFailed')
END

BEGIN /* REGION [ EnumIBanStatus ] */
	DELETE FROM [ib].[EnumIBanStatus]
	INSERT INTO [ib].[EnumIBanStatus] (StatusId, IbanStatus) VALUES
	(11, 'Available'),
	(12, 'Assigned'),
	(13, 'Canceled'),
	(14, 'Active')
END