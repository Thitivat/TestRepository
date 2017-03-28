
CREATE ROLE [Otp_Role]
GO

/* [Attempt] */
GRANT INSERT ON [otp].[Attempt] TO [Otp_Role]
GO
GRANT SELECT ON [otp].[Attempt] TO [Otp_Role]
GO
GRANT UPDATE ON [otp].[Attempt] TO [Otp_Role]
GO
GRANT DELETE ON [otp].[Attempt] TO [Otp_Role]
GO

/* [EnumChannelType] */
GRANT INSERT ON [otp].[EnumChannelType] TO [Otp_Role]
GO
GRANT SELECT ON [otp].[EnumChannelType] TO [Otp_Role]
GO
GRANT UPDATE ON [otp].[EnumChannelType] TO [Otp_Role]
GO
GRANT DELETE ON [otp].[EnumChannelType] TO [Otp_Role]
GO

/* [EnumStatus] */
GRANT INSERT ON [otp].[EnumStatus] TO [Otp_Role]
GO
GRANT SELECT ON [otp].[EnumStatus] TO [Otp_Role]
GO
GRANT UPDATE ON [otp].[EnumStatus] TO [Otp_Role]
GO
GRANT DELETE ON [otp].[EnumStatus] TO [Otp_Role]
GO

/* [Log] */
GRANT INSERT ON [otp].[Log] TO [Otp_Role]
GO
GRANT SELECT ON [otp].[Log] TO [Otp_Role]
GO
GRANT UPDATE ON [otp].[Log] TO [Otp_Role]
GO

/* [OneTimePassword] */
GRANT INSERT ON [otp].[OneTimePassword] TO [Otp_Role]
GO
GRANT SELECT ON [otp].[OneTimePassword] TO [Otp_Role]
GO
GRANT UPDATE ON [otp].[OneTimePassword] TO [Otp_Role]
GO

/* [Setting] */
GRANT INSERT ON [otp].[Setting] TO [Otp_Role]
GO
GRANT SELECT ON [otp].[Setting] TO [Otp_Role]
GO
GRANT UPDATE ON [otp].[Setting] TO [Otp_Role]
GO
GRANT DELETE ON [otp].[Setting] TO [Otp_Role]
GO

/* [Account] */
GRANT INSERT ON [otp].[Account] TO [Otp_Role]
GO
GRANT SELECT ON [otp].[Account] TO [Otp_Role]
GO
GRANT UPDATE ON [otp].[Account] TO [Otp_Role]
GO
