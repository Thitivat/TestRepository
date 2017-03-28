CREATE TABLE [ideal].[TransactionStatusHistory]
(
	[TransactionStatusHistoryID] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [TransactionID] VARCHAR(16) NOT NULL, 
    [Status] NVARCHAR(32) NOT NULL, 
    [StatusRequestDateTimeStamp] DATETIME NOT NULL, 
    [StatusResponseDateTimeStamp] DATETIME NOT NULL, 
    [StatusDateTimeStamp] DATETIME NULL, 
    CONSTRAINT [FK_TransactionStatusHistory_To_Transaction] FOREIGN KEY ([TransactionID]) REFERENCES [ideal].[Transaction]([TransactionID])
)
