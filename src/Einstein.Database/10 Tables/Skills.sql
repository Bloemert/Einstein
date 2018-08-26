CREATE TABLE [dbo].[Skills]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
  [Deleted] BIT NOT NULL DEFAULT 0, 
  [Name] nvarchar(100) NOT NULL,
  [Description] nvarchar(max) NULL,
  [Version] nvarchar(255) NULL, 
    [SkillTypeId] INT NOT NULL, 
    [SkillCategoryId] INT NOT NULL
)
