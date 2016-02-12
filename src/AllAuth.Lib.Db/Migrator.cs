using System;
using System.Data;
using System.Linq;
using Dapper;

namespace AllAuth.Lib.Db
{
    public abstract class Migrator
    {
        public void MigrateToLatest(DbConnection db)
        {
			var versionInfo = GetVersionInfo (db);

            var currentVersion = versionInfo != null ? versionInfo.Version : 0;

            using (var conn = db.Connect())
            using (var transaction = conn.BeginTransaction())
            {
                do
                {
                    currentVersion++;
                } while (MigrateToVersion(conn, currentVersion));

                transaction.Commit();
            }
        }

        protected class VersionInfo
        {
            public int Version { get; set; }
            public DateTime AppliedOn { get; set; }
            public string Description { get; set; }
        }

        protected static VersionInfo GetVersionInfo(DbConnection db)
        {
            using (var conn = db.Connect())
            {
                var sql = "SELECT Version FROM `VersionInfo` ORDER BY Version DESC LIMIT 1";
                try
                {
                    return conn.Query<VersionInfo>(sql).FirstOrDefault();
                }
                catch (Exception e)
                {
                    if (!e.Message.Contains("no such table: VersionInfo"))
                        throw new Exception(e.Message, e);

                    CreateVersionInfo(db);
                    return conn.Query<VersionInfo>(sql).FirstOrDefault();
                }
            }
        }

        private static void CreateVersionInfo(DbConnection db)
        {
            using (var conn = db.Connect())
            {
                var sql = "CREATE TABLE `VersionInfo` (" +
                          "`Version` INTEGER NOT NULL, " +
                          "`AppliedOn` DATETIME DEFAULT CURRENT_TIMESTAMP, " +
                          "`Description` TEXT);";
                conn.Query(sql);

                sql = "CREATE UNIQUE INDEX IF NOT EXISTS `UC_Version` ON `VersionInfo` (`Version` ASC);";
                conn.Query(sql);
            }
        }

        protected static void InsertVersion(IDbConnection dbConn, int version, string description)
        {
            var sql = "INSERT INTO `VersionInfo` (Version, Description) VALUES (@Version, @Description)";
            var versionInfo = new VersionInfo {Version = version, Description = description};
            dbConn.Query(sql, versionInfo);
        }

        protected abstract bool MigrateToVersion(IDbConnection dbConn, int version);
    }
}
