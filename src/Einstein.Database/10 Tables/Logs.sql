CREATE TABLE [Logs] (
 
  [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(), 
  [EffectiveStartedOn] DATETIME NOT NULL DEFAULT GETDATE(), 
  [EffectiveStartedBy] UNIQUEIDENTIFIER NULL, 
  [EffectiveModifiedOn] DATETIME NOT NULL DEFAULT GETDATE(), 
  [EffectiveModifiedBy] UNIQUEIDENTIFIER NULL, 
  [EffectiveEndedOn] DATETIME NOT NULL, 
  [EffectiveEndedBy] UNIQUEIDENTIFIER NULL,
  [Comment] NVARCHAR(1024) NULL,
   [Message] nvarchar(2048) NULL,
   [Level] nvarchar(128) NULL,
   [TimeStamp] datetimeoffset(7) NOT NULL,  
   [Exception] nvarchar(max) NULL,
   [Properties] xml NULL,
   [LogEvent] nvarchar(256) NULL
) 
