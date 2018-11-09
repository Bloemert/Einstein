CREATE TABLE [dbo].[EmployeeEducations]
(
	[Id] INT NOT NULL PRIMARY KEY,
  [EffectiveStartedOn] DATETIME NOT NULL, 
  [EffectiveStartedBy] INT NOT NULL, 
  [EffectiveModifiedOn] DATETIME NOT NULL, 
  [EffectiveModifiedBy] INT NOT NULL, 
  [EffectiveEndedOn] DATETIME NOT NULL, 
  [EffectiveEndedBy] INT NOT NULL, 
  [Comment] NVARCHAR(MAX) NULL,
  [EducationId] INT NOT NULL,
  [EducationType] INT NOT NULL,
  [StartDate] DATE NULL,
  [FinishedDate] DATE NULL
)
