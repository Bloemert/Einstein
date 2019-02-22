CREATE TABLE [dbo].[Employees]
(
  [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(), 
  [EffectiveStartedOn] DATETIME NOT NULL DEFAULT GETDATE(), 
  [EffectiveStartedBy] UNIQUEIDENTIFIER NULL, 
  [EffectiveModifiedOn] DATETIME NOT NULL DEFAULT GETDATE(), 
  [EffectiveModifiedBy] UNIQUEIDENTIFIER NULL, 
  [EffectiveEndedOn] DATETIME NOT NULL, 
  [EffectiveEndedBy] UNIQUEIDENTIFIER NULL,
  [Comment] NVARCHAR(1024) NULL,
  [Firstname] NVARCHAR(150) NOT NULL, 
  [Lastname] NVARCHAR(150) NOT NULL, 
  [Email] NVARCHAR(150) NOT NULL,
  [EmployedSince] DATETIME NULL,
  [EmployeeNumber] INT NULL,
  [AvailabilityPerc] INT NULL,
  [FunctionLevel] NVARCHAR(150) NULL,
  [FunctionTitle] NVARCHAR(150) NULL,
  [ManagerId] INT NULL
)
