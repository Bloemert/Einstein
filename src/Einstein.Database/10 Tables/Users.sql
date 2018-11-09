CREATE TABLE [dbo].[Users]
(
  [Id] INT NOT NULL PRIMARY KEY IDENTITY, 
  [EffectiveStartedOn] DATETIME NOT NULL, 
  [EffectiveStartedBy] INT NOT NULL, 
  [EffectiveModifiedOn] DATETIME NOT NULL, 
  [EffectiveModifiedBy] INT NOT NULL, 
  [EffectiveEndedOn] DATETIME NOT NULL, 
  [EffectiveEndedBy] INT NOT NULL, 
  [Comment] NVARCHAR(MAX) NULL,
  [Active] BIT NOT NULL DEFAULT 1,
  [Login] NVARCHAR(128) NOT NULL,
  [PasswordData] NVARCHAR(2048) NOT NULL, 
  [ExpireDate] DATETIME NULL, 
  [LastLogin] DATETIME NULL, 
  [FailedAttempts] INT NOT NULL DEFAULT 0, 
  [GoodLogins] INT NOT NULL DEFAULT 0, 
  [Firstname] NVARCHAR(150) NULL, 
  [Lastname] NVARCHAR(150) NULL, 
  [Email] NVARCHAR(150) NULL
)
