USE [BND.Services.Security.OTP.Db]
GO

SET ANSI_PADDING ON
GO
INSERT [otp].[Account] ([AccountId], [ApiKey], [Name], [IpAddress], [IsActive], [Description], [Salt], [Email]) VALUES 
(N'5861f73f-cad5-419d-96d4-56bd07211297', N'lLTHykjZ3oZ5ORx1ePayit2+orgbNxI8d5N3296EhT7lSawnoygnfjKbyQdTrxVFLlZ8AfDjn1OnW5Hy2oAgWg==', N'AccountTest'
, N'192.168.1.69', 1, N'This is for testing', N'ALzIYz8BIgvLsk1q', N'Test@mail.com')
GO

INSERT [otp].[EnumChannelType] ([ChannelType], [Name]) VALUES (N'EMAIL', N'e-Mail')
INSERT [otp].[EnumChannelType] ([ChannelType], [Name]) VALUES (N'SMS', N'Short Message Service')
GO

INSERT [otp].[EnumStatus] ([Status], [Description]) VALUES (N'Canceled', N'The code has been cancelled by system because client re-generate code again with same suid.')
INSERT [otp].[EnumStatus] ([Status], [Description]) VALUES (N'Deleted', N'The code has been deleted by client or system.')
INSERT [otp].[EnumStatus] ([Status], [Description]) VALUES (N'Expired', N'The code has expired.')
INSERT [otp].[EnumStatus] ([Status], [Description]) VALUES (N'Locked', N'The code is locked by entered wrong more than limitation.')
INSERT [otp].[EnumStatus] ([Status], [Description]) VALUES (N'Pending', N'The code ready to use.')
INSERT [otp].[EnumStatus] ([Status], [Description]) VALUES (N'Verified', N'The code is used.')
GO
INSERT [otp].[Setting] ([Key], [Value]) VALUES (N'Expiration', N'900')
INSERT [otp].[Setting] ([Key], [Value]) VALUES (N'RetryCount', N'3')
GO