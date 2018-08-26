CREATE TABLE [dbo].[SkillScores]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
  [Deleted] BIT NOT NULL DEFAULT 0, 
  [Name] nvarchar(100) NOT NULL,
  [Description] nvarchar(max) NULL,
  [Value] INT NULL,
  [SkillId] INT NOT NULL
)
