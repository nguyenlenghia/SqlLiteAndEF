PRAGMA user_version=3; -- must equals the filename prefix
----------------------
-- edit column [StudentMasters].[Salary] from float to nvarchar type
PRAGMA foreign_keys=off;

ALTER TABLE [StudentMasters]
	RENAME TO [StudentMasters_beforeV3];

 CREATE TABLE [StudentMasters] (
  [ID] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [EmpName] nvarchar NOT NULL
, [Salary] nvarchar NOT NULL
, [Designation] nvarchar NOT NULL
);


INSERT INTO [StudentMasters] ([EmpName], [Salary], [Designation])
	SELECT [EmpName], [Salary], [Designation]
	FROM [StudentMasters_beforeV3];

DROP TABLE [StudentMasters_beforeV3];

PRAGMA foreign_keys=on;
