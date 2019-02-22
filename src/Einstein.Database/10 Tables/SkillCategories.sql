CREATE TABLE [dbo].[SkillCategories]
(
  [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(), 
  [EffectiveStartedOn] DATETIME NOT NULL DEFAULT GETDATE(), 
  [EffectiveStartedBy] UNIQUEIDENTIFIER NULL, 
  [EffectiveModifiedOn] DATETIME NOT NULL DEFAULT GETDATE(), 
  [EffectiveModifiedBy] UNIQUEIDENTIFIER NULL, 
  [EffectiveEndedOn] DATETIME NOT NULL, 
  [EffectiveEndedBy] UNIQUEIDENTIFIER NULL,
  [Comment] NVARCHAR(1024) NULL,
  [Name] nvarchar(150) NOT NULL,
  [Description] NVARCHAR(max) NULL,
)
