CREATE TABLE [dbo].[Spots]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
  [Deleted] BIT NOT NULL DEFAULT 0, 
  [SpotterId] INT NOT NULL, 
  [SpottedOn] Datetime NOT NULL DEFAULT GETDATE(),
  [Name] nvarchar(100) NOT NULL,
  [RingId]	INT NOT NULL,
  [SectorId] INT NOT NULL,
  [isNew]	BIT NOT NULL DEFAULT 1,
  [Description] nvarchar(max) NULL
)
