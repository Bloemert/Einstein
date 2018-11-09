CREATE TABLE [dbo].[UserRoles]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
  [EffectiveStartedOn] DATETIME NOT NULL, 
  [EffectiveStartedBy] INT NOT NULL, 
  [EffectiveModifiedOn] DATETIME NOT NULL, 
  [EffectiveModifiedBy] INT NOT NULL, 
  [EffectiveEndedOn] DATETIME NOT NULL, 
  [EffectiveEndedBy] INT NOT NULL, 
  [Comment] NVARCHAR(MAX) NULL,
	[UserId] INT NOT NULL,
	[RoleId] INT NOT NULL
)
