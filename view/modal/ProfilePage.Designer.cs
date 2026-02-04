namespace LibraryManagementSystem.view
{
    partial class ProfilePage
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
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.panelProfileCard = new System.Windows.Forms.Panel();
            this.pictureBoxProfile = new System.Windows.Forms.PictureBox();
            this.lblFullName = new System.Windows.Forms.Label();
            this.panelInfoSection = new System.Windows.Forms.Panel();
            this.lblFirstNameLabel = new System.Windows.Forms.Label();
            this.lblFirstNameValue = new System.Windows.Forms.Label();
            this.panelDivider1 = new System.Windows.Forms.Panel();
            this.lblLastNameLabel = new System.Windows.Forms.Label();
            this.lblLastNameValue = new System.Windows.Forms.Label();
            this.panelDivider2 = new System.Windows.Forms.Panel();
            this.lblEmailLabel = new System.Windows.Forms.Label();
            this.lblEmailValue = new System.Windows.Forms.Label();
            this.panelDivider3 = new System.Windows.Forms.Panel();
            this.lblContactLabel = new System.Windows.Forms.Label();
            this.lblContactValue = new System.Windows.Forms.Label();
            this.panelActionButtons = new System.Windows.Forms.Panel();
            this.btnEditProfile = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnChangePassword = new System.Windows.Forms.Button();
            this.panelHeader.SuspendLayout();
            this.panelProfileCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxProfile)).BeginInit();
            this.panelInfoSection.SuspendLayout();
            this.panelActionButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.panelHeader.Controls.Add(this.lblPageTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(600, 70);
            this.panelHeader.TabIndex = 0;
            // 
            // lblPageTitle
            // 
            this.lblPageTitle.AutoSize = true;
            this.lblPageTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPageTitle.ForeColor = System.Drawing.Color.White;
            this.lblPageTitle.Location = new System.Drawing.Point(20, 18);
            this.lblPageTitle.Name = "lblPageTitle";
            this.lblPageTitle.Size = new System.Drawing.Size(186, 46);
            this.lblPageTitle.TabIndex = 0;
            this.lblPageTitle.Text = "My Profile";
            // 
            // panelProfileCard
            // 
            this.panelProfileCard.BackColor = System.Drawing.Color.White;
            this.panelProfileCard.Controls.Add(this.pictureBoxProfile);
            this.panelProfileCard.Controls.Add(this.lblFullName);
            this.panelProfileCard.Location = new System.Drawing.Point(75, 95);
            this.panelProfileCard.Name = "panelProfileCard";
            this.panelProfileCard.Size = new System.Drawing.Size(450, 160);
            this.panelProfileCard.TabIndex = 1;
            // 
            // pictureBoxProfile
            // 
            this.pictureBoxProfile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pictureBoxProfile.Image = global::LibraryManagementSystem.Properties.Resources.account;
            this.pictureBoxProfile.Location = new System.Drawing.Point(155, 15);
            this.pictureBoxProfile.Name = "pictureBoxProfile";
            this.pictureBoxProfile.Size = new System.Drawing.Size(140, 90);
            this.pictureBoxProfile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxProfile.TabIndex = 0;
            this.pictureBoxProfile.TabStop = false;
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFullName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblFullName.Location = new System.Drawing.Point(80, 118);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(139, 32);
            this.lblFullName.TabIndex = 1;
            this.lblFullName.Text = "User Name";
            this.lblFullName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelInfoSection
            // 
            this.panelInfoSection.BackColor = System.Drawing.Color.White;
            this.panelInfoSection.Controls.Add(this.lblFirstNameLabel);
            this.panelInfoSection.Controls.Add(this.lblFirstNameValue);
            this.panelInfoSection.Controls.Add(this.panelDivider1);
            this.panelInfoSection.Controls.Add(this.lblLastNameLabel);
            this.panelInfoSection.Controls.Add(this.lblLastNameValue);
            this.panelInfoSection.Controls.Add(this.panelDivider2);
            this.panelInfoSection.Controls.Add(this.lblEmailLabel);
            this.panelInfoSection.Controls.Add(this.lblEmailValue);
            this.panelInfoSection.Controls.Add(this.panelDivider3);
            this.panelInfoSection.Controls.Add(this.lblContactLabel);
            this.panelInfoSection.Controls.Add(this.lblContactValue);
            this.panelInfoSection.Location = new System.Drawing.Point(50, 280);
            this.panelInfoSection.Name = "panelInfoSection";
            this.panelInfoSection.Size = new System.Drawing.Size(500, 280);
            this.panelInfoSection.TabIndex = 2;
            // 
            // lblFirstNameLabel
            // 
            this.lblFirstNameLabel.AutoSize = true;
            this.lblFirstNameLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFirstNameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.lblFirstNameLabel.Location = new System.Drawing.Point(25, 15);
            this.lblFirstNameLabel.Name = "lblFirstNameLabel";
            this.lblFirstNameLabel.Size = new System.Drawing.Size(92, 23);
            this.lblFirstNameLabel.TabIndex = 0;
            this.lblFirstNameLabel.Text = "First Name";
            // 
            // lblFirstNameValue
            // 
            this.lblFirstNameValue.AutoSize = true;
            this.lblFirstNameValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFirstNameValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblFirstNameValue.Location = new System.Drawing.Point(25, 35);
            this.lblFirstNameValue.Name = "lblFirstNameValue";
            this.lblFirstNameValue.Size = new System.Drawing.Size(52, 25);
            this.lblFirstNameValue.TabIndex = 1;
            this.lblFirstNameValue.Text = "John";
            // 
            // panelDivider1
            // 
            this.panelDivider1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.panelDivider1.Location = new System.Drawing.Point(0, 60);
            this.panelDivider1.Name = "panelDivider1";
            this.panelDivider1.Size = new System.Drawing.Size(500, 1);
            this.panelDivider1.TabIndex = 2;
            // 
            // lblLastNameLabel
            // 
            this.lblLastNameLabel.AutoSize = true;
            this.lblLastNameLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastNameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.lblLastNameLabel.Location = new System.Drawing.Point(25, 75);
            this.lblLastNameLabel.Name = "lblLastNameLabel";
            this.lblLastNameLabel.Size = new System.Drawing.Size(91, 23);
            this.lblLastNameLabel.TabIndex = 3;
            this.lblLastNameLabel.Text = "Last Name";
            // 
            // lblLastNameValue
            // 
            this.lblLastNameValue.AutoSize = true;
            this.lblLastNameValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastNameValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblLastNameValue.Location = new System.Drawing.Point(25, 95);
            this.lblLastNameValue.Name = "lblLastNameValue";
            this.lblLastNameValue.Size = new System.Drawing.Size(46, 25);
            this.lblLastNameValue.TabIndex = 4;
            this.lblLastNameValue.Text = "Doe";
            // 
            // panelDivider2
            // 
            this.panelDivider2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.panelDivider2.Location = new System.Drawing.Point(0, 120);
            this.panelDivider2.Name = "panelDivider2";
            this.panelDivider2.Size = new System.Drawing.Size(500, 1);
            this.panelDivider2.TabIndex = 5;
            // 
            // lblEmailLabel
            // 
            this.lblEmailLabel.AutoSize = true;
            this.lblEmailLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmailLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.lblEmailLabel.Location = new System.Drawing.Point(25, 135);
            this.lblEmailLabel.Name = "lblEmailLabel";
            this.lblEmailLabel.Size = new System.Drawing.Size(51, 23);
            this.lblEmailLabel.TabIndex = 6;
            this.lblEmailLabel.Text = "Email";
            // 
            // lblEmailValue
            // 
            this.lblEmailValue.AutoSize = true;
            this.lblEmailValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmailValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblEmailValue.Location = new System.Drawing.Point(25, 155);
            this.lblEmailValue.Name = "lblEmailValue";
            this.lblEmailValue.Size = new System.Drawing.Size(179, 25);
            this.lblEmailValue.TabIndex = 7;
            this.lblEmailValue.Text = "john@example.com";
            // 
            // panelDivider3
            // 
            this.panelDivider3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.panelDivider3.Location = new System.Drawing.Point(0, 180);
            this.panelDivider3.Name = "panelDivider3";
            this.panelDivider3.Size = new System.Drawing.Size(500, 1);
            this.panelDivider3.TabIndex = 8;
            // 
            // lblContactLabel
            // 
            this.lblContactLabel.AutoSize = true;
            this.lblContactLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContactLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.lblContactLabel.Location = new System.Drawing.Point(25, 195);
            this.lblContactLabel.Name = "lblContactLabel";
            this.lblContactLabel.Size = new System.Drawing.Size(138, 23);
            this.lblContactLabel.TabIndex = 9;
            this.lblContactLabel.Text = "Contact Number";
            // 
            // lblContactValue
            // 
            this.lblContactValue.AutoSize = true;
            this.lblContactValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContactValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblContactValue.Location = new System.Drawing.Point(25, 215);
            this.lblContactValue.Name = "lblContactValue";
            this.lblContactValue.Size = new System.Drawing.Size(165, 25);
            this.lblContactValue.TabIndex = 10;
            this.lblContactValue.Text = "+1 (555) 123-4567";
            // 
            // panelActionButtons
            // 
            this.panelActionButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.panelActionButtons.Controls.Add(this.btnEditProfile);
            this.panelActionButtons.Controls.Add(this.btnChangePassword);
            this.panelActionButtons.Controls.Add(this.btnBack);
            this.panelActionButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelActionButtons.Location = new System.Drawing.Point(0, 600);
            this.panelActionButtons.Name = "panelActionButtons";
            this.panelActionButtons.Size = new System.Drawing.Size(600, 100);
            this.panelActionButtons.TabIndex = 3;
            // 
            // btnEditProfile
            // 
            this.btnEditProfile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnEditProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditProfile.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditProfile.ForeColor = System.Drawing.Color.White;
            this.btnEditProfile.Location = new System.Drawing.Point(50, 20);
            this.btnEditProfile.Name = "btnEditProfile";
            this.btnEditProfile.Size = new System.Drawing.Size(150, 45);
            this.btnEditProfile.TabIndex = 0;
            this.btnEditProfile.Text = "Edit Profile";
            this.btnEditProfile.UseVisualStyleBackColor = false;
            this.btnEditProfile.Click += new System.EventHandler(this.btnEditProfile_Click);
            this.btnEditProfile.MouseEnter += new System.EventHandler(this.BtnEditProfile_MouseEnter);
            this.btnEditProfile.MouseLeave += new System.EventHandler(this.BtnEditProfile_MouseLeave);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(450, 20);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(100, 45);
            this.btnBack.TabIndex = 2;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            this.btnBack.MouseEnter += new System.EventHandler(this.BtnBack_MouseEnter);
            this.btnBack.MouseLeave += new System.EventHandler(this.BtnBack_MouseLeave);
            // 
            // btnChangePassword
            // 
            this.btnChangePassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnChangePassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangePassword.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangePassword.ForeColor = System.Drawing.Color.White;
            this.btnChangePassword.Location = new System.Drawing.Point(225, 20);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Size = new System.Drawing.Size(170, 45);
            this.btnChangePassword.TabIndex = 1;
            this.btnChangePassword.Text = "Change Password";
            this.btnChangePassword.UseVisualStyleBackColor = false;
            this.btnChangePassword.Click += new System.EventHandler(this.btnChangePassword_Click);
            this.btnChangePassword.MouseEnter += new System.EventHandler(this.BtnChangePassword_MouseEnter);
            this.btnChangePassword.MouseLeave += new System.EventHandler(this.BtnChangePassword_MouseLeave);
            // 
            // ProfilePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(600, 700);
            this.Controls.Add(this.panelActionButtons);
            this.Controls.Add(this.panelInfoSection);
            this.Controls.Add(this.panelProfileCard);
            this.Controls.Add(this.panelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ProfilePage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Profile";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelProfileCard.ResumeLayout(false);
            this.panelProfileCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxProfile)).EndInit();
            this.panelInfoSection.ResumeLayout(false);
            this.panelInfoSection.PerformLayout();
            this.panelActionButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.Panel panelProfileCard;
        private System.Windows.Forms.PictureBox pictureBoxProfile;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.Panel panelInfoSection;
        private System.Windows.Forms.Label lblFirstNameLabel;
        private System.Windows.Forms.Label lblFirstNameValue;
        private System.Windows.Forms.Panel panelDivider1;
        private System.Windows.Forms.Label lblLastNameLabel;
        private System.Windows.Forms.Label lblLastNameValue;
        private System.Windows.Forms.Panel panelDivider2;
        private System.Windows.Forms.Label lblEmailLabel;
        private System.Windows.Forms.Label lblEmailValue;
        private System.Windows.Forms.Panel panelDivider3;
        private System.Windows.Forms.Label lblContactLabel;
        private System.Windows.Forms.Label lblContactValue;
        private System.Windows.Forms.Panel panelActionButtons;
        private System.Windows.Forms.Button btnEditProfile;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnChangePassword;
    }
}