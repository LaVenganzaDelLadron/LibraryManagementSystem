using LibraryManagementSystem.controller;
using LibraryManagementSystem.view;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagementSystem
{
    public partial class HomePage : Form
    {
        StudentPage studentPage = new StudentPage();
        BookPage bookPage = new BookPage();
        MonitoringPage monitoringPage = new MonitoringPage();
        ReportPage reportPage = new ReportPage();


        public HomePage()
        {
            InitializeComponent();
            InitializeTransactionsDataGridView();
            LoadCards();
            SetupProgressBarColors();
            InitializeOverdueActions();
        }

        private void InitializeOverdueActions()
        {
            panel5.Cursor = Cursors.Hand;
            lblOverDue.Cursor = Cursors.Hand;
            label10.Cursor = Cursors.Hand;

            panel5.Click += Overdue_Click;
            lblOverDue.Click += Overdue_Click;
            label10.Click += Overdue_Click;
        }

        private void SetupProgressBarColors()
        {
            // Style progress bars with custom colors
            progressBarInventory.ForeColor = System.Drawing.Color.FromArgb(46, 204, 113);
            progressBarReturnRate.ForeColor = System.Drawing.Color.FromArgb(52, 152, 219);
            progressBarBorrowRate.ForeColor = System.Drawing.Color.FromArgb(155, 89, 182);
        }

        private void Panel_Paint(object sender, PaintEventArgs e)
        {
            // Add subtle shadow effect to panels
            Panel panel = sender as Panel;
            if (panel != null)
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    path.AddRectangle(new Rectangle(0, 0, panel.Width, panel.Height));
                    using (System.Drawing.Drawing2D.PathGradientBrush brush = new System.Drawing.Drawing2D.PathGradientBrush(path))
                    {
                        brush.CenterColor = panel.BackColor;
                        brush.SurroundColors = new Color[] { Color.FromArgb(20, 0, 0, 0) };
                    }
                }
            }
        }

        private void InitializeTransactionsDataGridView()
        {
            // Clear existing columns
            dataGridViewTransactions.Columns.Clear();

            // Add columns
            dataGridViewTransactions.Columns.Add("Student", "Student");
            dataGridViewTransactions.Columns.Add("Books", "Books");
            dataGridViewTransactions.Columns.Add("Date", "Date");
            dataGridViewTransactions.Columns.Add("Status", "Status");

            // Add Remove button column
            DataGridViewButtonColumn removeButtonColumn = new DataGridViewButtonColumn();
            removeButtonColumn.Name = "Remove";
            removeButtonColumn.HeaderText = "Action";
            removeButtonColumn.Text = "Remove";
            removeButtonColumn.UseColumnTextForButtonValue = true;
            removeButtonColumn.Width = 100;
            dataGridViewTransactions.Columns.Add(removeButtonColumn);

            // Style the DataGridView
            dataGridViewTransactions.EnableHeadersVisualStyles = false;
            dataGridViewTransactions.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(0, 128, 128);
            dataGridViewTransactions.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dataGridViewTransactions.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewTransactions.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewTransactions.ColumnHeadersHeight = 40;
            dataGridViewTransactions.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 248, 255);
            dataGridViewTransactions.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(0, 150, 136);
            dataGridViewTransactions.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewTransactions.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(5, 2, 5, 2);
            dataGridViewTransactions.RowTemplate.Height = 35;
            dataGridViewTransactions.BorderStyle = BorderStyle.None;
            
            // Center align Date and Status columns
            dataGridViewTransactions.Columns["Date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewTransactions.Columns["Status"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewTransactions.Columns["Status"].DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);

            // Add event handler for cell formatting
            dataGridViewTransactions.CellFormatting += DataGridViewTransactions_CellFormatting;
            dataGridViewTransactions.CellMouseEnter += DataGridViewTransactions_CellMouseEnter;
            dataGridViewTransactions.CellMouseLeave += DataGridViewTransactions_CellMouseLeave;

            // Add sample data
            AddSampleTransactionData();
        }

        private void AddSampleTransactionData()
        {
            // Load recent transactions from database
            var recentTransactions = HomeContoller.GetRecentTransactions(7);

            if (recentTransactions.Count == 0)
            {
                // Show placeholder if no transactions exist
                dataGridViewTransactions.Rows.Add("No transactions", "-", "-", "-");
                return;
            }

            foreach (var transaction in recentTransactions)
            {
                string displayStatus = transaction.Status;
                if (displayStatus.Equals("Returning", StringComparison.OrdinalIgnoreCase))
                {
                    displayStatus = "Returned";
                }
                
                dataGridViewTransactions.Rows.Add(
                    transaction.Student,
                    transaction.Book,
                    transaction.Date,
                    displayStatus
                );
            }
        }

        private void dataGridViewTransactions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridViewTransactions.Columns["Remove"].Index)
            {
                string student = dataGridViewTransactions.Rows[e.RowIndex].Cells["Student"].Value.ToString();
                string book = dataGridViewTransactions.Rows[e.RowIndex].Cells["Books"].Value.ToString();
                
                DialogResult result = MessageBox.Show(
                    $"Are you sure you want to remove this transaction?\n\nStudent: {student}\nBook: {book}",
                    "Confirm Remove",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
                
                if (result == DialogResult.Yes)
                {
                    dataGridViewTransactions.Rows.RemoveAt(e.RowIndex);
                    MessageBox.Show("Transaction removed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void LoadCards()
        {
            // Load card statistics
            int totalStudents = HomeContoller.GetTotalStudents();
            lblGetTotalStudents.Text = totalStudents.ToString();

            int availableBooks = HomeContoller.GetAvailableBookTitles();
            lblAvailableBooks.Text = availableBooks.ToString();

            int borrowedCount = HomeContoller.GetBorrowedCount();
            lblBorrowed.Text = borrowedCount.ToString(); // Borrowed count

            int pendingCount = HomeContoller.GetPendingCount();
            lblPending.Text = pendingCount.ToString(); // Pending count

            int overdueCount = HomeContoller.GetOverdueCount();
            lblOverDue.Text = overdueCount.ToString(); // Overdue count

            // Load progress bars
            int inventoryUtilization = HomeContoller.GetInventoryUtilization();
            progressBarInventory.Value = Math.Min(inventoryUtilization, 100);
            label12.Text = $"Inventory Utilization: {inventoryUtilization}%";

            int returnRate = HomeContoller.GetReturnRate();
            progressBarReturnRate.Value = Math.Min(returnRate, 100);
            label13.Text = $"Return Rate: {returnRate}%";

            int borrowRate = HomeContoller.GetBorrowRate();
            progressBarBorrowRate.Value = Math.Min(borrowRate, 100);
            label14.Text = $"Active Borrow Rate: {borrowRate}%";
        }

        private void Overdue_Click(object sender, EventArgs e)
        {
            ShowOverdueDetails();
        }

        private void ShowOverdueDetails()
        {
            var overdueBooks = HomeContoller.GetOverdueBooks();

            if (overdueBooks == null || overdueBooks.Count == 0)
            {
                MessageBox.Show("No overdue books found.", "Overdue", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var builder = new StringBuilder();
            builder.AppendLine("Overdue Books:");
            builder.AppendLine();

            foreach (var item in overdueBooks)
            {
                builder.AppendLine($"• {item.StudentBook}");
                builder.AppendLine($"  {item.DueDateDelay}");
                builder.AppendLine($"  {item.PenaltyBreakdown}");
                builder.AppendLine();
            }

            MessageBox.Show(builder.ToString(), "Overdue Details", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void DataGridViewTransactions_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewTransactions.Columns["Status"].Index && e.RowIndex >= 0)
            {
                string status = e.Value?.ToString() ?? "";
                
                if (status.IndexOf("Borrowed", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(0, 128, 0); // Green
                }
                else if (status.IndexOf("Pending", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(255, 140, 0); // Orange
                }
                else if (status.IndexOf("Overdue", StringComparison.OrdinalIgnoreCase) >= 0 || status.IndexOf("Late", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(220, 20, 60); // Red
                }
                else if (status.IndexOf("Returned", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(70, 130, 180); // Steel Blue
                }
            }
        }

        private void DataGridViewTransactions_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridViewTransactions.Columns["Remove"].Index)
            {
                dataGridViewTransactions.Cursor = Cursors.Hand;
            }
        }

        private void DataGridViewTransactions_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewTransactions.Cursor = Cursors.Default;
        }
    }
}
