/* Transaction */
IF NOT EXISTS(
    SELECT *
    FROM sys.columns 
    WHERE Name      = N'BookingGuid'
      AND Object_ID = Object_ID(N'[ideal].[Transaction]'))
BEGIN
    ALTER TABLE [ideal].[Transaction] ADD [BookingGuid] uniqueidentifier NULL DEFAULT (newid())
	EXEC(N'UPDATE [ideal].[Transaction] SET [BookingGuid]= newid()')
END


GO	
IF EXISTS(
    SELECT *
    FROM sys.columns 
    WHERE Name      = N'BookingGuid'
      AND Object_ID = Object_ID(N'[ideal].[Transaction]'))
BEGIN
    ALTER TABLE [ideal].[Transaction] ALTER COLUMN [BookingGuid] uniqueidentifier NOT NULL	
END

GO
IF NOT EXISTS
(SELECT * 
 FROM
	 sys.objects
 WHERE
	 object_id = OBJECT_ID(N'[ideal].[UQ_BookingGuid_BookingGuid]')
	 AND type = 'UQ')
	BEGIN		
		ALTER TABLE [ideal].[Transaction]  WITH CHECK ADD  CONSTRAINT [UQ_BookingGuid_BookingGuid] UNIQUE ([BookingGuid])			
	END

	
GO
IF NOT EXISTS(
    SELECT *
    FROM sys.columns 
    WHERE Name      = N'BookingStatus'
      AND Object_ID = Object_ID(N'[ideal].[Transaction]'))
BEGIN
    ALTER TABLE [ideal].[Transaction] ADD  [BookingStatus] nvarchar(32) NULL DEFAULT ('NotBook')
	EXEC(N'UPDATE [ideal].[Transaction] SET [BookingStatus]=''NotBook''')
END

GO
IF NOT EXISTS(
    SELECT *
    FROM sys.columns 
    WHERE Name      = N'BookingDate'
      AND Object_ID = Object_ID(N'[ideal].[Transaction]'))
BEGIN
    ALTER TABLE [ideal].[Transaction] ADD  [BookingDate] datetime NULL DEFAULT (getdate())
END

GO
IF NOT EXISTS(
    SELECT *
    FROM sys.columns 
    WHERE Name      = N'MovementId'
      AND Object_ID = Object_ID(N'[ideal].[Transaction]'))
BEGIN
    ALTER TABLE [ideal].[Transaction] ADD  [MovementId] int NULL
END

