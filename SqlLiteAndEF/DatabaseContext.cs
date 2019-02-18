using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite;
using System.Text;

namespace SqlLiteAndEF
{
    public class DatabaseContext : SqLiteDbContext<DatabaseContext>
    {
        private const int CurrentSchemaVersion = 3;

        public DatabaseContext() : base("SqliteSampleDBContext", CurrentSchemaVersion)
        {
        }

        public DbSet<EmployeeMaster> EmployeeMaster { get; set; }
        public DbSet<StudentMaster> StudentMaster { get; set; }
    }
}
