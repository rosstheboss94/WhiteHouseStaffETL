CREATE TYPE EmployeeTableType AS TABLE
(
	RowNumber INT,
    FirstName NVARCHAR(100),
    MiddleInitial NVARCHAR(100),
    LastName NVARCHAR(100),
    Gender NVARCHAR(100)
);

CREATE TYPE PositionTableType AS TABLE
(
	RowNumber INT,
    PositionTitle VARCHAR(1000),
    PayBasis VARCHAR(100),
    Status VARCHAR(100)
);

CREATE TYPE SalaryTableType AS TABLE
(
	RowNumber INT,
    Salary INT,
    Year INT
);