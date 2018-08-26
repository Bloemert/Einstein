CREATE TABLE [dbo].[SkillTypes]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
  [Deleted] BIT NOT NULL DEFAULT 0, 
  [Name] nvarchar(100) NOT NULL,
  [Description] nvarchar(max) NULL
)
