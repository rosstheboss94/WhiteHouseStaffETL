USE [WhiteHouseETL]
GO
/****** Object:  StoredProcedure [dbo].[InsertPositions]    Script Date: 1/7/2024 5:55:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[InsertSalaries]
    @Salaries SalaryTableType READONLY
AS
BEGIN
	INSERT INTO Salaries (Salary, Year, [Employee ID],[Position ID])
	SELECT Salary, [Year], emp.[Employee ID], pos.[Position ID]
	FROM @Salaries sal
	JOIN ##EmployeesTemp empt ON sal.RowNumber = empt.[Row Number]
	JOIN ##PositionsTemp post ON sal.RowNumber = post.[Row Number]
	JOIN Employee emp ON empt.[First Name] = emp.[First Name] AND empt.[Middle Initial] = emp.[Middle Initial] AND empt.[Last Name] = emp.[Last Name] AND empt.Gender = emp.Gender
	JOIN Positions pos ON post.[Position Title] = pos.[Position Title] AND post.[Pay Basis] = pos.[Pay Basis] AND post.Status = pos.Status

	DROP TABLE ##EmployeesTemp
	DROP TABLE ##PositionsTemp
END;

