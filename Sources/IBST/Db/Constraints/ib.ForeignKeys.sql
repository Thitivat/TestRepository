ALTER TABLE [ib].[BbanFile]
ADD CONSTRAINT [AK_BbanFile_Hash] UNIQUE ([Hash])
GO


ALTER TABLE [ib].[BbanFileHistory]
ADD CONSTRAINT [FK_BbanFileHistory_BbanFile] FOREIGN KEY ([BbanFileId]) REFERENCES [ib].[BbanFile]([BbanFileId]);
GO
ALTER TABLE [ib].[BbanFileHistory]
ADD CONSTRAINT [FK_BbanFileHistory_BbanFileStatus] FOREIGN KEY ([BbanFileStatusId]) REFERENCES [ib].[EnumBbanFileStatus]([StatusId]);
GO


ALTER TABLE [ib].[BbanImport]
ADD CONSTRAINT [FK_BbanImport_BbanFile] FOREIGN KEY ([BbanFileId]) REFERENCES [ib].[BbanFile]([BbanFileId]);
GO
ALTER TABLE [ib].[BbanImport]
ADD CONSTRAINT [AK_BbanImport_Bban] UNIQUE ([Bban])
GO 


ALTER TABLE [ib].[IBan]
ADD CONSTRAINT [FK_IBan_BbanFile] FOREIGN KEY ([BbanFileId]) REFERENCES [ib].[BbanFile]([BbanFileId]);
GO


ALTER TABLE [ib].[IBanHistory]
ADD CONSTRAINT [FK_IBanHistory_IBan] FOREIGN KEY ([IbanId]) REFERENCES [ib].[IBan]([IbanId]);
GO
ALTER TABLE [ib].[IBanHistory]
ADD CONSTRAINT [FK_IBanHistory_IBanStatus] FOREIGN KEY ([IbanStatusId]) REFERENCES [ib].[EnumIBanStatus]([StatusId]);
GO

CREATE UNIQUE INDEX [AK_Iban_Uid] ON [ib].[Iban](Uid, UidPrefix) WHERE Uid IS NOT NULL AND UidPrefix IS NOT NULL;
GO