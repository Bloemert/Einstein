CREATE TABLE [Logs] (
 
   [Id] int IDENTITY(1,1) NOT NULL,
  [EffectiveStartedOn] DATETIME NOT NULL, 
  [EffectiveStartedBy] INT NOT NULL, 
  [EffectiveModifiedOn] DATETIME NOT NULL, 
  [EffectiveModifiedBy] INT NOT NULL, 
  [EffectiveEndedOn] DATETIME NOT NULL, 
  [EffectiveEndedBy] INT NOT NULL, 
  [Comment] NVARCHAR(MAX) NULL,
   [Message] nvarchar(max) NULL,
   [Level] nvarchar(128) NULL,
   [TimeStamp] datetimeoffset(7) NOT NULL,  
   [Exception] nvarchar(max) NULL,
   [Properties] xml NULL,
   [LogEvent] nvarchar(max) NULL
 
   CONSTRAINT [PK_Logs] 
     PRIMARY KEY CLUSTERED ([Id] DESC) 
 
) 
