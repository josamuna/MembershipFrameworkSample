using CustomUtilities;
using ManageConnection;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Web.Security;

namespace CustomProvider
{
    /// <summary>
    /// My Custom Provider Class that used instead of SqlMembershipProvider or ActiveDirectoryMembershipProvider
    /// </summary>
    public class CustomMembershipProvider : MembershipProvider
    {
        private string _applicationName = "JosamunaCustomProviderSample";// Default Project name
        private string _name = "CustomMembershipProvider";
        private bool _enablePasswordReset;
        private bool _enablePasswordRetrieval;
        private int _maxInvalidPasswordAttempts;
        private int _minRequiredNonAlphanumericCharacters;
        private int _minRequiredPasswordLength;
        private int _passwordAttemptWindow;
        private MembershipPasswordFormat _passwordFormat;
        private string _passwordStrengthRegularExpression;
        private bool _requiresQuestionAndAnswer;
        private bool _requiresUniqueEmail;
        private string _connectionString;

        public CustomMembershipProvider()
        {
        }

        public override string Description
        {
            get
            {
                string descriptionProvider = "CustomMembershipProvider as custom provider of MembershipProvider";
                return descriptionProvider;
            }
        }

        public override string Name
        {
            get
            {
                return _name;
            }
        }

        public override bool EnablePasswordRetrieval
        {
            get
            {
                return _enablePasswordRetrieval;
            }
        }

        public override bool EnablePasswordReset
        {
            get
            {
                return _enablePasswordReset;
            }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get
            {
                return _requiresQuestionAndAnswer;
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

        public override int MaxInvalidPasswordAttempts
        {
            get
            {
                return _maxInvalidPasswordAttempts;
            }
        }

        public override int PasswordAttemptWindow
        {
            get
            {
                return _passwordAttemptWindow;
            }
        }

        public override bool RequiresUniqueEmail
        {
            get
            {
                return _requiresUniqueEmail;
            }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get
            {
                return _passwordFormat;
            }
        }

        public override int MinRequiredPasswordLength
        {
            get
            {
                return _minRequiredPasswordLength;
            }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get
            {
                return _minRequiredNonAlphanumericCharacters;
            }
        }

        public override string PasswordStrengthRegularExpression
        {
            get
            {
                return _passwordStrengthRegularExpression;
            }
        }

        /// <summary>
        /// Return a warning message according the MembershipCreateStatus Enumerate value throught a string
        /// </summary>
        /// <param name="status">MembershipCreateStatus object</param>
        /// <returns>Strin message encapsulate warning message</returns>
        public static string ErrorMessageFromMembershipCreateStatus(MembershipCreateStatus status)
        {
            string message = "Error when perform create user";

            switch (status)
            {
                case MembershipCreateStatus.Success:
                    message = "The user was successfully created.";
                    break;
                case MembershipCreateStatus.InvalidUserName:
                    message = "The user name was not found in the database.";
                    break;
                case MembershipCreateStatus.InvalidPassword:
                    message = "The password is not formatted correctly.";
                    break;
                case MembershipCreateStatus.InvalidQuestion:
                    message = "The password question is not formatted correctly.";
                    break;
                case MembershipCreateStatus.InvalidAnswer:
                    message = "The password answer is not formatted correctly.";
                    break;
                case MembershipCreateStatus.InvalidEmail:
                    message = "The e-mail address is not formatted correctly.";
                    break;
                case MembershipCreateStatus.DuplicateUserName:
                    message = "The user name already exists in the database for the application.";
                    break;
                case MembershipCreateStatus.DuplicateEmail:
                    message = "The e-mail address already exists in the database for the application.";
                    break;
                case MembershipCreateStatus.UserRejected:
                    message = "The user was not created, for a reason defined by the provider.";
                    break;
                case MembershipCreateStatus.InvalidProviderUserKey:
                    message = "The provider user key is of an invalid type or format.";
                    break;
                case MembershipCreateStatus.DuplicateProviderUserKey:
                    message = "The provider user key already exists in the database for the application.";
                    break;
                case MembershipCreateStatus.ProviderError:
                    message = "The provider returned an error that is not described by other default error message";
                    break;
            }
            return message;
        }

        // Set the status when creating new user
        private MembershipCreateStatus setStatutUser(MembershipCreateStatus currentStatus, out MembershipCreateStatus statusCreateUser)
        {
            statusCreateUser = currentStatus;
            return statusCreateUser;
        }

        /// <summary>
        /// Create a new user into database with his credentials
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <param name="email">Email address</param>
        /// <param name="passwordQuestion">Password Question</param>
        /// <param name="passwordAnswer">Password Answer</param>
        /// <param name="isApproved">A boolean specified if user must be approved or not</param>
        /// <param name="providerUserKey">User Id</param>
        /// <param name="status">Status of created user</param>
        /// <returns>A MembershipUser Object</returns>
        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            MembershipCreateStatus statusUser = MembershipCreateStatus.Success;
            MembershipUser validUser = null;
            Guid? applicationId = null;
            IDbTransaction transaction = null;

            try
            {
                // Validate providerUserKey (UserId in the Data Store).
                if (providerUserKey == null)
                {
                    // If the User Id is null, then generate one using Guid Structure otherwise return null.
                    providerUserKey = Guid.NewGuid();

                    if (string.IsNullOrEmpty(providerUserKey.ToString()))
                    {
                        statusUser = setStatutUser(MembershipCreateStatus.InvalidProviderUserKey, out status);
                        return null;
                    }
                }
                else
                {
                    // Retreive user infos in database from UserId.
                    MembershipUser user = getUserFromUserKey(Guid.Parse(providerUserKey.ToString()));

                    if (user != null)
                    {
                        statusUser = setStatutUser(MembershipCreateStatus.DuplicateProviderUserKey, out status);
                        return null;
                    }
                }

                // Validate Username.
                if (string.IsNullOrEmpty(username))
                    statusUser = setStatutUser(MembershipCreateStatus.UserRejected, out status);
                else if (username.Length <= 3)
                    statusUser = setStatutUser(MembershipCreateStatus.UserRejected, out status);
                else if (!char.IsLetter(username[0]))
                    statusUser = setStatutUser(MembershipCreateStatus.UserRejected, out status);
                else
                {
                    // Retreive user infos from database using Username.
                    MembershipUser user = getUserFromUserName(username);

                    if (user != null)
                    {
                        statusUser = setStatutUser(MembershipCreateStatus.DuplicateUserName, out status);
                        return null;
                    }
                }

                // Validate password.
                ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(username, password, true);

                if (args.Cancel || string.IsNullOrEmpty(password))
                {
                    statusUser = setStatutUser(MembershipCreateStatus.InvalidPassword, out status);
                    return null;
                }

                // Validate Email Address.
                if (_requiresUniqueEmail)
                {
                    if (string.IsNullOrEmpty(email))
                    {
                        statusUser = setStatutUser(MembershipCreateStatus.InvalidEmail, out status);
                        return null;
                    }
                    else if (!System.Text.RegularExpressions.Regex.IsMatch(email, "^[_a-zA-Z0-9-]+(.[a-zA-Z0-9-]+)@[a-zA-Z0-9-]+(.[a-zA-Z0-9-]+)*(.[a-zA-Z]{2,4})$"))
                    {
                        statusUser = setStatutUser(MembershipCreateStatus.InvalidEmail, out status);
                        return null;
                    }
                    else
                    {
                        // Retreive user infos from database using email address.
                        MembershipUser user = getUserFromEmail(email);

                        if (user != null)
                        {
                            statusUser = setStatutUser(MembershipCreateStatus.DuplicateEmail, out status);
                            return null;
                        }
                    }
                }

                // Validate passwordQuestion and passwordAnswer.
                if (_requiresQuestionAndAnswer)
                {
                    if (string.IsNullOrEmpty(passwordQuestion))
                    {
                        statusUser = setStatutUser(MembershipCreateStatus.InvalidQuestion, out status);
                        return null;
                    }
                    if (string.IsNullOrEmpty(passwordAnswer))
                    {
                        statusUser = setStatutUser(MembershipCreateStatus.InvalidAnswer, out status);
                        return null;
                    }
                }

                // Validate Application ID.
                object appIdentify = getApplicationId(_applicationName);

                if (appIdentify != null)
                    applicationId = Guid.Parse(appIdentify.ToString());

                // If application doesn't exist, we should first insert it in database before performing save user.
                if (applicationId == null)
                {
                    // Insert application info into Database.
                    applicationId = Guid.NewGuid();


                    if (ImplementCustomConnection.Instance.Conn.State == ConnectionState.Closed)
                        ImplementCustomConnection.Instance.Conn.Open();

                    transaction = ImplementCustomConnection.Instance.Conn.BeginTransaction(IsolationLevel.Serializable);

                    using (IDbCommand cmd1 = ImplementCustomConnection.Instance.Conn.CreateCommand())
                    {
                        cmd1.CommandText = @"INSERT INTO aspnet_Applications(ApplicationName,LoweredApplicationName,ApplicationId,Description)
                        VALUES(@ApplicationName,@LoweredApplicationName,@ApplicationId,null)";

                        cmd1.Parameters.Add(CustomParameters.Add(cmd1, "@ApplicationName", 256, DbType.String, _applicationName));
                        cmd1.Parameters.Add(CustomParameters.Add(cmd1, "@LoweredApplicationName", 256, DbType.String, _applicationName.ToLower()));
                        cmd1.Parameters.Add(CustomParameters.Add(cmd1, "@ApplicationId", 36, DbType.Guid, applicationId));

                        cmd1.Transaction = transaction;
                        cmd1.ExecuteNonQuery();
                    }
                }
                else
                {
                    if (ImplementCustomConnection.Instance.Conn.State == ConnectionState.Closed)
                        ImplementCustomConnection.Instance.Conn.Open();

                    transaction = ImplementCustomConnection.Instance.Conn.BeginTransaction(IsolationLevel.Serializable);
                }

                // Insert User into Database using transaction.
                // Generate User ID.
                Guid userID = Guid.NewGuid();

                using (IDbCommand cmd2 = ImplementCustomConnection.Instance.Conn.CreateCommand())
                {
                    cmd2.CommandText = @"INSERT INTO aspnet_Users(ApplicationId,UserId,UserName,LoweredUserName,IsAnonymous,LastActivityDate)
                    VALUES(@ApplicationId,@UserId,@UserName,@LoweredUserName,@IsUserAnonymous,@LastActivityDate)";

                    cmd2.Parameters.Add(CustomParameters.Add(cmd2, "@ApplicationId", 36, DbType.Guid, applicationId));
                    cmd2.Parameters.Add(CustomParameters.Add(cmd2, "@UserId", 36, DbType.Guid, userID));
                    cmd2.Parameters.Add(CustomParameters.Add(cmd2, "@UserName", 256, DbType.String, username));
                    cmd2.Parameters.Add(CustomParameters.Add(cmd2, "@LoweredUserName", 256, DbType.String, username.ToLower()));
                    cmd2.Parameters.Add(CustomParameters.Add(cmd2, "@IsUserAnonymous", 2, DbType.Boolean, false));
                    cmd2.Parameters.Add(CustomParameters.Add(cmd2, "@LastActivityDate", 8, DbType.DateTime2, DateTime.Now));

                    cmd2.Transaction = transaction;
                    cmd2.ExecuteNonQuery();
                }

                using (IDbCommand cmd3 = ImplementCustomConnection.Instance.Conn.CreateCommand())
                {
                    cmd3.CommandText = @"INSERT INTO dbo.aspnet_Membership(ApplicationId,UserId,Password,PasswordSalt,Email,LoweredEmail,PasswordQuestion,PasswordAnswer,
                    PasswordFormat,IsApproved,IsLockedOut,CreateDate,LastLoginDate,LastPasswordChangedDate,LastLockoutDate,FailedPasswordAttemptCount,FailedPasswordAttemptWindowStart,
                    FailedPasswordAnswerAttemptCount,FailedPasswordAnswerAttemptWindowStart)
                    VALUES(@ApplicationId,@UserId,@Password, @PasswordSalt, @Email,@LoweredEmail,@PasswordQuestion,@PasswordAnswer,@PasswordFormat,@IsApproved,@IsLockedOut,@CreateDate,@CreateDate,
                    @CreateDate,CONVERT(datetime,'17540101',112),@FailedPasswordAttemptCount,CONVERT(datetime,'17540101',112),@FailedPasswordAnswerAttemptCount,CONVERT(datetime,'17540101',112))";

                    // Generate password and PasswordSalt.
                    byte[] saltByte = PerformHashPassword.GenerateSaltByte();
                    string passwordUser = PerformHashPassword.HashPassword(password, saltByte);
                    string passwordSalt = Convert.ToBase64String(saltByte);

                    cmd3.Parameters.Add(CustomParameters.Add(cmd3, "@ApplicationId", 36, DbType.Guid, applicationId));
                    cmd3.Parameters.Add(CustomParameters.Add(cmd3, "@UserId", 36, DbType.Guid, userID));
                    cmd3.Parameters.Add(CustomParameters.Add(cmd3, "@Password", 128, DbType.String, passwordUser));
                    cmd3.Parameters.Add(CustomParameters.Add(cmd3, "@PasswordSalt", 128, DbType.String, passwordSalt));
                    cmd3.Parameters.Add(CustomParameters.Add(cmd3, "@Email", 256, DbType.String, email));
                    cmd3.Parameters.Add(CustomParameters.Add(cmd3, "@LoweredEmail", 256, DbType.String, email.ToLower()));
                    cmd3.Parameters.Add(CustomParameters.Add(cmd3, "@PasswordQuestion", 256, DbType.String, passwordQuestion));
                    cmd3.Parameters.Add(CustomParameters.Add(cmd3, "@PasswordAnswer", 128, DbType.String, passwordAnswer));
                    cmd3.Parameters.Add(CustomParameters.Add(cmd3, "@PasswordFormat", 4, DbType.Int32, _passwordFormat));
                    cmd3.Parameters.Add(CustomParameters.Add(cmd3, "@IsApproved", 2, DbType.Boolean, isApproved));
                    cmd3.Parameters.Add(CustomParameters.Add(cmd3, "@IsLockedOut", 2, DbType.Boolean, false));
                    cmd3.Parameters.Add(CustomParameters.Add(cmd3, "@CreateDate", 8, DbType.DateTime2, DateTime.Now));
                    cmd3.Parameters.Add(CustomParameters.Add(cmd3, "@LastLoginDate", 8, DbType.DateTime2, DateTime.Now));
                    cmd3.Parameters.Add(CustomParameters.Add(cmd3, "@LastPasswordChangedDate", 8, DbType.DateTime2, DateTime.Now));
                    //cmd3.Parameters.Add(CustomParameters.Add(cmd3, "@LastLockoutDate", 8, DbType.DateTime2, userID));
                    cmd3.Parameters.Add(CustomParameters.Add(cmd3, "@FailedPasswordAttemptCount", 4, DbType.Int32, 0));
                    //cmd3.Parameters.Add(CustomParameters.Add(cmd3, "@FailedPasswordAttemptWindowStart", 8, DbType.DateTime2, DateTime.Now));
                    cmd3.Parameters.Add(CustomParameters.Add(cmd3, "@FailedPasswordAnswerAttemptCount", 4, DbType.Int32, 0));
                    //cmd3.Parameters.Add(CustomParameters.Add(cmd3, "@FailedPasswordAnswerAttemptWindowStart", 8, DbType.DateTime2, DateTime.Now));

                    cmd3.Transaction = transaction;
                    cmd3.ExecuteNonQuery();

                    transaction.Commit();
                    transaction.Dispose();
                }
            }
            catch (Exception)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                    statusUser = setStatutUser(MembershipCreateStatus.UserRejected, out status);
                }

                throw;
            }
            finally
            {
                ImplementCustomConnection.Instance.CloseConnection();
            }

            // If there is no exception, we return Success Status.
            statusUser = setStatutUser(MembershipCreateStatus.Success, out status);

            return validUser;
        }

        // Return application ID when his name has been passed into methode.
        private object getApplicationId(string applicationName)
        {
            if (string.IsNullOrEmpty(applicationName))
                return null;
            else
            {
                try
                {
                    if (ImplementCustomConnection.Instance.Conn.State == ConnectionState.Closed)
                        ImplementCustomConnection.Instance.Conn.Open();

                    using (IDbCommand cmd = ImplementCustomConnection.Instance.Conn.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT a.ApplicationId FROM aspnet_Applications a WHERE a.LoweredApplicationName=LOWER(@ApplicationName)";

                        cmd.Parameters.Add(CustomParameters.Add(cmd, "@ApplicationName", 256, DbType.String, applicationName));

                        IDataReader rd = cmd.ExecuteReader();

                        try
                        {
                            if (rd.Read())
                            {
                                object valueApp = rd["ApplicationId"];
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

        // Get user info throught username.
        private MembershipUser getUserFromUserName(string username)
        {
            MembershipUser user = null;

            try
            {
                if (ImplementCustomConnection.Instance.Conn.State == ConnectionState.Closed)
                    ImplementCustomConnection.Instance.Conn.Open();

                using (IDbCommand cmd = ImplementCustomConnection.Instance.Conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT u.UserName,u.UserId,m.Email,m.PasswordQuestion,m.Comment,m.IsApproved,m.IsLockedOut,m.CreateDate,m.LastLoginDate,u.LastActivityDate,
                    m.LastPasswordChangedDate,m.LastLockoutDate FROM aspnet_Membership m
                    INNER JOIN aspnet_Users u ON u.UserId=m.UserId 
                    INNER JOIN aspnet_Applications a ON a.ApplicationId=u.ApplicationId 
                    WHERE u.LoweredUserName=@LoweredUserName AND a.ApplicationId=(SELECT a.ApplicationId FROM aspnet_Applications a WHERE a.LoweredApplicationName=LOWER(@ApplicationName))";

                    cmd.Parameters.Add(CustomParameters.Add(cmd, "@LoweredUserName", 256, DbType.String, username.ToLower()));
                    cmd.Parameters.Add(CustomParameters.Add(cmd, "@ApplicationName", 256, DbType.String, _applicationName));

                    IDataReader rd = cmd.ExecuteReader();

                    if (rd.Read())
                    {
                        // Affect values in MembershipUser object
                        user = new MembershipUser(_name, rd["UserName"].ToString(), Guid.Parse(rd["UserId"].ToString()), rd["Email"].ToString(), rd["PasswordQuestion"].ToString(),
                            rd["Comment"].ToString(), Convert.ToBoolean(rd["IsApproved"].ToString()), Convert.ToBoolean(rd["IsLockedOut"].ToString()), Convert.ToDateTime(rd["CreateDate"]),
                            Convert.ToDateTime(rd["LastLoginDate"]), Convert.ToDateTime(rd["LastActivityDate"]), Convert.ToDateTime(rd["LastPasswordChangedDate"]), Convert.ToDateTime(rd["LastLockoutDate"]));
                    }

                    rd.Dispose();
                }

                return user;
            }
            finally
            {
                ImplementCustomConnection.Instance.CloseConnection();
            }
        }

        // Get the password answer for a particular user.
        private string getUserPasswordAnswer(string username)
        {
            string answer = null;

            try
            {
                if (ImplementCustomConnection.Instance.Conn.State == ConnectionState.Closed)
                    ImplementCustomConnection.Instance.Conn.Open();

                using (IDbCommand cmd = ImplementCustomConnection.Instance.Conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT m.PasswordAnswer FROM aspnet_Membership m
                    INNER JOIN aspnet_Users u ON u.UserId=m.UserId
                    INNER JOIN aspnet_Applications a ON a.ApplicationId=u.ApplicationId
                    WHERE u.LoweredUserName=@LoweredUserName AND a.ApplicationId=(SELECT a.ApplicationId FROM aspnet_Applications a WHERE a.LoweredApplicationName=LOWER(@ApplicationName))";

                    cmd.Parameters.Add(CustomParameters.Add(cmd, "@LoweredUserName", 256, DbType.String, username.ToLower()));
                    cmd.Parameters.Add(CustomParameters.Add(cmd, "@ApplicationName", 256, DbType.String, _applicationName));

                    IDataReader rd = cmd.ExecuteReader();

                    if (rd.Read())
                    {
                        answer = rd["PasswordAnswer"].ToString();
                        return answer.ToLower();
                    }

                    rd.Dispose();
                }

                return answer;
            }
            finally
            {
                ImplementCustomConnection.Instance.CloseConnection();
            }
        }

        // Get the password for a particular username in a string table.
        // Position 0 => Hashed Password
        // Position 1 => Password Salt
        private string[] getUserPassword(string username)
        {
            string[] userPassword = null;

            try
            {
                if (ImplementCustomConnection.Instance.Conn.State == ConnectionState.Closed)
                    ImplementCustomConnection.Instance.Conn.Open();

                using (IDbCommand cmd = ImplementCustomConnection.Instance.Conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT m.Password,m.PasswordSalt FROM aspnet_Membership m
                    INNER JOIN aspnet_Users u ON u.UserId=m.UserId
                    INNER JOIN aspnet_Applications a ON a.ApplicationId=u.ApplicationId
                    WHERE u.LoweredUserName=@LoweredUserName AND a.ApplicationId=(SELECT a.ApplicationId FROM aspnet_Applications a WHERE a.LoweredApplicationName=LOWER(@ApplicationName))";

                    cmd.Parameters.Add(CustomParameters.Add(cmd, "@LoweredUserName", 256, DbType.String, username.ToLower()));
                    cmd.Parameters.Add(CustomParameters.Add(cmd, "@ApplicationName", 256, DbType.String, _applicationName));

                    IDataReader rd = cmd.ExecuteReader();
                    userPassword = new string[2];

                    if (rd.Read())
                    {
                        userPassword[0] = rd["Password"].ToString();
                        userPassword[1] = rd["PasswordSalt"].ToString();
                        return userPassword;
                    }

                    rd.Dispose();
                }

                return userPassword;
            }
            finally
            {
                ImplementCustomConnection.Instance.CloseConnection();
            }
        }

        // Get user info throught email address.
        private MembershipUser getUserFromEmail(string email)
        {
            MembershipUser user = null;

            try
            {
                if (ImplementCustomConnection.Instance.Conn.State == ConnectionState.Closed)
                    ImplementCustomConnection.Instance.Conn.Open();

                using (IDbCommand cmd = ImplementCustomConnection.Instance.Conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT u.UserName,u.UserId,m.Email,m.PasswordQuestion,m.Comment,m.IsApproved,m.IsLockedOut,m.CreateDate,m.LastLoginDate,u.LastActivityDate,
                    m.LastPasswordChangedDate,m.LastLockoutDate FROM aspnet_Membership m
                    INNER JOIN aspnet_Users u ON u.UserId=m.UserId 
                    INNER JOIN aspnet_Applications a ON a.ApplicationId=u.ApplicationId 
                    WHERE m.Email=@Email AND a.ApplicationId=(SELECT a.ApplicationId FROM aspnet_Applications a WHERE a.LoweredApplicationName=LOWER(@ApplicationName))";

                    cmd.Parameters.Add(CustomParameters.Add(cmd, "@Email", 256, DbType.String, email));
                    cmd.Parameters.Add(CustomParameters.Add(cmd, "@ApplicationName", 256, DbType.String, _applicationName));

                    IDataReader rd = cmd.ExecuteReader();

                    int countUser = 0;

                    while (rd.Read())
                    {
                        countUser++;
                        // Affect values in MembershipUser object
                        user = new MembershipUser(_name, rd["UserName"].ToString(), Guid.Parse(rd["UserId"].ToString()), rd["Email"].ToString(), rd["PasswordQuestion"].ToString(),
                            rd["Comment"].ToString(), Convert.ToBoolean(rd["IsApproved"].ToString()), Convert.ToBoolean(rd["IsLockedOut"].ToString()), Convert.ToDateTime(rd["CreateDate"]),
                            Convert.ToDateTime(rd["LastLoginDate"]), Convert.ToDateTime(rd["LastActivityDate"]), Convert.ToDateTime(rd["LastPasswordChangedDate"]), Convert.ToDateTime(rd["LastLockoutDate"]));

                        if (countUser >= 1)
                            throw new ArgumentException("The e-mail address already exists in the database for the application.");
                    }

                    rd.Dispose();
                }

                return user;
            }
            finally
            {
                ImplementCustomConnection.Instance.CloseConnection();
            }
        }

        // Get user info throught username ID
        private MembershipUser getUserFromUserKey(object userKey)
        {
            MembershipUser user = null;

            try
            {
                if (ImplementCustomConnection.Instance.Conn.State == ConnectionState.Closed)
                    ImplementCustomConnection.Instance.Conn.Open();

                using (IDbCommand cmd = ImplementCustomConnection.Instance.Conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT u.UserName,u.UserId,m.Email,m.PasswordQuestion,m.Comment,m.IsApproved,m.IsLockedOut,m.CreateDate,m.LastLoginDate,u.LastActivityDate,
                    m.LastPasswordChangedDate,m.LastLockoutDate FROM aspnet_Membership m
                    INNER JOIN aspnet_Users u ON u.UserId=m.UserId 
                    INNER JOIN aspnet_Applications a ON a.ApplicationId=u.ApplicationId 
                    WHERE u.UserId=@UserId AND a.ApplicationId=(SELECT a.ApplicationId FROM aspnet_Applications a WHERE a.LoweredApplicationName=LOWER(@ApplicationName))";

                    cmd.Parameters.Add(CustomParameters.Add(cmd, "@UserId", 100, DbType.Guid, userKey));
                    cmd.Parameters.Add(CustomParameters.Add(cmd, "@ApplicationName", 256, DbType.String, _applicationName));

                    IDataReader rd = cmd.ExecuteReader();

                    if (rd.Read())
                    {
                        // Affect values in MembershipUser object
                        user = new MembershipUser(_name, rd["UserName"].ToString(), Guid.Parse(rd["UserId"].ToString()), rd["Email"].ToString(), rd["PasswordQuestion"].ToString(),
                            rd["Comment"].ToString(), Convert.ToBoolean(rd["IsApproved"].ToString()), Convert.ToBoolean(rd["IsLockedOut"].ToString()), Convert.ToDateTime(rd["CreateDate"]),
                            Convert.ToDateTime(rd["LastLoginDate"]), Convert.ToDateTime(rd["LastActivityDate"]), Convert.ToDateTime(rd["LastPasswordChangedDate"]), Convert.ToDateTime(rd["LastLockoutDate"]));
                    }

                    rd.Dispose();
                }

                return user;
            }
            finally
            {
                ImplementCustomConnection.Instance.CloseConnection();
            }
        }

        /// <summary>
        /// Change Password Question and Password Answer after provide valid credential.
        /// </summary>
        /// <param name="username">Username to match</param>
        /// <param name="password">Password to match</param>
        /// <param name="newPasswordQuestion">New Password Question string</param>
        /// <param name="newPasswordAnswer">New Password Answer string</param>
        /// <returns>Status of changing. True or False</returns>
        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            bool status = false;

            if (string.IsNullOrEmpty(username))
                throw new ArgumentException("Username cannot be empty.");
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Please provide a valid password.");
            if (string.IsNullOrEmpty(newPasswordQuestion))
                throw new ArgumentException("Please provide a valid password question.");
            if (string.IsNullOrEmpty(newPasswordAnswer))
                throw new ArgumentException("Please provide a valid password answer.");

            // First validate user with provided credential

            if (ValidateUser(username, password))
            {
                try
                {
                    if (ImplementCustomConnection.Instance.Conn.State == ConnectionState.Closed)
                        ImplementCustomConnection.Instance.Conn.Open();

                    using (IDbCommand cmd = ImplementCustomConnection.Instance.Conn.CreateCommand())
                    {
                        cmd.CommandText = @"UPDATE aspnet_Membership SET PasswordQuestion=@PasswordQuestion,PasswordAnswer=@PasswordAnswer
                        WHERE UserId=(SELECT u.UserId FROM aspnet_Membership m
                        INNER JOIN aspnet_Users u ON u.UserId=m.UserId
                        INNER JOIN aspnet_Applications a ON a.ApplicationId=u.ApplicationId
                        WHERE u.LoweredUserName=@LoweredUserName AND a.ApplicationId=(SELECT a.ApplicationId FROM aspnet_Applications a WHERE a.LoweredApplicationName=LOWER(@ApplicationName)))";

                        cmd.Parameters.Add(CustomParameters.Add(cmd, "@PasswordQuestion", 256, DbType.String, newPasswordQuestion));
                        cmd.Parameters.Add(CustomParameters.Add(cmd, "@PasswordAnswer", 128, DbType.String, newPasswordAnswer));
                        cmd.Parameters.Add(CustomParameters.Add(cmd, "@LoweredUserName", 256, DbType.String, username.ToLower()));
                        cmd.Parameters.Add(CustomParameters.Add(cmd, "@ApplicationName", 256, DbType.String, _applicationName));

                        int record = cmd.ExecuteNonQuery();

                        if (record == 0)
                            throw new ArgumentException("Failed to save modifications, please try again.");
                        else
                            status = true;
                    }
                }
                finally
                {
                    ImplementCustomConnection.Instance.CloseConnection();
                }
            }
            else
                throw new ArgumentException("User credential are not valid.");

            return status;
        }

        /// <summary>
        /// Retreive the user hashed Password from a valid username and password answer
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="answer">Pasword answer</param>
        /// <returns>hashed password</returns>
        public override string GetPassword(string username, string answer)
        {
            string hashedPassword = null;

            // Validate Username
            if (string.IsNullOrEmpty(username))
                throw new ArgumentException("Username cannot be empty.");

            if (string.IsNullOrEmpty(answer))
                throw new ArgumentException("Please provide a valid answer to reset password");

            MembershipUser user = getUserFromUserName(username);

            if (user == null)
                throw new ArgumentException("Please provide a valid username.");
            else
            {
                // Get the correct user answer
                string goodAnswer = getUserPasswordAnswer(username);

                // If the good answer doesn't match the provided answer, we retun a exception

                if (string.Compare(goodAnswer, answer.ToLower()) == 0)
                {
                    // Perform Get user password
                    try
                    {
                        if (ImplementCustomConnection.Instance.Conn.State == ConnectionState.Closed)
                            ImplementCustomConnection.Instance.Conn.Open();

                        using (IDbCommand cmd = ImplementCustomConnection.Instance.Conn.CreateCommand())
                        {
                            cmd.CommandText = @"SELECT m.Password FROM aspnet_Membership m
                            INNER JOIN aspnet_Users u ON u.UserId=m.UserId
                            INNER JOIN aspnet_Applications a ON a.ApplicationId=u.ApplicationId
                            WHERE u.LoweredUserName=@LoweredUserName AND a.ApplicationId=(SELECT a.ApplicationId FROM aspnet_Applications a WHERE a.LoweredApplicationName=LOWER(@ApplicationName))";

                            cmd.Parameters.Add(CustomParameters.Add(cmd, "@LoweredUserName", 256, DbType.String, username.ToLower()));
                            cmd.Parameters.Add(CustomParameters.Add(cmd, "@ApplicationName", 256, DbType.String, _applicationName));

                            IDataReader rd = cmd.ExecuteReader();

                            if (rd.Read())
                                hashedPassword = rd["Password"].ToString();

                            rd.Dispose();
                        }
                    }
                    finally
                    {
                        ImplementCustomConnection.Instance.CloseConnection();
                    }
                }
                else
                    throw new ArgumentException("The password answer provided doesn't match.");
            }

            return hashedPassword;
        }

        /// <summary>
        /// Retreive the user hashed Password from a valid username
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>hashed password</returns>
        public string GetPassword(string username)
        {
            string hashedPassword = null;

            // Validate Username
            if (string.IsNullOrEmpty(username))
                throw new ArgumentException("Username cannot be empty.");

            MembershipUser user = getUserFromUserName(username);

            if (user == null)
                throw new ArgumentException("Please provide a valid username.");
            else
            {
                // Perform Get user password
                try
                {
                    if (ImplementCustomConnection.Instance.Conn.State == ConnectionState.Closed)
                        ImplementCustomConnection.Instance.Conn.Open();

                    using (IDbCommand cmd = ImplementCustomConnection.Instance.Conn.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT m.Password FROM aspnet_Membership m
                            INNER JOIN aspnet_Users u ON u.UserId=m.UserId
                            INNER JOIN aspnet_Applications a ON a.ApplicationId=u.ApplicationId
                            WHERE u.LoweredUserName=@LoweredUserName AND a.ApplicationId=(SELECT a.ApplicationId FROM aspnet_Applications a WHERE a.LoweredApplicationName=LOWER(@ApplicationName))";

                        cmd.Parameters.Add(CustomParameters.Add(cmd, "@LoweredUserName", 256, DbType.String, username.ToLower()));
                        cmd.Parameters.Add(CustomParameters.Add(cmd, "@ApplicationName", 256, DbType.String, _applicationName));

                        IDataReader rd = cmd.ExecuteReader();

                        if (rd.Read())
                            hashedPassword = rd["Password"].ToString();

                        rd.Dispose();
                    }
                }
                finally
                {
                    ImplementCustomConnection.Instance.CloseConnection();
                }
            }

            return hashedPassword;
        }

        /// <summary>
        /// Changing old password with a new one.
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="oldPassword">Old user password</param>
        /// <param name="newPassword">New user password</param>
        /// <returns>Status of modification. True or False</returns>
        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            bool status = false;

            // Validate Username
            if (string.IsNullOrEmpty(username))
                throw new ArgumentException("Username cannot be empty.");

            if (string.IsNullOrEmpty(oldPassword))
                throw new ArgumentException("Old password cannot be empty.");

            if (string.IsNullOrEmpty(newPassword))
                throw new ArgumentException("New password cannot be empty.");

            MembershipUser user = getUserFromUserName(username);

            if (user == null)
                throw new ArgumentException("Please provide a valid username.");
            else
            {
                // Get the correct user password
                string[] oldUserPassword = getUserPassword(username);

                // If the old password doesn't match the provided password, we retun a exception
                // Otherwire generate new password and update Database
                if (PerformHashPassword.ValidatePassword(oldPassword, oldUserPassword[1], oldUserPassword[0]))
                {
                    try
                    {
                        if (ImplementCustomConnection.Instance.Conn.State == ConnectionState.Closed)
                            ImplementCustomConnection.Instance.Conn.Open();

                        using (IDbCommand cmd = ImplementCustomConnection.Instance.Conn.CreateCommand())
                        {
                            // Generate new password and PasswordSalt.
                            byte[] saltByte = PerformHashPassword.GenerateSaltByte();
                            string newPasswordUser = PerformHashPassword.HashPassword(newPassword, saltByte);
                            string newPasswordSalt = Convert.ToBase64String(saltByte);

                            cmd.CommandText = @"UPDATE aspnet_Membership SET Password=@Password,PasswordSalt=@PasswordSalt,LastPasswordChangedDate=GETDATE() WHERE UserId=(SELECT m.UserId FROM aspnet_Membership m
                            INNER JOIN aspnet_Users u ON u.UserId=m.UserId
                            INNER JOIN aspnet_Applications a ON a.ApplicationId=u.ApplicationId
                            WHERE u.LoweredUserName=@LoweredUserName AND a.ApplicationId=(SELECT a.ApplicationId FROM aspnet_Applications a WHERE a.LoweredApplicationName=LOWER(@ApplicationName)))";

                            cmd.Parameters.Add(CustomParameters.Add(cmd, "@Password", 128, DbType.String, newPasswordUser));
                            cmd.Parameters.Add(CustomParameters.Add(cmd, "@PasswordSalt", 128, DbType.String, newPasswordSalt));
                            cmd.Parameters.Add(CustomParameters.Add(cmd, "@LoweredUserName", 256, DbType.String, username.ToLower()));
                            cmd.Parameters.Add(CustomParameters.Add(cmd, "@ApplicationName", 256, DbType.String, _applicationName));

                            int record = cmd.ExecuteNonQuery();

                            if (record == 0)
                                throw new ArgumentException("Failed to save modifications, please try again.");
                            else
                                status = true;
                        }
                    }
                    finally
                    {
                        ImplementCustomConnection.Instance.CloseConnection();
                    }
                }
                else
                    throw new ArgumentException("Old password doesn't match the correct password.");
            }

            return status;
        }

        /// <summary>
        /// Reset user password for a new one if the passeword answer match that stored in Database.
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="answer">Answer password to match</param>
        /// <returns>New auto-generated clear text password</returns>
        public override string ResetPassword(string username, string answer)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentException("Username cannot be empty.");
            if (string.IsNullOrEmpty(answer))
                throw new ArgumentException("Please provide a valid password answer.");

            // First validate user with provided username

            MembershipUser user = getUserFromUserName(username);

            if (user != null)
            {
                string goodAnswer = getUserPasswordAnswer(username);

                if (!string.IsNullOrEmpty(goodAnswer))
                {
                    // Chechk match answer
                    if (string.Compare(goodAnswer, answer.ToLower()) == 0)
                    {
                        // Generate a new password
                        string newPassword = TemporaryPassword.Generate(4, 6);

                        //After return password, we update it in Database
                        bool status = updateLastUserLoginDateAndPassword(username, newPassword);

                        if(status)
                            return newPassword;
                        else
                            throw new ArgumentException("Failed to reset user password.");
                    }
                    else
                        throw new ArgumentException("Answer reset not match that has been provided.");
                }
                else
                    throw new ArgumentException("Answer reset provided for this user doesn't exist.");
            }
            else
                throw new ArgumentException("Username not found, please try again.");
        }

        /// <summary>
        /// Update user infos when pass a MembershipUser object as param.
        /// </summary>
        /// <param name="user">MembershipUser object</param>
        public override void UpdateUser(MembershipUser user)
        {
            if(user == null)
                throw new ArgumentException("User cannot be empty.");

            IDbTransaction transaction = null;

            try
            {
                if (ImplementCustomConnection.Instance.Conn.State == ConnectionState.Closed)
                    ImplementCustomConnection.Instance.Conn.Open();

                transaction = ImplementCustomConnection.Instance.Conn.BeginTransaction(IsolationLevel.Serializable);

                using (IDbCommand cmd1 = ImplementCustomConnection.Instance.Conn.CreateCommand())
                {
                    if (user.ProviderUserKey == null)
                        throw new ArgumentNullException("Please provide a valid User ID.");

                    // User has provided a ProviderUserKey.
                    cmd1.CommandText = @"UPDATE aspnet_Users SET UserName=@UserName,LoweredUserName=LOWER(@UserName),LastActivityDate=@LastActivityDate,
                    ApplicationId=(SELECT a.ApplicationId FROM aspnet_Applications a WHERE a.LoweredApplicationName=LOWER(@ApplicationName)) WHERE UserId=@UserId";

                    cmd1.Parameters.Add(CustomParameters.Add(cmd1, "@UserName", 256, DbType.String, user.UserName));
                    cmd1.Parameters.Add(CustomParameters.Add(cmd1, "@LastActivityDate", 8, DbType.DateTime2, user.LastActivityDate));
                    cmd1.Parameters.Add(CustomParameters.Add(cmd1, "@ApplicationName", 256, DbType.String, _applicationName));
                    cmd1.Parameters.Add(CustomParameters.Add(cmd1, "@UserId", 256, DbType.Guid, Guid.Parse(user.ProviderUserKey.ToString())));

                    cmd1.Transaction = transaction;
                    cmd1.ExecuteNonQuery();
                }

                using (IDbCommand cmd2 = ImplementCustomConnection.Instance.Conn.CreateCommand())
                {
                    // User the UserProvided Key
                    cmd2.CommandText = @"UPDATE aspnet_Membership SET Email=@Email,LoweredEmail=LOWER(@Email),PasswordQuestion=@PasswordQuestion,IsApproved=@IsApproved,
                        Comment=@Comment,IsLockedOut=@IsLockedOut,CreateDate=@CreateDate,LastLoginDate=@LastLoginDate,LastPasswordChangedDate=@LastPasswordChangedDate,
                        LastLockoutDate=@LastLockoutDate,ApplicationId=(SELECT a.ApplicationId FROM aspnet_Applications a WHERE a.LoweredApplicationName=LOWER(@ApplicationName))
                        WHERE UserId=@UserId";

                    cmd2.Parameters.Add(CustomParameters.Add(cmd2, "@Email", 256, DbType.String, user.Email));
                    cmd2.Parameters.Add(CustomParameters.Add(cmd2, "@PasswordQuestion", 256, DbType.String, user.PasswordQuestion));
                    cmd2.Parameters.Add(CustomParameters.Add(cmd2, "@IsApproved", 2, DbType.Boolean, user.IsApproved));
                    cmd2.Parameters.Add(CustomParameters.Add(cmd2, "@Comment", 300, DbType.String, user.Comment));
                    cmd2.Parameters.Add(CustomParameters.Add(cmd2, "@IsLockedOut", 2, DbType.Boolean, user.IsLockedOut));
                    cmd2.Parameters.Add(CustomParameters.Add(cmd2, "@CreateDate", 8, DbType.DateTime2, user.CreationDate));
                    cmd2.Parameters.Add(CustomParameters.Add(cmd2, "@LastLoginDate", 8, DbType.DateTime2, user.LastLoginDate));
                    cmd2.Parameters.Add(CustomParameters.Add(cmd2, "@LastPasswordChangedDate", 8, DbType.DateTime2, user.LastPasswordChangedDate));
                    cmd2.Parameters.Add(CustomParameters.Add(cmd2, "@LastLockoutDate", 8, DbType.DateTime, user.LastLockoutDate));
                    cmd2.Parameters.Add(CustomParameters.Add(cmd2, "@ApplicationName", 256, DbType.String, _applicationName));
                    cmd2.Parameters.Add(CustomParameters.Add(cmd2, "@UserId", 36, DbType.Guid, Guid.Parse(user.ProviderUserKey.ToString())));

                    cmd2.Transaction = transaction;
                    cmd2.ExecuteNonQuery();

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
                    throw;
                }
            }
            finally
            {
                ImplementCustomConnection.Instance.CloseConnection();
            }
        }

        /// <summary>
        /// Update user infos when pass a MembershipUser object as param and others info not in MembershipUser class
        /// likes PasswordAnswer and Comment.
        /// </summary>
        /// <param name="user">MembershipUser object</param>
        /// <param name="passwordAnswer">Password answer</param>
        /// <param name="comment">User comment</param>
        public void UpdateUser(MembershipUser user, string passwordAnswer = null, string comment = null)
        {
            if (user == null)
                throw new ArgumentException("User cannot be empty.");

            IDbTransaction transaction = null;

            try
            {
                if (ImplementCustomConnection.Instance.Conn.State == ConnectionState.Closed)
                    ImplementCustomConnection.Instance.Conn.Open();

                transaction = ImplementCustomConnection.Instance.Conn.BeginTransaction(IsolationLevel.Serializable);

                using (IDbCommand cmd1 = ImplementCustomConnection.Instance.Conn.CreateCommand())
                {
                    if (user.ProviderUserKey == null)
                        throw new ArgumentNullException("Please provide a valid User ID.");

                    // User has provided a ProviderUserKey.
                    cmd1.CommandText = @"UPDATE aspnet_Users SET UserName=@UserName,LoweredUserName=LOWER(@UserName),LastActivityDate=GETDATE(),
                    ApplicationId=(SELECT a.ApplicationId FROM aspnet_Applications a WHERE a.LoweredApplicationName=LOWER(@ApplicationName))
                    WHERE UserId=@UserId";

                    cmd1.Parameters.Add(CustomParameters.Add(cmd1, "@UserName", 256, DbType.String, user.UserName));
                    //cmd1.Parameters.Add(CustomParameters.Add(cmd1, "@LastActivityDate", 8, DbType.DateTime2, user.LastActivityDate));
                    cmd1.Parameters.Add(CustomParameters.Add(cmd1, "@ApplicationName", 256, DbType.String, _applicationName));
                    cmd1.Parameters.Add(CustomParameters.Add(cmd1, "@UserId", 256, DbType.Guid, Guid.Parse(user.ProviderUserKey.ToString())));

                    cmd1.Transaction = transaction;
                    cmd1.ExecuteNonQuery();
                }

                using (IDbCommand cmd2 = ImplementCustomConnection.Instance.Conn.CreateCommand())
                {
                    // User the UserProvided Key
                    cmd2.CommandText = @"UPDATE aspnet_Membership SET Email=@Email,LoweredEmail=LOWER(@Email),PasswordQuestion=@PasswordQuestion,IsApproved=@IsApproved,
                    IsLockedOut=@IsLockedOut,CreateDate=@CreateDate,LastLoginDate=@LastLoginDate,LastPasswordChangedDate=@LastPasswordChangedDate,
                    LastLockoutDate=@LastLockoutDate,ApplicationId=(SELECT a.ApplicationId FROM aspnet_Applications a WHERE a.LoweredApplicationName=LOWER(@ApplicationName)),
                    PasswordAnswer=@PasswordAnswer,Comment=@Comment WHERE UserId=@UserId";

                    cmd2.Parameters.Add(CustomParameters.Add(cmd2, "@Email", 256, DbType.String, user.Email));
                    cmd2.Parameters.Add(CustomParameters.Add(cmd2, "@PasswordQuestion", 256, DbType.String, user.PasswordQuestion));
                    cmd2.Parameters.Add(CustomParameters.Add(cmd2, "@IsApproved", 2, DbType.Boolean, user.IsApproved));
                    //cmd2.Parameters.Add(CustomParameters.Add(cmd2, "@Comment", 300, DbType.String, user.Comment));
                    cmd2.Parameters.Add(CustomParameters.Add(cmd2, "@IsLockedOut", 2, DbType.Boolean, user.IsLockedOut));
                    cmd2.Parameters.Add(CustomParameters.Add(cmd2, "@CreateDate", 8, DbType.DateTime2, user.CreationDate));
                    cmd2.Parameters.Add(CustomParameters.Add(cmd2, "@LastLoginDate", 8, DbType.DateTime2, user.LastLoginDate));
                    cmd2.Parameters.Add(CustomParameters.Add(cmd2, "@LastPasswordChangedDate", 8, DbType.DateTime2, user.LastPasswordChangedDate));
                    cmd2.Parameters.Add(CustomParameters.Add(cmd2, "@LastLockoutDate", 8, DbType.DateTime, user.LastLockoutDate));
                    cmd2.Parameters.Add(CustomParameters.Add(cmd2, "@ApplicationName", 256, DbType.String, _applicationName));
                    cmd2.Parameters.Add(CustomParameters.Add(cmd2, "@UserId", 36, DbType.Guid, Guid.Parse(user.ProviderUserKey.ToString())));

                    cmd2.Parameters.Add(CustomParameters.Add(cmd2, "@PasswordAnswer", 128, DbType.String, passwordAnswer));
                    cmd2.Parameters.Add(CustomParameters.Add(cmd2, "@Comment", 300, DbType.String, comment));

                    cmd2.Transaction = transaction;
                    cmd2.ExecuteNonQuery();

                    transaction.Commit();
                    transaction.Dispose();
                }
            }
            catch (Exception)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                    transaction.Dispose();
                    throw;
                }
            }
            finally
            {
                ImplementCustomConnection.Instance.CloseConnection();
            }
        }

        /// <summary>
        /// Validate user credential by providing username and password.
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Pasword</param>
        /// <returns>Statut of validation. True if it succed or otherwise False</returns>
        public override bool ValidateUser(string username, string password)
        {
            bool status = false;

            if (string.IsNullOrEmpty(username))
                throw new ArgumentException("Please provide a valid username.");
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Please provide a valid password.");

            try
            {
                if (ImplementCustomConnection.Instance.Conn.State == ConnectionState.Closed)
                    ImplementCustomConnection.Instance.Conn.Open();

                using (IDbCommand cmd = ImplementCustomConnection.Instance.Conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT u.UserName,u.UserId,m.Password,m.PasswordSalt,m.IsApproved,m.IsLockedOut FROM aspnet_Membership m
                    INNER JOIN aspnet_Users u ON u.UserId=m.UserId
                    INNER JOIN aspnet_Applications a ON a.ApplicationId=u.ApplicationId
                    WHERE u.LoweredUserName=@LoweredUserName AND a.ApplicationId=(SELECT a.ApplicationId FROM aspnet_Applications a WHERE a.LoweredApplicationName=LOWER(@ApplicationName))";

                    cmd.Parameters.Add(CustomParameters.Add(cmd, "@LoweredUserName", 256, DbType.String, username.ToLower()));
                    cmd.Parameters.Add(CustomParameters.Add(cmd, "@ApplicationName", 256, DbType.String, _applicationName));

                    IDataReader rd = cmd.ExecuteReader();
                    bool ok = false;
                    string passwordSalt = null;
                    string passwordHashed = null;

                    try
                    {
                        if (rd.Read())
                        {
                            passwordSalt = rd["PasswordSalt"].ToString();
                            passwordHashed = rd["Password"].ToString();
                            bool isApprovedUser = Convert.ToBoolean(rd["IsApproved"].ToString());
                            bool isLockedOutUser = Convert.ToBoolean(rd["IsLockedOut"].ToString());

                            // User is not approved.
                            if (!isApprovedUser)
                                return false;

                            // User is locked out.
                            if (isLockedOutUser)
                                return false;

                            ok = true;
                        }
                    }
                    finally
                    {
                        rd.Dispose();
                    }

                    if(ok)
                    {
                        // Validate password.
                        if (PerformHashPassword.ValidatePassword(password, passwordSalt, passwordHashed))
                            status = updateLastUserLoginDate(username);
                    }
                }
            }
            finally
            {
                ImplementCustomConnection.Instance.CloseConnection();
            }

            return status;
        }

        // Update LastLogin user date
        private bool updateLastUserLoginDate(string username)
        {
            bool status = false;

            try
            {
                if (ImplementCustomConnection.Instance.Conn.State == ConnectionState.Closed)
                    ImplementCustomConnection.Instance.Conn.Open();

                using (IDbCommand cmd = ImplementCustomConnection.Instance.Conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE aspnet_Users SET LastActivityDate=GETDATE() WHERE LoweredUserName=@LoweredUserName 
                    AND ApplicationId=(SELECT a.ApplicationId FROM aspnet_Applications a WHERE a.LoweredApplicationName=LOWER(@ApplicationName))";

                    cmd.Parameters.Add(CustomParameters.Add(cmd, "@LoweredUserName", 256, DbType.String, username.ToLower()));
                    cmd.Parameters.Add(CustomParameters.Add(cmd, "@ApplicationName", 256, DbType.String, _applicationName));

                    int record = cmd.ExecuteNonQuery();

                    if (record == 0)
                        throw new ArgumentException("Failed to update user, please try again.");
                    else
                        status = true;
                }

                return status;
            }
            finally
            {
                ImplementCustomConnection.Instance.CloseConnection();
            }
        }

        // Update LastLogin user date and password after reset user password.
        private bool updateLastUserLoginDateAndPassword(string username, string password)
        {
            bool status = false;
            IDbTransaction transaction = null;

            try
            {
                if (ImplementCustomConnection.Instance.Conn.State == ConnectionState.Closed)
                    ImplementCustomConnection.Instance.Conn.Open();

                transaction = ImplementCustomConnection.Instance.Conn.BeginTransaction(IsolationLevel.Serializable);

                using (IDbCommand cmd1 = ImplementCustomConnection.Instance.Conn.CreateCommand())
                {
                    cmd1.CommandText = @"UPDATE aspnet_Users SET LastActivityDate=GETDATE() WHERE LoweredUserName=@LoweredUserName 
                    AND ApplicationId=(SELECT a.ApplicationId FROM aspnet_Applications a WHERE a.LoweredApplicationName=LOWER(@ApplicationName))";

                    cmd1.Parameters.Add(CustomParameters.Add(cmd1, "@LoweredUserName", 256, DbType.String, username.ToLower()));
                    cmd1.Parameters.Add(CustomParameters.Add(cmd1, "@ApplicationName", 256, DbType.String, _applicationName));

                    cmd1.Transaction = transaction;
                    int record = cmd1.ExecuteNonQuery();

                    if (record == 0)
                        throw new ArgumentException("Failed to update user, please try again.");
                }

                using (IDbCommand cmd2 = ImplementCustomConnection.Instance.Conn.CreateCommand())
                {
                    // Generate password salt and hashed password
                    byte[] saltByte = PerformHashPassword.GenerateSaltByte();
                    string passwordUser = PerformHashPassword.HashPassword(password, saltByte);
                    string passwordSalt = Convert.ToBase64String(saltByte);

                    cmd2.CommandText = @"UPDATE aspnet_Membership SET Password=@Password,PasswordSalt=@PasswordSalt WHERE UserId=(SELECT u.UserId FROM aspnet_Users u
                    INNER JOIN aspnet_Membership m ON u.UserId=m.UserId 
                    INNER JOIN aspnet_Applications a ON a.ApplicationId=u.ApplicationId 
                    WHERE u.LoweredUserName=@LoweredUserName AND a.ApplicationId=(SELECT a.ApplicationId FROM aspnet_Applications a WHERE a.LoweredApplicationName=LOWER(@ApplicationName)))
                    AND ApplicationId=(SELECT a.ApplicationId FROM aspnet_Applications a WHERE a.LoweredApplicationName=LOWER(@ApplicationName))";

                    cmd2.Parameters.Add(CustomParameters.Add(cmd2, "@Password", 128, DbType.String, passwordUser));
                    cmd2.Parameters.Add(CustomParameters.Add(cmd2, "@PasswordSalt", 128, DbType.String, passwordSalt));
                    cmd2.Parameters.Add(CustomParameters.Add(cmd2, "@LoweredUserName", 256, DbType.String, username.ToLower()));
                    cmd2.Parameters.Add(CustomParameters.Add(cmd2, "@ApplicationName", 256, DbType.String, _applicationName));

                    cmd2.Transaction = transaction;
                    int record = cmd2.ExecuteNonQuery();

                    if (record == 0)
                        throw new ArgumentException("Failed to update user, please try again.");
                    else
                    {
                        transaction.Commit();
                        status = true;
                    }
                }

                return status;
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

        /// <summary>
        /// Unlock user passed in parameter.
        /// </summary>
        /// <param name="userName">Username</param>
        /// <returns>A boolean value indicate the status, if update failed or not.</returns>
        public override bool UnlockUser(string userName)
        {
            bool status = false;

            // Validate Username
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentException("Username cannot be empty.");

            MembershipUser user = getUserFromUserName(userName);

            if (user == null)
                throw new ArgumentException("Please provide a valid username.");
            else
            {
                try
                {
                    if (ImplementCustomConnection.Instance.Conn.State == ConnectionState.Closed)
                        ImplementCustomConnection.Instance.Conn.Open();

                    using (IDbCommand cmd = ImplementCustomConnection.Instance.Conn.CreateCommand())
                    {
                        cmd.CommandText = @"UPDATE aspnet_Membership SET IsLockedOut=0 WHERE UserId=(SELECT m.UserId FROM aspnet_Membership m
                        INNER JOIN aspnet_Users u ON u.UserId=m.UserId
                        INNER JOIN aspnet_Applications a ON a.ApplicationId=u.ApplicationId
                        WHERE u.LoweredUserName=@LoweredUserName AND a.ApplicationId=(SELECT a.ApplicationId FROM aspnet_Applications a WHERE a.LoweredApplicationName=LOWER(@ApplicationName)))";

                        cmd.Parameters.Add(CustomParameters.Add(cmd, "@LoweredUserName", 256, DbType.String, userName.ToLower()));
                        cmd.Parameters.Add(CustomParameters.Add(cmd, "@ApplicationName", 256, DbType.String, _applicationName));

                        int record = cmd.ExecuteNonQuery();

                        if (record == 0)
                            throw new ArgumentException("Failed to save modifications, please try again.");
                        else
                            status = true;
                    }
                }
                finally
                {
                    ImplementCustomConnection.Instance.CloseConnection();
                }
            }

            return status;
        }

        /// <summary>
        /// Get user details when User ID and his online status are know. If userIsOnline param is true, 
        /// we update the LastActivity date and otherwise, user infos are return.
        /// </summary>
        /// <param name="providerUserKey">User ID</param>
        /// <param name="userIsOnline">User status online or not</param>
        /// <returns>MembershipUser object</returns>
        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            MembershipUser user = null;

            if (providerUserKey == null)
                throw new ArgumentException("User ID cannot be empty.");
            else
            {
                try
                {
                    if (ImplementCustomConnection.Instance.Conn.State == ConnectionState.Closed)
                        ImplementCustomConnection.Instance.Conn.Open();

                    // Retreive user infos without update Last activity Date / Time
                    using (IDbCommand cmd = ImplementCustomConnection.Instance.Conn.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT u.UserName,u.UserId,m.Email,m.PasswordQuestion,m.Comment,m.IsApproved,m.IsLockedOut,m.CreateDate,m.LastLoginDate,u.LastActivityDate,
                        m.LastPasswordChangedDate,m.LastLockoutDate FROM aspnet_Membership m
                        INNER JOIN aspnet_Users u ON u.UserId=m.UserId 
                        INNER JOIN aspnet_Applications a ON a.ApplicationId=u.ApplicationId 
                        WHERE u.UserId=@UserId AND a.ApplicationId=(SELECT a.ApplicationId FROM aspnet_Applications a WHERE a.LoweredApplicationName=LOWER(@ApplicationName))";

                        cmd.Parameters.Add(CustomParameters.Add(cmd, "@UserId", 36, DbType.Guid, Guid.Parse(providerUserKey.ToString())));
                        cmd.Parameters.Add(CustomParameters.Add(cmd, "@ApplicationName", 256, DbType.String, _applicationName));

                        IDataReader rd = cmd.ExecuteReader();

                        if (rd.Read())
                        {
                            // Affect values in MembershipUser object
                            user = new MembershipUser(_name, rd["UserName"].ToString(), Guid.Parse(rd["UserId"].ToString()), rd["Email"].ToString(), rd["PasswordQuestion"].ToString(),
                                rd["Comment"].ToString(), Convert.ToBoolean(rd["IsApproved"].ToString()), Convert.ToBoolean(rd["IsLockedOut"].ToString()), Convert.ToDateTime(rd["CreateDate"]),
                                Convert.ToDateTime(rd["LastLoginDate"]), Convert.ToDateTime(rd["LastActivityDate"]), Convert.ToDateTime(rd["LastPasswordChangedDate"]), Convert.ToDateTime(rd["LastLockoutDate"]));
                        }

                        rd.Dispose();
                    }

                    if (userIsOnline)
                    {
                        // Update Last activity Date / Time
                        using (IDbCommand cmd = ImplementCustomConnection.Instance.Conn.CreateCommand())
                        {
                            cmd.CommandText = @"UPDATE aspnet_Membership SET LastLoginDate=@LastLoginDate WHERE UserId=@UserId 
                            AND ApplicationId=(SELECT a.ApplicationId FROM aspnet_Applications a WHERE a.LoweredApplicationName=LOWER(@ApplicationName))";

                            cmd.Parameters.Add(CustomParameters.Add(cmd, "@UserId", 36, DbType.Guid, Guid.Parse(providerUserKey.ToString())));
                            cmd.Parameters.Add(CustomParameters.Add(cmd, "@LastLoginDate", 8, DbType.DateTime2, DateTime.Now));
                            cmd.Parameters.Add(CustomParameters.Add(cmd, "@ApplicationName", 256, DbType.String, _applicationName));

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                finally
                {
                    ImplementCustomConnection.Instance.CloseConnection();
                }
            }

            return user;
        }

        /// <summary>
        /// Get user details when Username and his online status are know. If userIsOnline param is true, 
        /// we update the LastActivity date and otherwise, user infos are return.
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="userIsOnline">User status online or not</param>
        /// <returns>MembershipUser object</returns>
        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            MembershipUser user = null;

            if (username == null)
                throw new ArgumentException("Username cannot be empty.");
            else
            {
                try
                {
                    if (ImplementCustomConnection.Instance.Conn.State == ConnectionState.Closed)
                        ImplementCustomConnection.Instance.Conn.Open();

                    // Retreive user infos without update Last activity Date / Time
                    using (IDbCommand cmd = ImplementCustomConnection.Instance.Conn.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT u.UserName,u.UserId,m.Email,m.PasswordQuestion,m.Comment,m.IsApproved,m.IsLockedOut,m.CreateDate,m.LastLoginDate,u.LastActivityDate,
                        m.LastPasswordChangedDate,m.LastLockoutDate FROM aspnet_Membership m
                        INNER JOIN aspnet_Users u ON u.UserId=m.UserId 
                        INNER JOIN aspnet_Applications a ON a.ApplicationId=u.ApplicationId 
                        WHERE u.LoweredUserName=@LoweredUserName AND a.ApplicationId=(SELECT a.ApplicationId FROM aspnet_Applications a WHERE a.LoweredApplicationName=LOWER(@ApplicationName))";

                        cmd.Parameters.Add(CustomParameters.Add(cmd, "@LoweredUserName", 256, DbType.Guid, username.ToLower()));
                        cmd.Parameters.Add(CustomParameters.Add(cmd, "@ApplicationName", 256, DbType.String, _applicationName));

                        IDataReader rd = cmd.ExecuteReader();

                        if (rd.Read())
                        {
                            // Affect values in MembershipUser object
                            user = new MembershipUser(_name, rd["UserName"].ToString(), Guid.Parse(rd["UserId"].ToString()), rd["Email"].ToString(), rd["PasswordQuestion"].ToString(),
                                rd["Comment"].ToString(), Convert.ToBoolean(rd["IsApproved"].ToString()), Convert.ToBoolean(rd["IsLockedOut"].ToString()), Convert.ToDateTime(rd["CreateDate"]),
                                Convert.ToDateTime(rd["LastLoginDate"]), Convert.ToDateTime(rd["LastActivityDate"]), Convert.ToDateTime(rd["LastPasswordChangedDate"]), Convert.ToDateTime(rd["LastLockoutDate"]));
                        }

                        rd.Dispose();
                    }

                    if (userIsOnline)
                    {
                        // Update Last activity Date / Time
                        using (IDbCommand cmd = ImplementCustomConnection.Instance.Conn.CreateCommand())
                        {
                            cmd.CommandText = @"UPDATE aspnet_Membership SET LastLoginDate=@LastLoginDate WHERE LoweredUserName=@LoweredUserName 
                            AND ApplicationId=(SELECT a.ApplicationId FROM aspnet_Applications a WHERE a.LoweredApplicationName=LOWER(@ApplicationName))";

                            cmd.Parameters.Add(CustomParameters.Add(cmd, "@LoweredUserName", 256, DbType.String, username.ToLower()));
                            cmd.Parameters.Add(CustomParameters.Add(cmd, "@LastLoginDate", 8, DbType.DateTime2, DateTime.Now));
                            cmd.Parameters.Add(CustomParameters.Add(cmd, "@ApplicationName", 256, DbType.String, _applicationName));

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                finally
                {
                    ImplementCustomConnection.Instance.CloseConnection();
                }
            }

            return user;
        }

        /// <summary>
        /// Retreive username from Database using email address as param.
        /// </summary>
        /// <param name="email">Email address to make search</param>
        /// <returns>The username</returns>
        public override string GetUserNameByEmail(string email)
        {
            string user = null;

            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("Username cannot be empty.");
            else if (!System.Text.RegularExpressions.Regex.IsMatch(email, "^[_a-zA-Z0-9-]+(.[a-zA-Z0-9-]+)@[a-zA-Z0-9-]+(.[a-zA-Z0-9-]+)*(.[a-zA-Z]{2,4})$"))
                throw new ArgumentException("Invalid Email address.");
            else
            {
                try
                {
                    if (ImplementCustomConnection.Instance.Conn.State == ConnectionState.Closed)
                        ImplementCustomConnection.Instance.Conn.Open();

                    using (IDbCommand cmd = ImplementCustomConnection.Instance.Conn.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT u.UserName FROM aspnet_Membership m
                        INNER JOIN aspnet_Users u ON u.UserId=m.UserId 
                        INNER JOIN aspnet_Applications a ON a.ApplicationId=u.ApplicationId 
                        WHERE m.Email=@Email AND a.ApplicationId=(SELECT a.ApplicationId FROM aspnet_Applications a WHERE a.LoweredApplicationName=LOWER(@ApplicationName))";

                        cmd.Parameters.Add(CustomParameters.Add(cmd, "@Email", 256, DbType.String, email));
                        cmd.Parameters.Add(CustomParameters.Add(cmd, "@ApplicationName", 256, DbType.String, _applicationName));

                        IDataReader rd = cmd.ExecuteReader();

                        if (rd.Read())
                            user = rd["UserName"].ToString();

                        rd.Dispose();
                    }

                    return user;
                }
                finally
                {
                    ImplementCustomConnection.Instance.CloseConnection();
                }
            }
        }

        /// <summary>
        /// Delete user data from Database after specified username and a confirm boolean (True or False) as param.
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="deleteAllRelatedData">Allow to leave data or not. True to delete data related to the user from the database; false to leave data</param>
        /// <returns>Status of deletion. True for Success and Fasle for failed</returns>
        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            bool status = false;
            IDbTransaction transaction = null;
            Guid? UserID = null;

            if (string.IsNullOrEmpty(username))
                throw new ArgumentException("Username cannot be empty.");
            if (!deleteAllRelatedData)
                throw new ArgumentException("No deletion needed, data has been leaved.");

            try
            {
                if (ImplementCustomConnection.Instance.Conn.State == ConnectionState.Closed)
                    ImplementCustomConnection.Instance.Conn.Open();

                transaction = ImplementCustomConnection.Instance.Conn.BeginTransaction(IsolationLevel.Serializable);

                // Get User ID first
                using (IDbCommand cmd1 = ImplementCustomConnection.Instance.Conn.CreateCommand())
                {
                    cmd1.CommandText = @"SELECT u.UserId FROM aspnet_Membership m
	                INNER JOIN aspnet_Users u ON u.UserId=m.UserId 
	                INNER JOIN aspnet_Applications a ON a.ApplicationId=u.ApplicationId 
	                WHERE u.LoweredUserName=@LoweredUserName AND a.ApplicationId=(SELECT a.ApplicationId FROM aspnet_Applications a WHERE a.LoweredApplicationName=LOWER(@ApplicationName))";

                    cmd1.Parameters.Add(CustomParameters.Add(cmd1, "@LoweredUserName", 256, DbType.String, username.ToLower()));
                    cmd1.Parameters.Add(CustomParameters.Add(cmd1, "@ApplicationName", 256, DbType.String, _applicationName));

                    cmd1.Transaction = transaction;
                    IDataReader rd = cmd1.ExecuteReader();

                    if (rd.Read())
                        UserID = Guid.Parse(rd["UserId"].ToString());

                    rd.Dispose();
                }

                //We delete all data in aspnet_UsersInRoles table for user provided
                using (IDbCommand cmd2 = ImplementCustomConnection.Instance.Conn.CreateCommand())
                {
                    cmd2.CommandText = @"DELETE FROM aspnet_UsersInRoles WHERE UserId=@UserId";

                    cmd2.Parameters.Add(CustomParameters.Add(cmd2, "@UserId", 36, DbType.Guid, UserID));

                    cmd2.Transaction = transaction;
                    cmd2.ExecuteNonQuery();
                }

                //We delete all data in aspnet_Membership table for user provided
                using (IDbCommand cmd3 = ImplementCustomConnection.Instance.Conn.CreateCommand())
                {
                    cmd3.CommandText = @"DELETE FROM aspnet_Membership WHERE UserId=@UserId";

                    cmd3.Parameters.Add(CustomParameters.Add(cmd3, "@UserId", 36, DbType.Guid, UserID));

                    cmd3.Transaction = transaction;
                    cmd3.ExecuteNonQuery();
                }

                //We delete all data in aspnet_Users table for user provided
                using (IDbCommand cmd4 = ImplementCustomConnection.Instance.Conn.CreateCommand())
                {
                    cmd4.CommandText = @"DELETE FROM aspnet_Users WHERE UserId=@UserId";

                    cmd4.Parameters.Add(CustomParameters.Add(cmd4, "@UserId", 36, DbType.Guid, UserID));

                    cmd4.Transaction = transaction;
                    int record = cmd4.ExecuteNonQuery();

                    if (record > 0)
                    {
                        status = true;
                        transaction.Commit();
                        transaction.Dispose();
                    }
                    else
                    {
                        throw new ArgumentException("No deletion done.");
                    }
                }                
            }
            catch (Exception)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                    transaction.Dispose();
                    throw;
                }
            }
            finally
            {
                ImplementCustomConnection.Instance.CloseConnection();
            }

            return status;
        }

        /// <summary>
        /// Get users collections in Database based on page index and page size
        /// </summary>
        /// <param name="pageIndex">Page index zero-based</param>
        /// <param name="pageSize">Size for all pages</param>
        /// <param name="totalRecords">Total records recorded</param>
        /// <returns>MembershipUserCollection object</returns>
        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            totalRecords = 0;
            MembershipUserCollection users = new MembershipUserCollection();
            MembershipUser user = null;

            try
            {
                if (ImplementCustomConnection.Instance.Conn.State == ConnectionState.Closed)
                    ImplementCustomConnection.Instance.Conn.Open();

                using (IDbCommand cmd = ImplementCustomConnection.Instance.Conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY u.UserId ASC) AS RowNum,
                    u.UserName,u.UserId,m.Email,m.PasswordQuestion,m.Comment,m.IsApproved,m.IsLockedOut,m.CreateDate,m.LastLoginDate,u.LastActivityDate,
                    m.LastPasswordChangedDate,m.LastLockoutDate FROM aspnet_Membership m
                    INNER JOIN aspnet_Users u ON u.UserId=m.UserId 
                    INNER JOIN aspnet_Applications a ON a.ApplicationId=u.ApplicationId
                    WHERE a.ApplicationId=(SELECT a.ApplicationId FROM aspnet_Applications a WHERE a.LoweredApplicationName=LOWER(@ApplicationName))) AS alias
                    WHERE RowNum BETWEEN @firstRecord AND @lastRecord";

                    if (pageIndex == 0)
                    {
                        // First page index => 0
                        cmd.Parameters.Add(CustomParameters.Add(cmd, "@firstRecord", 4, DbType.Int32, pageIndex + 1));
                        cmd.Parameters.Add(CustomParameters.Add(cmd, "@lastRecord", 4, DbType.Int32, pageSize));
                        cmd.Parameters.Add(CustomParameters.Add(cmd, "@ApplicationName", 256, DbType.String, _applicationName));
                        totalRecords = (pageIndex + 1) * pageSize;
                    }
                    else
                    {
                        // Not first page index => 1 and so one
                        cmd.Parameters.Add(CustomParameters.Add(cmd, "@firstRecord", 4, DbType.Int32, (pageSize * pageIndex) + 1));
                        cmd.Parameters.Add(CustomParameters.Add(cmd, "@lastRecord", 4, DbType.Int32, pageSize * (pageIndex + 1)));
                        cmd.Parameters.Add(CustomParameters.Add(cmd, "@ApplicationName", 256, DbType.String, _applicationName));
                        totalRecords = (pageIndex + 1) * pageSize;
                    }

                    IDataReader rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        // Affect values in MembershipUser object
                        user = new MembershipUser(_name, rd["UserName"].ToString(), Guid.Parse(rd["UserId"].ToString()), rd["Email"].ToString(), rd["PasswordQuestion"].ToString(),
                            rd["Comment"].ToString(), Convert.ToBoolean(rd["IsApproved"].ToString()), Convert.ToBoolean(rd["IsLockedOut"].ToString()), Convert.ToDateTime(rd["CreateDate"]),
                            Convert.ToDateTime(rd["LastLoginDate"]), Convert.ToDateTime(rd["LastActivityDate"]), Convert.ToDateTime(rd["LastPasswordChangedDate"]), Convert.ToDateTime(rd["LastLockoutDate"]));

                        users.Add(user);
                    }

                    rd.Dispose();
                }
            }
            finally
            {
                ImplementCustomConnection.Instance.CloseConnection();
            }

            return users;
        }

        /// <summary>
        /// Get users collections in Database based on a specific application name passed as param.
        /// </summary>
        /// <param name="applicationName">Application name</param>
        /// <returns>MembershipUserCollection object</returns>
        public MembershipUserCollection GetAllUsers(string applicationName)
        {
            MembershipUserCollection users = new MembershipUserCollection();
            MembershipUser user = null;

            try
            {
                if (ImplementCustomConnection.Instance.Conn.State == ConnectionState.Closed)
                    ImplementCustomConnection.Instance.Conn.Open();

                using (IDbCommand cmd = ImplementCustomConnection.Instance.Conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT u.UserName,u.UserId,m.Email,m.PasswordQuestion,m.Comment,m.IsApproved,m.IsLockedOut,m.CreateDate,m.LastLoginDate,u.LastActivityDate,
                    m.LastPasswordChangedDate,m.LastLockoutDate FROM aspnet_Membership m
                    INNER JOIN aspnet_Users u ON u.UserId=m.UserId 
                    INNER JOIN aspnet_Applications a ON a.ApplicationId=u.ApplicationId
                    WHERE a.ApplicationId=(SELECT a.ApplicationId FROM aspnet_Applications a WHERE a.LoweredApplicationName=LOWER(@ApplicationName))";

                    cmd.Parameters.Add(CustomParameters.Add(cmd, "@ApplicationName", 256, DbType.String, applicationName));

                    IDataReader rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        // Affect values in MembershipUser object
                        user = new MembershipUser(_name, rd["UserName"].ToString(), Guid.Parse(rd["UserId"].ToString()), rd["Email"].ToString(), rd["PasswordQuestion"].ToString(),
                            rd["Comment"].ToString(), Convert.ToBoolean(rd["IsApproved"].ToString()), Convert.ToBoolean(rd["IsLockedOut"].ToString()), Convert.ToDateTime(rd["CreateDate"]),
                            Convert.ToDateTime(rd["LastLoginDate"]), Convert.ToDateTime(rd["LastActivityDate"]), Convert.ToDateTime(rd["LastPasswordChangedDate"]), Convert.ToDateTime(rd["LastLockoutDate"]));

                        users.Add(user);
                    }

                    rd.Dispose();
                }
            }
            finally
            {
                ImplementCustomConnection.Instance.CloseConnection();
            }

            return users;
        }

        /// <summary>
        /// Get users collections in Database for all applications.
        /// </summary>
        /// <returns>MembershipUserCollection object</returns>
        public MembershipUserCollection GetAllUsers()
        {
            MembershipUserCollection users = new MembershipUserCollection();
            MembershipUser user = null;

            try
            {
                if (ImplementCustomConnection.Instance.Conn.State == ConnectionState.Closed)
                    ImplementCustomConnection.Instance.Conn.Open();

                using (IDbCommand cmd = ImplementCustomConnection.Instance.Conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT u.UserName,u.UserId,m.Email,m.PasswordQuestion,m.Comment,m.IsApproved,m.IsLockedOut,m.CreateDate,m.LastLoginDate,u.LastActivityDate,
                    m.LastPasswordChangedDate,m.LastLockoutDate FROM aspnet_Membership m
                    INNER JOIN aspnet_Users u ON u.UserId=m.UserId 
                    INNER JOIN aspnet_Applications a ON a.ApplicationId=u.ApplicationId
                    WHERE a.ApplicationId=(SELECT a.ApplicationId FROM aspnet_Applications a WHERE a.LoweredApplicationName=LOWER(@ApplicationName))";

                    cmd.Parameters.Add(CustomParameters.Add(cmd, "@ApplicationName", 256, DbType.String, _applicationName));

                    IDataReader rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        // Affect values in MembershipUser object
                        user = new MembershipUser(_name, rd["UserName"].ToString(), Guid.Parse(rd["UserId"].ToString()), rd["Email"].ToString(), rd["PasswordQuestion"].ToString(),
                            rd["Comment"].ToString(), Convert.ToBoolean(rd["IsApproved"].ToString()), Convert.ToBoolean(rd["IsLockedOut"].ToString()), Convert.ToDateTime(rd["CreateDate"]),
                            Convert.ToDateTime(rd["LastLoginDate"]), Convert.ToDateTime(rd["LastActivityDate"]), Convert.ToDateTime(rd["LastPasswordChangedDate"]), Convert.ToDateTime(rd["LastLockoutDate"]));

                        users.Add(user);
                    }

                    rd.Dispose();
                }
            }
            finally
            {
                ImplementCustomConnection.Instance.CloseConnection();
            }

            return users;
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Find user info by criteria (Like Username, Email address or any Comment)
        /// </summary>
        /// <param name="criteria">Criteria to be match</param>
        /// <returns>MembershipUserCollection Object</returns>
        public MembershipUserCollection FindUsers(string criteria)
        {
            MembershipUserCollection users = new MembershipUserCollection();
            MembershipUser user = null;

            try
            {
                if (ImplementCustomConnection.Instance.Conn.State == ConnectionState.Closed)
                    ImplementCustomConnection.Instance.Conn.Open();

                using (IDbCommand cmd = ImplementCustomConnection.Instance.Conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT u.UserName,u.UserId,m.Email,m.PasswordQuestion,m.Comment,m.IsApproved,m.IsLockedOut,m.CreateDate,m.LastLoginDate,u.LastActivityDate,
                    m.LastPasswordChangedDate,m.LastLockoutDate FROM aspnet_Membership m
                    INNER JOIN aspnet_Users u ON u.UserId=m.UserId 
                    INNER JOIN aspnet_Applications a ON a.ApplicationId=u.ApplicationId 
                    WHERE (u.LoweredUserName LIKE @LoweredUserName OR m.Email LIKE @Email OR m.Comment LIKE @Comment) AND a.ApplicationId=(SELECT a.ApplicationId FROM aspnet_Applications a WHERE a.LoweredApplicationName=LOWER(@ApplicationName))";

                    cmd.Parameters.Add(CustomParameters.Add(cmd, "@LoweredUserName", 256, DbType.String, criteria.ToLower()));
                    cmd.Parameters.Add(CustomParameters.Add(cmd, "@Email", 256, DbType.String, criteria));
                    cmd.Parameters.Add(CustomParameters.Add(cmd, "@Comment", 256, DbType.String, criteria));
                    cmd.Parameters.Add(CustomParameters.Add(cmd, "@ApplicationName", 256, DbType.String, _applicationName));

                    IDataReader rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        // Affect values in MembershipUser object
                        user = new MembershipUser(_name, rd["UserName"].ToString(), Guid.Parse(rd["UserId"].ToString()), rd["Email"].ToString(), rd["PasswordQuestion"].ToString(),
                            rd["Comment"].ToString(), Convert.ToBoolean(rd["IsApproved"].ToString()), Convert.ToBoolean(rd["IsLockedOut"].ToString()), Convert.ToDateTime(rd["CreateDate"]),
                            Convert.ToDateTime(rd["LastLoginDate"]), Convert.ToDateTime(rd["LastActivityDate"]), Convert.ToDateTime(rd["LastPasswordChangedDate"]), Convert.ToDateTime(rd["LastLockoutDate"]));

                        users.Add(user);
                    }

                    rd.Dispose();
                }
            }
            finally
            {
                ImplementCustomConnection.Instance.CloseConnection();
            }

            return users;
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
            _enablePasswordRetrieval = CustomProviderUtility.GetBooleanValue(config, "enablePasswordRetrieval", false);
            _enablePasswordReset = CustomProviderUtility.GetBooleanValue(config, "enablePasswordReset", true);
            _requiresQuestionAndAnswer = CustomProviderUtility.GetBooleanValue(config, "requiresQuestionAndAnswer", false);
            _requiresUniqueEmail = CustomProviderUtility.GetBooleanValue(config, "requiresUniqueEmail", false);
            _maxInvalidPasswordAttempts = CustomProviderUtility.GetIntValue(config, "maxInvalidPasswordAttempts", 5, false, 0);
            _passwordAttemptWindow = CustomProviderUtility.GetIntValue(config, "passwordAttemptWindow", 10, false, 0);
            _minRequiredPasswordLength = CustomProviderUtility.GetIntValue(config, "minRequiredPasswordLength", 3, false, 0);
            _minRequiredNonAlphanumericCharacters = CustomProviderUtility.GetIntValue(config, "minRequiredNonalphanumericCharacters", 0, true, 0);
            _passwordStrengthRegularExpression = config["passwordStrengthRegularExpression"];

            if (config["passwordFormat"] != null)
                _passwordFormat = (MembershipPasswordFormat)Enum.Parse(typeof(MembershipPasswordFormat), config["passwordFormat"]);
            else
                _passwordFormat = MembershipPasswordFormat.Hashed;

            base.Initialize(name, config);
        }
    }
}
