using System;
using System.Data.Common;
using SQLite.Net;
using SQLite.Net.Interop;

namespace AllAuth.Lib.Db.Sqlite
{
    public abstract class DbConnection
    {
        public string DatabaseFilepath { get; protected set; }
        
        public SQLiteConnection Connect()
        {
            if (string.IsNullOrWhiteSpace(DatabaseFilepath))
                throw new Exception("A path for the database file has not been provided");

            var connectionPlatform = GetDbConnectionPlatform();

            var connection = new SQLiteConnection(connectionPlatform, DatabaseFilepath);

            return connection;
        }
        
        protected abstract ISQLitePlatform GetDbConnectionPlatform();

        public DbCommandBuilder GetCommandBuilder()
        {
            return new Mono.Data.Sqlite.SqliteCommandBuilder();
        }

        public string GetLastIdFunction()
        {
            return "last_insert_rowid()";
        }

        public string GetCurrentTimeFunction()
        {
            return "datetime()";
        }
    }
}
