using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibraryManagementSystem.controller;

namespace LibraryManagementSystem.view
{
    public partial class ReportPage : Form
    {
        private List<dynamic> overdueBooksList;
        private class ReturnRowInfo
        {
            public Guid BorrowId { get; set; }
            public string StudentName { get; set; }
            public string BookTitle { get; set; }
        }

        public ReportPage()
        {
            InitializeComponent();
            LoadReportData();
            LoadOverduePenaltyData();
            InitializeDataGridViewEvents();
        }

        private void InitializeDataGridViewEvents()
        {
            dgvOverduePenalty.CellMouseEnter += DgvOverduePenalty_CellMouseEnter;
            dgvOverduePenalty.CellMouseLeave += DgvOverduePenalty_CellMouseLeave;
            dgvOverduePenalty.CellFormatting += DgvOverduePenalty_CellFormatting;
        }

        private void DgvOverduePenalty_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvOverduePenalty.Columns["colActions"].Index)
            {
                dgvOverduePenalty.Cursor = Cursors.Hand;
            }
        }

        private void DgvOverduePenalty_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dgvOverduePenalty.Cursor = Cursors.Default;
        }

        private void DgvOverduePenalty_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dgvOverduePenalty.Columns["colPayment"].Index && e.RowIndex >= 0)
            {
                string payment = e.Value?.ToString() ?? "";
                
                if (payment.IndexOf("Paid", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(46, 204, 113); // Green
                }
                else if (payment.IndexOf("Unpaid", StringComparison.OrdinalIgnoreCase) >= 0 || payment.IndexOf("Pending", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(231, 76, 60); // Red
                }
                else if (payment.IndexOf("No Penalty", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(127, 140, 141); // Gray
                }
                else if (payment.IndexOf("Partial", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(241, 196, 15); // Yellow/Orange
                }
            }
            else if (e.ColumnIndex == dgvOverduePenalty.Columns["colPenaltyBreakdown"].Index && e.RowIndex >= 0)
            {
                // Make penalty amounts stand out
                e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(231, 76, 60); // Red for amounts
            }
        }

        private void LoadReportData()
        {
            try
            {
                // Load Circulation Rate
                double circulationRate = HomeContoller.GetCirculationRate();
                lblCirculationPercent.Text = circulationRate.ToString("0.0") + "%";

                // Load Active Borrowers
                int activeBorrowers = HomeContoller.GetActiveBorrowers();
                lblActiveBorrowerPercent.Text = activeBorrowers.ToString();

                // Load Total Books (New Acquisition count)
                int totalBooks = HomeContoller.GetTotalBooks();
                lblNewAcquisitionPercent.Text = totalBooks.ToString();

                // Load Total Unpaid Penalties
                decimal totalUnpaidPenalties = HomeContoller.GetTotalUnpaidPenalties();
                lblTotalUnpaid.Text = "₱ " + totalUnpaidPenalties.ToString("N0");

                // Load Collection Progress
                int collectionProgress = HomeContoller.GetPenaltyCollectionProgress();
                progressBarPaid.Value = Math.Min(collectionProgress, 100);
                progressBarUnpaid.Value = Math.Min(100 - collectionProgress, 100);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading report data: {ex.Message}");
            }
        }

        private void LoadOverduePenaltyData()
        {
            try
            {
                dgvOverduePenalty.Rows.Clear();
                overdueBooksList = HomeContoller.GetOverdueBooks();

                if (overdueBooksList.Count == 0)
                {
                    dgvOverduePenalty.Rows.Add("No overdue books", "-", "-", "-", "");
                    return;
                }

                foreach (var overdue in overdueBooksList)
                {
                    int rowIndex = dgvOverduePenalty.Rows.Add(
                        overdue.StudentBook,
                        overdue.DueDateDelay,
                        overdue.PenaltyBreakdown,
                        overdue.Payment,
                        overdue.ActionText
                    );

                    dgvOverduePenalty.Rows[rowIndex].Tag = new ReturnRowInfo
                    {
                        BorrowId = overdue.Id,
                        StudentName = overdue.StudentName,
                        BookTitle = overdue.BookTitle
                    };

                    if (string.IsNullOrWhiteSpace(overdue.ActionText))
                    {
                        dgvOverduePenalty.Rows[rowIndex].Cells["colActions"].ReadOnly = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading overdue data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvOverduePenalty_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Check if Receive button was clicked
            if (e.ColumnIndex == dgvOverduePenalty.Columns["colActions"].Index)
            {
                var rowInfo = dgvOverduePenalty.Rows[e.RowIndex].Tag as ReturnRowInfo;
                if (rowInfo == null)
                {
                    return;
                }

                string actionText = dgvOverduePenalty.Rows[e.RowIndex].Cells["colActions"].Value?.ToString();
                if (!string.Equals(actionText, "Receive", StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }

                var result = MessageBox.Show(
                    $"Receive returned book '{rowInfo.BookTitle}' from {rowInfo.StudentName}?",
                    "Receive Return",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    bool received = HomeContoller.ReceiveReturnedBook(rowInfo.BorrowId);
                    if (!received)
                    {
                        MessageBox.Show("Failed to receive the return. Please try again.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    MessageBox.Show("Return received successfully. Notification sent to the student.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadOverduePenaltyData();
                    LoadReportData();
                }
            }
        }
    }
}
