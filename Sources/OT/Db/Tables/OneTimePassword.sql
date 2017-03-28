CREATE TABLE [otp].[OneTimePassword]
(
	[OtpId] VARCHAR(128) NOT NULL PRIMARY KEY, 
    [AccountId] UNIQUEIDENTIFIER NOT NULL, 
    [Suid] VARCHAR(128) NOT NULL, 
    [ChannelType] VARCHAR(16) NOT NULL, 
	[ChannelSender] VARCHAR(64) NOT NULL, 
    [ChannelAddress] VARCHAR(256) NOT NULL, 
    [ChannelMessage] VARCHAR(MAX) NOT NULL, 
    [ExpiryDate] DATETIME NOT NULL, 
    [Payload] VARCHAR(MAX) NULL, 
	[RefCode] VARCHAR(8) NULL, 
    [Code] VARCHAR(8) NOT NULL, 
    [Status] VARCHAR(16) NOT NULL, 
    CONSTRAINT [FK_OneTimePassword_ToEnumChannelType] FOREIGN KEY ([ChannelType]) REFERENCES [otp].[EnumChannelType]([ChannelType]), 
    CONSTRAINT [FK_OneTimePassword_ToEnumStatus] FOREIGN KEY ([Status]) REFERENCES [otp].[EnumStatus]([Status]), 
    CONSTRAINT [FK_OneTimePassword_ToSystem] FOREIGN KEY ([AccountId]) REFERENCES [otp].[Account]([AccountId])
)
