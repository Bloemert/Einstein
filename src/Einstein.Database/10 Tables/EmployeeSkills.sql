CREATE TABLE [dbo].[ScoredSkills]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
  [Deleted] BIT NOT NULL DEFAULT 0, 
  [SkillId] INT NOT NULL,
  [ScoreId] INT NOT NULL,
  [EmployeeId] INT NOT NULL
)
