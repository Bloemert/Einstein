CREATE TABLE [dbo].[EmployeeExperiences]
(
	[Id] INT NOT NULL PRIMARY KEY,
  [EffectiveStartedOn] DATETIME NOT NULL, 
  [EffectiveStartedBy] INT NOT NULL, 
  [EffectiveModifiedOn] DATETIME NOT NULL, 
  [EffectiveModifiedBy] INT NOT NULL, 
  [EffectiveEndedOn] DATETIME NOT NULL, 
  [EffectiveEndedBy] INT NOT NULL, 
  [Comment] NVARCHAR(MAX) NULL,
  [Client] NVARCHAR(150) NULL,
  [Employer] NVARCHAR(150) NOT NULL,
  [FunctionTitle] NVARCHAR(150) NULL,
  [FunctionLevel] NVARCHAR(150) NULL,
  [Location] NVARCHAR(150) NULL,
  [AvailabilityPerc] decimal(18) NULL,
  [StartDate] DATE NULL,
  [FinishedDate] DATE NULL,
  [Technics] NVARCHAR(MAX) NULL,
  [Description] NVARCHAR(MAX) NULL,
  [Work] NVARCHAR(MAX) NULL
)
