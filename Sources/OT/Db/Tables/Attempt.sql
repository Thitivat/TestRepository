CREATE TABLE [otp].[Attempt]
(
	[AttemptId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [OtpId] VARCHAR(128) NOT NULL, 
    [Date] DATETIME2 NOT NULL, 
    [IpAddress] VARCHAR(64) NOT NULL, 
    [UserAgent] VARCHAR(max) NOT NULL, 
    CONSTRAINT [FK_Attempt_ToOneTimePassword] FOREIGN KEY ([OtpId]) REFERENCES [otp].[OneTimePassword]([OtpId])
)
