CREATE TABLE [dbo].[Spots]
(
  [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(), 
  [EffectiveStartedOn] DATETIME NOT NULL DEFAULT GETDATE(), 
  [EffectiveStartedBy] UNIQUEIDENTIFIER NULL, 
  [EffectiveModifiedOn] DATETIME NOT NULL DEFAULT GETDATE(), 
  [EffectiveModifiedBy] UNIQUEIDENTIFIER NULL, 
  [EffectiveEndedOn] DATETIME NOT NULL, 
  [EffectiveEndedBy] UNIQUEIDENTIFIER NULL,
  [Comment] NVARCHAR(1024) NULL,
  [SpotterId] INT NOT NULL, 
  [SpottedOn] Datetime NOT NULL DEFAULT GETDATE(),
  [Name] nvarchar(100) NOT NULL,
  [RingId]	INT NOT NULL,
  [SectorId] INT NOT NULL,
  [isNew]	BIT NOT NULL DEFAULT 1,
  [Description] nvarchar(1024) NULL
)
