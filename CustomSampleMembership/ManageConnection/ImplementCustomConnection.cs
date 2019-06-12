using MySql.Data.MySqlClient;
using Npgsql;
using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace ManageConnection
{
    public class ImplementCustomConnection : ICustomConnection
    {
        private ImplementCustomConnection()
        {
        }

        private IDbConnection _conn = null;
        private static ImplementCustomConnection _instance = null;

        public IDbConnection Conn
        {
            get
            {
                return _conn;
            }

            set
            {
                _conn = value;
            }
        }

        public static ImplementCustomConnection Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ImplementCustomConnection();
                return _instance;
            }
        }

        public void CloseConnection()
        {
            if(_conn != null)
            {
                if (_conn.State == ConnectionState.Open)
                    _conn.Close();
            }
        }

        public IDbConnection Initialise(CustomConnection customConnection, CustomConnectionType customConnectionType)
        { 
            switch (customConnectionType)
            {
                case CustomConnectionType.SQLServer:
                    _conn = new SqlConnection(string.Format("Data Source={0};Initial catalog={1};User ID={2};Password={3}",
                        customConnection.Serveur, customConnection.Database, customConnection.User, customConnection.Password));
                    break;
                case CustomConnectionType.MySQL:
                    _conn = new MySqlConnection(string.Format("Server={0};Database={1};Port={2};Uid={3};Pwd={4}",
                        customConnection.Serveur, customConnection.Database, customConnection.Port = 3306, customConnection.User, customConnection.Password));
                    break;
                case CustomConnectionType.PostGrsSQL:
                    _conn = new NpgsqlConnection(string.Format("Server={0};Database={1};Port={2};Uer ID={3};Password={4}",
                        customConnection.Serveur, customConnection.Database, customConnection.Port = 5432, customConnection.User, customConnection.Password));
                    break;
                case CustomConnectionType.Oracle:
                    //Conn = new OracleConnection(string.Format("Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = {0})(PORT = {1}))(CONNECT_DATA = (SERVER=DEDICARED)(SERVICE_NAME = {2})));User ID={3};Password={4}",
                    //    customConnection.Serveur, customConnection.Port = 1201, "OracleServiceXE", customConnection.User, customConnection.Password));
                    throw new NotImplementedException("Not implemented for this Database.");
                case CustomConnectionType.Access:
                    Conn = new OleDbConnection(string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}{1}",
                        customConnection.Path, customConnection.Database));
                    break;
            }
            return _conn;
        }

        public IDbConnection Initialise(string connectionString, CustomConnectionType customConnectionType)
        {
            switch (customConnectionType)
            {
                case CustomConnectionType.SQLServer:
                    _conn = new SqlConnection(connectionString);
                    break;
                case CustomConnectionType.MySQL:
                    _conn = new MySqlConnection(connectionString);
                    break;
                case CustomConnectionType.PostGrsSQL:
                    _conn = new NpgsqlConnection(connectionString);
                    break;
                case CustomConnectionType.Oracle:
                    throw new NotImplementedException("Not implemented for this Database.");
                case CustomConnectionType.Access:
                    Conn = new OleDbConnection(connectionString);
                    break;
            }
            return _conn;
        }
    }
}