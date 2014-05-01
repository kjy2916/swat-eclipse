using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;
using System.Collections;
using System.IO;
using System.Collections.Specialized;

namespace SWAT_SQLite_Result.ArcSWAT
{
    /// <summary>
    /// Public access column definition, which may be hosted as part of a public access table.
    /// </summary>
    public interface IAccessColumn
    {
        string ColumnName { get; }
        Type Type { get; }
        object DefaultValue { get; }
        bool PrimaryKey { get; }
        bool Unique { get; }
    }

    public class AccessColumn : IAccessColumn
    {
        #region variables

        protected string _columnName;
        protected Type _type;
        protected object _defaultValue;

        #endregion

        /// <summary>
        /// Name of the data column.
        /// </summary>
        public virtual string ColumnName
        {
            get
            {
                if (string.IsNullOrEmpty(_columnName) || string.IsNullOrWhiteSpace(_columnName))
                    return string.Empty;
                else
                    return _columnName;
            }
            set { _columnName = value; }
        }

        /// <summary>
        /// Type of the data column.
        /// </summary>
        public virtual Type Type
        {
            get
            {
                if (_type == null)
                    return typeof(object);
                else
                    return _type;
            }
            set { _type = value; }
        }

        /// <summary>
        /// Default value.
        /// </summary>
        public virtual object DefaultValue
        {
            get { return _defaultValue; }
            set { _defaultValue = value; }
        }

        /// <summary>
        /// True if the column is the primary key of the table.
        /// </summary>
        public virtual bool PrimaryKey { get; set; }

        /// <summary>
        /// True if the values of the column are unique in combination with the values of another column in the table.
        /// </summary>
        public virtual bool Unique { get; set; }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(ColumnName))
                return base.ToString();
            else
                return ColumnName;
        }

        /// <summary>
        /// Return the access column as a type-set data column.
        /// </summary>
        /// <returns></returns>
        public virtual DataColumn ToDataColumn()
        {
            DataColumn dc = new DataColumn();
            dc.ColumnName = ColumnName;
            dc.Caption = ColumnName;
            dc.DefaultValue = DefaultValue;
            dc.DataType = Type;
            dc.Unique = PrimaryKey;
            return dc;
        }
    }

    /// <summary>
    /// Threaded class to retrieve data tables with delegates to provide status information.
    /// </summary>
    internal sealed class DataAccess
    {
        /// <summary>
        /// Query data from a database.
        /// </summary>
        /// <param name="format">Format of the database.</param>
        /// <param name="query">Query.</param>
        /// <param name="connection">Open database connection.</param>
        /// <returns>The retrieved table.</returns>
        public DataTable GetTable(string query, IDbConnection connection)
        {

            //Build the data table;
            DataTable dt = new DataTable();

            //Collect the table;
 
            //SQLite;
            if (connection as SQLiteConnection != null)
                using (SQLiteDataAdapter a = new SQLiteDataAdapter(query, connection as SQLiteConnection))
                using (SQLiteCommandBuilder b = new SQLiteCommandBuilder(a))
                    try
                    {
                        a.Fill(dt);
                    }
                    catch(System.Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine("Query: " + query);
                        System.Diagnostics.Debug.WriteLine(e.ToString());
                        dt = null;
                    }

            //Return the table;
            return dt;
        }
    }

    /// <summary>
    /// Delegate of the data collection method.
    /// </summary>
    /// <param name="format">Format of the database.</param>
    /// <param name="query">Query.</param>
    /// <param name="connection">Open database connection.</param>
    /// <returns>The retrieved table.</returns>
    public delegate DataTable CollectionMethod(string query, IDbConnection connection);

    public class Query
    {
        /// <summary>
        /// List of active connections.
        /// </summary>
        private static Hashtable activeConnections = new Hashtable();

        public static void CloseConnection(string path)
        {
            IDbConnection connection = null;
            path = Path.GetFullPath(path);
            if (activeConnections.ContainsKey(path))
                connection = (IDbConnection)activeConnections[path];

            if (connection != null)
                try
                {
                    if (connection.State != ConnectionState.Closed)
                        connection.Close();
                }
                catch
                {
                    connection = null;
                }

            connection = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        /// <summary>
        /// Get the connection of the associated path.
        /// </summary>
        /// <param name="path">Full path to the database file.</param>
        /// <param name="sqlCreateMissing">Create the non-existing database (SQL-format only).</param>
        /// <param name="persistentConnection">Use an existing connection. False to open a new connection.</param>
        /// <returns>A connection of type System.Data.IDbConnection or null.</returns>
        public static IDbConnection OpenConnection(string path, bool sqlCreateMissing = true, bool persistentConnection = true)
        {

            //If the connection has already been established;
            IDbConnection connection = null;
            path = Path.GetFullPath(path);
            if (persistentConnection && activeConnections.ContainsKey(path))
                connection = (IDbConnection)activeConnections[path];

            //Open a new connection;
            else
            {
                //Check if the file exists;
                if (!File.Exists(path) && !sqlCreateMissing)
                {
                }
                //ActiveStatus.UpdateInnerStatus(new FileOpStatus("The file could not be found or created in accessing a database.", Environment.StackTrace, Path.GetFullPath(path)));
                else
                {
                    //Check if the directory exists;
                    if (!Directory.Exists(Path.GetDirectoryName(path)))
                        try
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(path));
                        }
                        catch
                        {
                            //ActiveStatus.UpdateInnerStatus(new FileOpStatus("The directory could not be found or created in accessing a database.", Environment.StackTrace, Path.GetDirectoryName(path)));
                        }


                    //Build the connection string;
                    SQLiteConnectionStringBuilder s = new SQLiteConnectionStringBuilder();
                    s.DataSource = path;

                    //Allow database as missing;
                    if (sqlCreateMissing)
                    {
                        s.Version = 3;
                        s.FailIfMissing = false;
                    }

                    //Open the connection;
                    connection = new SQLiteConnection(s.ConnectionString);
                }

                //Update status if the connection fails;
                if (connection == null)
                {
                }
                    //ActiveStatus.UpdateInnerStatus(new FileOpStatus("The database cannot be read. The format may not be supported.", Environment.StackTrace, Path.GetFileName(path)));

                //Otherwise register the persistent connection;
                else if (persistentConnection)
                    activeConnections[path] = connection;
            }

            //Open and append the connection;
            if (connection != null)
                try
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();
                }
                catch
                {
                    connection = null;
                }

            //Return null if connection will not open;
            return connection;
        }

        /// <summary>
        /// Retrieve the data table to the associated query.
        /// </summary>
        /// <param name="query">SQL command (query).</param>
        /// <param name="path">Full path to the database file.</param>
        /// <returns>A dataset of type System.Data.DataTable.</returns>
        public static DataTable GetDataTable(string query, string path, string tableName = null)
        {
            //debug output the query string
            System.Diagnostics.Debug.WriteLine(query);

            //Build the data table;
            DataTable fetchedTable = null;
 
            //Configure the data access service;
            DataAccess da = new DataAccess();
            CollectionMethod method = new CollectionMethod(da.GetTable);

            //Execute the query;
            IAsyncResult result = method.BeginInvoke(query, Query.OpenConnection(path), null, null);

            //Fetch the result of the query from the data service;
            fetchedTable = method.EndInvoke(result) as DataTable;

            //Return the table;
            if (fetchedTable != null)
                return fetchedTable;
            else
                return new DataTable();
        }

        public static StringCollection GetDataColumns(string path, string table)
        {
            DataTable dt = GetDataTable(string.Format("PRAGMA table_info({0})", table),path);
            StringCollection cols = new StringCollection();
            foreach (DataRow r in dt.Rows)
            {
                RowItem item = new RowItem(r);
                if (item.getColumnValue_String("type").ToLower().Equals("float"))
                    cols.Add(item.getColumnValue_String("name"));
            }
            return cols;
        }
    }

    /// <summary>
    /// Transactions for an SQLite database.
    /// </summary>
    public static class SQLite
    {
        /// <summary>
        /// Insert a table into the project database .
        /// </summary>
        /// <param name="dataTable">Insert this table to the project database.</param>
        /// <param name="pathDB">Path to the database.</param>
        /// <param name="requiredCols">Collection of required columns.</param>
        public static bool CreateTable(DataTable dataTable, string pathDB, params IAccessColumn[] requiredCols)
        {

            //Validate the table;
            if (dataTable == null || string.IsNullOrEmpty(dataTable.TableName))
            {
                //ActiveStatus.UpdateStatus(new DataStatus("The table is not valid and cannot be parsed. The table may not be named.", Environment.StackTrace, "Unknown Table"));
                return false;
            }

            //List of column names and indices;
            StringCollection colName = new StringCollection();
            StringCollection primaryKey = new StringCollection();
            StringCollection unique = new StringCollection();

            //Check if the required columns exists;
            if (requiredCols != null)
                foreach (IAccessColumn col in requiredCols)
                {

                    //Check if the column exists;
                    if (!dataTable.Columns.Contains(col.ColumnName))
                    {
                        //ActiveStatus.UpdateStatus(new DataStatus("The table is missing a required column and cannot be parsed. The names of the columns are case-sensitive.", Environment.StackTrace, dataTable.TableName));
                        return false;
                    }

                    //Store the name of the column;
                    colName.Add(col.ColumnName);

                    //Store the indices;
                    if (col.PrimaryKey)
                        primaryKey.Add(col.ColumnName);
                    if (col.Unique)
                        unique.Add(col.ColumnName);
                }

            //Remove the existing table;
            string sql = string.Format("drop table if exists {0};", dataTable.TableName);

            //Create the new table declaration;
            sql += string.Format("create table {0}(", dataTable.TableName);

            //Declare the columns;
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {

                //Give priority to pre-defined columns;
                if (colName.Contains(dataTable.Columns[i].ColumnName))
                    sql += BuildColumnDefinition(requiredCols[colName.IndexOf(dataTable.Columns[i].ColumnName)]);
                else
                    sql += BuildColumnDefinition(dataTable.Columns[i]);

                //Append a comma or close the bracket;
                if (i < dataTable.Columns.Count - 1)
                    sql += ",";
                else
                {

                    //Add constraints;
                    if (primaryKey.Count > 0)
                    {
                        string[] sz = new string[primaryKey.Count];
                        primaryKey.CopyTo(sz, 0);
                        sql += string.Format(",primary key ({0}) on conflict ignore", string.Join(",", sz).ToString());
                    }
                    if (unique.Count > 0)
                    {
                        string[] sz = new string[unique.Count];
                        unique.CopyTo(sz, 0);
                        sql += string.Format(",unique ({0}) on conflict ignore", string.Join(",", sz));
                    }

                    //Close command;
                    sql += ");";
                }
            }

            //Establish the connection;
            SQLiteConnection conn = Query.OpenConnection(pathDB) as SQLiteConnection;

            //Begin a transaction;
            if (conn != null)
                using (SQLiteTransaction t = conn.BeginTransaction())
                {

                    //Create the table;
                    SQLiteCommand cmd = new SQLiteCommand(sql, conn, t);
                    try { cmd.ExecuteNonQuery(); }
                    catch { return false; }

                    //Import the data;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        cmd.CommandText = string.Format("insert into {0} values(", dataTable.TableName);
                        for (int i = 0; i < dataTable.Columns.Count; i++)
                        {

                            //Append the value;
                            Type dataType = dataTable.Columns[i].DataType;
                            if (colName.Contains(dataTable.Columns[i].ColumnName))
                                dataType = requiredCols[colName.IndexOf(dataTable.Columns[i].ColumnName)].Type;
                            if (dataType == typeof(float) || dataType == typeof(double) || dataType == typeof(int))
                                cmd.CommandText += dr[i].ToString();
                            else
                                cmd.CommandText += string.Format("'{0}'", dr[i].ToString());

                            //Append comma or else bracket at the end of the line;
                            if (i < dataTable.Columns.Count - 1)
                                cmd.CommandText += ",";
                            else
                                cmd.CommandText += ");";
                        }
                        try { cmd.ExecuteNonQuery(); }
                        catch { return false; }
                    }

                    //Close the transation;
                    t.Commit();

                    //Return;
                    //ActiveStatus.UpdateStatus(new DataStatus(string.Format("The table {0} was successfully parsed to: {1}", dataTable.TableName, pathDB), Environment.StackTrace, dataTable.TableName));
                    return true;
                }

            //Return false if the transaction was not initialized or else did not complete;
            return false;
        }

        /// <summary>
        /// Create an SQLite table declaration string.
        /// </summary>
        /// <param name="tableName">Name of the table to create.</param>
        /// <param name="cols">Collection of columns that are required.</param>
        /// <returns>The table declaration string.</returns>
        public static string CreateTableDeclaration(string tableName, params IAccessColumn[] cols)
        {

            //Collect unique columns or primary keys;
            StringCollection primaryKey = new StringCollection();
            StringCollection unique = new StringCollection();
            foreach (IAccessColumn col in cols)
            {
                if (col.PrimaryKey)
                    primaryKey.Add(col.ColumnName);
                if (col.Unique)
                    unique.Add(col.ColumnName);
            }

            //Remove the existing table;
            string sql = ""; //string.Format("drop table if exists {0};", tableName);

            //Create the new table declaration;
            sql += string.Format("create table if not exists {0}(", tableName);

            //Declare the columns;
            for (int i = 0; i < cols.Length; i++)
            {

                //Append the column definition;
                sql += BuildColumnDefinition(cols[i]);

                //Append a comma or close the bracket;
                if (i < cols.Length - 1)
                    sql += ",";
                else
                {

                    //Add constraints;
                    if (primaryKey.Count > 0)
                    {
                        string[] sz = new string[primaryKey.Count];
                        primaryKey.CopyTo(sz, 0);
                        sql += string.Format(",primary key ({0}) on conflict ignore", string.Join(",", sz).ToString());
                    }
                    if (unique.Count > 0)
                    {
                        string[] sz = new string[unique.Count];
                        unique.CopyTo(sz, 0);
                        sql += string.Format(",unique ({0}) on conflict ignore", string.Join(",", sz));
                    }

                    //Close command;
                    sql += ");";
                }
            }

            //Return the declaration;
            return sql;
        }

        /// <summary>
        /// Build the SQLite declaration statement for a table column.
        /// </summary>
        /// <param name="col">AccessColumn or DataColumn</param>
        /// <returns>Declaration of the column.</returns>
        public static string BuildColumnDefinition(object col)
        {
            if (col is DataColumn)
                return buildColumnDeclarationDataColumn((DataColumn)col);
            else if (col is IAccessColumn)
                return buildColumnDeclarationAccessColumn((IAccessColumn)col);
            else
                return string.Empty;
        }

        /// <summary>
        /// Build the SQLite declaration statement for a table column.
        /// </summary>
        /// <param name="dc">Data column.</param>
        /// <returns>Declaration of the column.</returns>
        private static string buildColumnDeclarationDataColumn(DataColumn dc)
        {

            //Column name;
            string sql = string.Format("{0} ", dc.ColumnName);

            //Determine the appropriate data type;
            Type dataType = dc.DataType;

            //Save the type;
            if (dataType == typeof(float) || dataType == typeof(double))
                sql += "real";
            else if (dataType == typeof(int))
                sql += "int";
            else
                sql += "text";

            //Default value;
            object defaultValue = dc.DefaultValue;
            if (defaultValue != null)
            {
                sql += " default ";
                string sz = string.Empty;
                if (dataType != typeof(float) || dataType != typeof(double) || dataType != typeof(int))
                    sz = "'";
                sql += string.Format("{0}{1}{0}", sz, defaultValue);
            }

            //Return the declaration;
            return sql;
        }

        /// <summary>
        /// Build the SQLite declaration statement for a table column.
        /// </summary>
        /// <param name="dc">Data column.</param>
        /// <returns>Declaration of the column.</returns>
        private static string buildColumnDeclarationAccessColumn(IAccessColumn col)
        {

            //Column name;
            string sql = string.Format("{0} ", col.ColumnName);

            //Determine the appropriate data type;
            Type dataType = col.Type;

            //Save the type;
            if (dataType == typeof(float) || dataType == typeof(double))
                sql += "real";
            else if (dataType == typeof(int))
                sql += "int";
            else
                sql += "text";

            //Default value;
            object defaultValue = col.DefaultValue;
            if (defaultValue != null)
            {
                sql += " default ";
                string sz = string.Empty;
                if (dataType != typeof(float) || dataType != typeof(double) || dataType != typeof(int))
                    sz = "'";
                sql += string.Format("{0}{1}{0}", sz, defaultValue);
            }

            //Return the declaration;
            return sql;
        }

        /// <summary>
        /// Create the table if it doesn't exist.
        /// </summary>
        /// <param name="pathDB">Path to the database.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="cols">Required columns.</param>
        public static bool PrepareTable(string pathDB, string tableName, params IAccessColumn[] cols)
        {

            //Initialize the database connection;
            SQLiteConnection conn = Query.OpenConnection(pathDB) as SQLiteConnection;
            if (conn != null)
            {
                using (SQLiteCommand cmd = new SQLiteCommand(conn))
                    try
                    {
                        //try to create the table
                        cmd.CommandText = CreateTableDeclaration(tableName, cols);
                        cmd.ExecuteNonQuery();

                        //empty all the data
                        cmd.CommandText = "delete from " + tableName;
                        cmd.ExecuteNonQuery();

                        return true;
                    }
                    catch { return false; }
            }

            //Return false if the command did not execute successfully;
            return false;
        }

        /// <summary>
        /// Determing the SQLite type-set string of a type.
        /// </summary>
        /// <param name="type">Type of interest</param>
        /// <returns>The SQLite type-set string.</returns>
        public static string GetType(Type type)
        {
            if (type == typeof(double) || type == typeof(float))
                return "real";
            else if (type == typeof(int))
                return "int";
            else
                return "text";
        }
    }
}
