CREATE TABLE [ib].[Iban]
(
	[IbanId]					INT				PRIMARY KEY IDENTITY(1, 1) NOT NULL, 
	[BbanFileId]				INT				NOT NULL, 
	[Uid]						VARCHAR(256)	NULL,
	[UidPrefix]					VARCHAR(256)	NULL,
	[ReservedTime]				DATETIME		NULL,  
    [CountryCode]				VARCHAR(2)		NOT NULL, 
    [BankCode]					VARCHAR(4)		NOT NULL, 
    [CheckSum]					VARCHAR(2)				NOT NULL, 
    [Bban]						VARCHAR(10)		NOT NULL, 
    [CurrentStatusHistoryId]	INT				NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [AK_Iban_Bban] UNIQUE (Bban) 
)
