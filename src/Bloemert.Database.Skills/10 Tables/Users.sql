CREATE TABLE [dbo].[Users]
(
  [Id] INT NOT NULL PRIMARY KEY IDENTITY, 
  [Deleted] BIT NOT NULL DEFAULT 0, 
  [Active] BIT NOT NULL DEFAULT 1,
  [Login] NVARCHAR(128) NOT NULL,
  [PasswordData] NVARCHAR(2048) NOT NULL, 
    [ExpireDate] DATETIME NULL, 
    [LastLogin] DATETIME NULL, 
    [FailedAttempts] INT NOT NULL DEFAULT 0, 
    [GoodLogins] INT NOT NULL DEFAULT 0
)
