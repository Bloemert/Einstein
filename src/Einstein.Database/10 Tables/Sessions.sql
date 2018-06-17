CREATE TABLE [dbo].[Sessions]
(
  [Id] INT NOT NULL PRIMARY KEY IDENTITY, 
  [Deleted] BIT NOT NULL DEFAULT 0, 
  [ActiveUserId] INT NOT NULL,
  [ClientInfo] NVARCHAR(50) NULL, 
    [Started] DATETIME NOT NULL
)
