using ManageConnection;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Web.Security;

namespace CustomProvider
{
    public class CustomRoleProvider : RoleProvider
    {
        private string _applicationName = "JosamunaCustomProviderSample";// Default Project name
        private string _connectionString;

        public CustomRoleProvider()
        {
        }

        public override string Description
        {
            get
            {
                string descriptionProvider = "CustomRoleProvider as custom provider of RoleProvider";
                return descriptionProvider;
            }
        }

        public override string Name
        {
            get
            {
                return "CustomRoleProvider";
            }
        }

        public override string ApplicationName
        {
            get
            {
                return _applicationName;
            }

            set
            {
                _applicationName = value;
            }
        }

        /// <summary>
        /// Indicate if the specified user is in the specified role name.
        /// </summary>
        /// <param name="username">Username to match</param>
        /// <param name="roleName">Rolename to match</param>
        /// <returns>Boolean status indicate if user is in role. True or False</returns>
        public override bool IsUserInRole(string username, string roleName)
        {
            bool status = false;

            try
            {
                if (ImplementCustomConnection.Instance.Conn.State == ConnectionState.Closed)
                    ImplementCustomConnection.Instance.Conn.Open();

                using (IDbCommand cmd = ImplementCustomConnection.Instance.Conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT a.ApplicationId,u.UserName,u.UserId,r.RoleName,r.RoleId,r.Description FROM aspnet_Roles r
                    INNER JOIN aspnet_Applications a ON a.ApplicationId=r.ApplicationId 
                    INNER JOIN aspnet_Users u ON a.ApplicationId=u.ApplicationId 
                    INNER JOIN aspnet_UsersInRoles ur ON u.UserId=ur.UserId 
                    WHERE u.LoweredUserName=@LoweredUserName AND r.LoweredRoleName=@LoweredRoleName AND a.ApplicationId=(SELECT a.ApplicationId FROM aspnet_Applications a WHERE a.LoweredApplicationName=LOWER(@ApplicationName))";

                    cmd.Parameters.Add(CustomParameters.Add(cmd, "@LoweredUserName", 256, DbType.String, username.ToLower()));
                    cmd.Parameters.Add(CustomParameters.Add(cmd, "@LoweredRoleName", 256, DbType.String, roleName.ToLower()));
                    cmd.Parameters.Add(CustomParameters.Add(cmd, "@ApplicationName", 256, DbType.String, _applicationName));

                    IDataReader rd = cmd.ExecuteReader();

                    if (rd.Read())
                        status = true;

                    rd.Dispose();
                }
            }
            finally
            {
                ImplementCustomConnection.Instance.CloseConnection();
            }

            return status;
        }
        
        /// <summary>
        /// Get All roles for a specific username passed as param.
        /// </summary>
        /// <param name="username">Useranme to match</param>
        /// <returns>String table that content all roles for user</returns>
        public override string[] GetRolesForUser(string username)
        {
            string[] valuesRole = null;

            try
            {
                if (ImplementCustomConnection.Instance.Conn.State == ConnectionState.Closed)
                    ImplementCustomConnection.Instance.Conn.Open();

                using (IDbCommand cmd = ImplementCustomConnection.Instance.Conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT a.ApplicationId,u.UserName,u.UserId,r.RoleName,r.RoleId,r.Description FROM aspnet_Roles r
                    INNER JOIN aspnet_Applications a ON a.ApplicationId=r.ApplicationId 
                    INNER JOIN aspnet_Users u ON a.ApplicationId=u.ApplicationId 
                    INNER JOIN aspnet_UsersInRoles ur ON u.UserId=ur.UserId 
                    WHERE u.LoweredUserName=@LoweredUserName AND a.ApplicationId=(SELECT a.ApplicationId FROM aspnet_Applications a WHERE a.LoweredApplicationName=LOWER(@ApplicationName))";

                    cmd.Parameters.Add(CustomParameters.Add(cmd, "@LoweredUserName", 256, DbType.String, username.ToLower()));
                    cmd.Parameters.Add(CustomParameters.Add(cmd, "@ApplicationName", 256, DbType.String, _applicationName));

                    IDataReader rd = cmd.ExecuteReader();

                    int tableIndex = 0;

                    while (rd.Read())
                    {
                        valuesRole[tableIndex] = rd["RoleName"].ToString();
                        tableIndex++;
                    }

                    rd.Dispose();
                }
            }
            finally
            {
                ImplementCustomConnection.Instance.CloseConnection();
            }

            return valuesRole;
        }

        /// <summary>
        /// Add new Role in Database.
        /// </summary>
        /// <param name="roleName">Role name</param>
        public override void CreateRole(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
                throw new ArgumentException("Role name cannot be empty.");

            try
            {
                if (ImplementCustomConnection.Instance.Conn.State == ConnectionState.Closed)
                    ImplementCustomConnection.Instance.Conn.Open();

                using (IDbCommand cmd = ImplementCustomConnection.Instance.Conn.CreateCommand())
                {
                    Guid RoleIdent = Guid.NewGuid();

                    cmd.CommandText = @"INSERT INTO aspnet_Roles(ApplicationId,RoleId,RoleName,LoweredRoleName,Description)
                    VALUES((SELECT a.ApplicationId FROM aspnet_Applications a WHERE a.LoweredApplicationName=LOWER(@ApplicationName)),@RoleId,@RoleName,LOWER(@RoleName),null)";

                    cmd.Parameters.Add(CustomParameters.Add(cmd, "@RoleId", 36, DbType.Guid, RoleIdent));
                    cmd.Parameters.Add(CustomParameters.Add(cmd, "@RoleName", 256, DbType.String, roleName));
                    cmd.Parameters.Add(CustomParameters.Add(cmd, "@ApplicationName", 256, DbType.String, _applicationName));

                    int record = cmd.ExecuteNonQuery();

                    if (record == 0)
                        throw new ArgumentException("Failed to create role, please try again.");
                }
            }
            finally
            {
                ImplementCustomConnection.Instance.CloseConnection();
            }
        }

        // Return Role ID when his name has been passed as param.
        private object getRoleId(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
                return null;
            else
            {
                try
                {
                    if (ImplementCustomConnection.Instance.Conn.State == ConnectionState.Closed)
                        ImplementCustomConnection.Instance.Conn.Open();

                    using (IDbCommand cmd = ImplementCustomConnection.Instance.Conn.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT a.ApplicationId,r.RoleName,r.RoleId,r.Description FROM aspnet_Roles r
                        INNER JOIN aspnet_Applications a ON a.ApplicationId=r.ApplicationId  
                        WHERE r.LoweredRoleName=@LoweredRoleName AND a.ApplicationId=(SELECT a.ApplicationId FROM aspnet_Applications a WHERE a.LoweredApplicationName=LOWER(@ApplicationName))";

                        cmd.Parameters.Add(CustomParameters.Add(cmd, "@LoweredRoleName", 256, DbType.String, roleName.ToLower()));
                        cmd.Parameters.Add(CustomParameters.Add(cmd, "@ApplicationName", 256, DbType.String, _applicationName));

                        IDataReader rd = cmd.ExecuteReader();

                        try
                        {
                            if (rd.Read())
                            {
                                object valueApp = rd["RoleId"];
                                return valueApp;
                            }
                            else
                                return null;
                        }
                        finally
                        {
                            rd.Dispose();
                        }
                    }
                }
                finally
                {
                    ImplementCustomConnection.Instance.CloseConnection();
                }
            }
        }

        /// <summary>
        /// Delete Role when his name has been passed as param.
        /// </summary>
        /// <param name="roleName">RoleName to delete</param>
        /// <param name="throwOnPopulatedRole">State True that allow to return a exception when role has depending members</param>
        /// <returns>Status True or False for deletion</returns>
        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            bool status = false;
            Guid? RoleIdet = null;

            // Validate Rolename.
            if (string.IsNullOrEmpty(roleName))
                throw new ArgumentException("Role name cannot be empty.");

            if(throwOnPopulatedRole)
            {
                IDbTransaction transaction = null;

                try
                {

                    // Get Role ID
                    try
                    {
                        RoleIdet = Guid.Parse(getRoleId(roleName).ToString()); 
                    }
                    catch { }

                    if (ImplementCustomConnection.Instance.Conn.State == ConnectionState.Closed)
                        ImplementCustomConnection.Instance.Conn.Open();

                    transaction = ImplementCustomConnection.Instance.Conn.BeginTransaction(IsolationLevel.Serializable);

                    // Find Roles first
                    using (IDbCommand cmd1 = ImplementCustomConnection.Instance.Conn.CreateCommand())
                    {
                        cmd1.CommandText = @"SELECT a.ApplicationId,u.UserName,u.UserId,r.RoleName,r.RoleId,r.Description FROM aspnet_Roles r
                        INNER JOIN aspnet_Applications a ON a.ApplicationId=r.ApplicationId 
                        INNER JOIN aspnet_Users u ON a.ApplicationId=u.ApplicationId 
                        INNER JOIN aspnet_UsersInRoles ur ON u.UserId=ur.UserId 
                        WHERE r.LoweredRoleName=@LoweredRoleName AND a.ApplicationId=(SELECT a.ApplicationId FROM aspnet_Applications a WHERE a.LoweredApplicationName=LOWER(@ApplicationName))";

                        cmd1.Parameters.Add(CustomParameters.Add(cmd1, "@LoweredRoleName", 256, DbType.String, roleName.ToLower()));
                        cmd1.Parameters.Add(CustomParameters.Add(cmd1, "@ApplicationName", 256, DbType.String, _applicationName));

                        cmd1.Transaction = transaction;
                        IDataReader rd = cmd1.ExecuteReader();

                        try
                        {
                            if (rd.Read())
                                throw new ArgumentException("Failed to delete the specified role because has contains one or more members.");
                        }
                        finally
                        {
                            rd.Dispose();
                        }
                    }

                    // Delete role
                    using (IDbCommand cmd2 = ImplementCustomConnection.Instance.Conn.CreateCommand())
                    {
                        cmd2.CommandText = @"DELETE FROM aspnet_Roles WHERE RoleId=@RoleId";

                        cmd2.Parameters.Add(CustomParameters.Add(cmd2, "@RoleId", 36, DbType.Guid, RoleIdet));

                        cmd2.Transaction = transaction;
                        int record = cmd2.ExecuteNonQuery();

                        if (record == 0)
                            throw new ArgumentException("Failed to delete role into Database, please try again.");
                        else
                            status = true;

                        transaction.Commit();
                        transaction.Dispose();
                    }
                }
                catch(Exception)
                {
                    if(transaction != null)
                    {
                        transaction.Rollback();
                        transaction.Dispose();
                    }
                    throw;
                }
                finally
                {
                    ImplementCustomConnection.Instance.CloseConnection();
                }
            }
            else
            {
                // Delete Role witout chech its depending
                // Delete role
                using (IDbCommand cmd = ImplementCustomConnection.Instance.Conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM aspnet_Roles WHERE RoleId=@RoleId";

                    cmd.Parameters.Add(CustomParameters.Add(cmd, "@RoleId", 36, DbType.Guid, RoleIdet));

                    int record = cmd.ExecuteNonQuery();

                    if (record == 0)
                        throw new ArgumentException("Failed to delete role into Database, please try again.");
                    else
                        status = true;
                }
            }

            return status;
        }

        /// <summary>
        /// Return True if role existm otherwise return False.
        /// </summary>
        /// <param name="roleName">Role name to match</param>
        /// <returns>Status True if found, otherwise False</returns>
        public override bool RoleExists(string roleName)
        {
            bool status = false;

            if (string.IsNullOrEmpty(roleName))
                throw new ArgumentException("Role name cannot be empty.");

            try
            {
                if (ImplementCustomConnection.Instance.Conn.State == ConnectionState.Closed)
                    ImplementCustomConnection.Instance.Conn.Open();

                using (IDbCommand cmd = ImplementCustomConnection.Instance.Conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT a.ApplicationId,r.RoleName,r.RoleId,r.Description FROM aspnet_Roles r
                    INNER JOIN aspnet_Applications a ON a.ApplicationId=r.ApplicationId  
                    WHERE r.LoweredRoleName=@LoweredRoleName AND a.ApplicationId=(SELECT a.ApplicationId FROM aspnet_Applications a WHERE a.LoweredApplicationName=LOWER(@ApplicationName))";

                    cmd.Parameters.Add(CustomParameters.Add(cmd, "@LoweredRoleName", 256, DbType.String, roleName.ToLower()));
                    cmd.Parameters.Add(CustomParameters.Add(cmd, "@ApplicationName", 256, DbType.String, _applicationName));

                    IDataReader rd = cmd.ExecuteReader();

                    if (rd.Read())
                        status = true;

                    rd.Dispose();
                }
            }
            finally
            {
                ImplementCustomConnection.Instance.CloseConnection();
            }

            return status;
        }

        /// <summary>
        /// Add many user in many role for a single application.
        /// </summary>
        /// <param name="usernames">Usernames table</param>
        /// <param name="roleNames">Rolenames table</param>
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            IDbTransaction transaction = null;
            
            if (usernames.Length == 0)
                throw new ArgumentException("Username cannot be empty.");
            if (roleNames.Length == 0)
                throw new ArgumentException("Role name cannot be empty.");

            try
            {
                if (ImplementCustomConnection.Instance.Conn.State == ConnectionState.Closed)
                    ImplementCustomConnection.Instance.Conn.Open();

                transaction = ImplementCustomConnection.Instance.Conn.BeginTransaction(IsolationLevel.Serializable);

                foreach (string user in usernames)
                {
                    foreach(string role in roleNames)
                    {
                        using (IDbCommand cmd = ImplementCustomConnection.Instance.Conn.CreateCommand())
                        {
                            cmd.CommandText = @"INSERT INTO aspnet_UsersInRoles(UserId,RoleId)
                            VALUES((SELECT u.UserId FROM aspnet_Users u INNER JOIN aspnet_Applications a ON a.ApplicationId=u.ApplicationId WHERE u.LoweredUserName=@LoweredUserName AND a.LoweredApplicationName=LOWER(@ApplicationName)),
                            ((SELECT r.RoleId FROM aspnet_Roles r INNER JOIN aspnet_Applications a ON a.ApplicationId=r.ApplicationId WHERE r.LoweredRoleName=@LoweredRoleName AND a.LoweredApplicationName=LOWER(@ApplicationName))))";
                            
                            cmd.Parameters.Add(CustomParameters.Add(cmd, "@LoweredRoleName", 256, DbType.String, role.ToLower()));
                            cmd.Parameters.Add(CustomParameters.Add(cmd, "@LoweredUserName", 256, DbType.String, user.ToLower()));
                            cmd.Parameters.Add(CustomParameters.Add(cmd, "@ApplicationName", 256, DbType.String, _applicationName));
                            
                            cmd.Transaction = transaction;
                            int record = cmd.ExecuteNonQuery();

                            // Allow to recreate new command onject
                            cmd.Parameters.Clear();

                            if (record == 0)
                                throw new ArgumentException(string.Format("Failed to add user '{0}' in role '{1}', please try again.", user, role));
                        }
                    }
                }

                transaction.Commit();
                transaction.Dispose();
            }
            catch(Exception)
            {
                if(transaction != null)
                {
                    transaction.Rollback();
                    transaction.Dispose();
                }
                throw;
            }
            finally
            {
                ImplementCustomConnection.Instance.CloseConnection();
            }
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Return all users from a specific role name.
        /// </summary>
        /// <param name="roleName">Role name to match</param>
        /// <returns>Username string table</returns>
        public override string[] GetUsersInRole(string roleName)
        {
            string[] valuesUser = null;

            if (string.IsNullOrEmpty(roleName))
                throw new ArgumentException("Role name cannot be empty.");

            try
            {
                if (ImplementCustomConnection.Instance.Conn.State == ConnectionState.Closed)
                    ImplementCustomConnection.Instance.Conn.Open();

                using (IDbCommand cmd = ImplementCustomConnection.Instance.Conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT a.ApplicationId,u.UserName,u.UserId,r.RoleName,r.RoleId,r.Description FROM aspnet_Roles r
                    INNER JOIN aspnet_Applications a ON a.ApplicationId=r.ApplicationId 
                    INNER JOIN aspnet_Users u ON a.ApplicationId=u.ApplicationId 
                    INNER JOIN aspnet_UsersInRoles ur ON u.UserId=ur.UserId 
                    WHERE r.LoweredRoleName=@LoweredRoleName AND a.ApplicationId=(SELECT a.ApplicationId FROM aspnet_Applications a WHERE a.LoweredApplicationName=LOWER(@ApplicationName))";

                    cmd.Parameters.Add(CustomParameters.Add(cmd, "@LoweredRoleName", 256, DbType.String, roleName.ToLower()));
                    cmd.Parameters.Add(CustomParameters.Add(cmd, "@ApplicationName", 256, DbType.String, _applicationName));

                    IDataReader rd = cmd.ExecuteReader();

                    int tableIndex = 0;

                    while (rd.Read())
                    {
                        valuesUser[tableIndex] = rd["UserName"].ToString();
                        tableIndex++;
                    }

                    rd.Dispose();
                }
            }
            finally
            {
                ImplementCustomConnection.Instance.CloseConnection();
            }

            return valuesUser;
        }

        /// <summary>
        /// Return All roles from application.
        /// </summary>
        /// <returns>Roles table</returns>
        public override string[] GetAllRoles()
        {
            string[] roles = null;

            try
            {
                if (ImplementCustomConnection.Instance.Conn.State == ConnectionState.Closed)
                    ImplementCustomConnection.Instance.Conn.Open();

                using (IDbCommand cmd = ImplementCustomConnection.Instance.Conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT (SELECT COUNT(r.RoleName) as NumRecord FROM aspnet_Roles r
                    INNER JOIN aspnet_Applications a ON a.ApplicationId=r.ApplicationId  
                    WHERE a.ApplicationId=(SELECT a.ApplicationId FROM aspnet_Applications a WHERE a.LoweredApplicationName=LOWER(@ApplicationName))) as NumRecord,r.RoleName FROM aspnet_Roles r
                    INNER JOIN aspnet_Applications a ON a.ApplicationId=r.ApplicationId  
                    WHERE a.ApplicationId=(SELECT a.ApplicationId FROM aspnet_Applications a WHERE a.LoweredApplicationName=LOWER(@ApplicationName))";

                    cmd.Parameters.Add(CustomParameters.Add(cmd, "@ApplicationName", 256, DbType.String, _applicationName));

                    IDataReader rd = cmd.ExecuteReader();

                    int tableIndex = 0;

                    while (rd.Read())
                    {
                        if(tableIndex == 0)
                        {
                            int index = Convert.ToInt32(rd["NumRecord"].ToString());
                            roles = new string[index];
                        }

                        roles[tableIndex] = rd["RoleName"].ToString();
                        tableIndex++;
                    }

                    rd.Dispose();
                }
            }
            finally
            {
                ImplementCustomConnection.Instance.CloseConnection();
            }

            return roles;
        }

        /////// <summary>
        /////// Return All roles from application. All valus are formated likes this :
        /////// ApplicationID|RoleId|RoleName|Description.
        /////// To retreive any values, we must split each cell value
        /////// </summary>
        /////// <returns>Roles table</returns>
        ////public string[] GetAllRolesSplit()
        ////{
        ////    string[] roles = null;

        ////    try
        ////    {
        ////        if (ImplementCustomConnection.Instance.Conn.State == ConnectionState.Closed)
        ////            ImplementCustomConnection.Instance.Conn.Open();

        ////        using (IDbCommand cmd = ImplementCustomConnection.Instance.Conn.CreateCommand())
        ////        {
        ////            cmd.CommandText = @"SELECT a.ApplicationId,r.RoleName,r.RoleId,r.Description FROM aspnet_Roles r
        ////            INNER JOIN aspnet_Applications a ON a.ApplicationId=r.ApplicationId  
        ////            WHERE a.ApplicationId=(SELECT a.ApplicationId FROM aspnet_Applications a WHERE a.LoweredApplicationName=LOWER(@ApplicationName))";

        ////            cmd.Parameters.Add(CustomParameters.Add(cmd, "@ApplicationName", 256, DbType.String, _applicationName));

        ////            IDataReader rd = cmd.ExecuteReader();

        ////            int tableIndex = 0;

        ////            while (rd.Read())
        ////            {
        ////                roles[tableIndex] = string.Format("{0}|{1}|{2}|{3}", rd["ApplicationId"].ToString(), rd["RoleId"].ToString(), rd["RoleName"].ToString(), rd["Description"].ToString());
        ////                tableIndex++;
        ////            }

        ////            rd.Dispose();
        ////        }
        ////    }
        ////    finally
        ////    {
        ////        ImplementCustomConnection.Instance.CloseConnection();
        ////    }

        ////    return roles;
        ////}

        /// <summary>
        /// Return all username for a specifique role for current application after has passed role name and username as param.
        /// </summary>
        /// <param name="roleName">Role name to match</param>
        /// <param name="usernameToMatch">Username to match</param>
        /// <returns>String table of users</returns>
        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            string[] valuesUser = null;

            if (string.IsNullOrEmpty(roleName))
                throw new ArgumentException("Role name cannot be empty.");
            if (string.IsNullOrEmpty(usernameToMatch))
                throw new ArgumentException("Username name cannot be empty.");

            try
            {
                if (ImplementCustomConnection.Instance.Conn.State == ConnectionState.Closed)
                    ImplementCustomConnection.Instance.Conn.Open();

                using (IDbCommand cmd = ImplementCustomConnection.Instance.Conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT (SELECT COUNT(u.UserName) AS NumRecord FROM aspnet_Roles r INNER JOIN aspnet_UsersInRoles ur ON r.RoleId=ur.RoleId INNER JOIN aspnet_Users u ON u.UserId=ur.UserId 
                    WHERE r.LoweredRoleName LIKE @LoweredRoleName AND u.LoweredUserName LIKE @LoweredUserName) AS NumRecord,u.UserName,u.UserId,r.RoleName,r.RoleId,r.Description FROM aspnet_Roles r
                    INNER JOIN aspnet_UsersInRoles ur ON r.RoleId=ur.RoleId 
                    INNER JOIN aspnet_Users u ON u.UserId=ur.UserId 
                    WHERE r.LoweredRoleName LIKE @LoweredRoleName AND u.LoweredUserName LIKE @LoweredUserName";

                    cmd.Parameters.Add(CustomParameters.Add(cmd, "@LoweredRoleName", 256, DbType.String, roleName.ToLower()));
                    cmd.Parameters.Add(CustomParameters.Add(cmd, "@LoweredUserName", 256, DbType.String, string.Format("%{0}%", usernameToMatch.ToLower())));

                    IDataReader rd = cmd.ExecuteReader();

                    int tableIndex = 0;

                    while (rd.Read())
                    {
                        if(tableIndex == 0)
                        {
                            // Initialise users table
                            int index = Convert.ToInt32(rd["NumRecord"].ToString());
                            valuesUser = new string[index];
                        }

                        valuesUser[tableIndex] = rd["UserName"].ToString();
                        tableIndex++;
                    }

                    rd.Dispose();
                }
            }
            finally
            {
                ImplementCustomConnection.Instance.CloseConnection();
            }

            return valuesUser;
        }

        /// <summary>
        /// Return all username for a specifique role for current application.
        /// </summary>
        /// <param name="roleName">Role name to match</param>
        /// <returns>String table of users</returns>
        public string[] FindUsersInRole(string roleName)
        {
            string[] valuesUser = null;

            if (string.IsNullOrEmpty(roleName))
                throw new ArgumentException("Role name cannot be empty.");

            try
            {
                if (ImplementCustomConnection.Instance.Conn.State == ConnectionState.Closed)
                    ImplementCustomConnection.Instance.Conn.Open();

                using (IDbCommand cmd = ImplementCustomConnection.Instance.Conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT (SELECT COUNT(u.UserName) AS NumRecord FROM aspnet_Roles r INNER JOIN aspnet_UsersInRoles ur ON r.RoleId=ur.RoleId INNER JOIN aspnet_Users u ON u.UserId=ur.UserId 
                    WHERE r.LoweredRoleName LIKE @LoweredRoleName) AS NumRecord,u.UserName,u.UserId,r.RoleName,r.RoleId,r.Description FROM aspnet_Roles r
                    INNER JOIN aspnet_UsersInRoles ur ON r.RoleId=ur.RoleId 
                    INNER JOIN aspnet_Users u ON u.UserId=ur.UserId 
                    WHERE r.LoweredRoleName LIKE @LoweredRoleName";

                    cmd.Parameters.Add(CustomParameters.Add(cmd, "@LoweredRoleName", 256, DbType.String, roleName.ToLower()));

                    IDataReader rd = cmd.ExecuteReader();

                    int tableIndex = 0;

                    while (rd.Read())
                    {
                        if (tableIndex == 0)
                        {
                            // Initialise users table
                            int index = Convert.ToInt32(rd["NumRecord"].ToString());
                            valuesUser = new string[index];
                        }

                        valuesUser[tableIndex] = rd["UserName"].ToString();
                        tableIndex++;
                    }

                    rd.Dispose();
                }
            }
            finally
            {
                ImplementCustomConnection.Instance.CloseConnection();
            }

            return valuesUser;
        }

        /// <summary>
        /// Initialise Provider properties according params set in App.conf/Web.conf
        /// </summary>
        /// <param name="name">Param name</param>
        /// <param name="config">NameValueCollection object</param>
        public override void Initialize(string name, NameValueCollection config)
        {
            _applicationName = config["applicationName"];

            if (string.IsNullOrEmpty(_applicationName))
                _applicationName = "CustomMembershipProject";

            _connectionString = config["connectionStringName"];

            base.Initialize(name, config);
        }
    }
}
