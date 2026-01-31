namespace LibraryManagementSystem.view
{
    partial class BorrowingPage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.DataGridView dgvBorrowingRequests;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStudentName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBookRequested;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRequestDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewButtonColumn colActions;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripActions;
        private System.Windows.Forms.ToolStripMenuItem approveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rejectToolStripMenuItem;

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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvBorrowingRequests = new System.Windows.Forms.DataGridView();
            this.colStudentName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBookRequested = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRequestDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActions = new System.Windows.Forms.DataGridViewButtonColumn();
            this.lblTitle = new System.Windows.Forms.Label();
            this.contextMenuStripActions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.approveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rejectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.btnRejected = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBorrowingRequests)).BeginInit();
            this.contextMenuStripActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvBorrowingRequests
            // 
            this.dgvBorrowingRequests.AllowUserToAddRows = false;
            this.dgvBorrowingRequests.AllowUserToDeleteRows = false;
            this.dgvBorrowingRequests.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBorrowingRequests.BackgroundColor = System.Drawing.Color.White;
            this.dgvBorrowingRequests.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(0, 128, 128);
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBorrowingRequests.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvBorrowingRequests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBorrowingRequests.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colStudentName,
            this.colBookRequested,
            this.colRequestDate,
            this.colStatus,
            this.colActions});
            this.dgvBorrowingRequests.EnableHeadersVisualStyles = false;
            this.dgvBorrowingRequests.Location = new System.Drawing.Point(20, 133);
            this.dgvBorrowingRequests.Name = "dgvBorrowingRequests";
            this.dgvBorrowingRequests.ReadOnly = true;
            this.dgvBorrowingRequests.RowHeadersWidth = 51;
            this.dgvBorrowingRequests.RowTemplate.Height = 35;
            this.dgvBorrowingRequests.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 248, 255);
            this.dgvBorrowingRequests.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(0, 150, 136);
            this.dgvBorrowingRequests.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            this.dgvBorrowingRequests.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.dgvBorrowingRequests.Size = new System.Drawing.Size(975, 555);
            this.dgvBorrowingRequests.TabIndex = 0;
            this.dgvBorrowingRequests.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBorrowingRequests_CellContentClick);
            // 
            // colStudentName
            // 
            this.colStudentName.HeaderText = "Student Name";
            this.colStudentName.MinimumWidth = 6;
            this.colStudentName.Name = "colStudentName";
            this.colStudentName.ReadOnly = true;
            // 
            // colBookRequested
            // 
            this.colBookRequested.HeaderText = "Book Requested";
            this.colBookRequested.MinimumWidth = 6;
            this.colBookRequested.Name = "colBookRequested";
            this.colBookRequested.ReadOnly = true;
            // 
            // colRequestDate
            // 
            this.colRequestDate.HeaderText = "Request Date";
            this.colRequestDate.MinimumWidth = 6;
            this.colRequestDate.Name = "colRequestDate";
            this.colRequestDate.ReadOnly = true;
            this.colRequestDate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            // 
            // colStatus
            // 
            this.colStatus.HeaderText = "Status";
            this.colStatus.MinimumWidth = 6;
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            this.colStatus.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colStatus.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            // 
            // colActions
            // 
            this.colActions.HeaderText = "Actions";
            this.colActions.MinimumWidth = 6;
            this.colActions.Name = "colActions";
            this.colActions.ReadOnly = true;
            this.colActions.Text = "⋮";
            this.colActions.UseColumnTextForButtonValue = true;
            this.colActions.Width = 70;
            this.colActions.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colActions.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(16, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(204, 25);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Borrowing Requests";
            // 
            // contextMenuStripActions
            // 
            this.contextMenuStripActions.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStripActions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.approveToolStripMenuItem,
            this.rejectToolStripMenuItem});
            this.contextMenuStripActions.Name = "contextMenuStripActions";
            this.contextMenuStripActions.Size = new System.Drawing.Size(136, 52);
            // 
            // approveToolStripMenuItem
            // 
            this.approveToolStripMenuItem.Name = "approveToolStripMenuItem";
            this.approveToolStripMenuItem.Size = new System.Drawing.Size(135, 24);
            this.approveToolStripMenuItem.Text = "Approve";
            this.approveToolStripMenuItem.Click += new System.EventHandler(this.approveToolStripMenuItem_Click);
            // 
            // rejectToolStripMenuItem
            // 
            this.rejectToolStripMenuItem.Name = "rejectToolStripMenuItem";
            this.rejectToolStripMenuItem.Size = new System.Drawing.Size(135, 24);
            this.rejectToolStripMenuItem.Text = "Reject";
            this.rejectToolStripMenuItem.Click += new System.EventHandler(this.rejectToolStripMenuItem_Click);
            // 
            // button1 (All)
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(21, 88);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 39);
            this.button1.TabIndex = 2;
            this.button1.Text = "All";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2 (Pending)
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(241, 196, 15);
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(124, 88);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(96, 39);
            this.button2.TabIndex = 3;
            this.button2.Text = "Pending";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3 (Borrowed)
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(155, 89, 182);
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(226, 88);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(96, 39);
            this.button3.TabIndex = 4;
            this.button3.Text = "Borrowed";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4 (Returned)
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(328, 88);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(96, 39);
            this.button4.TabIndex = 5;
            this.button4.Text = "Returned";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5 (Overdue)
            // 
            this.button5.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button5.Location = new System.Drawing.Point(430, 88);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(96, 39);
            this.button5.TabIndex = 6;
            this.button5.Text = "Overdue";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // btnRejected
            // 
            this.btnRejected.BackColor = System.Drawing.Color.FromArgb(149, 165, 166);
            this.btnRejected.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRejected.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnRejected.ForeColor = System.Drawing.Color.White;
            this.btnRejected.Location = new System.Drawing.Point(532, 88);
            this.btnRejected.Name = "btnRejected";
            this.btnRejected.Size = new System.Drawing.Size(96, 39);
            this.btnRejected.TabIndex = 7;
            this.btnRejected.Text = "Rejected";
            this.btnRejected.UseVisualStyleBackColor = false;
            this.btnRejected.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRejected.Click += new System.EventHandler(this.btnRejected_Click);
            // 
            // BorrowingPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1015, 708);
            this.Controls.Add(this.btnRejected);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.dgvBorrowingRequests);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BorrowingPage";
            this.Text = "BorrowingPage";
            this.Load += new System.EventHandler(this.BorrowingPage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBorrowingRequests)).EndInit();
            this.contextMenuStripActions.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button btnRejected;
    }
}