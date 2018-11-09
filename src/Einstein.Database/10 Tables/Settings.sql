CREATE TABLE [dbo].[Settings]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
  [EffectiveStartedOn] DATETIME NOT NULL, 
  [EffectiveStartedBy] INT NOT NULL, 
  [EffectiveModifiedOn] DATETIME NOT NULL, 
  [EffectiveModifiedBy] INT NOT NULL, 
  [EffectiveEndedOn] DATETIME NOT NULL, 
  [EffectiveEndedBy] INT NOT NULL, 
  [Comment] NVARCHAR(MAX) NULL,
  [Description] NVARCHAR(MAX) NOT NULL, 
  [Key] NVARCHAR(128) NULL, 
  [Name] NVARCHAR(128) NULL, 
  [Value] NVARCHAR(256) NOT NULL,
)
