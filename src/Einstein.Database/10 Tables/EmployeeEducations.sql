CREATE TABLE [dbo].[EmployeeEducations]
(
  [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(), 
  [EffectiveStartedOn] DATETIME NOT NULL DEFAULT GETDATE(), 
  [EffectiveStartedBy] UNIQUEIDENTIFIER NULL, 
  [EffectiveModifiedOn] DATETIME NOT NULL DEFAULT GETDATE(), 
  [EffectiveModifiedBy] UNIQUEIDENTIFIER NULL, 
  [EffectiveEndedOn] DATETIME NOT NULL, 
  [EffectiveEndedBy] UNIQUEIDENTIFIER NULL,
  [Comment] NVARCHAR(1024) NULL,
  [EducationName] NVARCHAR(256) NOT NULL,
  [EducationType] INT NOT NULL,
  [StartDate] DATE NULL,
  [FinishedDate] DATE NULL,
  [EmployeeSkillSourceId] UNIQUEIDENTIFIER NOT NULL
)
