using System;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Security;
using System.Windows.Forms;

namespace MembershipProject
{
    public partial class ManageUsersAndRoles : Form
    {
        public ManageUsersAndRoles()
        {
            InitializeComponent();
        }

        private void loadUsers()
        {
            MembershipUserCollection users = Membership.GetAllUsers();
            MembershipUser[] valuesUser = new MembershipUser[users.Count];

            users.CopyTo(valuesUser, 0);
            dgvUsers.DataSource = valuesUser;

            IEnumerator enumerate = users.GetEnumerator();

            while (enumerate.MoveNext())
            {
                object obj = enumerate.Current;
                lstUsers.Items.Add(obj);
            }
        }

        private void loadRoles()
        {
            lstRoleName.Items.Clear();
            lstRoles.Items.Clear();

            string[] roles = Roles.GetAllRoles();
            string[] roles1 = new string[roles.Length];

            roles.CopyTo(roles1, 0);

            IEnumerator enumerator = roles.GetEnumerator();

            while(enumerator.MoveNext())
            {
                object obj = enumerator.Current;
                lstRoleName.Items.Add(obj);
                lstRoles.Items.Add(obj);
            }
        }

        private void ManageUsers_Load(object sender, EventArgs e)
        {
            btnAddOneUser.Enabled = false;
            btnRemoveOneUser.Enabled = false;

            rdAddInAllRoles.Checked = false;
            rdAddInOneRole.Checked = true;

            //Initialise connection string
            string connectionString = ConfigurationManager.ConnectionStrings["SQLServerConnectionString"].ConnectionString;

            try
            {
                loadUsers();
                loadRoles();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Unable to load Users or Roles from Datasource, " + ex.Message, "Loading users and roles", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Unable to load Users or Roles from Datasource, " + ex.Message, "Loading users and roles", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to load Users or Roles from Datasource, " + ex.Message, "Loading users and roles", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnNewUser_Click(object sender, EventArgs e)
        {
            clearUser();
        }

        private void clearUser()
        {
            txtUserId.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            txtOldPassword.Clear();
            txtNewPassword.Clear();
            txtEmail.Clear();
            txtDateCreate.Clear();
            txtPasswordQuestion.Clear();
            txtPasswordAnswer.Clear();
            txtComment.Clear();
            txtLastLoginDate.Clear();
            txtLastActivityDate.Clear();
            txtLastLockedOutDate.Clear();
            txtLastPasswordChagedDate.Clear();
            chkApproved.Checked = true;
            chkIsLockedOut.Checked = false;

            clearListBox(lstUsers);

            txtPassword.BackColor = System.Drawing.SystemColors.Window;
            txtPassword.ForeColor = System.Drawing.SystemColors.WindowText;

            txtNewPassword.BackColor = System.Drawing.SystemColors.Window;
            txtNewPassword.ForeColor = System.Drawing.SystemColors.WindowText;

            txtUsername.Focus();
        }

        private void clearRole()
        {
            txtRoleId.Clear();
            txtRoleName.Clear();
            txtRoleDescription.Clear();
            txtUserInRoleFind.Clear();

            clearListBox(lstUserAffectedInRole);
            clearListBox(lstRoleName);
            clearListBox(lstRoles);

            txtRoleName.Focus();
        }

        private void clearListBox(ListBox list)
        {
            list.Items.Clear();
        }

        private void btnNewRole_Click(object sender, EventArgs e)
        {
            txtRoleId.Clear();
            txtRoleName.Clear();
            txtRoleDescription.Clear();
            txtUserInRoleFind.Clear();

            txtRoleName.Focus();
        }

        private void btnSaveUser_Click(object sender, EventArgs e)
        {
            try
            {
                MembershipCreateStatus status = MembershipCreateStatus.Success;
                                
                Membership.CreateUser(txtUsername.Text, txtPassword.Text, txtEmail.Text, txtPasswordQuestion.Text, txtPasswordAnswer.Text, chkApproved.Checked, null, out status);

                if (status == MembershipCreateStatus.Success)
                {
                    MessageBox.Show("User created successfully", "Create user", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    loadUsers();
                }
                else
                    MessageBox.Show("Failed to create user, " + status.ToString(), "Create user", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Failed to create user, " + ex.Message, "Create user", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Failed to create user, " + ex.Message, "Create user", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to create user, " + ex.Message, "Create user", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnUpdateUser_Click(object sender, EventArgs e)
        {
            try
            {
                MembershipUser user = new MembershipUser(Membership.Provider.Name, txtUsername.Text, txtUserId.Text, txtEmail.Text, txtPasswordQuestion.Text,
                    txtComment.Text, chkApproved.Checked, chkIsLockedOut.Checked, Convert.ToDateTime(txtDateCreate.Text), Convert.ToDateTime(txtLastLoginDate.Text), 
                    Convert.ToDateTime(txtLastActivityDate.Text), Convert.ToDateTime(txtLastPasswordChagedDate.Text), Convert.ToDateTime(txtLastLockedOutDate.Text));

                Membership.UpdateUser(user);

                MessageBox.Show("User updated successfully", "Update user", MessageBoxButtons.OK, MessageBoxIcon.Information);

                loadUsers();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Failed to update user, " + ex.Message, "Update user", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Failed to update user, " + ex.Message, "Update user", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to update user, " + ex.Message, "Update user", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Do you want to delete this user ?", "Delete user", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    bool status = Membership.DeleteUser(txtUsername.Text, true);

                    if (status)
                    {
                        MessageBox.Show("User deleted successfully", "Delete user", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadUsers();
                    }
                    else
                        MessageBox.Show("Failed to delete user", "Delete user", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                    MessageBox.Show("Deletion aborted", "Delete user", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Failed to delete user, " + ex.Message, "Delete user", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Failed to delete user, " + ex.Message, "Delete user", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to delete user, " + ex.Message, "Delete user", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnUnlockUser_Click(object sender, EventArgs e)
        {
            try
            {
                bool status = Membership.Provider.UnlockUser(txtUsername.Text);

                if (status)
                {
                    MessageBox.Show("User unlocked successfully", "Unlock user", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadUsers();
                }
                else
                    MessageBox.Show("Failed to unlock user", "Unlock user", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Failed to unlock user, " + ex.Message, "Unlock user", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Failed to unlock user, " + ex.Message, "Unlock user", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to unlock user, " + ex.Message, "Unlock user", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnResetUserPassword_Click(object sender, EventArgs e)
        {
            try
            {
                string newPassword = Membership.Provider.ResetPassword(txtUsername.Text, txtPasswordAnswer.Text);

                if (!string.IsNullOrEmpty(newPassword))
                {
                    txtNewPassword.Text = newPassword;
                    txtNewPassword.BackColor = System.Drawing.Color.Red;
                    txtNewPassword.ForeColor = System.Drawing.Color.White;

                    MessageBox.Show("User reseted successfully, look at in New password field in UI", "Reset user password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Failed to reset user password", "Reset user password", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Failed to reset user password, " + ex.Message, "Reset user password", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Failed to reset user password, " + ex.Message, "Reset user password", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to reset user password, " + ex.Message, "Reset user password", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnChangeUserPassword_Click(object sender, EventArgs e)
        {
            try
            {
                bool status = Membership.Provider.ChangePassword(txtUsername.Text, txtOldPassword.Text, txtNewPassword.Text);

                if (status)
                {
                    MessageBox.Show("User password changed successfully", "Change user password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //loadUsers();
                }
                else
                    MessageBox.Show("Failed to change user password", "Change user password", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Failed to change user password, " + ex.Message, "Change user password", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Failed to change user password, " + ex.Message, "Change user password", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to change user password, " + ex.Message, "Change user password", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnGetPassword_Click(object sender, EventArgs e)
        {
            try
            {
                string currentPassword = Membership.Provider.GetPassword(txtUsername.Text, txtPasswordAnswer.Text);

                if (!string.IsNullOrEmpty(currentPassword))
                {
                    txtPassword.Text = currentPassword;
                    txtPassword.BackColor = System.Drawing.Color.Red;
                    txtPassword.ForeColor = System.Drawing.Color.White;

                    MessageBox.Show("User password retreived successfully, look at in password field in UI", "Retreive user password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Failed to retreive user password", "Retreive user password", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Failed to retreive user password, " + ex.Message, "Retreive user password", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Failed to retreive user password, " + ex.Message, "Retreive user password", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retreive user password, " + ex.Message, "Retreive user password", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void dgvUsers_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if(dgvUsers.SelectedRows.Count > 0)
                {
                    MembershipUser user = null;
                    user = (MembershipUser)dgvUsers.SelectedRows[0].DataBoundItem;

                    // Set MembershipUser values in Field
                    txtUserId.Text = user.ProviderUserKey.ToString();
                    txtUsername.Text = user.UserName.ToString();
                    //txtPassword.Text = Membership.Provider.GetPassword(user.UserName, null);
                    txtPassword.Clear();
                    txtOldPassword.Clear();
                    txtNewPassword.Clear();
                    txtEmail.Text = user.Email.ToString();
                    txtDateCreate.Text = user.CreationDate.ToString();
                    txtPasswordQuestion.Text = user.PasswordQuestion;
                    txtComment.Text = user.Comment;
                    txtLastLoginDate.Text = user.LastLoginDate.ToString();
                    txtLastActivityDate.Text = user.LastActivityDate.ToString();
                    txtLastLockedOutDate.Text = user.LastLockoutDate.ToString();
                    txtLastPasswordChagedDate.Text = user.LastPasswordChangedDate.ToString();
                    chkApproved.Checked = user.IsApproved;
                    chkIsLockedOut.Checked = user.IsLockedOut;

                    txtUserFind.Clear();

                    // ApplicationName
                    txtApplicationName.Text = Membership.ApplicationName;
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Failed to select user infos., " + ex.Message, "Select user info.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Failed to select user info., " + ex.Message, "Select user info.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to select user info., " + ex.Message, "Select user info.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnSaveRole_Click(object sender, EventArgs e)
        {
            try
            {
                Roles.CreateRole(txtRoleName.Text);
                MessageBox.Show("Role created successfully", "Create role", MessageBoxButtons.OK, MessageBoxIcon.Information);

                loadRoles();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Failed to create role, " + ex.Message, "Create role", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Failed to create role, " + ex.Message, "Create role", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to create role, " + ex.Message, "Create role", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnDeleteRole_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Do you want to delete this role ?", "Delete role", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    bool status = Roles.DeleteRole(txtRoleName.Text, true);

                    if (status)
                    {
                        MessageBox.Show("Role deleted successfully", "Delete role", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadRoles();
                    }
                    else
                        MessageBox.Show("Failed to delete role", "Delete role", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                    MessageBox.Show("Deletion aborted", "Delete role", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Failed to delete role, " + ex.Message, "Delete role", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Failed to delete role, " + ex.Message, "Delete user", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to delete role, " + ex.Message, "Delete user", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnFindUser_Click(object sender, EventArgs e)
        {
            try
            {
                MembershipUserCollection users = Membership.FindUsersByName(txtUserFind.Text);
                MembershipUser[] valuesUser = new MembershipUser[users.Count];

                users.CopyTo(valuesUser, 0);
                dgvUsers.DataSource = valuesUser;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Failed to find user, " + ex.Message, "Find user", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Failed to find user, " + ex.Message, "Find user", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to find user, " + ex.Message, "Find user", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void lstRoleName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lstRoleName.Items.Count > 0)
                {
                    string selectedRole = lstRoleName.SelectedItem.ToString();
                    txtRoleName.Text = selectedRole;

                    // ApplicationName
                    txtApplicationName.Text = Roles.ApplicationName;

                    try
                    {
                        // Find users in selected role
                        string[] usersInRole = Roles.FindUsersInRole(selectedRole, txtUsername.Text);

                        if (usersInRole != null)
                        {
                            if (usersInRole.Length > 0)
                            {
                                lstUsersInRoles.Items.Clear();

                                IEnumerator enumerator = usersInRole.GetEnumerator();

                                while (enumerator.MoveNext())
                                {
                                    object obj = enumerator.Current;
                                    lstUsersInRoles.Items.Add(obj);
                                }
                            }
                        }
                        else
                            lstUsersInRoles.Items.Clear();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed to select role info., " + ex.Message, "Select role info.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Failed to select role info., " + ex.Message, "Select role info.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Failed to select role info., " + ex.Message, "Select role info.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to select role info., " + ex.Message, "Select role info.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnRefreshAll_Click(object sender, EventArgs e)
        {
            try
            {
                clearUser();
                clearRole();

                loadUsers();
                loadRoles();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Unable to load Users or Roles from Datasource, " + ex.Message, "Loading users and roles", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Unable to load Users or Roles from Datasource, " + ex.Message, "Loading users and roles", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to load Users or Roles from Datasource, " + ex.Message, "Loading users and roles", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnAddOneUser_Click(object sender, EventArgs e)
        {
            if (lstUsers.Items.Count > 0)
            {
                if (lstUsers.SelectedIndex > -1)
                {
                    string selectedUser = lstUsers.SelectedItem.ToString();
                    int indexItem = lstUsers.SelectedIndex;

                    lstUserAffectedInRole.Items.Add(selectedUser);
                    lstUsers.Items.RemoveAt(indexItem);
                }
                else
                    btnAddOneUser.Enabled = false;
            }                
        }

        private void btnAddAllUsers_Click(object sender, EventArgs e)
        {
            if(lstUsers.Items.Count > 0)
            {
                IEnumerator enumerate = lstUsers.Items.GetEnumerator();

                while (enumerate.MoveNext())
                {
                    object obj = enumerate.Current;
                    lstUserAffectedInRole.Items.Add(obj);
                }

                lstUsers.Items.Clear();
                btnAddAllUsers.Enabled = false;
                btnRemoveAllUsers.Enabled = true;
            }
        }

        private void btnRemoveAllUsers_Click(object sender, EventArgs e)
        {
            if (lstUserAffectedInRole.Items.Count > 0)
            {
                IEnumerator enumerate = lstUserAffectedInRole.Items.GetEnumerator();

                while (enumerate.MoveNext())
                {
                    object obj = enumerate.Current;
                    lstUsers.Items.Add(obj);
                }

                lstUserAffectedInRole.Items.Clear();
                btnRemoveAllUsers.Enabled = false;
                btnAddAllUsers.Enabled = true;
            }
        }

        private void btnRemoveOneUser_Click(object sender, EventArgs e)
        {
            if (lstUserAffectedInRole.Items.Count > 0)
            {
                if (lstUserAffectedInRole.SelectedIndex > -1)
                {
                    string selectedUser = lstUserAffectedInRole.SelectedItem.ToString();
                    int indexItem = lstUserAffectedInRole.SelectedIndex;

                    lstUsers.Items.Add(selectedUser);
                    lstUserAffectedInRole.Items.RemoveAt(indexItem);
                }
                else
                    btnRemoveOneUser.Enabled = false;
            }
        }

        private void btnLoginUser_Click(object sender, EventArgs e)
        {
            try
            {
                bool status = Membership.ValidateUser(txtUsername.Text, txtPassword.Text);

                if (status)
                {
                    MessageBox.Show("Authenticated successfully", "User authentication", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Failed to authenticate user", "User authentication", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Failed to authenticate user, " + ex.Message, "User authentication", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Failed to authenticate user, " + ex.Message, "User authentication", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to authenticate user, " + ex.Message, "User authentication", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnSaveUsersInRoles_Click(object sender, EventArgs e)
        {
            try
            {
                string[] userTable = null;
                string[] roleTable = null;

                if (rdAddInOneRole.Checked)
                {
                    // We add users in only one selected role
                    roleTable = new string[1];
                    roleTable[0] = lstRoles.SelectedItem.ToString();
                }
                else if (rdAddInAllRoles.Checked)
                {
                    // We add users in all available roles
                    int count1 = 0;
                    int index1 = lstRoles.Items.Count;
                    roleTable = new string[index1];

                    foreach (string str in lstRoles.Items)
                    {
                        roleTable[count1] = str;
                        count1++;
                    }
                }

                int count2 = 0;
                int index2 = lstUserAffectedInRole.Items.Count;
                userTable = new string[index2];

                foreach (string str in lstUserAffectedInRole.Items)
                {
                    userTable[count2] = str;
                    count2++;
                }

                // Make Save
                Roles.AddUsersToRoles(userTable, roleTable);

                MessageBox.Show("User(s) added in role(s) successfully", "Add user(s) in role(s)", MessageBoxButtons.OK, MessageBoxIcon.Information);

                refreshListsUsers();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Unable to add User(s) in Role(s), " + ex.Message, "Add user(s) in role(s)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Unable to add User(s) in Role(s), " + ex.Message, "Add user(s) in role(s)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to add User(s) in Role(s), " + ex.Message, "Add user(s) in role(s)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void lstUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnAddOneUser.Enabled = true;
        }

        private void lstUserAffectedInRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRemoveOneUser.Enabled = true;
        }

        private void btnRefreshList_Click(object sender, EventArgs e)
        {
            try
            {
                refreshListsUsers();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Unable to load Users or Roles from Datasource, " + ex.Message, "Loading users and roles", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Unable to load Users or Roles from Datasource, " + ex.Message, "Loading users and roles", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to load Users or Roles from Datasource, " + ex.Message, "Loading users and roles", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void refreshListsUsers()
        {
            lstUsers.Items.Clear();
            lstRoles.Items.Clear();
            lstUserAffectedInRole.Items.Clear();

            loadListUsers();
            loadListRoles();
        }

        private void loadListRoles()
        {
            string[] roles = Roles.GetAllRoles();

            //lstRoles.DataSource = roles;
            IEnumerator enumerator = roles.GetEnumerator();

            while (enumerator.MoveNext())
            {
                object obj = enumerator.Current;
                lstRoles.Items.Add(obj);
            }
        }

        private void loadListUsers()
        {
            MembershipUserCollection users = Membership.GetAllUsers();
            MembershipUser[] valuesUser = new MembershipUser[users.Count];

            IEnumerator enumerate = users.GetEnumerator();

            while (enumerate.MoveNext())
            {
                object obj = enumerate.Current;
                lstUsers.Items.Add(obj);
            }
        }

        private void btnFinduserInRole_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstRoleName.Items.Count > 0)
                {
                    if (lstRoleName.SelectedIndex > -1)
                    {
                        string selectedRole = lstRoleName.SelectedItem.ToString();
                        string[] usersInRole = Roles.FindUsersInRole(selectedRole, txtUserInRoleFind.Text);

                        if (usersInRole != null)
                        {
                            if (usersInRole.Length > 0)
                            {
                                lstUsersInRoles.Items.Clear();

                                IEnumerator enumerator = usersInRole.GetEnumerator();

                                while (enumerator.MoveNext())
                                {
                                    object obj = enumerator.Current;
                                    lstUsersInRoles.Items.Add(obj);
                                }
                                MessageBox.Show("User(s) finded in role(s) successfully", "Find users in role", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                                MessageBox.Show("Failed to find users in role, No matched found", "Find users in role", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                            MessageBox.Show("Failed to find users in role, No matched found", "Find users in role", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                        MessageBox.Show("Failed to find users in role, No role selected", "Find users in role", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                    MessageBox.Show("Failed to find users in role, No role to available", "Find users in role", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Failed to find users in role, " + ex.Message, "Find users in role", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Failed to find users in role, " + ex.Message, "Find users in role", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to find users in role, " + ex.Message, "Find user in role", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
