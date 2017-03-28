CREATE TABLE [ideal].[Issuer]
(
	[AcquirerID] CHAR(4) NOT NULL , 
    [CountryNames] NCHAR(100) NOT NULL, 
    [IssuerID] VARCHAR(11) NOT NULL, 
    [IssuerName] NVARCHAR(35) NULL, 
    PRIMARY KEY ([AcquirerID], [IssuerID], [CountryNames]),
	CONSTRAINT [FK_Issuer_To_Directory] FOREIGN KEY ([AcquirerID]) REFERENCES [ideal].[Directory]([AcquirerID]) 
)