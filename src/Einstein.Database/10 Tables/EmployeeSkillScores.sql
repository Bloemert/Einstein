CREATE TABLE [dbo].[EmployeeSkillScores]
(
	[Id] INT NOT NULL PRIMARY KEY,
  [EffectiveStartedOn] DATETIME NOT NULL, 
  [EffectiveStartedBy] INT NOT NULL, 
  [EffectiveModifiedOn] DATETIME NOT NULL, 
  [EffectiveModifiedBy] INT NOT NULL, 
  [EffectiveEndedOn] DATETIME NOT NULL, 
  [EffectiveEndedBy] INT NOT NULL, 
  [Comment] NVARCHAR(1024) NULL,
  [EmployeeSkillId] INT NOT NULL,
  [SkillScoreId] INT NOT NULL
)
