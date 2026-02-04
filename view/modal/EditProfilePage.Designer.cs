namespace LibraryManagementSystem.view
{
    partial class EditProfilePage
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
            this.panelEditCard = new System.Windows.Forms.Panel();
            this.lblFirstNameLabel = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.panelDivider1 = new System.Windows.Forms.Panel();
            this.lblLastNameLabel = new System.Windows.Forms.Label();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.panelDivider2 = new System.Windows.Forms.Panel();
            this.lblEmailLabel = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.panelDivider3 = new System.Windows.Forms.Panel();
            this.lblContactLabel = new System.Windows.Forms.Label();
            this.txtContact = new System.Windows.Forms.TextBox();
            this.panelActionButtons = new System.Windows.Forms.Panel();
            this.btnSaveChanges = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.panelHeader.SuspendLayout();
            this.panelEditCard.SuspendLayout();
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
            this.panelHeader.Size = new System.Drawing.Size(700, 70);
            this.panelHeader.TabIndex = 0;
            // 
            // lblPageTitle
            // 
            this.lblPageTitle.AutoSize = true;
            this.lblPageTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPageTitle.ForeColor = System.Drawing.Color.White;
            this.lblPageTitle.Location = new System.Drawing.Point(20, 18);
            this.lblPageTitle.Name = "lblPageTitle";
            this.lblPageTitle.Size = new System.Drawing.Size(257, 46);
            this.lblPageTitle.TabIndex = 0;
            this.lblPageTitle.Text = "Edit My Profile";
            // 
            // panelEditCard
            // 
            this.panelEditCard.BackColor = System.Drawing.Color.White;
            this.panelEditCard.Controls.Add(this.lblFirstNameLabel);
            this.panelEditCard.Controls.Add(this.txtFirstName);
            this.panelEditCard.Controls.Add(this.panelDivider1);
            this.panelEditCard.Controls.Add(this.lblLastNameLabel);
            this.panelEditCard.Controls.Add(this.txtLastName);
            this.panelEditCard.Controls.Add(this.panelDivider2);
            this.panelEditCard.Controls.Add(this.lblEmailLabel);
            this.panelEditCard.Controls.Add(this.txtEmail);
            this.panelEditCard.Controls.Add(this.panelDivider3);
            this.panelEditCard.Controls.Add(this.lblContactLabel);
            this.panelEditCard.Controls.Add(this.txtContact);
            this.panelEditCard.Location = new System.Drawing.Point(50, 95);
            this.panelEditCard.Name = "panelEditCard";
            this.panelEditCard.Size = new System.Drawing.Size(600, 380);
            this.panelEditCard.TabIndex = 1;
            // 
            // lblFirstNameLabel
            // 
            this.lblFirstNameLabel.AutoSize = true;
            this.lblFirstNameLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFirstNameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.lblFirstNameLabel.Location = new System.Drawing.Point(25, 20);
            this.lblFirstNameLabel.Name = "lblFirstNameLabel";
            this.lblFirstNameLabel.Size = new System.Drawing.Size(92, 23);
            this.lblFirstNameLabel.TabIndex = 0;
            this.lblFirstNameLabel.Text = "First Name";
            // 
            // txtFirstName
            // 
            this.txtFirstName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txtFirstName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFirstName.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFirstName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.txtFirstName.Location = new System.Drawing.Point(25, 40);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(550, 25);
            this.txtFirstName.TabIndex = 1;
            this.txtFirstName.Text = "John";
            this.txtFirstName.GotFocus += new System.EventHandler(this.TxtInput_GotFocus);
            this.txtFirstName.LostFocus += new System.EventHandler(this.TxtInput_LostFocus);
            this.txtFirstName.MouseEnter += new System.EventHandler(this.TxtInput_MouseEnter);
            this.txtFirstName.MouseLeave += new System.EventHandler(this.TxtInput_MouseLeave);
            // 
            // panelDivider1
            // 
            this.panelDivider1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.panelDivider1.Location = new System.Drawing.Point(0, 70);
            this.panelDivider1.Name = "panelDivider1";
            this.panelDivider1.Size = new System.Drawing.Size(600, 1);
            this.panelDivider1.TabIndex = 2;
            // 
            // lblLastNameLabel
            // 
            this.lblLastNameLabel.AutoSize = true;
            this.lblLastNameLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastNameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.lblLastNameLabel.Location = new System.Drawing.Point(25, 90);
            this.lblLastNameLabel.Name = "lblLastNameLabel";
            this.lblLastNameLabel.Size = new System.Drawing.Size(91, 23);
            this.lblLastNameLabel.TabIndex = 3;
            this.lblLastNameLabel.Text = "Last Name";
            // 
            // txtLastName
            // 
            this.txtLastName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txtLastName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLastName.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLastName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.txtLastName.Location = new System.Drawing.Point(25, 110);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(550, 25);
            this.txtLastName.TabIndex = 4;
            this.txtLastName.Text = "Doe";
            this.txtLastName.GotFocus += new System.EventHandler(this.TxtInput_GotFocus);
            this.txtLastName.LostFocus += new System.EventHandler(this.TxtInput_LostFocus);
            this.txtLastName.MouseEnter += new System.EventHandler(this.TxtInput_MouseEnter);
            this.txtLastName.MouseLeave += new System.EventHandler(this.TxtInput_MouseLeave);
            // 
            // panelDivider2
            // 
            this.panelDivider2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.panelDivider2.Location = new System.Drawing.Point(0, 140);
            this.panelDivider2.Name = "panelDivider2";
            this.panelDivider2.Size = new System.Drawing.Size(600, 1);
            this.panelDivider2.TabIndex = 5;
            // 
            // lblEmailLabel
            // 
            this.lblEmailLabel.AutoSize = true;
            this.lblEmailLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmailLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.lblEmailLabel.Location = new System.Drawing.Point(25, 160);
            this.lblEmailLabel.Name = "lblEmailLabel";
            this.lblEmailLabel.Size = new System.Drawing.Size(116, 23);
            this.lblEmailLabel.TabIndex = 6;
            this.lblEmailLabel.Text = "Email Address";
            // 
            // txtEmail
            // 
            this.txtEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.txtEmail.Location = new System.Drawing.Point(25, 180);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(550, 25);
            this.txtEmail.TabIndex = 7;
            this.txtEmail.Text = "john.doe@example.com";
            this.txtEmail.GotFocus += new System.EventHandler(this.TxtInput_GotFocus);
            this.txtEmail.LostFocus += new System.EventHandler(this.TxtInput_LostFocus);
            this.txtEmail.MouseEnter += new System.EventHandler(this.TxtInput_MouseEnter);
            this.txtEmail.MouseLeave += new System.EventHandler(this.TxtInput_MouseLeave);
            // 
            // panelDivider3
            // 
            this.panelDivider3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.panelDivider3.Location = new System.Drawing.Point(0, 210);
            this.panelDivider3.Name = "panelDivider3";
            this.panelDivider3.Size = new System.Drawing.Size(600, 1);
            this.panelDivider3.TabIndex = 8;
            // 
            // lblContactLabel
            // 
            this.lblContactLabel.AutoSize = true;
            this.lblContactLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContactLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.lblContactLabel.Location = new System.Drawing.Point(25, 230);
            this.lblContactLabel.Name = "lblContactLabel";
            this.lblContactLabel.Size = new System.Drawing.Size(138, 23);
            this.lblContactLabel.TabIndex = 9;
            this.lblContactLabel.Text = "Contact Number";
            // 
            // txtContact
            // 
            this.txtContact.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txtContact.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtContact.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContact.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.txtContact.Location = new System.Drawing.Point(25, 250);
            this.txtContact.Name = "txtContact";
            this.txtContact.Size = new System.Drawing.Size(550, 25);
            this.txtContact.TabIndex = 10;
            this.txtContact.Text = "+1 (555) 123-4567";
            this.txtContact.GotFocus += new System.EventHandler(this.TxtInput_GotFocus);
            this.txtContact.LostFocus += new System.EventHandler(this.TxtInput_LostFocus);
            this.txtContact.MouseEnter += new System.EventHandler(this.TxtInput_MouseEnter);
            this.txtContact.MouseLeave += new System.EventHandler(this.TxtInput_MouseLeave);
            // 
            // panelActionButtons
            // 
            this.panelActionButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.panelActionButtons.Controls.Add(this.btnSaveChanges);
            this.panelActionButtons.Controls.Add(this.btnCancel);
            this.panelActionButtons.Controls.Add(this.btnBack);
            this.panelActionButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelActionButtons.Location = new System.Drawing.Point(0, 520);
            this.panelActionButtons.Name = "panelActionButtons";
            this.panelActionButtons.Size = new System.Drawing.Size(700, 100);
            this.panelActionButtons.TabIndex = 2;
            // 
            // btnSaveChanges
            // 
            this.btnSaveChanges.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnSaveChanges.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveChanges.FlatAppearance.BorderSize = 0;
            this.btnSaveChanges.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveChanges.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveChanges.ForeColor = System.Drawing.Color.White;
            this.btnSaveChanges.Location = new System.Drawing.Point(50, 25);
            this.btnSaveChanges.Name = "btnSaveChanges";
            this.btnSaveChanges.Size = new System.Drawing.Size(160, 50);
            this.btnSaveChanges.TabIndex = 0;
            this.btnSaveChanges.Text = "✓ Save Changes";
            this.btnSaveChanges.UseVisualStyleBackColor = false;
            this.btnSaveChanges.Click += new System.EventHandler(this.btnSaveChanges_Click);
            this.btnSaveChanges.MouseEnter += new System.EventHandler(this.BtnSaveChanges_MouseEnter);
            this.btnSaveChanges.MouseLeave += new System.EventHandler(this.BtnSaveChanges_MouseLeave);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(280, 25);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(140, 50);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "⟲ Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnCancel.MouseEnter += new System.EventHandler(this.BtnCancel_MouseEnter);
            this.btnCancel.MouseLeave += new System.EventHandler(this.BtnCancel_MouseLeave);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.btnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(530, 25);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(120, 50);
            this.btnBack.TabIndex = 2;
            this.btnBack.Text = "← Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            this.btnBack.MouseEnter += new System.EventHandler(this.BtnBack_MouseEnter);
            this.btnBack.MouseLeave += new System.EventHandler(this.BtnBack_MouseLeave);
            // 
            // EditProfilePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(700, 620);
            this.Controls.Add(this.panelActionButtons);
            this.Controls.Add(this.panelEditCard);
            this.Controls.Add(this.panelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "EditProfilePage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit Profile";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelEditCard.ResumeLayout(false);
            this.panelEditCard.PerformLayout();
            this.panelActionButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.Panel panelEditCard;
        private System.Windows.Forms.Label lblFirstNameLabel;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.Panel panelDivider1;
        private System.Windows.Forms.Label lblLastNameLabel;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.Panel panelDivider2;
        private System.Windows.Forms.Label lblEmailLabel;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Panel panelDivider3;
        private System.Windows.Forms.Label lblContactLabel;
        private System.Windows.Forms.TextBox txtContact;
        private System.Windows.Forms.Panel panelActionButtons;
        private System.Windows.Forms.Button btnSaveChanges;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnBack;
    }
}