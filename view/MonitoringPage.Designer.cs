namespace LibraryManagementSystem.view
{
    partial class MonitoringPage
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAll = new System.Windows.Forms.Button();
            this.btnBorrowed = new System.Windows.Forms.Button();
            this.btnOverdue = new System.Windows.Forms.Button();
            this.btnReturned = new System.Windows.Forms.Button();
            this.btnPending = new System.Windows.Forms.Button();
            this.dgvMonitoring = new System.Windows.Forms.DataGridView();
            this.colStudent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBook = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCountdown = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMonitoring)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(177, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "Monitoring";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label2.ForeColor = System.Drawing.Color.Gray;
            this.label2.Location = new System.Drawing.Point(16, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(301, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Track borrow activity, countdowns, and fines";
            // 
            // btnAll
            // 
            this.btnAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnAll.ForeColor = System.Drawing.Color.White;
            this.btnAll.Location = new System.Drawing.Point(520, 15);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(80, 32);
            this.btnAll.TabIndex = 2;
            this.btnAll.Text = "All";
            this.btnAll.UseVisualStyleBackColor = false;
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // btnBorrowed
            // 
            this.btnBorrowed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.btnBorrowed.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBorrowed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBorrowed.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnBorrowed.ForeColor = System.Drawing.Color.White;
            this.btnBorrowed.Location = new System.Drawing.Point(606, 15);
            this.btnBorrowed.Name = "btnBorrowed";
            this.btnBorrowed.Size = new System.Drawing.Size(95, 32);
            this.btnBorrowed.TabIndex = 3;
            this.btnBorrowed.Text = "Borrowed";
            this.btnBorrowed.UseVisualStyleBackColor = false;
            this.btnBorrowed.Click += new System.EventHandler(this.btnBorrowed_Click);
            // 
            // btnOverdue
            // 
            this.btnOverdue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnOverdue.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOverdue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOverdue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnOverdue.ForeColor = System.Drawing.Color.White;
            this.btnOverdue.Location = new System.Drawing.Point(707, 15);
            this.btnOverdue.Name = "btnOverdue";
            this.btnOverdue.Size = new System.Drawing.Size(90, 32);
            this.btnOverdue.TabIndex = 4;
            this.btnOverdue.Text = "Overdue";
            this.btnOverdue.UseVisualStyleBackColor = false;
            this.btnOverdue.Click += new System.EventHandler(this.btnOverdue_Click);
            // 
            // btnReturned
            // 
            this.btnReturned.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnReturned.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReturned.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReturned.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnReturned.ForeColor = System.Drawing.Color.White;
            this.btnReturned.Location = new System.Drawing.Point(803, 15);
            this.btnReturned.Name = "btnReturned";
            this.btnReturned.Size = new System.Drawing.Size(90, 32);
            this.btnReturned.TabIndex = 5;
            this.btnReturned.Text = "Returned";
            this.btnReturned.UseVisualStyleBackColor = false;
            this.btnReturned.Click += new System.EventHandler(this.btnReturned_Click);
            // 
            // btnPending
            // 
            this.btnPending.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(196)))), ((int)(((byte)(15)))));
            this.btnPending.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPending.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPending.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnPending.ForeColor = System.Drawing.Color.White;
            this.btnPending.Location = new System.Drawing.Point(899, 15);
            this.btnPending.Name = "btnPending";
            this.btnPending.Size = new System.Drawing.Size(86, 32);
            this.btnPending.TabIndex = 6;
            this.btnPending.Text = "Pending";
            this.btnPending.UseVisualStyleBackColor = false;
            this.btnPending.Click += new System.EventHandler(this.btnPending_Click);
            // 
            // dgvMonitoring
            // 
            this.dgvMonitoring.AllowUserToAddRows = false;
            this.dgvMonitoring.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.dgvMonitoring.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMonitoring.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMonitoring.BackgroundColor = System.Drawing.Color.White;
            this.dgvMonitoring.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMonitoring.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMonitoring.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMonitoring.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colStudent,
            this.colBook,
            this.colStatus,
            this.colCountdown,
            this.colFine});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.Padding = new System.Windows.Forms.Padding(5, 2, 5, 2);
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMonitoring.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvMonitoring.EnableHeadersVisualStyles = false;
            this.dgvMonitoring.Location = new System.Drawing.Point(19, 78);
            this.dgvMonitoring.Name = "dgvMonitoring";
            this.dgvMonitoring.ReadOnly = true;
            this.dgvMonitoring.RowHeadersWidth = 51;
            this.dgvMonitoring.RowTemplate.Height = 35;
            this.dgvMonitoring.Size = new System.Drawing.Size(966, 563);
            this.dgvMonitoring.TabIndex = 7;
            this.dgvMonitoring.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvMonitoring_CellFormatting);
            // 
            // colStudent
            // 
            this.colStudent.HeaderText = "Student";
            this.colStudent.MinimumWidth = 6;
            this.colStudent.Name = "colStudent";
            this.colStudent.ReadOnly = true;
            // 
            // colBook
            // 
            this.colBook.HeaderText = "Book Requested";
            this.colBook.MinimumWidth = 6;
            this.colBook.Name = "colBook";
            this.colBook.ReadOnly = true;
            // 
            // colStatus
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.colStatus.DefaultCellStyle = dataGridViewCellStyle3;
            this.colStatus.HeaderText = "Status";
            this.colStatus.MinimumWidth = 6;
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            // 
            // colCountdown
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colCountdown.DefaultCellStyle = dataGridViewCellStyle4;
            this.colCountdown.HeaderText = "Countdown";
            this.colCountdown.MinimumWidth = 6;
            this.colCountdown.Name = "colCountdown";
            this.colCountdown.ReadOnly = true;
            // 
            // colFine
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.colFine.DefaultCellStyle = dataGridViewCellStyle5;
            this.colFine.HeaderText = "Fines / Bill";
            this.colFine.MinimumWidth = 6;
            this.colFine.Name = "colFine";
            this.colFine.ReadOnly = true;
            // 
            // MonitoringPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(997, 661);
            this.Controls.Add(this.dgvMonitoring);
            this.Controls.Add(this.btnPending);
            this.Controls.Add(this.btnReturned);
            this.Controls.Add(this.btnOverdue);
            this.Controls.Add(this.btnBorrowed);
            this.Controls.Add(this.btnAll);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MonitoringPage";
            this.Text = "Monitoring";
            ((System.ComponentModel.ISupportInitialize)(this.dgvMonitoring)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAll;
        private System.Windows.Forms.Button btnBorrowed;
        private System.Windows.Forms.Button btnOverdue;
        private System.Windows.Forms.Button btnReturned;
        private System.Windows.Forms.Button btnPending;
        private System.Windows.Forms.DataGridView dgvMonitoring;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStudent;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBook;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCountdown;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFine;
    }
}