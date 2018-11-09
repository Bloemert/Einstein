CREATE TABLE [dbo].[Sessions]
(
  [Id] INT NOT NULL PRIMARY KEY IDENTITY, 
  [EffectiveStartedOn] DATETIME NOT NULL, 
  [EffectiveStartedBy] INT NOT NULL, 
  [EffectiveModifiedOn] DATETIME NOT NULL, 
  [EffectiveModifiedBy] INT NOT NULL, 
  [EffectiveEndedOn] DATETIME NOT NULL, 
  [EffectiveEndedBy] INT NOT NULL, 
  [Comment] NVARCHAR(MAX) NULL,
  [ActiveUserId] INT NOT NULL,
  [ClientInfo] NVARCHAR(50) NULL, 
    [Started] DATETIME NOT NULL
)
