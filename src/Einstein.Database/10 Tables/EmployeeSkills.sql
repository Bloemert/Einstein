CREATE TABLE [dbo].[ScoredSkills]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
  [EffectiveStartedOn] DATETIME NOT NULL, 
  [EffectiveStartedBy] INT NOT NULL, 
  [EffectiveModifiedOn] DATETIME NOT NULL, 
  [EffectiveModifiedBy] INT NOT NULL, 
  [EffectiveEndedOn] DATETIME NOT NULL, 
  [EffectiveEndedBy] INT NOT NULL, 
  [Comment] NVARCHAR(MAX) NULL,
  [EmployeeId] INT NOT NULL,
  [SkillId] INT NOT NULL,
  [SkillScoreId] INT NOT NULL
)
