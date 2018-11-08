CREATE TABLE [Logs] (
 
   [Id] int IDENTITY(1,1) NOT NULL,
   [Deleted] BIT NOT NULL DEFAULT 0, 
   [Message] nvarchar(max) NULL,
   [Level] nvarchar(128) NULL,
   [TimeStamp] datetimeoffset(7) NOT NULL,  
   [Exception] nvarchar(max) NULL,
   [Properties] xml NULL,
   [LogEvent] nvarchar(max) NULL
 
   CONSTRAINT [PK_Logs] 
     PRIMARY KEY CLUSTERED ([Id] DESC) 
 
) 
