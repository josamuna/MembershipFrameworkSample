using System;
using System.Data;

namespace ManageConnection
{
    public static class CustomParameters
    {
        public static IDbDataParameter Add(IDbCommand command, string parameterName, int size, DbType type, object value)
        {
            IDbDataParameter param = command.CreateParameter();

            param.ParameterName = parameterName;
            param.Size = size;
            param.DbType = type;
            param.Value = value;

            return param;
        }
    }
}
