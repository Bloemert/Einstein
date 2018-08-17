CREATE TABLE [dbo].[Rings]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
  [Deleted] BIT NOT NULL DEFAULT 0, 
  [Seqno] INT NOT NULL, 
  [Name] nvarchar(100) NOT NULL,
  [Description] nvarchar(max) NULL
)
