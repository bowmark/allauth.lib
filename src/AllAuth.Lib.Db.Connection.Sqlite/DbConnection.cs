using System.Data;
using System.Data.Common;

namespace AllAuth.Lib.Db.Connection.Sqlite
{
    public class DbConnection : Db.DbConnection
    {
        protected override IDbConnection GetDbConnection(string dbConnString)
        {
            return new Mono.Data.Sqlite.SqliteConnection(dbConnString);
        }

        public override DbCommandBuilder GetCommandBuilder()
        {
            return new Mono.Data.Sqlite.SqliteCommandBuilder();
        }

        public override string GetLastIdFunction()
        {
            return "last_insert_rowid()";
        }

        public override string GetCurrentTimeFunction()
        {
            return "datetime()";
        }
    }
}
