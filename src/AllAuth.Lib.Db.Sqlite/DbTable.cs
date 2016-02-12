using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Text.RegularExpressions;
using SQLite.Net;

namespace AllAuth.Lib.Db.Sqlite
{
    public abstract class DbTable<T> where T : DbRow
    {
        /// <summary>
        /// The database table name to direct our queries against.
        /// </summary>
        protected string Table {
            get { return !string.IsNullOrEmpty(_table) ? _table : GetType().Name; }
            set
            {
                if (Regex.IsMatch(value, "[^A-Za-z0-9]"))
                    throw new Exception("Invalid characters in table name");

                _table = value; 
            }
        }
        private string _table;

        private readonly object _dbLock = new {};

        /// <summary>
        /// The injected database class handling DB connections etc.
        /// </summary>
        public DbConnection Db { private get; set; }
        
        /// <summary>
        /// Inserts a new row for the given object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Create(T data)
        {
            var type = data.GetType();
            var properties = type.GetProperties();

            var cmdBuilder = GetCommandBuilder();
            var sqlColumns = new StringBuilder("(");
            var sqlValues = new StringBuilder("(");
            var count = 0;
            var modifiedValues = new List<object>();
            foreach (var property in properties)
            {
                if (Regex.IsMatch(property.Name, "[^A-Za-z0-9]"))
                    throw new Exception("Invalid characters in column name");

                if (property.Name == "Id")
                {
                    var val = (int) property.GetValue(data);
                    if (val == 0)
                        continue;
                }

                if (count != 0)
                {
                    sqlColumns.Append(", ");
                    sqlValues.Append(", ");
                }
                count++;

                sqlColumns.Append(cmdBuilder.QuoteIdentifier(property.Name));
                sqlValues.Append("?");

                modifiedValues.Add(GetStringValue(property, data));
            }
            sqlColumns.Append(")");
            sqlValues.Append(")");

            lock (_dbLock)
            {
                using (var conn = Db.Connect())
                {
                    try
                    {
                        var sql = "INSERT INTO " + cmdBuilder.QuoteIdentifier(Table) + " " +
                            sqlColumns + " VALUES " + sqlValues;

                        conn.Execute(sql, modifiedValues.ToArray());
                        return conn.CreateCommand("select last_insert_rowid()").ExecuteScalar<int>();
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message, e);
                    }
                }
            }
        }
        
        public TableQuery<T> Query()
        {
            lock (_dbLock)
            {
                var conn = Db.Connect();
                return conn.Table<T>();
            }
        }
        
        /// <summary>
        /// Updates a single row on it's primary key.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        public void Update(int id, T data)
        {
            var type = data.GetType();
            var properties = type.GetProperties();

            var modifiedProperties = data.GetModifiedProperties();
            if (modifiedProperties.Count == 0)
                return;

            var cmdBuilder = GetCommandBuilder();
            var sql = new StringBuilder("UPDATE " + cmdBuilder.QuoteIdentifier(Table) + " SET ");
            var count = 0;
            var modifiedValues = new List<object>();
            foreach (var property in properties)
            {
                if (Regex.IsMatch(property.Name, "[^A-Za-z0-9]"))
                    throw new Exception("Invalid characters in column name");

                if (!modifiedProperties.Contains(property.Name))
                    continue;

                if (count != 0)
                    sql.Append(", ");

                count++;
                sql.Append(cmdBuilder.QuoteIdentifier(property.Name) + " = ?");

                modifiedValues.Add(GetStringValue(property, data));
            }

            sql.Append(", " + cmdBuilder.QuoteIdentifier("ModifiedAt") + " = " + GetCurrentTimeFunction());

            sql.Append(" WHERE " + cmdBuilder.QuoteIdentifier("Id") + " = " + id);

            lock (_dbLock)
            {
                using (var conn = Db.Connect())
                {
                    conn.Execute(sql.ToString(), modifiedValues.ToArray());
                }
            }
        }

        /// <summary>
        /// Deletes a single row on it's primary key.
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            lock (_dbLock)
            {
                using (var conn = Db.Connect())
                {
                    conn.Delete<T>(id);
                }
            }
            
            // The Sqlite implmentation currently used does not support soft delete.
            // This is because we're currently using the Query (via linq) directly in the code, meaning we cannot 
            // inject the DeletedAt check (and I'm not going to implement that in every where query in the code, 
            // that would be a major mistake (imagine forgetting to add the check in just one place).

            /*(var cmdBuilder = GetCommandBuilder();

            var sql = "UPDATE " + cmdBuilder.QuoteIdentifier(Table) + 
                " SET " + cmdBuilder.QuoteIdentifier("DeletedAt") + " = " + GetCurrentTimeFunction() +
                " WHERE " + cmdBuilder.QuoteIdentifier("Id") + " = ?";

            using (var conn = Db.Connect())
            {
                conn.Execute(sql, id);
            }*/
        }

        private DbCommandBuilder GetCommandBuilder()
        {
            return Db.GetCommandBuilder();
        }
        
        private string GetCurrentTimeFunction()
        {
            return Db.GetCurrentTimeFunction();
        }

        private string GetStringValue(System.Reflection.PropertyInfo property, object data)
        {
            var propValue = property.GetValue(data);
            if (propValue == null)
                return null;
            if (propValue is bool)
                return (bool)propValue ? "1" : "0";
            if (propValue is DateTime)
                return ((DateTime)propValue).ToString("yyyy-MM-dd HH:mm:ss");
            
            return propValue.ToString();
        }
    }
}
