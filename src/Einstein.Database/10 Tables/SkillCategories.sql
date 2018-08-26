CREATE TABLE [dbo].[SkillCategories]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
  [Deleted] BIT NOT NULL DEFAULT 0, 
  [Name] nvarchar(150) NOT NULL,
  [Description] NVARCHAR(max) NULL,
)
