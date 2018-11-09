CREATE TABLE [dbo].[Spots]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
  [EffectiveStartedOn] DATETIME NOT NULL, 
  [EffectiveStartedBy] INT NOT NULL, 
  [EffectiveModifiedOn] DATETIME NOT NULL, 
  [EffectiveModifiedBy] INT NOT NULL, 
  [EffectiveEndedOn] DATETIME NOT NULL, 
  [EffectiveEndedBy] INT NOT NULL, 
  [Comment] NVARCHAR(MAX) NULL,
  [SpotterId] INT NOT NULL, 
  [SpottedOn] Datetime NOT NULL DEFAULT GETDATE(),
  [Name] nvarchar(100) NOT NULL,
  [RingId]	INT NOT NULL,
  [SectorId] INT NOT NULL,
  [isNew]	BIT NOT NULL DEFAULT 1,
  [Description] nvarchar(max) NULL
)
