CREATE TABLE [ib].[IbanHistory]
(
	[HistoryId]		INT				PRIMARY KEY IDENTITY (1, 1) NOT NULL, 
    [IbanId]		INT				NOT NULL, 
    [IbanStatusId]	INT				NOT NULL, 
    [Remark]		NVARCHAR(MAX)	NULL, 
    [Context]		NVARCHAR(50)	NULL, 
    [ChangedDate]	DATETIME		NOT NULL, 
    [ChangedBy]		NVARCHAR(50)	NOT NULL, 
    
)
