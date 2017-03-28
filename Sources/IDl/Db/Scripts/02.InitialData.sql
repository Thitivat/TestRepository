USE [BND.Services.Payments.iDeal.Db]
GO

SET ANSI_PADDING ON
GO

INSERT [ideal].[Setting] ([Key], [Value]) VALUES (N'DefaultExpirationPeriodSecond', N'900')
INSERT [ideal].[Setting] ([Key], [Value]) VALUES (N'DirectoryRequestInterval', N'1')
INSERT [ideal].[Setting] ([Key], [Value]) VALUES (N'MaxExpirationPeriodSecond', N'3600')
INSERT [ideal].[Setting] ([Key], [Value]) VALUES (N'MaxRetriesPerDays', N'5')
INSERT [ideal].[Setting] ([Key], [Value]) VALUES (N'MinExpirationPeriodSecond', N'60')
GO