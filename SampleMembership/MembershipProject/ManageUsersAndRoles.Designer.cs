namespace MembershipProject
{
    partial class ManageUsersAndRoles
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnUpdateUser = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPasswordAnswer = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPasswordQuestion = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.chkApproved = new System.Windows.Forms.CheckBox();
            this.btnSaveUser = new System.Windows.Forms.Button();
            this.btnDeleteUser = new System.Windows.Forms.Button();
            this.btnNewUser = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnLoginUser = new System.Windows.Forms.Button();
            this.btnRefreshAll = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnRefreshList = new System.Windows.Forms.Button();
            this.btnSaveUsersInRoles = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lstUserAffectedInRole = new System.Windows.Forms.ListBox();
            this.lstRoles = new System.Windows.Forms.ListBox();
            this.btnRemoveOneUser = new System.Windows.Forms.Button();
            this.btnRemoveAllUsers = new System.Windows.Forms.Button();
            this.btnAddAllUsers = new System.Windows.Forms.Button();
            this.rdAddInAllRoles = new System.Windows.Forms.RadioButton();
            this.rdAddInOneRole = new System.Windows.Forms.RadioButton();
            this.lstUsers = new System.Windows.Forms.ListBox();
            this.btnAddOneUser = new System.Windows.Forms.Button();
            this.btnGetPassword = new System.Windows.Forms.Button();
            this.btnChangeUserPassword = new System.Windows.Forms.Button();
            this.btnResetUserPassword = new System.Windows.Forms.Button();
            this.txtNewPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtOldPassword = new System.Windows.Forms.TextBox();
            this.txtUserFind = new System.Windows.Forms.TextBox();
            this.btnFindUser = new System.Windows.Forms.Button();
            this.btnUnlockUser = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lstRoleName = new System.Windows.Forms.ListBox();
            this.lstUsersInRoles = new System.Windows.Forms.ListBox();
            this.txtUserInRoleFind = new System.Windows.Forms.TextBox();
            this.btnFinduserInRole = new System.Windows.Forms.Button();
            this.btnSaveRole = new System.Windows.Forms.Button();
            this.btnNewRole = new System.Windows.Forms.Button();
            this.btnDeleteRole = new System.Windows.Forms.Button();
            this.txtRoleDescription = new System.Windows.Forms.TextBox();
            this.txtRoleId = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.txtRoleName = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.dgvUsers = new System.Windows.Forms.DataGridView();
            this.chkIsLockedOut = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtLastPasswordChagedDate = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtLastActivityDate = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtLastLockedOutDate = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtLastLoginDate = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDateCreate = new System.Windows.Forms.TextBox();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtApplicationName = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            this.SuspendLayout();
            // 
            // btnUpdateUser
            // 
            this.btnUpdateUser.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUpdateUser.Location = new System.Drawing.Point(206, 423);
            this.btnUpdateUser.Name = "btnUpdateUser";
            this.btnUpdateUser.Size = new System.Drawing.Size(79, 23);
            this.btnUpdateUser.TabIndex = 12;
            this.btnUpdateUser.Text = "&Update";
            this.btnUpdateUser.UseVisualStyleBackColor = true;
            this.btnUpdateUser.Click += new System.EventHandler(this.btnUpdateUser_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Username :";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(153, 70);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(217, 20);
            this.txtUsername.TabIndex = 1;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(153, 92);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(217, 20);
            this.txtPassword.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Password :";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(153, 161);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(217, 20);
            this.txtEmail.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 163);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Email :";
            // 
            // txtPasswordAnswer
            // 
            this.txtPasswordAnswer.Location = new System.Drawing.Point(153, 230);
            this.txtPasswordAnswer.Name = "txtPasswordAnswer";
            this.txtPasswordAnswer.Size = new System.Drawing.Size(217, 20);
            this.txtPasswordAnswer.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 233);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Password Answer :";
            // 
            // txtPasswordQuestion
            // 
            this.txtPasswordQuestion.Location = new System.Drawing.Point(153, 207);
            this.txtPasswordQuestion.Name = "txtPasswordQuestion";
            this.txtPasswordQuestion.Size = new System.Drawing.Size(217, 20);
            this.txtPasswordQuestion.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 210);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Password Question :";
            // 
            // txtUserId
            // 
            this.txtUserId.Location = new System.Drawing.Point(153, 48);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.ReadOnly = true;
            this.txtUserId.Size = new System.Drawing.Size(217, 20);
            this.txtUserId.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "User key :";
            // 
            // chkApproved
            // 
            this.chkApproved.AutoSize = true;
            this.chkApproved.Checked = true;
            this.chkApproved.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkApproved.Location = new System.Drawing.Point(39, 396);
            this.chkApproved.Name = "chkApproved";
            this.chkApproved.Size = new System.Drawing.Size(83, 17);
            this.chkApproved.TabIndex = 9;
            this.chkApproved.Text = "Is Approved";
            this.chkApproved.UseVisualStyleBackColor = true;
            // 
            // btnSaveUser
            // 
            this.btnSaveUser.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSaveUser.Location = new System.Drawing.Point(120, 423);
            this.btnSaveUser.Name = "btnSaveUser";
            this.btnSaveUser.Size = new System.Drawing.Size(79, 23);
            this.btnSaveUser.TabIndex = 11;
            this.btnSaveUser.Text = "&Save";
            this.btnSaveUser.UseVisualStyleBackColor = true;
            this.btnSaveUser.Click += new System.EventHandler(this.btnSaveUser_Click);
            // 
            // btnDeleteUser
            // 
            this.btnDeleteUser.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDeleteUser.ForeColor = System.Drawing.Color.Crimson;
            this.btnDeleteUser.Location = new System.Drawing.Point(291, 423);
            this.btnDeleteUser.Name = "btnDeleteUser";
            this.btnDeleteUser.Size = new System.Drawing.Size(79, 23);
            this.btnDeleteUser.TabIndex = 17;
            this.btnDeleteUser.Text = "&Delete";
            this.btnDeleteUser.UseVisualStyleBackColor = true;
            this.btnDeleteUser.Click += new System.EventHandler(this.btnDeleteUser_Click);
            // 
            // btnNewUser
            // 
            this.btnNewUser.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNewUser.Location = new System.Drawing.Point(34, 423);
            this.btnNewUser.Name = "btnNewUser";
            this.btnNewUser.Size = new System.Drawing.Size(79, 23);
            this.btnNewUser.TabIndex = 0;
            this.btnNewUser.Text = "&New";
            this.btnNewUser.UseVisualStyleBackColor = true;
            this.btnNewUser.Click += new System.EventHandler(this.btnNewUser_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLoginUser);
            this.groupBox1.Controls.Add(this.btnRefreshAll);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.btnGetPassword);
            this.groupBox1.Controls.Add(this.btnChangeUserPassword);
            this.groupBox1.Controls.Add(this.btnResetUserPassword);
            this.groupBox1.Controls.Add(this.txtNewPassword);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtOldPassword);
            this.groupBox1.Controls.Add(this.txtUserFind);
            this.groupBox1.Controls.Add(this.btnFindUser);
            this.groupBox1.Controls.Add(this.btnUnlockUser);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.dgvUsers);
            this.groupBox1.Controls.Add(this.chkIsLockedOut);
            this.groupBox1.Controls.Add(this.btnSaveUser);
            this.groupBox1.Controls.Add(this.btnNewUser);
            this.groupBox1.Controls.Add(this.btnDeleteUser);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.txtLastPasswordChagedDate);
            this.groupBox1.Controls.Add(this.btnUpdateUser);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txtLastActivityDate);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtLastLockedOutDate);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtLastLoginDate);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtDateCreate);
            this.groupBox1.Controls.Add(this.txtComment);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtApplicationName);
            this.groupBox1.Controls.Add(this.txtUserId);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtUsername);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.chkApproved);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtEmail);
            this.groupBox1.Controls.Add(this.txtPasswordAnswer);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtPasswordQuestion);
            this.groupBox1.Location = new System.Drawing.Point(8, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(917, 626);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Users";
            // 
            // btnLoginUser
            // 
            this.btnLoginUser.BackColor = System.Drawing.Color.Honeydew;
            this.btnLoginUser.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLoginUser.ForeColor = System.Drawing.Color.Green;
            this.btnLoginUser.Location = new System.Drawing.Point(422, 12);
            this.btnLoginUser.Name = "btnLoginUser";
            this.btnLoginUser.Size = new System.Drawing.Size(70, 22);
            this.btnLoginUser.TabIndex = 52;
            this.btnLoginUser.Text = "Lo&in user";
            this.btnLoginUser.UseVisualStyleBackColor = false;
            this.btnLoginUser.Click += new System.EventHandler(this.btnLoginUser_Click);
            // 
            // btnRefreshAll
            // 
            this.btnRefreshAll.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRefreshAll.Location = new System.Drawing.Point(498, 12);
            this.btnRefreshAll.Name = "btnRefreshAll";
            this.btnRefreshAll.Size = new System.Drawing.Size(70, 22);
            this.btnRefreshAll.TabIndex = 51;
            this.btnRefreshAll.Text = "Refres&h All";
            this.btnRefreshAll.UseVisualStyleBackColor = true;
            this.btnRefreshAll.Click += new System.EventHandler(this.btnRefreshAll_Click);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(7, 141);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(83, 13);
            this.label21.TabIndex = 45;
            this.label21.Text = "New password :";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnRefreshList);
            this.groupBox3.Controls.Add(this.btnSaveUsersInRoles);
            this.groupBox3.Controls.Add(this.label20);
            this.groupBox3.Controls.Add(this.label19);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.lstUserAffectedInRole);
            this.groupBox3.Controls.Add(this.lstRoles);
            this.groupBox3.Controls.Add(this.btnRemoveOneUser);
            this.groupBox3.Controls.Add(this.btnRemoveAllUsers);
            this.groupBox3.Controls.Add(this.btnAddAllUsers);
            this.groupBox3.Controls.Add(this.rdAddInAllRoles);
            this.groupBox3.Controls.Add(this.rdAddInOneRole);
            this.groupBox3.Controls.Add(this.lstUsers);
            this.groupBox3.Controls.Add(this.btnAddOneUser);
            this.groupBox3.Location = new System.Drawing.Point(376, 253);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(535, 223);
            this.groupBox3.TabIndex = 44;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Users in roles";
            // 
            // btnRefreshList
            // 
            this.btnRefreshList.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRefreshList.Location = new System.Drawing.Point(236, 191);
            this.btnRefreshList.Name = "btnRefreshList";
            this.btnRefreshList.Size = new System.Drawing.Size(68, 23);
            this.btnRefreshList.TabIndex = 67;
            this.btnRefreshList.Text = "Refres&h";
            this.btnRefreshList.UseVisualStyleBackColor = true;
            this.btnRefreshList.Click += new System.EventHandler(this.btnRefreshList_Click);
            // 
            // btnSaveUsersInRoles
            // 
            this.btnSaveUsersInRoles.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSaveUsersInRoles.Location = new System.Drawing.Point(310, 191);
            this.btnSaveUsersInRoles.Name = "btnSaveUsersInRoles";
            this.btnSaveUsersInRoles.Size = new System.Drawing.Size(68, 23);
            this.btnSaveUsersInRoles.TabIndex = 33;
            this.btnSaveUsersInRoles.Text = "Sav&e";
            this.btnSaveUsersInRoles.UseVisualStyleBackColor = true;
            this.btnSaveUsersInRoles.Click += new System.EventHandler(this.btnSaveUsersInRoles_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.ForeColor = System.Drawing.Color.SaddleBrown;
            this.label20.Location = new System.Drawing.Point(381, 15);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(157, 13);
            this.label20.TabIndex = 66;
            this.label20.Text = "User to affected in chosed roles";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.ForeColor = System.Drawing.Color.SaddleBrown;
            this.label19.Location = new System.Drawing.Point(233, 15);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(68, 13);
            this.label19.TabIndex = 65;
            this.label19.Text = "Roles names";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ForeColor = System.Drawing.Color.SaddleBrown;
            this.label18.Location = new System.Drawing.Point(7, 15);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(55, 13);
            this.label18.TabIndex = 45;
            this.label18.Text = "Username";
            // 
            // lstUserAffectedInRole
            // 
            this.lstUserAffectedInRole.FormattingEnabled = true;
            this.lstUserAffectedInRole.Location = new System.Drawing.Point(385, 31);
            this.lstUserAffectedInRole.Name = "lstUserAffectedInRole";
            this.lstUserAffectedInRole.Size = new System.Drawing.Size(143, 186);
            this.lstUserAffectedInRole.TabIndex = 64;
            this.lstUserAffectedInRole.SelectedIndexChanged += new System.EventHandler(this.lstUserAffectedInRole_SelectedIndexChanged);
            // 
            // lstRoles
            // 
            this.lstRoles.FormattingEnabled = true;
            this.lstRoles.Location = new System.Drawing.Point(236, 46);
            this.lstRoles.Name = "lstRoles";
            this.lstRoles.Size = new System.Drawing.Size(143, 134);
            this.lstRoles.TabIndex = 63;
            // 
            // btnRemoveOneUser
            // 
            this.btnRemoveOneUser.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRemoveOneUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveOneUser.Location = new System.Drawing.Point(173, 152);
            this.btnRemoveOneUser.Name = "btnRemoveOneUser";
            this.btnRemoveOneUser.Size = new System.Drawing.Size(43, 23);
            this.btnRemoveOneUser.TabIndex = 30;
            this.btnRemoveOneUser.Text = "<<";
            this.btnRemoveOneUser.UseVisualStyleBackColor = true;
            this.btnRemoveOneUser.Click += new System.EventHandler(this.btnRemoveOneUser_Click);
            // 
            // btnRemoveAllUsers
            // 
            this.btnRemoveAllUsers.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRemoveAllUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveAllUsers.Location = new System.Drawing.Point(173, 123);
            this.btnRemoveAllUsers.Name = "btnRemoveAllUsers";
            this.btnRemoveAllUsers.Size = new System.Drawing.Size(43, 23);
            this.btnRemoveAllUsers.TabIndex = 32;
            this.btnRemoveAllUsers.Text = "<<<<";
            this.btnRemoveAllUsers.UseVisualStyleBackColor = true;
            this.btnRemoveAllUsers.Click += new System.EventHandler(this.btnRemoveAllUsers_Click);
            // 
            // btnAddAllUsers
            // 
            this.btnAddAllUsers.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddAllUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddAllUsers.Location = new System.Drawing.Point(173, 94);
            this.btnAddAllUsers.Name = "btnAddAllUsers";
            this.btnAddAllUsers.Size = new System.Drawing.Size(43, 23);
            this.btnAddAllUsers.TabIndex = 31;
            this.btnAddAllUsers.Text = ">>>>";
            this.btnAddAllUsers.UseVisualStyleBackColor = true;
            this.btnAddAllUsers.Click += new System.EventHandler(this.btnAddAllUsers_Click);
            // 
            // rdAddInAllRoles
            // 
            this.rdAddInAllRoles.AutoSize = true;
            this.rdAddInAllRoles.Location = new System.Drawing.Point(154, 41);
            this.rdAddInAllRoles.Name = "rdAddInAllRoles";
            this.rdAddInAllRoles.Size = new System.Drawing.Size(78, 17);
            this.rdAddInAllRoles.TabIndex = 28;
            this.rdAddInAllRoles.TabStop = true;
            this.rdAddInAllRoles.Text = "In All Roles";
            this.rdAddInAllRoles.UseVisualStyleBackColor = true;
            // 
            // rdAddInOneRole
            // 
            this.rdAddInOneRole.AutoSize = true;
            this.rdAddInOneRole.Location = new System.Drawing.Point(154, 18);
            this.rdAddInOneRole.Name = "rdAddInOneRole";
            this.rdAddInOneRole.Size = new System.Drawing.Size(82, 17);
            this.rdAddInOneRole.TabIndex = 27;
            this.rdAddInOneRole.TabStop = true;
            this.rdAddInOneRole.Text = "In One Role";
            this.rdAddInOneRole.UseVisualStyleBackColor = true;
            // 
            // lstUsers
            // 
            this.lstUsers.FormattingEnabled = true;
            this.lstUsers.Location = new System.Drawing.Point(7, 31);
            this.lstUsers.Name = "lstUsers";
            this.lstUsers.Size = new System.Drawing.Size(143, 186);
            this.lstUsers.TabIndex = 55;
            this.lstUsers.SelectedIndexChanged += new System.EventHandler(this.lstUsers_SelectedIndexChanged);
            // 
            // btnAddOneUser
            // 
            this.btnAddOneUser.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddOneUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddOneUser.Location = new System.Drawing.Point(173, 65);
            this.btnAddOneUser.Name = "btnAddOneUser";
            this.btnAddOneUser.Size = new System.Drawing.Size(43, 23);
            this.btnAddOneUser.TabIndex = 29;
            this.btnAddOneUser.Text = ">>";
            this.btnAddOneUser.UseVisualStyleBackColor = true;
            this.btnAddOneUser.Click += new System.EventHandler(this.btnAddOneUser_Click);
            // 
            // btnGetPassword
            // 
            this.btnGetPassword.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGetPassword.Location = new System.Drawing.Point(291, 453);
            this.btnGetPassword.Name = "btnGetPassword";
            this.btnGetPassword.Size = new System.Drawing.Size(79, 23);
            this.btnGetPassword.TabIndex = 16;
            this.btnGetPassword.Text = "&Get pwd.";
            this.btnGetPassword.UseVisualStyleBackColor = true;
            this.btnGetPassword.Click += new System.EventHandler(this.btnGetPassword_Click);
            // 
            // btnChangeUserPassword
            // 
            this.btnChangeUserPassword.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnChangeUserPassword.Location = new System.Drawing.Point(206, 453);
            this.btnChangeUserPassword.Name = "btnChangeUserPassword";
            this.btnChangeUserPassword.Size = new System.Drawing.Size(79, 23);
            this.btnChangeUserPassword.TabIndex = 15;
            this.btnChangeUserPassword.Text = "&Change pwd.";
            this.btnChangeUserPassword.UseVisualStyleBackColor = true;
            this.btnChangeUserPassword.Click += new System.EventHandler(this.btnChangeUserPassword_Click);
            // 
            // btnResetUserPassword
            // 
            this.btnResetUserPassword.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnResetUserPassword.Location = new System.Drawing.Point(120, 453);
            this.btnResetUserPassword.Name = "btnResetUserPassword";
            this.btnResetUserPassword.Size = new System.Drawing.Size(79, 23);
            this.btnResetUserPassword.TabIndex = 14;
            this.btnResetUserPassword.Text = "&Reset pwd.";
            this.btnResetUserPassword.UseVisualStyleBackColor = true;
            this.btnResetUserPassword.Click += new System.EventHandler(this.btnResetUserPassword_Click);
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.Location = new System.Drawing.Point(153, 138);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.Size = new System.Drawing.Size(217, 20);
            this.txtNewPassword.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 37;
            this.label3.Text = "Old password :";
            // 
            // txtOldPassword
            // 
            this.txtOldPassword.Location = new System.Drawing.Point(153, 115);
            this.txtOldPassword.Name = "txtOldPassword";
            this.txtOldPassword.Size = new System.Drawing.Size(217, 20);
            this.txtOldPassword.TabIndex = 3;
            // 
            // txtUserFind
            // 
            this.txtUserFind.Location = new System.Drawing.Point(9, 14);
            this.txtUserFind.Name = "txtUserFind";
            this.txtUserFind.Size = new System.Drawing.Size(306, 20);
            this.txtUserFind.TabIndex = 18;
            // 
            // btnFindUser
            // 
            this.btnFindUser.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFindUser.Location = new System.Drawing.Point(319, 12);
            this.btnFindUser.Name = "btnFindUser";
            this.btnFindUser.Size = new System.Drawing.Size(51, 23);
            this.btnFindUser.TabIndex = 19;
            this.btnFindUser.Text = "&Find";
            this.btnFindUser.UseVisualStyleBackColor = true;
            this.btnFindUser.Click += new System.EventHandler(this.btnFindUser_Click);
            // 
            // btnUnlockUser
            // 
            this.btnUnlockUser.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUnlockUser.Location = new System.Drawing.Point(34, 453);
            this.btnUnlockUser.Name = "btnUnlockUser";
            this.btnUnlockUser.Size = new System.Drawing.Size(79, 23);
            this.btnUnlockUser.TabIndex = 13;
            this.btnUnlockUser.Text = "Un&lock";
            this.btnUnlockUser.UseVisualStyleBackColor = true;
            this.btnUnlockUser.Click += new System.EventHandler(this.btnUnlockUser_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lstRoleName);
            this.groupBox2.Controls.Add(this.lstUsersInRoles);
            this.groupBox2.Controls.Add(this.txtUserInRoleFind);
            this.groupBox2.Controls.Add(this.btnFinduserInRole);
            this.groupBox2.Controls.Add(this.btnSaveRole);
            this.groupBox2.Controls.Add(this.btnNewRole);
            this.groupBox2.Controls.Add(this.btnDeleteRole);
            this.groupBox2.Controls.Add(this.txtRoleDescription);
            this.groupBox2.Controls.Add(this.txtRoleId);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.txtRoleName);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Location = new System.Drawing.Point(376, 33);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(535, 217);
            this.groupBox2.TabIndex = 33;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Roles";
            // 
            // lstRoleName
            // 
            this.lstRoleName.FormattingEnabled = true;
            this.lstRoleName.Location = new System.Drawing.Point(81, 140);
            this.lstRoleName.Name = "lstRoleName";
            this.lstRoleName.Size = new System.Drawing.Size(448, 69);
            this.lstRoleName.TabIndex = 66;
            this.lstRoleName.SelectedIndexChanged += new System.EventHandler(this.lstRoleName_SelectedIndexChanged);
            // 
            // lstUsersInRoles
            // 
            this.lstUsersInRoles.FormattingEnabled = true;
            this.lstUsersInRoles.Location = new System.Drawing.Point(316, 42);
            this.lstUsersInRoles.Name = "lstUsersInRoles";
            this.lstUsersInRoles.Size = new System.Drawing.Size(213, 95);
            this.lstUsersInRoles.TabIndex = 54;
            // 
            // txtUserInRoleFind
            // 
            this.txtUserInRoleFind.Location = new System.Drawing.Point(6, 15);
            this.txtUserInRoleFind.Name = "txtUserInRoleFind";
            this.txtUserInRoleFind.Size = new System.Drawing.Size(306, 20);
            this.txtUserInRoleFind.TabIndex = 25;
            // 
            // btnFinduserInRole
            // 
            this.btnFinduserInRole.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFinduserInRole.Location = new System.Drawing.Point(316, 13);
            this.btnFinduserInRole.Name = "btnFinduserInRole";
            this.btnFinduserInRole.Size = new System.Drawing.Size(101, 23);
            this.btnFinduserInRole.TabIndex = 26;
            this.btnFinduserInRole.Text = "Find users in r&oles";
            this.btnFinduserInRole.UseVisualStyleBackColor = true;
            this.btnFinduserInRole.Click += new System.EventHandler(this.btnFinduserInRole_Click);
            // 
            // btnSaveRole
            // 
            this.btnSaveRole.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSaveRole.Location = new System.Drawing.Point(6, 163);
            this.btnSaveRole.Name = "btnSaveRole";
            this.btnSaveRole.Size = new System.Drawing.Size(71, 23);
            this.btnSaveRole.TabIndex = 23;
            this.btnSaveRole.Text = "Sa&ve";
            this.btnSaveRole.UseVisualStyleBackColor = true;
            this.btnSaveRole.Click += new System.EventHandler(this.btnSaveRole_Click);
            // 
            // btnNewRole
            // 
            this.btnNewRole.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNewRole.Location = new System.Drawing.Point(6, 138);
            this.btnNewRole.Name = "btnNewRole";
            this.btnNewRole.Size = new System.Drawing.Size(71, 23);
            this.btnNewRole.TabIndex = 20;
            this.btnNewRole.Text = "Ne&w";
            this.btnNewRole.UseVisualStyleBackColor = true;
            this.btnNewRole.Click += new System.EventHandler(this.btnNewRole_Click);
            // 
            // btnDeleteRole
            // 
            this.btnDeleteRole.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDeleteRole.ForeColor = System.Drawing.Color.Crimson;
            this.btnDeleteRole.Location = new System.Drawing.Point(6, 188);
            this.btnDeleteRole.Name = "btnDeleteRole";
            this.btnDeleteRole.Size = new System.Drawing.Size(71, 23);
            this.btnDeleteRole.TabIndex = 24;
            this.btnDeleteRole.Text = "Dele&te";
            this.btnDeleteRole.UseVisualStyleBackColor = true;
            this.btnDeleteRole.Click += new System.EventHandler(this.btnDeleteRole_Click);
            // 
            // txtRoleDescription
            // 
            this.txtRoleDescription.Location = new System.Drawing.Point(81, 91);
            this.txtRoleDescription.Multiline = true;
            this.txtRoleDescription.Name = "txtRoleDescription";
            this.txtRoleDescription.Size = new System.Drawing.Size(229, 45);
            this.txtRoleDescription.TabIndex = 22;
            // 
            // txtRoleId
            // 
            this.txtRoleId.Location = new System.Drawing.Point(81, 46);
            this.txtRoleId.Name = "txtRoleId";
            this.txtRoleId.ReadOnly = true;
            this.txtRoleId.Size = new System.Drawing.Size(229, 20);
            this.txtRoleId.TabIndex = 47;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(7, 94);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(66, 13);
            this.label15.TabIndex = 48;
            this.label15.Text = "Description :";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(7, 50);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(55, 13);
            this.label17.TabIndex = 46;
            this.label17.Text = "Role key :";
            // 
            // txtRoleName
            // 
            this.txtRoleName.Location = new System.Drawing.Point(81, 68);
            this.txtRoleName.Name = "txtRoleName";
            this.txtRoleName.Size = new System.Drawing.Size(229, 20);
            this.txtRoleName.TabIndex = 21;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(7, 71);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(64, 13);
            this.label16.TabIndex = 44;
            this.label16.Text = "Role name :";
            // 
            // dgvUsers
            // 
            this.dgvUsers.AllowUserToAddRows = false;
            this.dgvUsers.AllowUserToDeleteRows = false;
            this.dgvUsers.AllowUserToOrderColumns = true;
            this.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsers.Location = new System.Drawing.Point(9, 484);
            this.dgvUsers.Name = "dgvUsers";
            this.dgvUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsers.Size = new System.Drawing.Size(902, 135);
            this.dgvUsers.TabIndex = 50;
            this.dgvUsers.SelectionChanged += new System.EventHandler(this.dgvUsers_SelectionChanged);
            // 
            // chkIsLockedOut
            // 
            this.chkIsLockedOut.AutoSize = true;
            this.chkIsLockedOut.Location = new System.Drawing.Point(122, 396);
            this.chkIsLockedOut.Name = "chkIsLockedOut";
            this.chkIsLockedOut.Size = new System.Drawing.Size(90, 17);
            this.chkIsLockedOut.TabIndex = 10;
            this.chkIsLockedOut.Text = "Is LockedOut";
            this.chkIsLockedOut.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(7, 373);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(145, 13);
            this.label13.TabIndex = 29;
            this.label13.Text = "Last pasword changed date :";
            // 
            // txtLastPasswordChagedDate
            // 
            this.txtLastPasswordChagedDate.Location = new System.Drawing.Point(153, 370);
            this.txtLastPasswordChagedDate.Name = "txtLastPasswordChagedDate";
            this.txtLastPasswordChagedDate.ReadOnly = true;
            this.txtLastPasswordChagedDate.Size = new System.Drawing.Size(217, 20);
            this.txtLastPasswordChagedDate.TabIndex = 30;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(7, 327);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(93, 13);
            this.label12.TabIndex = 27;
            this.label12.Text = "Last activity date :";
            // 
            // txtLastActivityDate
            // 
            this.txtLastActivityDate.Location = new System.Drawing.Point(153, 324);
            this.txtLastActivityDate.Name = "txtLastActivityDate";
            this.txtLastActivityDate.ReadOnly = true;
            this.txtLastActivityDate.Size = new System.Drawing.Size(217, 20);
            this.txtLastActivityDate.TabIndex = 28;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 350);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(109, 13);
            this.label11.TabIndex = 25;
            this.label11.Text = "Last lockedOut date :";
            // 
            // txtLastLockedOutDate
            // 
            this.txtLastLockedOutDate.Location = new System.Drawing.Point(153, 347);
            this.txtLastLockedOutDate.Name = "txtLastLockedOutDate";
            this.txtLastLockedOutDate.ReadOnly = true;
            this.txtLastLockedOutDate.Size = new System.Drawing.Size(217, 20);
            this.txtLastLockedOutDate.TabIndex = 26;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 304);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(82, 13);
            this.label10.TabIndex = 23;
            this.label10.Text = "Last login date :";
            // 
            // txtLastLoginDate
            // 
            this.txtLastLoginDate.Location = new System.Drawing.Point(153, 301);
            this.txtLastLoginDate.Name = "txtLastLoginDate";
            this.txtLastLoginDate.ReadOnly = true;
            this.txtLastLoginDate.Size = new System.Drawing.Size(217, 20);
            this.txtLastLoginDate.TabIndex = 24;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 187);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "Created date :";
            // 
            // txtDateCreate
            // 
            this.txtDateCreate.Location = new System.Drawing.Point(153, 184);
            this.txtDateCreate.Name = "txtDateCreate";
            this.txtDateCreate.ReadOnly = true;
            this.txtDateCreate.Size = new System.Drawing.Size(217, 20);
            this.txtDateCreate.TabIndex = 22;
            // 
            // txtComment
            // 
            this.txtComment.Location = new System.Drawing.Point(153, 253);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(217, 45);
            this.txtComment.TabIndex = 8;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 256);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Comment :";
            // 
            // txtApplicationName
            // 
            this.txtApplicationName.Location = new System.Drawing.Point(678, 13);
            this.txtApplicationName.Name = "txtApplicationName";
            this.txtApplicationName.ReadOnly = true;
            this.txtApplicationName.Size = new System.Drawing.Size(231, 20);
            this.txtApplicationName.TabIndex = 15;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(578, 17);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(94, 13);
            this.label14.TabIndex = 14;
            this.label14.Text = "Application name :";
            // 
            // ManageUsersAndRoles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 636);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.Name = "ManageUsersAndRoles";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manage users and roles";
            this.Load += new System.EventHandler(this.ManageUsers_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnUpdateUser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPasswordAnswer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPasswordQuestion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtUserId;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkApproved;
        private System.Windows.Forms.Button btnSaveUser;
        private System.Windows.Forms.Button btnDeleteUser;
        private System.Windows.Forms.Button btnNewUser;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvUsers;
        private System.Windows.Forms.CheckBox chkIsLockedOut;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtLastPasswordChagedDate;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtLastActivityDate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtLastLockedOutDate;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtLastLoginDate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtDateCreate;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnUnlockUser;
        private System.Windows.Forms.Button btnFindUser;
        private System.Windows.Forms.TextBox txtUserFind;
        private System.Windows.Forms.TextBox txtNewPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtOldPassword;
        private System.Windows.Forms.Button btnChangeUserPassword;
        private System.Windows.Forms.Button btnResetUserPassword;
        private System.Windows.Forms.Button btnGetPassword;
        private System.Windows.Forms.TextBox txtApplicationName;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtRoleId;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtRoleName;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnSaveRole;
        private System.Windows.Forms.Button btnNewRole;
        private System.Windows.Forms.Button btnDeleteRole;
        private System.Windows.Forms.TextBox txtUserInRoleFind;
        private System.Windows.Forms.Button btnFinduserInRole;
        private System.Windows.Forms.ListBox lstUsersInRoles;
        private System.Windows.Forms.RadioButton rdAddInAllRoles;
        private System.Windows.Forms.RadioButton rdAddInOneRole;
        private System.Windows.Forms.ListBox lstUsers;
        private System.Windows.Forms.Button btnAddOneUser;
        private System.Windows.Forms.ListBox lstUserAffectedInRole;
        private System.Windows.Forms.ListBox lstRoles;
        private System.Windows.Forms.Button btnRemoveOneUser;
        private System.Windows.Forms.Button btnRemoveAllUsers;
        private System.Windows.Forms.Button btnAddAllUsers;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button btnSaveUsersInRoles;
        private System.Windows.Forms.TextBox txtRoleDescription;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ListBox lstRoleName;
        private System.Windows.Forms.Button btnRefreshAll;
        private System.Windows.Forms.Button btnLoginUser;
        private System.Windows.Forms.Button btnRefreshList;
    }
}

