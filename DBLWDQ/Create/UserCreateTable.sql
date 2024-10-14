CREATE TABLE [dbo].[User] (
    [id] INT IDENTITY(1,1) PRIMARY KEY,
    [password] NVARCHAR(128) NOT NULL,
    [last_login] DATETIME NULL,
    [is_superuser] BIT NOT NULL,
    [username] NVARCHAR(150) NOT NULL UNIQUE,
    [first_name] NVARCHAR(128) NULL,
    [last_name] NVARCHAR(128) NULL,
    [email] NVARCHAR(254) NULL UNIQUE,
    [is_staff] BIT NOT NULL,
    [is_active] BIT NOT NULL,
    [date_joined] DATETIME NOT NULL
);
