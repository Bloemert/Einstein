CREATE TABLE [dbo].[TechSpots]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
  [Deleted] BIT NOT NULL DEFAULT 0, 
  [SpotterId] INT NOT NULL, 
  [SpottedOn] Datetime NOT NULL DEFAULT GETDATE(),
  [Name] nvarchar(100) NOT NULL,
  [Ring]	nvarchar(150) NOT NULL,
  [Quadrant] nvarchar(150) NOT NULL,
  [isNew]	BIT NOT NULL DEFAULT 1,
  [Description] nvarchar(max) NULL
)
