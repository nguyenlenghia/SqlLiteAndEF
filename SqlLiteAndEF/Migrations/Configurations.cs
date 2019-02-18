using SQLite.CodeFirst;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;

namespace SqlLiteAndEF
{
    public class SqLiteDbContext<T> : DbContext where T : DbContext
    {
        private readonly int _currentSchemaVersion;
        public SqLiteDbContext(string connectionString, int currentSchemaVersion)
            : base(connectionString)
        {
            _currentSchemaVersion = currentSchemaVersion;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new CreateOrMigrateDatabaseInitializer<T>(modelBuilder, _currentSchemaVersion));
        }
    }

    internal class CreateOrMigrateDatabaseInitializer<TContext> :
        SqliteCreateDatabaseIfNotExists<TContext> where TContext : DbContext
    {
        private readonly int _currentSchemaVersion;
        public CreateOrMigrateDatabaseInitializer(DbModelBuilder modelBuilder,
            int currentSchemaVersion) :
            base(modelBuilder)
        {
            _currentSchemaVersion = currentSchemaVersion;
        }
        protected override void Seed(TContext context)
        {
            //base.Seed(context);
            //context.Database.ExecuteSqlCommand($"PRAGMA user_version={_currentSchemaVersion}");
        }
        public override void InitializeDatabase(TContext context)
        {
            //base.InitializeDatabase(context);
            if (context.Database.Exists())
                Migrate(context);
        }
        private void Migrate(DbContext context)
        {
            var currentDatabaseVersion = context.Database.SqlQuery<int>("PRAGMA user_version").First();

            if (!Directory.Exists("migrations")) return;
            var scriptFiles = Directory.GetFiles("migrations/", "*.sql");

            var executeScriptFiles = new Dictionary<int, string>();
            foreach (var scriptFile in scriptFiles)
            {
                var filenamePrefix = Path.GetFileName(scriptFile)?.Split('.').First();
                // bỏ qua các file ko có version
                if (!int.TryParse(filenamePrefix, out var fileVersion))
                    return;

                // chỉ execute các file từ điểm dbVer tới appVer.
                // currentDatabaseVersion < fileVersion <= _currentSchemaVersion
                if (fileVersion <= currentDatabaseVersion || _currentSchemaVersion < fileVersion)
                    continue;

                executeScriptFiles.Add(fileVersion, scriptFile);
            }
            // sắp xếp chạy theo thứ tự
            foreach (var scriptFile in executeScriptFiles.OrderBy(o => o.Key))
            {
                var migrationScript = File.ReadAllText(scriptFile.Value);
                context.Database.ExecuteSqlCommand(migrationScript);
            }   
        }
    }
}
