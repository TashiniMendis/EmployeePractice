create database EmployeeInfoDB;

use EmployeeInfoDB;

CREATE TABLE Employees (
    EmployeeNo INT IDENTITY(1,1) PRIMARY KEY,  -- Automatically incrementing Employee Number
    FirstName NVARCHAR(50) NOT NULL,           -- First Name of the Employee
    LastName NVARCHAR(50) NOT NULL,            -- Last Name of the Employee
    DateOfBirth DATE NOT NULL,                 -- Date of Birth of the Employee
    Salary DECIMAL(18, 2) NOT NULL             -- Salary of the Employee
);

CREATE PROCEDURE sp_GetAllEmployees
AS
BEGIN
    SELECT EmployeeNo, FirstName, LastName, DateOfBirth, Salary FROM Employees
END

CREATE PROCEDURE sp_GetEmployeeById
    @EmployeeNo INT
AS
BEGIN
    SELECT EmployeeNo, FirstName, LastName, DateOfBirth, Salary 
    FROM Employees
    WHERE EmployeeNo = @EmployeeNo
END


CREATE PROCEDURE sp_AddEmployee
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @DateOfBirth DATETIME,
    @Salary DECIMAL(18, 2)
AS
BEGIN
    INSERT INTO Employees (FirstName, LastName, DateOfBirth, Salary)
    VALUES (@FirstName, @LastName, @DateOfBirth, @Salary)
END


CREATE PROCEDURE sp_UpdateEmployee
    @EmployeeNo INT,
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @DateOfBirth DATETIME,
    @Salary DECIMAL(18, 2)
AS
BEGIN
    UPDATE Employees
    SET FirstName = @FirstName,
        LastName = @LastName,
        DateOfBirth = @DateOfBirth,
        Salary = @Salary
    WHERE EmployeeNo = @EmployeeNo
END


CREATE PROCEDURE sp_DeleteEmployee
    @EmployeeNo INT
AS
BEGIN
    DELETE FROM Employees WHERE EmployeeNo = @EmployeeNo
END



CREATE PROCEDURE sp_GetAverageSalary
AS
BEGIN
    SELECT AVG(Salary) AS AverageSalary FROM Employees
END