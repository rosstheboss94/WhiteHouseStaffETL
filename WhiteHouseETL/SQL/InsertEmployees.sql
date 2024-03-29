USE [WhiteHouseETL]
GO
/****** Object:  StoredProcedure [dbo].[InsertEmployees]    Script Date: 1/8/2024 10:10:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[InsertEmployees]
    @Employees EmployeeTableType READONLY
AS
BEGIN

	CREATE TABLE ##EmployeesTemp (
		[Row Number] INT,
		[First Name] NVARCHAR(50) NULL,
		[Middle Initial] NVARCHAR(50) NULL,
		[Last Name] NVARCHAR(50) NULL,
		[Gender] NVARCHAR(10) NULL
	)

	INSERT INTO ##EmployeesTemp([Row Number], [First Name], [Middle Initial], [Last Name], Gender)
	SELECT RowNumber, FirstName, MiddleInitial, LastName, Gender FROM @Employees

	INSERT INTO Employee([First Name], [Middle Initial], [Last Name], Gender)
	SELECT 
		MAX(FirstName) AS FirstName,
		MAX(MiddleInitial) AS MiddleInitial,
		MAX(LastName) AS LastName,
		MAX(Gender) AS Gender
	FROM @Employees
	GROUP BY FirstName, MiddleInitial, LastName, Gender;
END;