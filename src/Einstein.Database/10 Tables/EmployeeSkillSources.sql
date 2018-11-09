CREATE TABLE [dbo].[EmployeeSkillSources]
(
	[Id] INT NOT NULL PRIMARY KEY,
  [EffectiveStartedOn] DATETIME NOT NULL, 
  [EffectiveStartedBy] INT NOT NULL, 
  [EffectiveModifiedOn] DATETIME NOT NULL, 
  [EffectiveModifiedBy] INT NOT NULL, 
  [EffectiveEndedOn] DATETIME NOT NULL, 
  [EffectiveEndedBy] INT NOT NULL, 
  [Comment] NVARCHAR(MAX) NULL,
  [SourceType] INT NOT NULL,
  [SourceId] INT NOT NULL,
  [EmployeeSkillId] INT NOT NULL
)
