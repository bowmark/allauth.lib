using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Dapper;

namespace AllAuth.Lib.Db
{
    public abstract class DbTable<T>
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
        public virtual int Create(T data)
        {
            var type = data.GetType();
            var properties = type.GetProperties();

            var cmdBuilder = GetCommandBuilder();
            var sqlColumns = new StringBuilder("(");
            var sqlValues = new StringBuilder("(");
            var count = 0;
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
                sqlValues.Append("@" + property.Name);
            }
            sqlColumns.Append(")");
            sqlValues.Append(")");

            using (var conn = Db.Connect())
            {
                try
                {
                    var query = "INSERT INTO " + cmdBuilder.QuoteIdentifier(Table) + " " + 
                        sqlColumns + " VALUES " + sqlValues;

                    // This parametised SQL query does not expose sensitive information, so we're OK to log it.
                    Logger.Verbose(query);

                    conn.Query(query, data);
                    
                    return Convert.ToInt32(conn.ExecuteScalar("select " + GetLastIdFunction() + ";"));
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message, e);
                }
            }
        }

        /// <summary>
        /// Retrieves the row for the given primary key value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public virtual T Get(int primaryKey)
        {
            var results = Find(new {Id = primaryKey});
            try
            {
                return results.First();
            }
            catch (Exception)
            {
                throw new Exception("No record for primary key " + primaryKey);
            }
        }

        /// <summary>
        /// Queries for all records matching the where criteria.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> Find(T where)
        {
            var modifiedProperties = ((DbRow)(object)where).GetModifiedProperties();
            return modifiedProperties.Count == 0 ? Find(new {}) : Find(@where, modifiedProperties);
        }
        
        /// <summary>
        /// Queries for all records matching the where criteria.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <param name="propertiesToSearch"></param>
        /// <returns></returns>
        private IEnumerable<T> Find(object where, ICollection<string> propertiesToSearch = null)
        {
            var type = where.GetType();
            var properties = type.GetProperties();

            var cmdBuilder = GetCommandBuilder();
            var sql = new StringBuilder("SELECT * FROM " + cmdBuilder.QuoteIdentifier(Table) + " WHERE ");
            if (properties.Length > 0)
            {
                var count = 0;
                foreach (var property in properties)
                {
                    if (Regex.IsMatch(property.Name, "[^A-Za-z0-9]"))
                        throw new Exception("Invalid characters in column name");

                    if (propertiesToSearch != null && !propertiesToSearch.Contains(property.Name))
                        continue;

                    if (count != 0)
                    {
                        sql.Append(" AND");
                    }
                    count++;

                    sql.Append(" " + cmdBuilder.QuoteIdentifier(property.Name) + " = @" + property.Name);
                }
                sql.Append(" AND");
            }

            sql.Append(" " + cmdBuilder.QuoteIdentifier("DeletedAt") + " IS NULL");
            sql.Append(" ORDER BY " + cmdBuilder.QuoteIdentifier("Id") + " DESC");
            
            using (var conn = Db.Connect())
            {
                var sqlQuery = sql.ToString();

                // This parametised SQL query does not expose sensitive information, so we're OK to log it.
                //Logger.Verbose(sqlQuery);

                return conn.Query<T>(sqlQuery, where);
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

            var modifiedProperties = ((DbRow) (object) data).GetModifiedProperties();
            if (modifiedProperties.Count == 0)
                return;

            var cmdBuilder = GetCommandBuilder();
            var sql = new StringBuilder("UPDATE " + cmdBuilder.QuoteIdentifier(Table) + " SET ");
            var count = 0;
            foreach (var property in properties)
            {
                if (Regex.IsMatch(property.Name, "[^A-Za-z0-9]"))
                    throw new Exception("Invalid characters in column name");

                if (!modifiedProperties.Contains(property.Name))
                    continue;

                if (count != 0)
                    sql.Append(", ");

                count++;
                sql.Append(cmdBuilder.QuoteIdentifier(property.Name) + " = @" + property.Name);
            }

            sql.Append(", " + cmdBuilder.QuoteIdentifier("ModifiedAt") + " = " + GetCurrentTimeFunction());

            sql.Append(" WHERE " + cmdBuilder.QuoteIdentifier("Id") + " = " + id);

            using (var conn = Db.Connect())
            {
                var sqlQuery = sql.ToString();

                // This parametised SQL query does not expose sensitive information, so we're OK to log it.
                Logger.Verbose(sqlQuery);

                conn.Execute(sqlQuery, data);
            }
        }

        /// <summary>
        /// Deletes a single row on it's primary key.
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var cmdBuilder = GetCommandBuilder();

            var sql = "UPDATE " + cmdBuilder.QuoteIdentifier(Table) + 
                " SET " + cmdBuilder.QuoteIdentifier("DeletedAt") + " = " + GetCurrentTimeFunction() +
                " WHERE " + cmdBuilder.QuoteIdentifier("Id") + " = @Id";

            using (var conn = Db.Connect())
            {
                // This parametised SQL query does not expose sensitive information, so we're OK to log it.
                Logger.Verbose(sql);

                conn.Execute(sql, new {Id = id});
            }
        }

        private DbCommandBuilder GetCommandBuilder()
        {
            return Db.GetCommandBuilder();
        }

        private string GetLastIdFunction()
        {
            return Db.GetLastIdFunction();
        }

        private string GetCurrentTimeFunction()
        {
            return Db.GetCurrentTimeFunction();
        }
    }
}
