PRAGMA user_version=1; -- must equals the filename prefix
----------------------
CREATE TABLE [EmployeeMasters] (
  [ID] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [EmpName] nvarchar NOT NULL
, [Salary] float NOT NULL
, [Designation] nvarchar NOT NULL
);