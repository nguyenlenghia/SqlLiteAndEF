PRAGMA user_version=2; -- must equals the filename prefix
----------------------
CREATE TABLE [StudentMasters] (
  [ID] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [EmpName] nvarchar NOT NULL
, [Salary] float NOT NULL
, [Designation] nvarchar NOT NULL
);

INSERT INTO [StudentMasters] ([EmpName], [Salary], [Designation])
	VALUES ('xx', 'yy', 'zz');
