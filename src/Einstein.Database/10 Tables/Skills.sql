CREATE TABLE [dbo].[Skills]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
  [EffectiveStartedOn] DATETIME NOT NULL, 
  [EffectiveStartedBy] INT NOT NULL, 
  [EffectiveModifiedOn] DATETIME NOT NULL, 
  [EffectiveModifiedBy] INT NOT NULL, 
  [EffectiveEndedOn] DATETIME NOT NULL, 
  [EffectiveEndedBy] INT NOT NULL, 
  [Comment] NVARCHAR(MAX) NULL,
  [Name] nvarchar(100) NOT NULL,
  [Description] nvarchar(max) NULL,
  [SkillTypeId] INT NOT NULL, 
  [SkillCategoryId] INT NOT NULL
)
