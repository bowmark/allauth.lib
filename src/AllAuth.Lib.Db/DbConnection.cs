using System;
using System.Data;
using System.Data.Common;

namespace AllAuth.Lib.Db
{
    public abstract class DbConnection
    {
        public string DbConnString { get; protected set; }
        
        public IDbConnection Connect()
        {
            if (string.IsNullOrWhiteSpace(DbConnString))
                throw new Exception("A connection string to the database has not been provided");

            var conn = GetDbConnection(DbConnString);
            conn.Open();
            return conn;
        }

        protected abstract IDbConnection GetDbConnection(string dbConnString);
        public abstract DbCommandBuilder GetCommandBuilder();
        public abstract string GetLastIdFunction();
        public abstract string GetCurrentTimeFunction();
    }
}
