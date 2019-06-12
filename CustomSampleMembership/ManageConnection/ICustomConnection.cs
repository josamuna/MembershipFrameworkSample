using System;
using System.Data;

namespace ManageConnection
{
    internal interface ICustomConnection
    {
        IDbConnection Initialise(CustomConnection customConnection, CustomConnectionType customConnectionType);
        IDbConnection Initialise(string connectionString, CustomConnectionType customConnectionType);
        void CloseConnection();
    }
}
