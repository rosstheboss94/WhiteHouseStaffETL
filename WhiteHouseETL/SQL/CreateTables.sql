USE [WhiteHouseETL];

CREATE TABLE [dbo].[Employee](
	[Employee ID] INT IDENTITY(1,1) PRIMARY KEY,
	[First Name] NVARCHAR(50),
	[Middle Initial] NVARCHAR(50),
	[Last Name] NVARCHAR(50),
	[Gender] NVARCHAR(10)
);

CREATE TABLE [dbo].[Positions](
	[Position ID] INT IDENTITY(1,1) PRIMARY KEY,
	[Position Title] NVARCHAR(1000),
	[Pay Basis] NVARCHAR(100),
	[Status] NVARCHAR(100)
);

CREATE TABLE [dbo].[Salaries](
	Salary INT,
	[Year] INT,
	[Employee ID] INT,
	[Position ID] INT,
	FOREIGN KEY ([Employee ID]) REFERENCES Employee([Employee ID]),
	FOREIGN KEY ([Position ID]) REFERENCES Positions([Position ID])
);




