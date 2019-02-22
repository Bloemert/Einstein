CREATE TABLE [dbo].[EmployeeExperiences]
(
  [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(), 
  [EffectiveStartedOn] DATETIME NOT NULL DEFAULT GETDATE(), 
  [EffectiveStartedBy] UNIQUEIDENTIFIER NULL, 
  [EffectiveModifiedOn] DATETIME NOT NULL DEFAULT GETDATE(), 
  [EffectiveModifiedBy] UNIQUEIDENTIFIER NULL, 
  [EffectiveEndedOn] DATETIME NOT NULL, 
  [EffectiveEndedBy] UNIQUEIDENTIFIER NULL,
  [Comment] NVARCHAR(1024) NULL,
  [Client] NVARCHAR(150) NULL,
  [Employer] NVARCHAR(150) NOT NULL,
  [FunctionTitle] NVARCHAR(150) NULL,
  [FunctionLevel] NVARCHAR(150) NULL,
  [Location] NVARCHAR(150) NULL,
  [AvailabilityPerc] decimal(18) NULL,
  [StartDate] DATE NULL,
  [FinishedDate] DATE NULL,
  [Technics] NVARCHAR(MAX) NULL,
  [Description] NVARCHAR(MAX) NULL,
  [Work] NVARCHAR(MAX) NULL,
  [EmployeeSkillSourceId] UNIQUEIDENTIFIER NOT NULL
)
