-- Run this script in SQL Server Management Studio 22.
-- It creates the database and the table used by Parks_LabAssignment8.

IF DB_ID('UserLoginDB') IS NULL
BEGIN
    CREATE DATABASE UserLoginDB;
END
GO

USE UserLoginDB;
GO

IF OBJECT_ID('dbo.Users', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Users
    (
        UserID INT IDENTITY(1,1) PRIMARY KEY,
        Username VARCHAR(50) NOT NULL UNIQUE,
        PasswordHash VARBINARY(32) NOT NULL,
        PasswordSalt VARBINARY(16) NOT NULL
    );
END
GO
