CREATE TABLE [dbo].[Employees]
(
  [Id] INT NOT NULL PRIMARY KEY IDENTITY, 
  [EffectiveStartedOn] DATETIME NOT NULL, 
  [EffectiveStartedBy] INT NOT NULL, 
  [EffectiveModifiedOn] DATETIME NOT NULL, 
  [EffectiveModifiedBy] INT NOT NULL, 
  [EffectiveEndedOn] DATETIME NOT NULL, 
  [EffectiveEndedBy] INT NOT NULL, 
  [Comment] NVARCHAR(MAX) NULL,
  [Firstname] NVARCHAR(150) NULL, 
  [Lastname] NVARCHAR(150) NULL, 
  [Email] NVARCHAR(150) NULL,
  [EmployedSince] DATETIME NULL,
  [EmployeeNumber] INT NULL,
  [AvailabilityPerc] INT NULL,
  [FunctionLevel] NVARCHAR(150) NULL,
  [FunctionTitle] NVARCHAR(150) NULL,
  [ManagerId] INT NULL
)
