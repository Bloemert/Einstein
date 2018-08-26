CREATE TABLE [dbo].[Employees]
(
  [Id] INT NOT NULL PRIMARY KEY IDENTITY, 
  [Deleted] BIT NOT NULL DEFAULT 0, 
  [Firstname] NVARCHAR(150) NULL, 
  [Lastname] NVARCHAR(150) NULL, 
  [Email] NVARCHAR(150) NULL,
  [EmployedSince] DATETIME NULL,
  [EmployeeNumber] INT NULL,
  [AvailabilityPerWeek] INT NULL,
  [FunctionLevel] NVARCHAR(150) NULL,
  [FunctionTitle] NVARCHAR(150) NULL,
  [ManagerId] INT NULL
)
