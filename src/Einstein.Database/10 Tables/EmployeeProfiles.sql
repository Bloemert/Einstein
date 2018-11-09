CREATE TABLE [dbo].[EmployeeProfiles]
(
	[Id] INT NOT NULL PRIMARY KEY,
  [EffectiveStartedOn] DATETIME NOT NULL, 
  [EffectiveStartedBy] INT NOT NULL, 
  [EffectiveModifiedOn] DATETIME NOT NULL, 
  [EffectiveModifiedBy] INT NOT NULL, 
  [EffectiveEndedOn] DATETIME NOT NULL, 
  [EffectiveEndedBy] INT NOT NULL, 
  [Comment] NVARCHAR(MAX) NULL,
  [Text] NVARCHAR(MAX) NULL,
  [Tags] NVARCHAR(250) NULL,
  [EmployeeId] INT NOT NULL
)
